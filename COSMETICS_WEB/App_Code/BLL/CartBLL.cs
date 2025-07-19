using COSMETICS_WEB.App_Code.DAL;
using COSMETICS_WEB.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.BLL
{
    public class CartBLL
    {
        private CartDAO dao = new CartDAO();

        public void AddItemToCart(int userId, int productId, int quantity)
        {
            dao.AddItemToCart(userId, productId, quantity);
        }

        public List<CartItem> GetCartItems(int userId)
        {
            return dao.GetCartItems(userId);
        }

        public void UpdateItemQuantity(int userId, int productId, int quantity)
        {
            // Đảm bảo số lượng không nhỏ hơn 1
            if (quantity < 1)
            {
                quantity = 1;
            }
            dao.UpdateItemQuantity(userId, productId, quantity);
        }

        public void RemoveItemFromCart(int userId, int productId)
        {
            dao.RemoveItemFromCart(userId, productId);
        }
    }
}