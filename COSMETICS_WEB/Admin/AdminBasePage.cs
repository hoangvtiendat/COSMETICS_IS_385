using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using COSMETICS_WEB.Models;

namespace COSMETICS_WEB.Admin
{
    public class AdminBasePage : Page
    {
        protected override void OnInit(EventArgs e)
        {
            // Kiểm tra xem session có tồn tại không
            if (Session["User"] == null)
            {
                // Nếu không, đẩy về trang đăng nhập
                Response.Redirect("/Login.aspx?ReturnUrl=" + Request.Url.PathAndQuery);
                return;
            }

            // Nếu có session, kiểm tra vai trò
            User currentUser = (User)Session["User"];
            if (currentUser.UserType != "Admin" && currentUser.UserType != "Staff")
            {
                // Nếu không phải Admin/Staff, cũng đẩy về trang đăng nhập
                Response.Redirect("/Login.aspx");
                return;
            }

            base.OnInit(e);
        }
    }
}