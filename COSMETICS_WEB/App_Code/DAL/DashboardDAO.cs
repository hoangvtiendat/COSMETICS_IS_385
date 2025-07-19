using COSMETICS_WEB.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.DAL
{
    public class DashboardDAO
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public DashboardStats GetDashboardStats()
        {
            DashboardStats stats = new DashboardStats();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Lấy tất cả thông số trong 1 lần truy vấn để tối ưu
                string sqlQuery = @"
                    SELECT
                    (SELECT COUNT(*) FROM Orders) AS OrderCount,
                    (SELECT SUM(TotalPrice) FROM Orders WHERE OrderStatus = N'Đã giao') AS TotalRevenue,
                    (SELECT COUNT(*) FROM Users WHERE UserType = 'Customer') AS UserCount,
                    (SELECT COUNT(*) FROM Products) AS ProductCount";

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    stats.OrderCount = Convert.ToInt32(rdr["OrderCount"]);
                    stats.TotalRevenue = rdr["TotalRevenue"] != DBNull.Value ? Convert.ToDecimal(rdr["TotalRevenue"]) : 0;
                    stats.UserCount = Convert.ToInt32(rdr["UserCount"]);
                    stats.ProductCount = Convert.ToInt32(rdr["ProductCount"]);
                }
            }
            return stats;
        }
    }
}