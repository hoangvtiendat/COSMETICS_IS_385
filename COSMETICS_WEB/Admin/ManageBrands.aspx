<%@ Page Title="Quản lý thương hiệu" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageBrands.aspx.cs" Inherits="COSMETICS_WEB.Admin.ManageBrands" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Quản lý Thương hiệu</h1>
    <div class="add-panel">
        <h4>Thêm thương hiệu mới</h4>
        <div class="form-inline">
            <asp:TextBox ID="txtNewBrandName" runat="server" placeholder="Tên thương hiệu" CssClass="form-control"></asp:TextBox>
            <asp:TextBox ID="txtNewDescription" runat="server" placeholder="Mô tả" CssClass="form-control"></asp:TextBox>
            <asp:FileUpload ID="fileUploadLogo" runat="server" />
            <asp:Button ID="btnAddNewBrand" runat="server" Text="Thêm mới" CssClass="hero-button" OnClick="btnAddNewBrand_Click" />
        </div>
    </div>

    <asp:GridView ID="gvBrands" runat="server" AutoGenerateColumns="false" CssClass="gridview"
        DataKeyNames="BrandID" OnRowEditing="gvBrands_RowEditing" OnRowCancelingEdit="gvBrands_RowCancelingEdit"
        OnRowUpdating="gvBrands_RowUpdating" OnRowDeleting="gvBrands_RowDeleting">
        <Columns>
            <asp:BoundField DataField="BrandID" HeaderText="ID" ReadOnly="True" />
            <%-- <asp:TemplateField HeaderText="Logo">
                <ItemTemplate>
                    <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# Eval("LogoPath") %>' Width="100px" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Image ID="imgCurrentLogo" runat="server" ImageUrl='<%# Eval("LogoPath") %>' Width="100px" CssClass="current-product-image" />
                    <asp:FileUpload ID="fileUploadEditLogo" runat="server" />
                    <p class="help-text">Chọn file mới để thay đổi logo.</p>
                </EditItemTemplate>
            </asp:TemplateField>--%>

            <asp:TemplateField HeaderText="Logo">
                <ItemTemplate>
                    <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# Eval("LogoPath") %>' Width="100px" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Image ID="imgCurrentLogo" runat="server" ImageUrl='<%# Eval("LogoPath") %>' Width="100px" CssClass="current-product-image" />

                    <div style="margin-top: 5px;">
                        <asp:CheckBox ID="chkDeleteLogo" runat="server" Text="Xóa logo hiện tại" />
                    </div>

                    <asp:FileUpload ID="fileUploadEditLogo" runat="server" />
                    <p class="help-text">Chọn file mới để thay đổi logo.</p>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Tên Thương hiệu">
                <ItemTemplate><%# Eval("BrandName") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtBrandName" runat="server" Text='<%# Bind("BrandName") %>' CssClass="form-control" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mô tả">
                <ItemTemplate><%# Eval("Description") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>' CssClass="form-control" TextMode="MultiLine" Rows="2" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hành động">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CssClass="btn-action btn-edit">Sửa</asp:LinkButton>
                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CssClass="btn-action btn-delete" OnClientClick="return confirm('Bạn có chắc muốn xóa thương hiệu này?');">Xóa</asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" CssClass="btn-action btn-update">Lưu</asp:LinkButton>
                    <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" CssClass="btn-action btn-cancel">Hủy</asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
