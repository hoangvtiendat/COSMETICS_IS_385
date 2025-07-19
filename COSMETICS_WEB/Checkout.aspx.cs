using COSMETICS_WEB.App_Code.BLL;
using COSMETICS_WEB.App_Code.Models;
using COSMETICS_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRCoder;
using System.Drawing;
using System.IO;


namespace COSMETICS_WEB
{
    public partial class Checkout : System.Web.UI.Page
    {
        private List<CartItem> currentCart;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiểm tra đăng nhập trước
            if (Session["User"] == null)
            {
                Response.Redirect("/Login.aspx?ReturnUrl=/Checkout.aspx");
                return;
            }

            User currentUser = (User)Session["User"];
            currentCart = new CartBLL().GetCartItems(currentUser.UserID);

            if (!IsPostBack)
            {
                // Chỉ chạy khi trang được tải lần đầu
                if (currentCart.Count == 0)
                {
                    Response.Redirect("/Cart.aspx");
                    return;
                }

                txtFullName.Text = currentUser.FullName;
                BindOrderSummary();
            }
            else
            {
                // === THÊM KHỐI CODE NÀY VÀO ===
                // Xử lý khi trang postback để giữ lại lựa chọn địa chỉ
                string provinceCode = Request.Form[ddlProvince.UniqueID];
                string districtCode = Request.Form[ddlDistrict.UniqueID];
                string wardCode = Request.Form[ddlWard.UniqueID];

                string script = $"var selectedProvinceCode = '{provinceCode}'; " +
                                $"var selectedDistrictCode = '{districtCode}'; " +
                                $"var selectedWardCode = '{wardCode}';";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "RepopulateAddress", script, true);

            }
        }

        private void BindOrderSummary()
        {
            rptOrderSummary.DataSource = currentCart;
            rptOrderSummary.DataBind();
            decimal totalPrice = currentCart.Sum(item => item.TotalPrice);
            litTotalPrice.Text = string.Format("{0:N0} đ", totalPrice);
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            // Kiểm tra tất cả validator trước khi chạy
            if (!Page.IsValid)
            {
                return;
            }

            User currentUser = (User)Session["User"];

            // Ghép các phần địa chỉ lại thành một chuỗi hoàn chỉnh
            string fullAddress = string.Format("{0}, {1}, {2}, {3}",
                txtStreetAddress.Text.Trim(),
                hdnWardName.Value,      // Lấy từ HiddenField
                hdnDistrictName.Value,  // Lấy từ HiddenField
                hdnProvinceName.Value);

            Order order = new Order
            {
                UserID = currentUser.UserID,
                ShippingAddress = fullAddress, // Lưu địa chỉ đầy đủ
                TotalPrice = currentCart.Sum(item => item.TotalPrice),
                PaymentMethod = rblPaymentMethod.SelectedValue // Lưu phương thức thanh toán
            };

            OrderBLL orderBLL = new OrderBLL();
            bool isSuccess = orderBLL.CreateOrder(order, currentCart);

            if (isSuccess)
            {
                Response.Redirect("/Default.aspx?order=success");
            }
            else
            {
                // Xử lý lỗi
            }
        }

        protected void rblPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblPaymentMethod.SelectedValue == "QR")
            {
                pnlQRCode.Visible = true;
                GenerateQRCode();
            }
            else
            {
                pnlQRCode.Visible = false;
            }
        }

        // Hàm tạo mã QR
        private void GenerateQRCode()
        {
            // Thông tin tài khoản của bạn
            string bankId = "970422"; // Ví dụ: MB Bank
            string accountNumber = "0943061255"; // Số tài khoản của bạn
            string amount = currentCart.Sum(item => item.TotalPrice).ToString();
            string orderInfo = $"DH{DateTime.Now.Ticks}"; // Nội dung chuyển khoản

            litPaymentContent.Text = orderInfo;

            // === PHẦN SỬA ĐỔI ===
            // 1. Tạo chuỗi URL của dịch vụ VietQR
            string vietQR_Url = $"https://img.vietqr.io/image/{bankId}-{accountNumber}-print.png?amount={amount}&addInfo={orderInfo}";

            // 2. Gán thẳng URL này cho ảnh. KHÔNG CẦN DÙNG QRCoder.
            imgQRCode.ImageUrl = vietQR_Url;
        }
    }
}