<%@ Page Title="Thanh toán" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="COSMETICS_WEB.Checkout" EnableEventValidation="false" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container checkout-page">
        <h2 class="section-title">THANH TOÁN ĐƠN HÀNG</h2>
        <div class="checkout-container">
            <div class="shipping-form">
                <h3>Thông tin giao hàng</h3>
                <asp:HiddenField ID="hdnProvinceName" runat="server" />
                <asp:HiddenField ID="hdnDistrictName" runat="server" />
                <asp:HiddenField ID="hdnWardName" runat="server" />
                <div class="form-group">
                    <label>Họ và tên người nhận:</label>
                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Tỉnh/Thành phố:</label>
                    <asp:DropDownList ID="ddlProvince" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Quận/Huyện:</label>
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Phường/Xã:</label>
                    <asp:DropDownList ID="ddlWard" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Địa chỉ cụ thể (Số nhà, tên đường):</label>
                    <asp:TextBox ID="txtStreetAddress" runat="server" CssClass="form-control" placeholder="Ví dụ: 123 Nguyễn Văn Linh"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Số điện thoại:</label>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>

                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPhone"
                        ErrorMessage="Vui lòng nhập số điện thoại." CssClass="error-message" Display="Dynamic"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtPhone"
                        ValidationExpression="^0\d{9}$"
                        ErrorMessage="Số điện thoại không hợp lệ (phải đủ 10 số và bắt đầu bằng 0)." CssClass="error-message" Display="Dynamic">
                    </asp:RegularExpressionValidator>
                </div>

                <h3 class="payment-title">Phương thức thanh toán</h3>
                <asp:RadioButtonList ID="rblPaymentMethod" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="rblPaymentMethod_SelectedIndexChanged" RepeatDirection="Vertical" CssClass="payment-options" Style="margin-left: 5px;">
                    <asp:ListItem Value="COD" Selected="True">Thanh toán khi nhận hàng</asp:ListItem>

                    <asp:ListItem Value="QR">Chuyển khoản QR Code</asp:ListItem>
                </asp:RadioButtonList>

                <asp:Panel ID="pnlQRCode" runat="server" Visible="false" CssClass="qr-code-panel">
                    <p margin-top="50px">
                        Vui lòng quét mã VietQR dưới đây để thanh toán. Nội dung chuyển khoản:
                        <asp:Literal ID="litPaymentContent" runat="server"></asp:Literal>
                    </p>
                    <asp:Image ID="imgQRCode" runat="server" />
                </asp:Panel>
            </div>

            <div class="order-summary">
                <h3>Đơn hàng của bạn</h3>
                <asp:Repeater ID="rptOrderSummary" runat="server">
                    <ItemTemplate>
                        <div class="summary-item">
                            <img src='<%# Eval("ImagePath") %>' class="summary-item-image" />
                            <div class="summary-item-info">
                                <span><%# Eval("ProductName") %></span>
                                <span>SL: <%# Eval("Quantity") %></span>
                            </div>
                            <span class="summary-item-price"><%# string.Format("{0:N0} đ", Eval("TotalPrice")) %></span>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <hr />
                <div class="summary-total">
                    <strong>Tổng cộng:</strong>
                    <strong>
                        <asp:Literal ID="litTotalPrice" runat="server"></asp:Literal></strong>
                </div>
                <asp:Button ID="btnPlaceOrder" runat="server" Text="ĐẶT HÀNG" CssClass="hero-button" OnClick="btnPlaceOrder_Click" />
            </div>
        </div>
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            const host = "https://provinces.open-api.vn/api/";
            var provinceDropdown = $("#<%= ddlProvince.ClientID %>");
        var districtDropdown = $("#<%= ddlDistrict.ClientID %>");
        var wardDropdown = $("#<%= ddlWard.ClientID %>");

            // Load danh sách tỉnh/thành
            function loadProvinces() {
                $.get(host + "?depth=1", function (data) {
                    provinceDropdown.empty().append('<option value="">-- Chọn Tỉnh/Thành --</option>');
                    $.each(data, function (index, item) {
                        provinceDropdown.append('<option value="' + item.code + '">' + item.name + '</option>');
                    });
                });
            }

            // Load danh sách quận/huyện khi chọn tỉnh/thành
            provinceDropdown.change(function () {
                var provinceCode = $(this).val();
                districtDropdown.empty().append('<option value="">-- Chọn Quận/Huyện --</option>');
                wardDropdown.empty().append('<option value="">-- Chọn Phường/Xã --</option>');
                if (provinceCode) {
                    $.get(host + "p/" + provinceCode + "?depth=2", function (data) {
                        $.each(data.districts, function (index, item) {
                            districtDropdown.append('<option value="' + item.code + '">' + item.name + '</option>');
                        });
                    });
                }
            });

            // Load danh sách phường/xã khi chọn quận/huyện
            districtDropdown.change(function () {
                var districtCode = $(this).val();
                wardDropdown.empty().append('<option value="">-- Chọn Phường/Xã --</option>');
                if (districtCode) {
                    $.get(host + "d/" + districtCode + "?depth=2", function (data) {
                        $.each(data.wards, function (index, item) {
                            wardDropdown.append('<option value="' + item.code + '">' + item.name + '</option>');
                        });
                    });
                }
            });

            loadProvinces(); // Chạy lần đầu để load tỉnh/thành
        });
    </script>
</asp:Content>
