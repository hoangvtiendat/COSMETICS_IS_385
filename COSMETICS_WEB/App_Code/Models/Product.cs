using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } 
        public string ImagePath { get; set; }
        public string BrandName { get; set; } 
        public string CategoryName { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryID { get; set; } 
        public int BrandID { get; set; }    
        public string SKU { get; set; }    
    }
}