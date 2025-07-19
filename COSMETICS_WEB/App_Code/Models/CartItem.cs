using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.Models
{
    public class CartItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ImagePath { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }
}