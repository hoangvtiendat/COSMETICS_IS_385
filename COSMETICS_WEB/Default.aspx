<%@ Page Title="Trang chủ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="COSMETICS_WEB.Default" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="hero-section">
            <h1>Skin For You</h1>
            <p>Chăm sóc làn da của bạn, nâng niu vẻ đẹp tự nhiên.</p>
            <a href="/Products.aspx" class="hero-button">KHÁM PHÁ NGAY</a>
        </div>
    </div>

    <section class="home-section">
        <div class="container">
            <h2 class="section-title">DANH MỤC SẢN PHẨM</h2>
            <div class="category-grid">
                <asp:Repeater ID="rptCategories" runat="server">
                    <ItemTemplate>
                        <a class="category-card" href='Products.aspx?catid=<%# Eval("CategoryID") %>'>
                            <i class="fa-solid fa-star"></i><%-- Thay icon nếu muốn --%>
                            <h3><%# Eval("CategoryName") %></h3>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>

    <section class="home-section">
        <div class="container">
            <h2 class="section-title">SẢN PHẨM NỔI BẬT</h2>
            <div class="product-grid">
                <asp:Repeater ID="rptFeaturedProducts" runat="server" OnItemCommand="rptProducts_ItemCommand">
                    <ItemTemplate>
                        <%-- Dùng lại giao diện product-card đã tạo ở trang Products.aspx --%>
                        <div class="product-card">
                            <a href='ProductDetails.aspx?id=<%# Eval("ProductID") %>'>
                                <img class="product-image" src='<%# Eval("ImagePath") %>' alt='<%# Eval("ProductName") %>' />
                                <h3 class="product-name"><%# Eval("ProductName") %></h3>
                                <p class="product-price"><%# string.Format("{0:N0} đ", Eval("Price")) %></p>
                            </a>
                            <asp:Button runat="server" Text="Thêm vào giỏ" CssClass="add-to-cart-btn"
                                CommandName="AddToCart" CommandArgument='<%# Eval("ProductID") %>' />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>

</asp:Content>
