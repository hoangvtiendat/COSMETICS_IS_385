<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="COSMETICS_WEB.UserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2 class="section-title">LỊCH SỬ ĐƠN HÀNG</h2>
        
        <asp:GridView ID="gvOrderHistory" runat="server" AutoGenerateColumns="false" CssClass="gridview" EmptyDataText="Bạn chưa có đơn hàng nào.">
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="Mã ĐH" />
                <asp:BoundField DataField="OrderDate" HeaderText="Ngày đặt" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:BoundField DataField="TotalPrice" HeaderText="Tổng tiền" DataFormatString="{0:N0} đ" />
                <asp:TemplateField HeaderText="Trạng thái">
                    <ItemTemplate>
                        <span class="order-status status-<%# Eval("OrderStatus").ToString().ToLower().Replace(" ", "-") %>">
                            <%# Eval("OrderStatus") %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
