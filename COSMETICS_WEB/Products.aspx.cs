using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COSMETICS_WEB.App_Code.BLL;
using COSMETICS_WEB.Models;

namespace COSMETICS_WEB
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    BindCategories();
            //    BindBrands();

            //    if (Request.QueryString["catid"] != null)
            //    {
            //        int categoryId = Convert.ToInt32(Request.QueryString["catid"]);
            //        BindProductsByCategory(categoryId);

            //        // Cập nhật tiêu đề
            //        CategoryBLL catBLL = new CategoryBLL();
            //        litPageTitle.Text = catBLL.GetCategoryNameById(categoryId);
            //    }
            //    else if (Request.QueryString["brandid"] != null)
            //    {
            //        int brandId = Convert.ToInt32(Request.QueryString["brandid"]);
            //        BindProductsByBrand(brandId);

            //        // Cập nhật tiêu đề
            //        BrandBLL brandBLL = new BrandBLL();
            //        litPageTitle.Text = brandBLL.GetBrandNameById(brandId);
            //    }
            //    else
            //    {
            //        BindAllProducts();
            //        // Giữ nguyên tiêu đề mặc định là "Tất cả sản phẩm"
            //    }
            //}

            if (!IsPostBack)
            {
                BindCategories();
                BindBrands();

                // Ưu tiên kiểm tra tham số 'search' trước
                if (!string.IsNullOrEmpty(Request.QueryString["search"]))
                {
                    string searchTerm = Request.QueryString["search"];
                    BindSearchResults(searchTerm);
                }
                else if (Request.QueryString["catid"] != null)
                {
                    int categoryId = Convert.ToInt32(Request.QueryString["catid"]);
                    BindProductsByCategory(categoryId);
                    CategoryBLL catBLL = new CategoryBLL();
                    litPageTitle.Text = catBLL.GetCategoryNameById(categoryId);
                }
                else if (Request.QueryString["brandid"] != null)
                {
                    int brandId = Convert.ToInt32(Request.QueryString["brandid"]);
                    BindProductsByBrand(brandId);
                    BrandBLL brandBLL = new BrandBLL();
                    litPageTitle.Text = brandBLL.GetBrandNameById(brandId);
                }
                else
                {
                    BindAllProducts();
                }
            }
        }

        private void BindSearchResults(string searchTerm)
        {
            ProductBLL productBLL = new ProductBLL();
            rptProducts.DataSource = productBLL.SearchProducts(searchTerm);
            rptProducts.DataBind();
            litPageTitle.Text = "Kết quả tìm kiếm cho: '" + searchTerm + "'";
        }
        private void BindBrands()
        {
            BrandBLL brandBLL = new BrandBLL();
            rptBrands.DataSource = brandBLL.GetAllBrands();
            rptBrands.DataBind();
        }

        // Thêm hàm bind sản phẩm theo thương hiệu
        private void BindProductsByBrand(int brandId)
        {
            ProductBLL productBLL = new ProductBLL();
            rptProducts.DataSource = productBLL.GetProductsByBrandID(brandId);
            rptProducts.DataBind();
        }

        private void BindCategories()
        {
            CategoryBLL categoryBLL = new CategoryBLL();
            rptCategories.DataSource = categoryBLL.GetAllCategories();
            rptCategories.DataBind();
        }

        private void BindAllProducts()
        {
            ProductBLL productBLL = new ProductBLL();
            rptProducts.DataSource = productBLL.GetAllProducts();
            rptProducts.DataBind();
        }

        private void BindProductsByCategory(int categoryId)
        {
            ProductBLL productBLL = new ProductBLL();
            rptProducts.DataSource = productBLL.GetProductsByCategoryID(categoryId);
            rptProducts.DataBind();
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

                    string script = "showToast('success', 'Đã thêm sản phẩm vào giỏ hàng!');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
                }
                catch (Exception ex)
                {
                    string script = "showToast('error', 'Có lỗi xảy ra, vui lòng thử lại.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showerror", script, true);
                }
            }
        }


    }
}