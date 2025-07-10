<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="COSMETICS_WEB.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="section-title">
            <h2>LIÊN HỆ VỚI CHÚNG TÔI</h2>
        </div>
        
        <div class="contact-container">
            <div class="contact-form-column">
                <h3>Gửi tin nhắn cho chúng tôi</h3>
                <div class="form-group">
                    <label for="txtName">Họ và tên:</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                        ErrorMessage="Vui lòng nhập họ tên." CssClass="error-message" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="txtEmail">Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="Vui lòng nhập email." CssClass="error-message" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="txtSubject">Tiêu đề:</label>
                    <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtMessage">Nội dung:</label>
                    <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ControlToValidate="txtMessage"
                        ErrorMessage="Vui lòng nhập nội dung." CssClass="error-message" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <asp:Button ID="btnSendMessage" runat="server" Text="Gửi tin nhắn" CssClass="hero-button" OnClick="btnSendMessage_Click" />
                <asp:Label ID="lblStatusMessage" runat="server" CssClass="status-message" EnableViewState="false"></asp:Label>
            </div>

            <div class="contact-info-column">
                <h3>Thông tin cửa hàng</h3>
                <div class="info-block">
                    <p><i class="fa-solid fa-location-dot"></i> 120 Hoàng Minh Thảo, P. Hòa Khánh, TP. Đà Nẵng</p>
                    <p><i class="fa-solid fa-phone"></i> (028) 12 345 678</p>
                    <p><i class="fa-solid fa-envelope"></i> hngvtdat010@gmail.com</p>
                </div>

                <h3>Chúng tôi trên bản đồ</h3>
                <div class="map-container">
                    <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3834.311997231086!2d108.15548317538283!3d16.049291550652207!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31421938d61a3ce5%3A0x29d80f3ebbdcb44a!2zxJDhuqFpIEjhu41jIER1eSBUw6JuIEjDsmEgS2jDoW5oIE5hbQ!5e0!3m2!1svi!2s!4v1752176770219!5m2!1svi!2s" width="600" height="450" style="border:0;></iframe>
                        width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
