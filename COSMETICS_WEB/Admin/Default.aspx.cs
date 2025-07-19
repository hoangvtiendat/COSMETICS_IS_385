using COSMETICS_WEB.App_Code.BLL;
using COSMETICS_WEB.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COSMETICS_WEB.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDashboardStats();
                BindRecentOrders();
            }
        }

        private void LoadDashboardStats()
        {
            DashboardBLL bll = new DashboardBLL();
            DashboardStats stats = bll.GetDashboardStats();

            litTotalRevenue.Text = string.Format("{0:N0} đ", stats.TotalRevenue);
            litOrderCount.Text = stats.OrderCount.ToString();
            litUserCount.Text = stats.UserCount.ToString();
            litProductCount.Text = stats.ProductCount.ToString();
        }

        private void BindRecentOrders()
        {
            OrderBLL bll = new OrderBLL();
            // Lấy tất cả đơn hàng và chỉ hiển thị 5 đơn hàng mới nhất
            var recentOrders = bll.GetAllOrders().Take(5);
            gvRecentOrders.DataSource = recentOrders;
            gvRecentOrders.DataBind();
        }
    }
}