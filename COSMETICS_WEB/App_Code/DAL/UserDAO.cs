using COSMETICS_WEB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.DAL
{
    public class UserDAO
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        public User CheckLogin(string email, string hashedPassword)
        {


            User user = null; // Khởi tạo user là null
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Câu lệnh SQL kiểm tra email VÀ mật khẩu đã được băm
                string sqlQuery = "SELECT UserID, FullName, UserType FROM Users WHERE Email = @Email AND PasswordHash = @PasswordHash";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read()) // Nếu tìm thấy một dòng khớp
                {
                    user = new User();
                    user.UserID = Convert.ToInt32(rdr["UserID"]);
                    user.FullName = rdr["FullName"].ToString();
                    user.UserType = rdr["UserType"].ToString();
                }
            }
            return user; // Sẽ trả về null nếu không tìm thấy, hoặc trả về đối tượng user nếu tìm thấy
        }

        public bool IsEmailExists(string email)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @Email", con);
                cmd.Parameters.AddWithValue("@Email", email);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        public bool IsUsernameExists(string username)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username", con);
                cmd.Parameters.AddWithValue("@Username", username);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public void RegisterUser(User user, string hashedPassword)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "INSERT INTO Users (Username, FullName, Email, PasswordHash, UserType, CreatedDate) VALUES (@Username, @FullName, @Email, @PasswordHash, @UserType, @CreatedDate)";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@Username", user.Username); 
                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@Email", user.Email); 
                cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                cmd.Parameters.AddWithValue("@UserType", "Customer");
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> userList = new List<User>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT UserID, Username, FullName, Email, UserType FROM Users";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    userList.Add(new User
                    {
                        UserID = Convert.ToInt32(rdr["UserID"]),
                        Username = rdr["Username"].ToString(),
                        FullName = rdr["FullName"].ToString(),
                        Email = rdr["Email"].ToString(),
                        UserType = rdr["UserType"].ToString()
                    });
                }
            }
            return userList;
        }

        // Cập nhật thông tin người dùng (ở đây là vai trò và họ tên)
        public void UpdateUser(User user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "UPDATE Users SET FullName = @FullName, UserType = @UserType WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@UserType", user.UserType);
                cmd.Parameters.AddWithValue("@UserID", user.UserID);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Xóa người dùng
        public void DeleteUser(int userId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Cảnh báo: Cần xử lý các đơn hàng/giỏ hàng của user này trước khi xóa
                string sqlQuery = "DELETE FROM Users WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@UserID", userId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool HasOrders(int userId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Đếm số đơn hàng của UserID này
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Orders WHERE UserID = @UserID", con);
                cmd.Parameters.AddWithValue("@UserID", userId);
                con.Open();
                int orderCount = (int)cmd.ExecuteScalar();
                return orderCount > 0; // Trả về true nếu có nhiều hơn 0 đơn hàng
            }
        }
    }
}