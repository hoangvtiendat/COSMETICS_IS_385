using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COSMETICS_WEB.App_Code.BLL;
namespace COSMETICS_WEB
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Luôn hiển thị danh sách danh mục
                BindCategories();

                // Kiểm tra URL để lọc sản phẩm
                if (Request.QueryString["catid"] != null)
                {
                    int categoryId = Convert.ToInt32(Request.QueryString["catid"]);
                    BindProductsByCategory(categoryId);
                }
                else
                {
                    BindAllProducts();
                }
            }
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


    }
}