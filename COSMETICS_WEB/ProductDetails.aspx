<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="COSMETICS_WEB.ProductDetails" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="product-detail-container">
            <div class="product-images">
                <div class="main-image">
                    <asp:Image ID="imgMainProduct" runat="server" />
                </div>
                <div class="thumbnail-list">
                    <asp:Repeater ID="rptProductImages" runat="server">
                        <ItemTemplate>
                            <img src='<%# Eval("ImagePath") %>' alt="thumbnail" class="thumbnail-item" onclick="changeImage(this)" />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="product-info">
                <h1>
                    <asp:Literal ID="litProductName" runat="server"></asp:Literal>
                </h1>
                <div class="info-meta">
                    <span>Thương hiệu:
                        <asp:Literal ID="litBrand" runat="server"></asp:Literal>
                    </span>
                    <span>Danh mục:
                        <asp:Literal ID="litCategory" runat="server"></asp:Literal>
                    </span>
                </div>
                <div class="price-tag">
                    <asp:Literal ID="litPrice" runat="server"></asp:Literal>
                </div>
                <div class="description">
                    <h4>Mô tả sản phẩm</h4>
                    <asp:Literal ID="litDescription" runat="server"></asp:Literal>
                </div>
                <div class="add-to-cart-action">
                    <asp:Button ID="btnAdd_Click" runat="server" Text="Thêm vào giỏ" OnClick="btnAdd_Click_Click" CssClass="hero-button" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function changeImage(thumbnail) {
            var mainImage = document.getElementById('<%= imgMainProduct.ClientID %>');
            mainImage.src = thumbnail.src;
        }
    </script>
</asp:Content>
