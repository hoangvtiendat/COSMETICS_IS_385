using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.Models
{
    public class DashboardStats
    {
        public int OrderCount { get; set; }
        public decimal TotalRevenue { get; set; }
        public int UserCount { get; set; }
        public int ProductCount { get; set; }
    }
}