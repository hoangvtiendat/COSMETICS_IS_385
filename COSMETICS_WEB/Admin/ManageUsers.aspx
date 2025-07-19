<%@ Page Title="Quản lý người dùng" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="COSMETICS_WEB.Admin.ManageUsers" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Quản lý Người dùng</h1>

    <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" CssClass="gridview"
        DataKeyNames="UserID"
        OnRowEditing="gvUsers_RowEditing"
        OnRowCancelingEdit="gvUsers_RowCancelingEdit"
        OnRowUpdating="gvUsers_RowUpdating"
        OnRowDeleting="gvUsers_RowDeleting"
        OnRowDataBound="gvUsers_RowDataBound"> 
        <Columns>
            <asp:BoundField DataField="UserID" HeaderText="ID" ReadOnly="true" />
            <asp:BoundField DataField="Username" HeaderText="Username" ReadOnly="true" />

            <asp:TemplateField HeaderText="Họ Tên">
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtFullName" runat="server" Text='<%# Bind("FullName") %>' CssClass="form-control"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="true" />

            <asp:TemplateField HeaderText="Vai trò">
                <ItemTemplate>
                    <asp:Label ID="lblUserType" runat="server" Text='<%# Eval("UserType") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control">
                        <asp:ListItem>Customer</asp:ListItem>
                        <asp:ListItem>Staff</asp:ListItem>
                        <asp:ListItem>Admin</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Hành động">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CssClass="btn-action btn-edit">Sửa</asp:LinkButton>
                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CssClass="btn-action btn-delete"
                        OnClientClick="return confirm('Bạn có chắc muốn xóa người dùng này?');">Xóa</asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update" CssClass="btn-action btn-update">Lưu</asp:LinkButton>
                    <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" CssClass="btn-action btn-cancel">Hủy</asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
