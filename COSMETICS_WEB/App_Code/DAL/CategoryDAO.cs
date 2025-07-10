using COSMETICS_WEB.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.DAL
{
    public class CategoryDAO
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public List<Category> GetAllCategories()
        {
            List<Category> categoryList = new List<Category>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT CategoryID, CategoryName FROM Categories", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Category category = new Category();
                    category.CategoryID = Convert.ToInt32(rdr["CategoryID"]);
                    category.CategoryName = rdr["CategoryName"].ToString();
                    categoryList.Add(category);
                }
            }
            return categoryList;
        }
    }
}