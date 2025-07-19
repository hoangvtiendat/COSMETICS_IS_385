using COSMETICS_WEB.App_Code.BLL;
using COSMETICS_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COSMETICS_WEB.Admin
{
    public partial class ManageUsers : COSMETICS_WEB.Admin.AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Bảo mật: Chỉ có Admin mới được vào trang này
            User currentUser = (User)Session["User"];
            if (currentUser.UserType != "Admin")
            {
                Response.Redirect("/Admin/Default.aspx");
            }

            if (!IsPostBack)
            {
                BindUsers();
            }
        }

        private void BindUsers()
        {
            UserBLL bll = new UserBLL();
            gvUsers.DataSource = bll.GetAllUsers();
            gvUsers.DataBind();
        }

        // Bật chế độ sửa
        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsers.EditIndex = e.NewEditIndex;
            BindUsers();
        }

        // Hủy chế độ sửa
        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1;
            BindUsers();
        }

        // Xóa một dòng
        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);
            UserBLL bll = new UserBLL();

            // Gọi hàm DeleteUser và nhận kết quả trả về
            bool isSuccess = bll.DeleteUser(userId);

            if (isSuccess)
            {
                // Nếu xóa thành công, tải lại dữ liệu
                BindUsers();
                // Hiển thị thông báo thành công
                string script = "showToast('success', 'Đã xóa người dùng thành công.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", script, true);
            }
            else
            {
                // Nếu xóa thất bại, hiển thị thông báo lỗi
                string script = "showToast('error', 'Không thể xóa người dùng này vì họ đã có đơn hàng.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "showerror", script, true);
            }
        }

        // Cập nhật một dòng
        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Lấy các control trong chế độ Edit
            TextBox txtFullName = (TextBox)gvUsers.Rows[e.RowIndex].FindControl("txtFullName");
            DropDownList ddlUserType = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("ddlUserType");
            int userId = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);

            // Tạo đối tượng User và gọi BLL
            User user = new User
            {
                UserID = userId,
                FullName = txtFullName.Text,
                UserType = ddlUserType.SelectedValue
            };
            UserBLL bll = new UserBLL();
            bll.UpdateUser(user);

            // Tắt chế độ sửa và tải lại lưới
            gvUsers.EditIndex = -1;
            BindUsers();
        }
        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Chỉ xử lý khi dòng đang ở trạng thái Edit
            if (e.Row.RowType == DataControlRowType.DataRow && gvUsers.EditIndex == e.Row.RowIndex)
            {
                // 1. Tìm control DropDownList trong dòng đang sửa
                DropDownList ddlUserType = (DropDownList)e.Row.FindControl("ddlUserType");

                // 2. Lấy dữ liệu của dòng đó
                User user = (User)e.Row.DataItem;

                // 3. Chọn giá trị hiện tại của user cho DropDownList
                if (ddlUserType != null && user != null)
                {
                    ddlUserType.SelectedValue = user.UserType;
                }
            }
        }
    }
}