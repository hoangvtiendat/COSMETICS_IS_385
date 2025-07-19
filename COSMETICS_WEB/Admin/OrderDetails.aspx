<%@ Page Title="Chi tiết đơn hàng" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="COSMETICS_WEB.Admin.OrderDetails" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Chi tiết Đơn hàng #<asp:Literal ID="litOrderId" runat="server" /></h1>
    <h3>Các sản phẩm trong đơn</h3>
    <asp:GridView ID="gvOrderItems" runat="server" AutoGenerateColumns="false" CssClass="gridview">
        <Columns>
            <asp:BoundField DataField="ProductID" HeaderText="Mã SP" />
            <asp:BoundField DataField="ProductName" HeaderText="Tên sản phẩm" />
            <asp:BoundField DataField="Quantity" HeaderText="Số lượng" />
            <asp:BoundField DataField="Price" HeaderText="Đơn giá" DataFormatString="{0:N0} đ" />
        </Columns>
    </asp:GridView>

    <h3>Cập nhật trạng thái</h3>
    <div class="form-group">
        <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="form-control">
            <asp:ListItem>Chờ xác nhận</asp:ListItem>
            <asp:ListItem>Đang xử lý</asp:ListItem>
            <asp:ListItem>Đang giao</asp:ListItem>
            <asp:ListItem>Đã giao</asp:ListItem>
            <asp:ListItem>Đã hủy</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnUpdateStatus" runat="server" Text="Cập nhật" CssClass="hero-button" OnClick="btnUpdateStatus_Click" />
        <asp:Label ID="lblStatus" runat="server" CssClass="status-message success"></asp:Label>
    </div>
</asp:Content>