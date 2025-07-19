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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategories();
                BindFeaturedProducts();
            }
        }

        private void BindCategories()
        {
            CategoryBLL bll = new CategoryBLL();
            rptCategories.DataSource = bll.GetAllCategories();
            rptCategories.DataBind();
        }

        private void BindFeaturedProducts()
        {
            ProductBLL bll = new ProductBLL();
            // Lấy 8 sản phẩm nổi bật
            rptFeaturedProducts.DataSource = bll.GetFeaturedProducts(8);
            rptFeaturedProducts.DataBind();
        }

        protected void rptProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Chỉ xử lý khi nút "Thêm vào giỏ" được bấm
            if (e.CommandName == "AddToCart")
            {
                // === KIỂM TRA ĐĂNG NHẬP ===
                if (Session["User"] == null)
                {
                    // Nếu chưa đăng nhập, chuyển đến trang Login và lưu lại URL hiện tại để quay về
                    Response.Redirect("/Login.aspx?ReturnUrl=" + Server.UrlEncode(Request.RawUrl));
                    return; // Dừng thực thi
                }

                // Nếu đã đăng nhập, tiến hành thêm sản phẩm
                try
                {
                    // Lấy thông tin
                    User currentUser = (User)Session["User"];
                    int userId = currentUser.UserID;
                    int productId = Convert.ToInt32(e.CommandArgument);

                    // Gọi BLL để thêm vào giỏ
                    CartBLL cartBLL = new CartBLL();
                    cartBLL.AddItemToCart(userId, productId, 1); // Mặc định thêm 1 sản phẩm

                    // (Tùy chọn) Hiển thị thông báo thành công
                    // Ví dụ: lblPageMessage.Text = "Đã thêm sản phẩm vào giỏ hàng!";
                }
                catch (Exception ex)
                {
                    // (Tùy chọn) Xử lý lỗi
                    // Ví dụ: lblPageMessage.Text = "Có lỗi xảy ra, vui lòng thử lại.";
                }
            }
        }
    }
}