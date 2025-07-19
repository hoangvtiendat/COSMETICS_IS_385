using COSMETICS_WEB.App_Code.BLL;
using COSMETICS_WEB.App_Code.Models;
using COSMETICS_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COSMETICS_WEB
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("/Login.aspx?ReturnUrl=/Cart.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindCart();
            }
        }

        private void BindCart()
        {
            User currentUser = (User)Session["User"];
            CartBLL cartBLL = new CartBLL();
            List<CartItem> cartItems = cartBLL.GetCartItems(currentUser.UserID);

            if (cartItems.Count > 0)
            {
                rptCartItems.DataSource = cartItems;
                rptCartItems.DataBind();
                // Tính tổng tiền
                decimal totalPrice = cartItems.Sum(item => item.TotalPrice);
                litTotalPrice.Text = string.Format("{0:N0} đ", totalPrice);
                pnlEmptyCart.Visible = false;
            }
            else
            {
                // Ẩn bảng và hiện thông báo giỏ hàng trống
                rptCartItems.Visible = false;
                pnlEmptyCart.Visible = true;
            }
        }

        // Xử lý khi bấm nút Xóa
        protected void rptCartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "RemoveItem")
            {
                User currentUser = (User)Session["User"];
                int productId = Convert.ToInt32(e.CommandArgument);
                CartBLL cartBLL = new CartBLL();
                cartBLL.RemoveItemFromCart(currentUser.UserID, productId);
                BindCart(); // Tải lại giỏ hàng
            }
        }

        // Xử lý khi thay đổi số lượng
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            TextBox txtQuantity = (TextBox)sender;
            RepeaterItem item = (RepeaterItem)txtQuantity.NamingContainer;
            HiddenField hdnProductId = (HiddenField)item.FindControl("hdnProductId");

            User currentUser = (User)Session["User"];
            int productId = Convert.ToInt32(hdnProductId.Value);
            int quantity = Convert.ToInt32(txtQuantity.Text);

            CartBLL cartBLL = new CartBLL();
            cartBLL.UpdateItemQuantity(currentUser.UserID, productId, quantity);
            BindCart(); // Tải lại giỏ hàng
        }
    }
}