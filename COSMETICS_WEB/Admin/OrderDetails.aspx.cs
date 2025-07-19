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
    public partial class OrderDetails : COSMETICS_WEB.Admin.AdminBasePage
    {
        private int orderId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out orderId))
            {
                Response.Redirect("ManageOrders.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindOrderDetails();
            }
        }
        private void BindOrderDetails()
        {
            OrderBLL bll = new OrderBLL();
            Order order = bll.GetOrderById(orderId);

            if (order != null)
            {
                litOrderId.Text = order.OrderID.ToString();
                // Bạn có thể thêm các Literal khác để hiển thị tên khách, địa chỉ...
                // litCustomerName.Text = order.CustomerName;
                // litShippingAddress.Text = order.ShippingAddress;

                // Chọn đúng trạng thái hiện tại trong DropDownList
                ddlOrderStatus.SelectedValue = order.OrderStatus;
            }

            // Lấy và hiển thị danh sách sản phẩm (giữ nguyên)
            gvOrderItems.DataSource = bll.GetOrderDetails(orderId);
            gvOrderItems.DataBind();
        }
        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            OrderBLL bll = new OrderBLL();
            bll.UpdateOrderStatus(orderId, ddlOrderStatus.SelectedValue);
            lblStatus.Text = "Cập nhật trạng thái thành công!";
        }
    }
}