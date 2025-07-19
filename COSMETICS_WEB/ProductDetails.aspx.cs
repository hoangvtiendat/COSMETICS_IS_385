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
    public partial class ProductDetails : System.Web.UI.Page
    {
        private int productId;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Lấy ID sản phẩm từ URL
            if (string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out productId))
            {
                Response.Redirect("Products.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindProductDetails();
            }
        }


        private void BindProductDetails()
        {
            ProductBLL productBLL = new ProductBLL();
            Product product = productBLL.GetProductById(productId);

            if (product != null)
            {
                // Đổ dữ liệu vào các control
                litProductName.Text = product.ProductName;
                litPrice.Text = string.Format("{0:N0} đ", product.Price);

                // === ĐẢM BẢO BẠN CÓ CÁC DÒNG NÀY ===
                litDescription.Text = product.Description;
                litBrand.Text = product.BrandName;
                litCategory.Text = product.CategoryName;
                // ===================================

                // Lấy và hiển thị hình ảnh
                List<ProductImage> images = productBLL.GetProductImages(productId);
                if (images.Any())
                {
                    imgMainProduct.ImageUrl = images[0].ImagePath;
                    rptProductImages.DataSource = images;
                    rptProductImages.DataBind();
                }
            }
            else
            {
                Response.Redirect("Products.aspx");
            }
        }

        protected void btnAdd_Click_Click(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("/Login.aspx?ReturnUrl=" + Request.RawUrl);
                return;
            }

            User currentUser = (User)Session["User"];
            CartBLL cartBLL = new CartBLL();
            cartBLL.AddItemToCart(currentUser.UserID, productId, 1);

            string script = "showToast('success', 'Đã thêm sản phẩm vào giỏ hàng!');";
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
        }
    }
}