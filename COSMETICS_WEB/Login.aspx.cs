using COSMETICS_WEB.App_Code.BLL;
using COSMETICS_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COSMETICS_WEB
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["reg"] == "success")
            {
                string script = "showToast('success', 'Đăng ký thành công! Vui lòng đăng nhập.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "showRegSuccess", script, true);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            UserBLL userBLL = new UserBLL();
            User user = userBLL.CheckLogin(email, password);

            if (user != null) // Nếu tìm thấy user
            {
                // Lưu thông tin user vào Session
                Session["User"] = user;

                // ===== PHẦN PHÂN QUYỀN =====
                // Kiểm tra vai trò (UserType) của người dùng
                if (user.UserType == "Admin" || user.UserType == "Staff")
                {
                    // Nếu là Admin hoặc Nhân viên, chuyển đến trang quản trị
                    Response.Redirect("/Admin/Default.aspx");
                }
                else
                {
                    // Nếu là Khách hàng (Customer), chuyển về trang chủ
                    string returnUrl = Request.QueryString["ReturnUrl"];
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        Response.Redirect(returnUrl);
                    }
                    else
                    {
                        // Chuyển hướng mặc định nếu không có returnUrl
                        Response.Redirect("/Default.aspx");
                    }
                }
            }
            else // Nếu không tìm thấy
            {
                lblMessage.Text = "Sai email hoặc mật khẩu.";
            }
        }
    }
}