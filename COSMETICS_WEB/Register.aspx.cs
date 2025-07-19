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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                User user = new User
                {
                    Username = txtUsername.Text.Trim(),
                    FullName = txtFullName.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };

                string password = txtPassword.Text;

                UserBLL userBLL = new UserBLL();
                string result = userBLL.RegisterUser(user, password);

                switch (result)
                {
                    case "SUCCESS":
                        Response.Redirect("Login.aspx?reg=success");
                        break;
                    case "USERNAME_EXISTS":
                        lblMessage.Text = "Tên đăng nhập này đã được sử dụng.";
                        break;
                    case "EMAIL_EXISTS":
                        lblMessage.Text = "Email này đã được sử dụng.";
                        break;
                    default:
                        lblMessage.Text = "Đã có lỗi xảy ra. Vui lòng thử lại.";
                        break;
                }
            }
        }
    }
}