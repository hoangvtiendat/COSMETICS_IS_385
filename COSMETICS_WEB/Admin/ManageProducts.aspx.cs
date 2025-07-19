using COSMETICS_WEB.App_Code.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COSMETICS_WEB.Admin
{
    public partial class ManageProducts : COSMETICS_WEB.Admin.AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProducts();
            }
        }

        private void BindProducts()
        {
            ProductBLL productBLL = new ProductBLL();
            gvProducts.DataSource = productBLL.GetAllProducts();
            gvProducts.DataBind();
        }
 
        protected void btnAddProduct_Click1(object sender, EventArgs e)
        {
            Response.Redirect("ProductEditor.aspx");
        }

        protected void gvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                // Lấy ProductID từ DataKeyNames
                int productId = Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value);

                ProductBLL productBLL = new ProductBLL();
                productBLL.DeleteProduct(productId);

                // Tải lại dữ liệu
                BindProducts();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
            }
        }
        protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Lấy ProductID
            int productId = Convert.ToInt32(gvProducts.DataKeys[e.NewEditIndex].Value);
            // Chuyển hướng đến trang Editor với ID
            Response.Redirect("ProductEditor.aspx?id=" + productId);
        }
    }
}