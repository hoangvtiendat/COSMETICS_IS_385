using COSMETICS_WEB.App_Code.BLL;
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
    }
}