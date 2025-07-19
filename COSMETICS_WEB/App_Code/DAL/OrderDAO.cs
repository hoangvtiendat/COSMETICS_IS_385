using COSMETICS_WEB.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.DAL
{
    public class OrderDAO
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public bool CreateOrder(Order order, List<CartItem> cartItems)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                // Bắt đầu một giao dịch
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    // Bước 1: Thêm vào bảng Orders và lấy OrderID vừa tạo
                    string sqlInsertOrder = @"
                        INSERT INTO Orders (UserID, OrderDate, TotalPrice, OrderStatus, ShippingAddress, PaymentMethod) 
                        VALUES (@UserID, @OrderDate, @TotalPrice, @OrderStatus, @ShippingAddress, @PaymentMethod);
                        SELECT SCOPE_IDENTITY();"; // Lấy ID vừa insert

                    SqlCommand cmdOrder = new SqlCommand(sqlInsertOrder, con, transaction);
                    cmdOrder.Parameters.AddWithValue("@UserID", order.UserID);
                    cmdOrder.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                    cmdOrder.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                    cmdOrder.Parameters.AddWithValue("@OrderStatus", "Chờ xác nhận");
                    cmdOrder.Parameters.AddWithValue("@ShippingAddress", order.ShippingAddress);
                    cmdOrder.Parameters.AddWithValue("@PaymentMethod", "COD"); // Mặc định

                    int newOrderId = Convert.ToInt32(cmdOrder.ExecuteScalar());

                    // Bước 2: Thêm từng sản phẩm từ giỏ hàng vào OrderDetails
                    string sqlInsertDetail = @"
                        INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice) 
                        VALUES (@OrderID, @ProductID, @Quantity, @UnitPrice)";

                    foreach (var item in cartItems)
                    {
                        SqlCommand cmdDetail = new SqlCommand(sqlInsertDetail, con, transaction);
                        cmdDetail.Parameters.AddWithValue("@OrderID", newOrderId);
                        cmdDetail.Parameters.AddWithValue("@ProductID", item.ProductID);
                        cmdDetail.Parameters.AddWithValue("@Quantity", item.Quantity);
                        cmdDetail.Parameters.AddWithValue("@UnitPrice", item.Price);
                        cmdDetail.ExecuteNonQuery();
                    }

                    // Bước 3: Xóa giỏ hàng của người dùng
                    string sqlDeleteCart = "DELETE ci FROM CartItems ci INNER JOIN Carts c ON ci.CartID = c.CartID WHERE c.UserID = @UserID";
                    SqlCommand cmdDeleteCart = new SqlCommand(sqlDeleteCart, con, transaction);
                    cmdDeleteCart.Parameters.AddWithValue("@UserID", order.UserID);
                    cmdDeleteCart.ExecuteNonQuery();

                    // Nếu tất cả thành công, commit giao dịch
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    // Nếu có lỗi, rollback tất cả thay đổi
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orderList = new List<Order>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
            SELECT o.OrderID, o.OrderDate, o.TotalPrice, o.OrderStatus, u.FullName 
            FROM Orders o 
            LEFT JOIN Users u ON o.UserID = u.UserID 
            ORDER BY o.OrderDate DESC";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Order order = new Order();
                    order.OrderID = Convert.ToInt32(rdr["OrderID"]);
                    order.OrderDate = Convert.ToDateTime(rdr["OrderDate"]);
                    order.TotalPrice = Convert.ToDecimal(rdr["TotalPrice"]);
                    order.OrderStatus = rdr["OrderStatus"].ToString();
                    order.CustomerName = rdr["FullName"]?.ToString();
                    orderList.Add(order);
                }
            }
            return orderList;
        }

        public List<CartItem> GetOrderDetails(int orderId)
        {
            List<CartItem> items = new List<CartItem>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = @"
            SELECT od.ProductID, p.ProductName, od.Quantity, od.UnitPrice 
            FROM OrderDetails od 
            INNER JOIN Products p ON od.ProductID = p.ProductID 
            WHERE od.OrderID = @OrderID";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    items.Add(new CartItem
                    {
                        ProductID = Convert.ToInt32(rdr["ProductID"]),
                        ProductName = rdr["ProductName"].ToString(),
                        Quantity = Convert.ToInt32(rdr["Quantity"]),
                        Price = Convert.ToDecimal(rdr["UnitPrice"])
                    });
                }
            }
            return items;
        }

        public Order GetOrderById(int orderId)
        {
            Order order = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Lấy tất cả thông tin từ bảng Orders và tên khách hàng từ bảng Users
                string sqlQuery = @"
            SELECT o.*, u.FullName 
            FROM Orders o
            LEFT JOIN Users u ON o.UserID = u.UserID
            WHERE o.OrderID = @OrderID";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read()) // Chỉ mong đợi một dòng
                {
                    order = new Order();
                    order.OrderID = Convert.ToInt32(rdr["OrderID"]);
                    order.UserID = Convert.ToInt32(rdr["UserID"]);
                    order.OrderDate = Convert.ToDateTime(rdr["OrderDate"]);
                    order.TotalPrice = Convert.ToDecimal(rdr["TotalPrice"]);
                    order.OrderStatus = rdr["OrderStatus"].ToString();
                    order.ShippingAddress = rdr["ShippingAddress"].ToString();
                    order.PaymentMethod = rdr["PaymentMethod"].ToString();
                    order.CustomerName = rdr["FullName"]?.ToString();
                }
            }
            return order;
        }

        public void UpdateOrderStatus(int orderId, string newStatus)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Orders SET OrderStatus = @Status WHERE OrderID = @OrderID", con);
                cmd.Parameters.AddWithValue("@Status", newStatus);
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Order> GetOrdersByUserId(int userId)
        {
            List<Order> orderList = new List<Order>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Lấy các đơn hàng của một UserID cụ thể, sắp xếp mới nhất lên đầu
                string sqlQuery = "SELECT OrderID, OrderDate, TotalPrice, OrderStatus FROM Orders WHERE UserID = @UserID ORDER BY OrderDate DESC";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@UserID", userId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Order order = new Order();
                    order.OrderID = Convert.ToInt32(rdr["OrderID"]);
                    order.OrderDate = Convert.ToDateTime(rdr["OrderDate"]);
                    order.TotalPrice = Convert.ToDecimal(rdr["TotalPrice"]);
                    order.OrderStatus = rdr["OrderStatus"].ToString();
                    orderList.Add(order);
                }
            }
            return orderList;
        }

    }
}