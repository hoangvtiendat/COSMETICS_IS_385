using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.Models
{
    public class ProductImage
    {
        public int ImageID { get; set; }
        public string ImagePath { get; set; }
        public bool IsMainImage { get; set; }
    }
}