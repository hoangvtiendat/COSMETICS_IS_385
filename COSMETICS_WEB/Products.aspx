<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="COSMETICS_WEB.Products" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container page-container">
        <aside class="filter-sidebar">
            <h3>DANH MỤC</h3>
            <ul class="category-list">
                <li><a href="Products.aspx">Tất cả sản phẩm</a></li>
                <asp:Repeater ID="rptCategories" runat="server">
                    <ItemTemplate>
                        <li>
                            <a href='Products.aspx?catid=<%# Eval("CategoryID") %>'>
                                <%# Eval("CategoryName") %>
                            </a>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </aside>

        <main class="product-content">
            <h2>Tất cả sản phẩm</h2>
            <div class="product-grid">
                <asp:Repeater ID="rptProducts" runat="server">
                    <ItemTemplate>
                        <div class="product-card">
                            <a href='ProductDetails.aspx?id=<%# Eval("ProductID") %>'>
                                <img class="product-image" src='<%# Eval("ImagePath") %>' alt='<%# Eval("ProductName") %>' />
                                <h3 class="product-name"><%# Eval("ProductName") %></h3>
                                <p class="product-price"><%# string.Format("{0:N0} đ", Eval("Price")) %></p>
                            </a>
                            <asp:Button runat="server" Text="Thêm vào giỏ" CssClass="add-to-cart-btn" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </main>
    </div>
</asp:Content>