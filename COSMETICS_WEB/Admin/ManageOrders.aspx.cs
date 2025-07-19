using COSMETICS_WEB.App_Code.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COSMETICS_WEB.Admin
{
    public partial class ManageOrders : COSMETICS_WEB.Admin.AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrders();
            }
        }
        private void BindOrders()
        {
            OrderBLL bll = new OrderBLL();
            gvOrders.DataSource = bll.GetAllOrders();
            gvOrders.DataBind();
        }
    }
}