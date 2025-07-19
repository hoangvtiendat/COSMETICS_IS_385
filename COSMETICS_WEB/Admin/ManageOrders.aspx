<%@ Page Title="Quản lý đơn hàng" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageOrders.aspx.cs" Inherits="COSMETICS_WEB.Admin.ManageOrders" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Quản lý Đơn hàng</h1>
    <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass="gridview">
        <Columns>
            <asp:BoundField DataField="OrderID" HeaderText="Mã ĐH" />
            <asp:BoundField DataField="CustomerName" HeaderText="Tên khách hàng" />
            <asp:BoundField DataField="OrderDate" HeaderText="Ngày đặt" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            <asp:BoundField DataField="TotalPrice" HeaderText="Tổng tiền" DataFormatString="{0:N0} đ" />
            <asp:BoundField DataField="OrderStatus" HeaderText="Trạng thái" />
             <asp:HyperLinkField HeaderText="Hành động" Text="Xem chi tiết" 
                DataNavigateUrlFormatString="~/Admin/OrderDetails.aspx?id={0}" 
                DataNavigateUrlFields="OrderID" />
        </Columns>
    </asp:GridView>
</asp:Content>
