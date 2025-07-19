<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="COSMETICS_WEB.Login" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container login-container">
        <div class="login-form">
            <h2>ĐĂNG NHẬP</h2>
            <div class="form-group">
                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtPassword">Mật khẩu:</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
            <asp:Label ID="lblMessage" runat="server" CssClass="error-message" EnableViewState="false"></asp:Label>
            <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" CssClass="hero-button" OnClick="btnLogin_Click" />
            <div class="login-links">
                <a href="/Register.aspx">Chưa có tài khoản? Đăng ký ngay</a>
            </div>
        </div>
    </div>
</asp:Content>
