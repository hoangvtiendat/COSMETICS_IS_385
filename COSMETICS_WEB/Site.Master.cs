using COSMETICS_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COSMETICS_WEB
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                // Nếu đã đăng nhập
                User user = (User)Session["User"];
                litUserName.Text = user.FullName;
                pnlUser.Visible = true;
                pnlGuest.Visible = false;
                liOrderHistory.Visible = true;
            }
            else
            {
                // Nếu chưa đăng nhập
                pnlUser.Visible = false;
                pnlGuest.Visible = true;
                liOrderHistory.Visible = false;
            }
        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            // Xóa toàn bộ Session để đăng xuất
            Session.Clear();
            // Chuyển hướng người dùng về trang chủ
            Response.Redirect("/Default.aspx");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Chuyển hướng đến trang sản phẩm với từ khóa tìm kiếm
                Response.Redirect("/Products.aspx?search=" + Server.UrlEncode(searchTerm));
            }
        }
    }
}