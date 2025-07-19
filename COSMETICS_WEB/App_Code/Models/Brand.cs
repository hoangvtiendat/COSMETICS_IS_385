using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.Models
{
    public class Brand
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        // Thêm LogoPath nếu bạn muốn hiển thị logo
        public string LogoPath { get; set; }
        public string Description { get; set; }
    }
}