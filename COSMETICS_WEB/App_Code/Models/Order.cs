using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
        // Thêm các trường để lấy thông tin người nhận
        public string FullNameNguoiNhan { get; set; }
        public string SoDienThoaiNguoiNhan { get; set; }
        public string CustomerName { get; set; }
    }
}