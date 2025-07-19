<%@ Page Title="Quản lý sản phẩm" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageProducts.aspx.cs" Inherits="COSMETICS_WEB.Admin.ManageProducts" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Quản lý Sản phẩm</h1>
    <p>
        <asp:Button ID="btnAddProduct" runat="server" Text="Thêm sản phẩm mới" CssClass="hero-button" OnClick="btnAddProduct_Click1" />
    </p>


    <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" CssClass="gridview" DataKeyNames="ProductID" OnRowDeleting="gvProducts_RowDeleting" OnRowEditing="gvProducts_RowEditing">
        <Columns>
            <asp:BoundField DataField="ProductID" HeaderText="ID" />
            <asp:BoundField DataField="ProductName" HeaderText="Tên sản phẩm" />
            <asp:BoundField DataField="Price" HeaderText="Giá" DataFormatString="{0:N0} đ" />
            <asp:BoundField DataField="StockQuantity" HeaderText="Tồn kho" />
            <asp:BoundField DataField="CategoryName" HeaderText="Danh mục" />
            <asp:BoundField DataField="BrandName" HeaderText="Thương hiệu" />
            <%--<asp:CommandField ShowEditButton="true" ShowDeleteButton="true" HeaderText="Hành động" />--%>
            <asp:TemplateField HeaderText="Hành động">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CssClass="btn-action btn-edit">Sửa</asp:LinkButton>
                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CssClass="btn-action btn-delete"
                        OnClientClick="return confirm('Bạn có chắc muốn xóa sản phẩm này?');">Xóa</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
</asp:Content>
