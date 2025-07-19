using COSMETICS_WEB.App_Code.DAL;
using COSMETICS_WEB.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.BLL
{
    public class OrderBLL
    {
        private OrderDAO dao = new OrderDAO();
        public bool CreateOrder(Order order, List<CartItem> cartItems)
        {
            return dao.CreateOrder(order, cartItems);
        }

        public List<Order> GetAllOrders()
        {
            return dao.GetAllOrders();
        }
        public Order GetOrderById(int orderId)
        {
            return dao.GetOrderById(orderId);
        }
        public List<CartItem> GetOrderDetails(int orderId)
        {
            return dao.GetOrderDetails(orderId);
        }

        public void UpdateOrderStatus(int orderId, string newStatus)
        {
            dao.UpdateOrderStatus(orderId, newStatus);
        }

        public List<Order> GetOrdersByUserId(int userId)
        {
            return dao.GetOrdersByUserId(userId);
        }

    }
}