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
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Bắt buộc phải đăng nhập để xem trang này
            if (Session["User"] == null)
            {
                Response.Redirect("/Login.aspx?ReturnUrl=/UserProfile.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindOrderHistory();
            }
        }

        private void BindOrderHistory()
        {
            User currentUser = (User)Session["User"];
            OrderBLL bll = new OrderBLL();
            gvOrderHistory.DataSource = bll.GetOrdersByUserId(currentUser.UserID);
            gvOrderHistory.DataBind();
        }
    }
}