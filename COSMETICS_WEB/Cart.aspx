<%@ Page Title="Giỏ hàng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="COSMETICS_WEB.Cart" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container cart-page">
        <h2 class="section-title">GIỎ HÀNG CỦA BẠN</h2>
        <asp:Repeater ID="rptCartItems" runat="server" OnItemCommand="rptCartItems_ItemCommand">
            <HeaderTemplate>
                <table class="cart-table">
                    <thead>
                        <tr>
                            <th colspan="2">Sản phẩm</th>
                            <th>Đơn giá</th>
                            <th>Số lượng</th>
                            <th>Thành tiền</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <img src='<%# Eval("ImagePath") %>' class="cart-item-image" /></td>
                    <td><%# Eval("ProductName") %></td>
                    <td><%# string.Format("{0:N0} đ", Eval("Price")) %></td>
                    <td>
                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("Quantity") %>'
                            CssClass="quantity-input" TextMode="Number" AutoPostBack="true"
                            OnTextChanged="txtQuantity_TextChanged"></asp:TextBox>
                        <asp:HiddenField ID="hdnProductId" runat="server" Value='<%# Eval("ProductID") %>' />
                    </td>
                    <td><%# string.Format("{0:N0} đ", Eval("TotalPrice")) %></td>
                    <td>
                        <asp:LinkButton ID="btnRemove" runat="server" CssClass="remove-btn"
                            CommandName="RemoveItem" CommandArgument='<%# Eval("ProductID") %>'>
                            <i class="fa-solid fa-trash"></i>
                        </asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <div class="cart-summary">
            <h3>Tổng cộng:
                <asp:Literal ID="litTotalPrice" runat="server"></asp:Literal></h3>
            <asp:Button ID="btnCheckout" runat="server" Text="Tiến hành thanh toán"
                CssClass="hero-button" PostBackUrl="~/Checkout.aspx" />
        </div>

        <asp:Panel ID="pnlEmptyCart" runat="server" Visible="false" CssClass="empty-cart-panel">
            <p>Giỏ hàng của bạn đang trống.</p>
            <a href="/Products.aspx" class="hero-button">Tiếp tục mua sắm</a>
        </asp:Panel>
    </div>
</asp:Content>
