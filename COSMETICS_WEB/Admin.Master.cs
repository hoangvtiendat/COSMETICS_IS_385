using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COSMETICS_WEB
{
    public partial class Admin_Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbtnLogoutAdmin_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/Login.aspx");
        }
    }
}