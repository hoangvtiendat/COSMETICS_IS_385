using COSMETICS_WEB.App_Code.DAL;
using COSMETICS_WEB.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.BLL
{
    public class DashboardBLL
    {
        private DashboardDAO dao = new DashboardDAO();
        public DashboardStats GetDashboardStats()
        {
            return dao.GetDashboardStats();
        }
    }
}