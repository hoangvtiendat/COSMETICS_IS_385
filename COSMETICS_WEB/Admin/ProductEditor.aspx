<%@ Page Title="Chỉnh sửa sản phẩm" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ProductEditor.aspx.cs" Inherits="COSMETICS_WEB.Admin.ProductEditor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Thêm sản phẩm mới</h1>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="control-label col-md-2">Tên sản phẩm</label>
            <div class="col-md-10">
                <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-2">Mô tả</label>
            <div class="col-md-10">
                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-2">Giá</label>
            <div class="col-md-10">
                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-2">Số lượng tồn kho</label>
            <div class="col-md-10">
                <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-2">SKU</label>
            <div class="col-md-10">
                <asp:TextBox ID="txtSKU" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-2">Danh mục</label>
            <div class="col-md-10">
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-2">Thương hiệu</label>
            <div class="col-md-10">
                <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>


        <div class="form-group">
            <label class="control-label col-md-2">Các ảnh hiện tại</label>
            <div class="col-md-10">
                <asp:UpdatePanel ID="updPanelImages" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Repeater ID="rptExistingImages" runat="server" OnItemCommand="rptExistingImages_ItemCommand">
                            <ItemTemplate>
                                <div class="existing-image-item">
                                    <img src='<%# Eval("ImagePath") %>' />
                                    <span><%# Convert.ToBoolean(Eval("IsMainImage")) ? "(Ảnh chính)" : "" %></span>
                                    <asp:LinkButton ID="btnDeleteImage" runat="server"
                                        CommandName="DeleteImage" CommandArgument='<%# Eval("ImageID") %>'
                                        CssClass="delete-image-btn" OnClientClick="return confirm('Bạn có chắc muốn xóa ảnh này?');">
                                <i class="fa-solid fa-trash"></i> Xóa
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                    <Triggers>
                        <%-- Báo cho UpdatePanel biết phải làm mới khi sự kiện ItemCommand của Repeater xảy ra --%>
                        <asp:AsyncPostBackTrigger ControlID="rptExistingImages" EventName="ItemCommand" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="form-group">
            <label class="control-label col-md-2">Thêm ảnh mới</label>
            <div class="col-md-10">
                <asp:FileUpload ID="fileUploadImage" runat="server" AllowMultiple="true" CssClass="form-control" />
                <p class="help-text">Bạn có thể chọn nhiều ảnh để thêm vào sản phẩm.</p>
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="btnSave" runat="server" Text="Lưu sản phẩm" CssClass="hero-button" OnClick="btnSave_Click" />
                <a href="ManageProducts.aspx" class="cancel-link">Hủy</a>
            </div>
        </div>
    </div>
</asp:Content>
