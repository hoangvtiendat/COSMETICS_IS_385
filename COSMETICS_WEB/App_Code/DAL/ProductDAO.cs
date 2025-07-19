using COSMETICS_WEB.App_Code.Models;
using COSMETICS_WEB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.DAL
{
    public class ProductDAO
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // === SỬA LẠI CÂU LỆNH SQL ĐỂ LẤY THÊM IMAGEPATH ===
                string sqlQuery = @"
            SELECT p.ProductID, p.ProductName, p.Price, p.StockQuantity, c.CategoryName, b.BrandName, pi.ImagePath
            FROM Products p
            LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
            LEFT JOIN Brands b ON p.BrandID = b.BrandID
            LEFT JOIN (SELECT ProductID, ImagePath FROM ProductImages WHERE IsMainImage = 1) pi ON p.ProductID = pi.ProductID";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Product product = new Product();
                    product.ProductID = Convert.ToInt32(rdr["ProductID"]);
                    product.ProductName = rdr["ProductName"].ToString();
                    product.Price = Convert.ToDecimal(rdr["Price"]);
                    product.StockQuantity = Convert.ToInt32(rdr["StockQuantity"]);
                    product.CategoryName = rdr["CategoryName"]?.ToString();
                    product.BrandName = rdr["BrandName"]?.ToString();

                    // === THÊM DÒNG LẤY DỮ LIỆU ẢNH ===
                    product.ImagePath = rdr["ImagePath"] != DBNull.Value ? rdr["ImagePath"].ToString() : "";

                    productList.Add(product);
                }
            }
            return productList;
        }

        public List<Product> GetProductsByCategoryID(int categoryId)
        {
            List<Product> productList = new List<Product>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
            SELECT p.ProductID, p.ProductName, p.Price, pi.ImagePath 
            FROM Products p
            INNER JOIN ProductImages pi ON p.ProductID = pi.ProductID
            WHERE pi.IsMainImage = 1 AND p.CategoryID = @CategoryID"; // Thêm điều kiện lọc

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId); // Dùng parameter để tránh SQL Injection
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    // ... (code đọc dữ liệu y hệt hàm GetAllProducts)
                    Product product = new Product();
                    product.ProductID = Convert.ToInt32(rdr["ProductID"]);
                    product.ProductName = rdr["ProductName"].ToString();
                    product.Price = Convert.ToDecimal(rdr["Price"]);
                    product.ImagePath = rdr["ImagePath"].ToString();
                    productList.Add(product);
                }
            }
            return productList;
        }

        public List<Product> GetFeaturedProducts(int count)
        {
            List<Product> productList = new List<Product>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Dùng TOP (@count) để lấy ra số lượng sản phẩm giới hạn
                string sqlQuery = @"
            SELECT TOP (@count) p.ProductID, p.ProductName, p.Price, pi.ImagePath 
            FROM Products p
            INNER JOIN ProductImages pi ON p.ProductID = pi.ProductID
            WHERE pi.IsMainImage = 1
            ORDER BY p.ProductID DESC"; // Lấy các sản phẩm mới nhất làm sản phẩm nổi bật

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@count", count);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Product product = new Product();
                    product.ProductID = Convert.ToInt32(rdr["ProductID"]);
                    product.ProductName = rdr["ProductName"].ToString();
                    product.Price = Convert.ToDecimal(rdr["Price"]);
                    product.ImagePath = rdr["ImagePath"].ToString();
                    productList.Add(product);
                }
            }
            return productList;
        }

        public List<Product> GetProductsByBrandID(int brandId)
        {
            List<Product> productList = new List<Product>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
            SELECT p.ProductID, p.ProductName, p.Price, pi.ImagePath 
            FROM Products p
            INNER JOIN ProductImages pi ON p.ProductID = pi.ProductID
            WHERE pi.IsMainImage = 1 AND p.BrandID = @BrandID"; // Lọc theo BrandID

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@BrandID", brandId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    // ... code đọc dữ liệu y hệt các hàm trước ...
                    Product product = new Product();
                    product.ProductID = Convert.ToInt32(rdr["ProductID"]);
                    product.ProductName = rdr["ProductName"].ToString();
                    product.Price = Convert.ToDecimal(rdr["Price"]);
                    product.ImagePath = rdr["ImagePath"].ToString();
                    productList.Add(product);
                }
            }
            return productList;
        }

        public Product GetProductById(int productId)
        {
            Product product = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Đảm bảo câu lệnh SELECT có JOIN và lấy đủ các cột
                string sqlQuery = @"
            SELECT p.ProductID, p.ProductName, p.Description, p.Price, p.StockQuantity, p.SKU, p.CategoryID, p.BrandID, b.BrandName, c.CategoryName 
            FROM Products p
            LEFT JOIN Brands b ON p.BrandID = b.BrandID
            LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
            WHERE p.ProductID = @ProductID";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@ProductID", productId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    product = new Product();
                    product.ProductID = Convert.ToInt32(rdr["ProductID"]);
                    product.ProductName = rdr["ProductName"].ToString();
                    product.Price = Convert.ToDecimal(rdr["Price"]);

                    // === ĐẢM BẢO BẠN CÓ CÁC DÒNG NÀY ===
                    product.Description = rdr["Description"].ToString();
                    product.BrandName = rdr["BrandName"].ToString();
                    product.CategoryName = rdr["CategoryName"].ToString();
                    // ===================================

                    product.StockQuantity = Convert.ToInt32(rdr["StockQuantity"]);
                    product.SKU = rdr["SKU"].ToString();
                    product.CategoryID = Convert.ToInt32(rdr["CategoryID"]);
                    product.BrandID = Convert.ToInt32(rdr["BrandID"]);
                }
            }
            return product;
        }

        public List<ProductImage> GetProductImages(int productId)
        {
            List<ProductImage> imageList = new List<ProductImage>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Lấy thêm ImageID và IsMainImage
                SqlCommand cmd = new SqlCommand("SELECT ImageID, ImagePath, IsMainImage FROM ProductImages WHERE ProductID = @ProductID ORDER BY IsMainImage DESC", con);
                cmd.Parameters.AddWithValue("@ProductID", productId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ProductImage img = new ProductImage();
                    img.ImageID = Convert.ToInt32(rdr["ImageID"]);
                    img.ImagePath = rdr["ImagePath"].ToString();
                    img.IsMainImage = Convert.ToBoolean(rdr["IsMainImage"]);
                    imageList.Add(img);
                }
            }
            return imageList;
        }

        // Thêm hàm mới để xóa một ảnh cụ thể bằng ImageID
        public void DeleteProductImage(int imageId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM ProductImages WHERE ImageID = @ImageID", con);
                cmd.Parameters.AddWithValue("@ImageID", imageId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int AddProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlInsertProduct = @"
            INSERT INTO Products (ProductName, Description, Price, StockQuantity, CategoryID, BrandID, SKU, CreatedDate) 
            VALUES (@ProductName, @Description, @Price, @StockQuantity, @CategoryID, @BrandID, @SKU, @CreatedDate);
            SELECT SCOPE_IDENTITY();";

                SqlCommand cmdProduct = new SqlCommand(sqlInsertProduct, con);
                cmdProduct.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmdProduct.Parameters.AddWithValue("@Description", product.Description);
                cmdProduct.Parameters.AddWithValue("@Price", product.Price);
                cmdProduct.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
                cmdProduct.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                cmdProduct.Parameters.AddWithValue("@BrandID", product.BrandID);
                cmdProduct.Parameters.AddWithValue("@SKU", product.SKU);
                cmdProduct.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                con.Open();
                // Trả về ID của sản phẩm vừa được tạo
                return Convert.ToInt32(cmdProduct.ExecuteScalar());
            }
        }

        // Thêm hàm mới để xử lý việc thêm nhiều ảnh
        public void AddProductImages(int productId, List<string> imagePaths)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                for (int i = 0; i < imagePaths.Count; i++)
                {
                    string sqlInsertImage = "INSERT INTO ProductImages (ProductID, ImagePath, IsMainImage) VALUES (@ProductID, @ImagePath, @IsMainImage)";
                    SqlCommand cmdImage = new SqlCommand(sqlInsertImage, con);
                    cmdImage.Parameters.AddWithValue("@ProductID", productId);
                    cmdImage.Parameters.AddWithValue("@ImagePath", imagePaths[i]);
                    // Ảnh đầu tiên trong danh sách sẽ là ảnh chính
                    cmdImage.Parameters.AddWithValue("@IsMainImage", i == 0 ? 1 : 0);
                    cmdImage.ExecuteNonQuery();
                }
            }
        }
        public void DeleteProduct(int productId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    // Xóa các ảnh liên quan trước
                    SqlCommand cmdImages = new SqlCommand("DELETE FROM ProductImages WHERE ProductID = @ProductID", con, transaction);
                    cmdImages.Parameters.AddWithValue("@ProductID", productId);
                    cmdImages.ExecuteNonQuery();

                    // Xóa sản phẩm
                    SqlCommand cmdProduct = new SqlCommand("DELETE FROM Products WHERE ProductID = @ProductID", con, transaction);
                    cmdProduct.Parameters.AddWithValue("@ProductID", productId);
                    cmdProduct.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }


        public void UpdateProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
            UPDATE Products 
            SET ProductName = @ProductName, 
                Description = @Description, 
                Price = @Price, 
                StockQuantity = @StockQuantity, 
                CategoryID = @CategoryID, 
                BrandID = @BrandID, 
                SKU = @SKU
            WHERE ProductID = @ProductID";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                // ĐẢM BẢO BẠN CÓ ĐẦY ĐỦ CÁC DÒNG THÊM THAM SỐ NÀY
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
                cmd.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                cmd.Parameters.AddWithValue("@BrandID", product.BrandID);
                cmd.Parameters.AddWithValue("@SKU", product.SKU);
                cmd.Parameters.AddWithValue("@ProductID", product.ProductID);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateMainImage(int productId, string newImagePath)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Cập nhật đường dẫn ảnh của dòng có IsMainImage = 1
                string sqlQuery = "UPDATE ProductImages SET ImagePath = @ImagePath WHERE ProductID = @ProductID AND IsMainImage = 1";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@ImagePath", newImagePath);
                cmd.Parameters.AddWithValue("@ProductID", productId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            List<Product> productList = new List<Product>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Dùng LIKE để tìm kiếm gần đúng trong Tên và Mô tả sản phẩm
                string sqlQuery = @"
            SELECT p.ProductID, p.ProductName, p.Price, pi.ImagePath 
            FROM Products p
            INNER JOIN ProductImages pi ON p.ProductID = pi.ProductID
            WHERE pi.IsMainImage = 1 AND (p.ProductName LIKE @SearchTerm OR p.Description LIKE @SearchTerm)";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                // Thêm % để tìm kiếm bất kỳ vị trí nào trong chuỗi
                cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Product product = new Product();
                    product.ProductID = Convert.ToInt32(rdr["ProductID"]);
                    product.ProductName = rdr["ProductName"].ToString();
                    product.Price = Convert.ToDecimal(rdr["Price"]);
                    product.ImagePath = rdr["ImagePath"].ToString();
                    productList.Add(product);
                }
            }
            return productList;
        }


    }
}