using COSMETICS_WEB.App_Code.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using COSMETICS_WEB.Models;

namespace COSMETICS_WEB.App_Code.BLL
{
    public class UserBLL
    {
        private UserDAO dao = new UserDAO();

        // Hàm băm mật khẩu bằng SHA256
        private string ToSHA256(string s)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public User CheckLogin(string email, string password)
        {
            // Băm mật khẩu người dùng nhập vào trước khi kiểm tra
            string hashedPassword = ToSHA256(password);
            return dao.CheckLogin(email, hashedPassword);
        }

        public bool IsEmailExists(string email)
        {
            return dao.IsEmailExists(email);
        }

        public bool IsUsernameExists(string username) // Thêm hàm mới
        {
            return dao.IsUsernameExists(username);
        }

        // Hàm này sẽ điều phối việc đăng ký
        public string RegisterUser(User user, string password)
        {
            if (dao.IsUsernameExists(user.Username))
            {
                return "USERNAME_EXISTS"; // Username đã tồn tại
            }
            if (dao.IsEmailExists(user.Email))
            {
                return "EMAIL_EXISTS"; // Email đã tồn tại
            }

            string hashedPassword = ToSHA256(password);
            dao.RegisterUser(user, hashedPassword);
            return "SUCCESS"; // Thành công
        }
        public List<User> GetAllUsers()
        {
            return dao.GetAllUsers();
        }

        public void UpdateUser(User user)
        {
            dao.UpdateUser(user);
        }

        public bool DeleteUser(int userId)
        {
            // 1. Kiểm tra xem user có đơn hàng không
            if (dao.HasOrders(userId))
            {
                // Nếu có, không xóa và trả về false
                return false;
            }
            else
            {
                // Nếu không có, tiến hành xóa và trả về true
                dao.DeleteUser(userId);
                return true;
            }
        }


    }
}