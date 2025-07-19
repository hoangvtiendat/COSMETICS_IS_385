<%@ Page Title="Quản lý danh mục" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageCategories.aspx.cs" Inherits="COSMETICS_WEB.Admin.ManageCategories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Quản lý Danh mục</h1>
    <div class="add-panel">
        <h4>Thêm danh mục mới</h4>
        <div class="form-inline">
            <asp:TextBox ID="txtNewCategoryName" runat="server" placeholder="Tên danh mục mới" CssClass="form-control"></asp:TextBox>
            <asp:DropDownList ID="ddlNewParentCategory" runat="server" CssClass="form-control"></asp:DropDownList>
            <asp:Button ID="btnAddNewCategory" runat="server" Text="Thêm mới" CssClass="hero-button" OnClick="btnAddNewCategory_Click" />
        </div>
    </div>

    <asp:GridView ID="gvCategories" runat="server" AutoGenerateColumns="false" CssClass="gridview"
        DataKeyNames="CategoryID" OnRowEditing="gvCategories_RowEditing" OnRowCancelingEdit="gvCategories_RowCancelingEdit"
        OnRowUpdating="gvCategories_RowUpdating" OnRowDeleting="gvCategories_RowDeleting" OnRowDataBound="gvCategories_RowDataBound">
        <Columns>
            <asp:BoundField DataField="CategoryID" HeaderText="ID" ReadOnly="True" />
            <asp:TemplateField HeaderText="Tên Danh mục">
                <ItemTemplate><%# Eval("CategoryName") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtCategoryName" runat="server" Text='<%# Bind("CategoryName") %>' CssClass="form-control" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Danh mục cha">
                <ItemTemplate><%# Eval("ParentCategoryName") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlParentCategory" runat="server" CssClass="form-control" />
                </EditItemTemplate>
            </asp:TemplateField>
            <%-- <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" HeaderText="Hành động" />--%>
            <asp:TemplateField HeaderText="Hành động">

                <%-- Giao diện khi ở trạng thái xem bình thường --%>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CssClass="btn-action btn-edit">Sửa</asp:LinkButton>
                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CssClass="btn-action btn-delete"
                        OnClientClick="return confirm('Bạn có chắc muốn xóa danh mục này?');">Xóa</asp:LinkButton>
                </ItemTemplate>

                <%-- Giao diện khi bấm nút "Sửa" (trạng thái chỉnh sửa) --%>
                <EditItemTemplate>
                    <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" CssClass="btn-action btn-update">Lưu</asp:LinkButton>
                    <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" CssClass="btn-action btn-cancel">Hủy</asp:LinkButton>
                </EditItemTemplate>

            </asp:TemplateField>

        </Columns>
    </asp:GridView>
</asp:Content>
