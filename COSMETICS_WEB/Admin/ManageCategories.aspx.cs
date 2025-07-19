using COSMETICS_WEB.App_Code.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COSMETICS_WEB.Admin
{
    public partial class ManageCategories : COSMETICS_WEB.Admin.AdminBasePage
    {
        CategoryBLL bll = new CategoryBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
                BindParentCategoryDropDown();
            }
        }

        private void BindGridView()
        {
            gvCategories.DataSource = bll.GetAllCategories();
            gvCategories.DataBind();
        }

        private void BindParentCategoryDropDown()
        {
            ddlNewParentCategory.DataSource = bll.GetAllCategories();
            ddlNewParentCategory.DataTextField = "CategoryName";
            ddlNewParentCategory.DataValueField = "CategoryID";
            ddlNewParentCategory.DataBind();
            ddlNewParentCategory.Items.Insert(0, new ListItem("-- Không có danh mục cha --", ""));
        }

        protected void btnAddNewCategory_Click(object sender, EventArgs e)
        {
            string name = txtNewCategoryName.Text.Trim();
            object parentId = string.IsNullOrEmpty(ddlNewParentCategory.SelectedValue) ? null : (object)Convert.ToInt32(ddlNewParentCategory.SelectedValue);

            if (!string.IsNullOrEmpty(name))
            {
                bll.AddCategory(name, parentId);
                BindGridView();
                BindParentCategoryDropDown();
                txtNewCategoryName.Text = "";
            }
        }

        protected void gvCategories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCategories.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void gvCategories_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCategories.EditIndex = -1;
            BindGridView();
        }

        protected void gvCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvCategories.DataKeys[e.RowIndex].Value);
            if (bll.DeleteCategory(id))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "showToast('success', 'Đã xóa danh mục thành công.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showerror", "showToast('error', 'Không thể xóa danh mục này vì nó đang chứa sản phẩm.');", true);
            }
            BindGridView();
        }

        protected void gvCategories_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvCategories.DataKeys[e.RowIndex].Value);
            TextBox txtName = (TextBox)gvCategories.Rows[e.RowIndex].FindControl("txtCategoryName");
            DropDownList ddlParent = (DropDownList)gvCategories.Rows[e.RowIndex].FindControl("ddlParentCategory");

            string name = txtName.Text.Trim();
            object parentId = string.IsNullOrEmpty(ddlParent.SelectedValue) ? null : (object)Convert.ToInt32(ddlParent.SelectedValue);

            if (!string.IsNullOrEmpty(name))
            {
                bll.UpdateCategory(id, name, parentId);
            }

            gvCategories.EditIndex = -1;
            BindGridView();
        }

        protected void gvCategories_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvCategories.EditIndex == e.Row.RowIndex)
            {
                // Lấy ra DropDownList trong dòng đang sửa
                DropDownList ddlParent = (DropDownList)e.Row.FindControl("ddlParentCategory");
                // Lấy dữ liệu của cả dòng
                DataRowView drv = e.Row.DataItem as DataRowView;
                // Đổ dữ liệu vào DropDownList
                ddlParent.DataSource = bll.GetAllCategories();
                ddlParent.DataTextField = "CategoryName";
                ddlParent.DataValueField = "CategoryID";
                ddlParent.DataBind();
                ddlParent.Items.Insert(0, new ListItem("-- Không có danh mục cha --", ""));
                // Chọn giá trị cũ
                if (drv["ParentID"] != DBNull.Value)
                {
                    ddlParent.SelectedValue = drv["ParentID"].ToString();
                }
            }
        }
    }
}