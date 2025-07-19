<%@ Page Title="Trang Chủ" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="COSMETICS_WEB.Admin.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Dashboard</h1>

    <div class="stat-cards-container">
        <div class="stat-card revenue">
            <h4>Tổng doanh thu</h4>
            <h2>
                <asp:Literal ID="litTotalRevenue" runat="server"></asp:Literal></h2>
            <i class="fa-solid fa-dollar-sign"></i>
        </div>
        <div class="stat-card orders">
            <h4>Tổng đơn hàng</h4>
            <h2>
                <asp:Literal ID="litOrderCount" runat="server"></asp:Literal></h2>
            <i class="fa-solid fa-receipt"></i>
        </div>
        <div class="stat-card users">
            <h4>Số khách hàng</h4>
            <h2>
                <asp:Literal ID="litUserCount" runat="server"></asp:Literal></h2>
            <i class="fa-solid fa-users"></i>
        </div>
        <div class="stat-card products">
            <h4>Số sản phẩm</h4>
            <h2>
                <asp:Literal ID="litProductCount" runat="server"></asp:Literal></h2>
            <i class="fa-solid fa-box"></i>
        </div>
    </div>

    <h2 class="section-title-admin">Đơn hàng gần đây</h2>
    <asp:GridView ID="gvRecentOrders" runat="server" AutoGenerateColumns="false" CssClass="gridview">
        <Columns>
            <asp:BoundField DataField="OrderID" HeaderText="Mã ĐH" />
            <asp:BoundField DataField="CustomerName" HeaderText="Tên khách hàng" />
            <asp:BoundField DataField="OrderDate" HeaderText="Ngày đặt" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            <asp:BoundField DataField="TotalPrice" HeaderText="Tổng tiền" DataFormatString="{0:N0} đ" />
            <asp:BoundField DataField="OrderStatus" HeaderText="Trạng thái" />
            <asp:HyperLinkField HeaderText="Hành động" Text="Xem" DataNavigateUrlFormatString="~/Admin/OrderDetails.aspx?id={0}" DataNavigateUrlFields="OrderID" />
        </Columns>
    </asp:GridView>

</asp:Content>
