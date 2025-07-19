<%@ Page Title="Đăng kí" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="COSMETICS_WEB.Register" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container login-container">
        <div class="login-form">
            <h2>TẠO TÀI KHOẢN</h2>
            <div class="form-group">
                <label for="txtFullName">Họ và tên:</label>
                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFullName" CssClass="error-message" ErrorMessage="Họ tên là bắt buộc."></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label for="txtUsername">Tên đăng nhập (Username):</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUsername" CssClass="error-message" ErrorMessage="Tên đăng nhập là bắt buộc."></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" CssClass="error-message" ErrorMessage="Email là bắt buộc."></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail" CssClass="error-message" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Email không hợp lệ."></asp:RegularExpressionValidator>
            </div>
            <div class="form-group">
                <label for="txtPassword">Mật khẩu:</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" CssClass="error-message" ErrorMessage="Mật khẩu là bắt buộc."></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="txtConfirmPassword">Xác nhận mật khẩu:</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" Operator="Equal" CssClass="error-message" ErrorMessage="Mật khẩu không khớp."></asp:CompareValidator>
            </div>
            <asp:Label ID="lblMessage" runat="server" CssClass="error-message" EnableViewState="false"></asp:Label>
            <asp:Button ID="btnRegister" runat="server" Text="Đăng ký" CssClass="hero-button" OnClick="btnRegister_Click" />
            <div class="login-links">
                <a href="/Login.aspx">Đã có tài khoản? Đăng nhập</a>
            </div>
        </div>
    </div>
</asp:Content>
