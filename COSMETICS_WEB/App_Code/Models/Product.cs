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
        public string ImagePath { get; set; } // Giả sử bạn lưu đường dẫn ảnh chính ở đây
        // Thêm các thuộc tính khác nếu cần
    }
}