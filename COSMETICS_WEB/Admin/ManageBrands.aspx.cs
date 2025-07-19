using COSMETICS_WEB.App_Code.BLL;
using COSMETICS_WEB.App_Code.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COSMETICS_WEB.Admin
{
    public partial class ManageBrands : COSMETICS_WEB.Admin.AdminBasePage
    {
        BrandBLL bll = new BrandBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            gvBrands.DataSource = bll.GetAllBrands();
            gvBrands.DataBind();
        }

        protected void btnAddNewBrand_Click(object sender, EventArgs e)
        {
            string logoPath = "/assets/images/brands/default.png"; // Ảnh mặc định
            if (fileUploadLogo.HasFile)
            {
                string fileName = Path.GetFileName(fileUploadLogo.FileName);
                string savePath = Server.MapPath("~/assets/images/brands/") + fileName;
                fileUploadLogo.SaveAs(savePath);
                logoPath = "/assets/images/brands/" + fileName;
            }

            Brand brand = new Brand
            {
                BrandName = txtNewBrandName.Text.Trim(),
                Description = txtNewDescription.Text.Trim(),
                LogoPath = logoPath
            };
            bll.AddBrand(brand);

            // Reset form và tải lại lưới
            txtNewBrandName.Text = "";
            txtNewDescription.Text = "";
            BindGridView();
        }

        protected void gvBrands_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvBrands.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void gvBrands_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvBrands.EditIndex = -1;
            BindGridView();
        }

        protected void gvBrands_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvBrands.DataKeys[e.RowIndex].Value);
            if (bll.DeleteBrand(id))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "showToast('success', 'Đã xóa thương hiệu.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showerror", "showToast('error', 'Không thể xóa, thương hiệu này đang được sử dụng.');", true);
            }
            BindGridView();
        }

        protected void gvBrands_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Lấy ID và các control text như cũ
            int id = Convert.ToInt32(gvBrands.DataKeys[e.RowIndex].Value);
            TextBox txtName = (TextBox)gvBrands.Rows[e.RowIndex].FindControl("txtBrandName");
            TextBox txtDesc = (TextBox)gvBrands.Rows[e.RowIndex].FindControl("txtDescription");

            // Cập nhật thông tin text
            Brand brand = new Brand
            {
                BrandID = id,
                BrandName = txtName.Text.Trim(),
                Description = txtDesc.Text.Trim()
            };
            bll.UpdateBrand(brand);

            // === PHẦN XỬ LÝ UPLOAD VÀ XÓA LOGO ===
            CheckBox chkDeleteLogo = (CheckBox)gvBrands.Rows[e.RowIndex].FindControl("chkDeleteLogo");
            FileUpload fileUpload = (FileUpload)gvBrands.Rows[e.RowIndex].FindControl("fileUploadEditLogo");

            if (chkDeleteLogo != null && chkDeleteLogo.Checked)
            {
                // Nếu tick vào ô "Xóa logo", cập nhật lại bằng ảnh mặc định
                string defaultLogoPath = "/assets/images/brands/default.png";
                bll.UpdateBrandLogo(id, defaultLogoPath);
            }
            else if (fileUpload.HasFile)
            {
                // Nếu không xóa và có upload file mới, thì cập nhật logo
                string fileName = Path.GetFileName(fileUpload.FileName);
                string savePath = Server.MapPath("~/assets/images/brands/") + fileName;
                fileUpload.SaveAs(savePath);
                string logoPath = "/assets/images/brands/" + fileName;
                bll.UpdateBrandLogo(id, logoPath);
            }

            // Tắt chế độ sửa và tải lại lưới
            gvBrands.EditIndex = -1;
            BindGridView();
        }
    }
}