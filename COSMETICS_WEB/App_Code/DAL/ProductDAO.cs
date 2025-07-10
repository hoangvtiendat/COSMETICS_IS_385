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
                // ===== CÂU LỆNH SQL ĐÃ ĐƯỢC CẬP NHẬT =====
                // Ta JOIN bảng Products với ProductImages và chỉ lấy những ảnh là ảnh chính (IsMainImage = 1)
                string sqlQuery = @"
                    SELECT p.ProductID, p.ProductName, p.Price, pi.ImagePath 
                    FROM Products p
                    INNER JOIN ProductImages pi ON p.ProductID = pi.ProductID
                    WHERE pi.IsMainImage = 1";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Product product = new Product();
                    product.ProductID = Convert.ToInt32(rdr["ProductID"]);
                    product.ProductName = rdr["ProductName"].ToString();
                    product.Price = Convert.ToDecimal(rdr["Price"]);

                    // Sửa lại tên cột cho đúng với database của bạn
                    product.ImagePath = rdr["ImagePath"].ToString();

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

    }
}