using COSMETICS_WEB.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.DAL
{
    public class CartDAO
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        // Lấy CartID của user, nếu chưa có thì tạo mới
        private int GetOrCreateCart(int userId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetOrCreateCart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                con.Open();
                // Thủ tục này sẽ trả về CartID
                return (int)cmd.ExecuteScalar();
            }
        }

        // Thêm sản phẩm vào giỏ, hoặc tăng số lượng nếu đã có
        public void AddItemToCart(int userId, int productId, int quantity)
        {
            int cartId = GetOrCreateCart(userId);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddItemToCart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CartID", cartId);
                cmd.Parameters.AddWithValue("@ProductID", productId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<CartItem> GetCartItems(int userId)
        {
            List<CartItem> cartItems = new List<CartItem>();
            int cartId = GetOrCreateCart(userId); // Dùng lại hàm đã có

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
            SELECT ci.ProductID, p.ProductName, p.Price, ci.Quantity, pi.ImagePath
            FROM CartItems ci
            INNER JOIN Products p ON ci.ProductID = p.ProductID
            LEFT JOIN (SELECT ProductID, ImagePath FROM ProductImages WHERE IsMainImage = 1) pi ON p.ProductID = pi.ProductID
            WHERE ci.CartID = @CartID";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@CartID", cartId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    CartItem item = new CartItem();
                    item.ProductID = Convert.ToInt32(rdr["ProductID"]);
                    item.ProductName = rdr["ProductName"].ToString();
                    item.Price = Convert.ToDecimal(rdr["Price"]);
                    item.Quantity = Convert.ToInt32(rdr["Quantity"]);
                    item.ImagePath = rdr["ImagePath"].ToString();
                    cartItems.Add(item);
                }
            }
            return cartItems;
        }

        // Hàm 2: Cập nhật số lượng
        public void UpdateItemQuantity(int userId, int productId, int quantity)
        {
            int cartId = GetOrCreateCart(userId);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE CartItems SET Quantity = @Quantity WHERE CartID = @CartID AND ProductID = @ProductID", con);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@CartID", cartId);
                cmd.Parameters.AddWithValue("@ProductID", productId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Hàm 3: Xóa sản phẩm khỏi giỏ
        public void RemoveItemFromCart(int userId, int productId)
        {
            int cartId = GetOrCreateCart(userId);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM CartItems WHERE CartID = @CartID AND ProductID = @ProductID", con);
                cmd.Parameters.AddWithValue("@CartID", cartId);
                cmd.Parameters.AddWithValue("@ProductID", productId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}