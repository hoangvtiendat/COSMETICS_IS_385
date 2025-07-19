using COSMETICS_WEB.App_Code.BLL;
using COSMETICS_WEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COSMETICS_WEB.Admin
{
    public partial class ProductEditor : COSMETICS_WEB.Admin.AdminBasePage
    {
        private bool IsEditMode => !string.IsNullOrEmpty(Request.QueryString["id"]);
        private int ProductId => Convert.ToInt32(Request.QueryString["id"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    BindCategories();
                    BindBrands();

                    if (IsEditMode)
                    {
                        // CHẾ ĐỘ SỬA: Tải dữ liệu cũ lên form
                        LoadProductData();
                    }
                }
            }
        }



        private void BindCategories()
        {
            CategoryBLL bll = new CategoryBLL();
            ddlCategory.DataSource = bll.GetAllCategories();
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataBind();
        }

        private void BindBrands()
        {
            BrandBLL bll = new BrandBLL();
            ddlBrand.DataSource = bll.GetAllBrands();
            ddlBrand.DataTextField = "BrandName";
            ddlBrand.DataValueField = "BrandID";
            ddlBrand.DataBind();
        }

        // Trong file ProductEditor.aspx.cs
        private void LoadProductData()
        {
            ProductBLL bll = new ProductBLL();
            Product product = bll.GetProductById(ProductId);
            if (product != null)
            {

                txtProductName.Text = product.ProductName;
                txtDescription.Text = product.Description;
                txtPrice.Text = product.Price.ToString("F0"); // F0 để bỏ phần thập phân
                txtStock.Text = product.StockQuantity.ToString();
                txtSKU.Text = product.SKU;

                // Chọn đúng giá trị trong DropDownList
                // Cần đảm bảo BindCategories() và BindBrands() đã được gọi trước hàm này
                ddlCategory.SelectedValue = product.CategoryID.ToString();
                ddlBrand.SelectedValue = product.BrandID.ToString();

                // Hiển thị danh sách các ảnh hiện có
                rptExistingImages.DataSource = bll.GetProductImages(ProductId);
                rptExistingImages.DataBind();
            }
        }
        protected void rptExistingImages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteImage")
            {
                int imageId = Convert.ToInt32(e.CommandArgument);
                ProductBLL bll = new ProductBLL();
                bll.DeleteProductImage(imageId);

                // Tải lại dữ liệu để cập nhật danh sách ảnh
                LoadProductData();
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            ProductBLL bll = new ProductBLL();
            Product product = new Product
            {
                ProductName = txtProductName.Text,
                Description = txtDescription.Text,
                Price = Convert.ToDecimal(txtPrice.Text),
                StockQuantity = Convert.ToInt32(txtStock.Text),
                SKU = txtSKU.Text,
                CategoryID = Convert.ToInt32(ddlCategory.SelectedValue),
                BrandID = Convert.ToInt32(ddlBrand.SelectedValue)
            };

            if (IsEditMode)
            {
                // === CHẾ ĐỘ SỬA ===
                product.ProductID = ProductId;
                bll.UpdateProduct(product); // 1. Cập nhật thông tin text

                // 2. Kiểm tra và upload các file ảnh MỚI
                if (fileUploadImage.HasFiles)
                {
                    List<string> newImagePaths = new List<string>();
                    foreach (HttpPostedFile uploadedFile in fileUploadImage.PostedFiles)
                    {
                        // ... (code upload và lưu file như cũ) ...
                        string fileName = Path.GetFileName(uploadedFile.FileName); // Cần using System.IO;
                        string savePath = Server.MapPath("~/assets/images/products/") + fileName;
                        uploadedFile.SaveAs(savePath);
                        newImagePaths.Add("/assets/images/products/" + fileName);
                    }

                    // 3. Thêm các ảnh mới này vào DB
                    if (newImagePaths.Any())
                    {
                        bll.AddProductImages(ProductId, newImagePaths);
                    }
                }
                Response.Redirect("ManageProducts.aspx");
            }
            else
            {
                // === CHẾ ĐỘ THÊM MỚI ===
                // 1. Kiểm tra xem có file nào được chọn không
                if (!fileUploadImage.HasFiles) // Dùng HasFiles (số nhiều)
                {
                    // lblMessage.Text = "Vui lòng chọn ít nhất một ảnh.";
                    return;
                }

                // 2. Lưu tất cả các file đã chọn
                List<string> uploadedImagePaths = new List<string>();
                foreach (HttpPostedFile uploadedFile in fileUploadImage.PostedFiles)
                {
                    try
                    {
                        string fileName = Path.GetFileNameWithoutExtension(uploadedFile.FileName);
                        string extension = Path.GetExtension(uploadedFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string savePath = Server.MapPath("~/assets/images/products/") + fileName;
                        uploadedFile.SaveAs(savePath);
                        uploadedImagePaths.Add("/assets/images/products/" + fileName);
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi upload
                    }
                }

                Response.Redirect("ManageProducts.aspx");
            }


            //protected void btnSave_Click(object sender, EventArgs e)
            //{
            //    if (!Page.IsValid) return;

            //    // 1. Xử lý upload file ảnh (nếu có)
            //    string imagePath = null; // Sẽ giữ giá trị null nếu không có file mới
            //    if (fileUploadImage.HasFile)
            //    {
            //        try
            //        {
            //            // Logic tạo tên file duy nhất và lưu file
            //            string fileName = Path.GetFileNameWithoutExtension(fileUploadImage.FileName);
            //            string extension = Path.GetExtension(fileUploadImage.FileName);
            //            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            //            string savePath = Server.MapPath("~/assets/images/products/") + fileName;
            //            fileUploadImage.SaveAs(savePath);
            //            imagePath = "/assets/images/products/" + fileName;
            //        }
            //        catch (Exception ex)
            //        {
            //            // Xử lý lỗi nếu cần
            //            return;
            //        }
            //    }

            //    // 2. Gom dữ liệu từ form
            //    Product product = new Product
            //    {
            //        ProductName = txtProductName.Text,
            //        Description = txtDescription.Text,
            //        Price = Convert.ToDecimal(txtPrice.Text),
            //        StockQuantity = Convert.ToInt32(txtStock.Text),
            //        SKU = txtSKU.Text,
            //        CategoryID = Convert.ToInt32(ddlCategory.SelectedValue),
            //        BrandID = Convert.ToInt32(ddlBrand.SelectedValue)
            //    };

            //    ProductBLL bll = new ProductBLL();

            //    // 3. Kiểm tra chế độ Sửa hay Thêm
            //    if (IsEditMode)
            //    {
            //        // === CHẾ ĐỘ SỬA ===
            //        product.ProductID = ProductId;
            //        bll.UpdateProduct(product); // Cập nhật thông tin (tên, giá, mô tả...)

            //        // Chỉ cập nhật ảnh nếu có file mới được upload (imagePath != null)
            //        if (imagePath != null)
            //        {
            //            bll.UpdateMainImage(ProductId, imagePath);
            //        }
            //    }
            //    else
            //    {
            //        // === CHẾ ĐỘ THÊM MỚI ===
            //        // 1. Kiểm tra xem có file nào được chọn không
            //        if (!fileUploadImage.HasFiles) // Dùng HasFiles (số nhiều)
            //        {
            //            // lblMessage.Text = "Vui lòng chọn ít nhất một ảnh.";
            //            return;
            //        }

            //        // 2. Lưu tất cả các file đã chọn
            //        List<string> uploadedImagePaths = new List<string>();
            //        foreach (HttpPostedFile uploadedFile in fileUploadImage.PostedFiles)
            //        {
            //            try
            //            {
            //                string fileName = Path.GetFileNameWithoutExtension(uploadedFile.FileName);
            //                string extension = Path.GetExtension(uploadedFile.FileName);
            //                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            //                string savePath = Server.MapPath("~/assets/images/products/") + fileName;
            //                uploadedFile.SaveAs(savePath);
            //                uploadedImagePaths.Add("/assets/images/products/" + fileName);
            //            }
            //            catch (Exception ex)
            //            {
            //                // Xử lý lỗi upload
            //            }
            //        }

            //        // 3. Thêm sản phẩm vào DB và lấy ID trả về
            //        int newProductId = bll.AddProduct(product);

            //        // 4. Thêm các đường dẫn ảnh vào DB với ID sản phẩm vừa tạo
            //        if (newProductId > 0 && uploadedImagePaths.Any())
            //        {
            //            // Gọi hàm AddProductImages đã sửa để set IsMainImage
            //            bll.AddProductImages(newProductId, uploadedImagePaths);
            //        }
            //    }

            //    Response.Redirect("ManageProducts.aspx");
            //}


        }
    }
}