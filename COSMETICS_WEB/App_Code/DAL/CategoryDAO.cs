using COSMETICS_WEB.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.DAL
{
    public class CategoryDAO
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        //public List<Category> GetAllCategories()
        //{
        //    List<Category> categoryList = new List<Category>();
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("SELECT CategoryID, CategoryName FROM Categories", con);
        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();
        //        while (rdr.Read())
        //        {
        //            Category category = new Category();
        //            category.CategoryID = Convert.ToInt32(rdr["CategoryID"]);
        //            category.CategoryName = rdr["CategoryName"].ToString();
        //            categoryList.Add(category);
        //        }
        //    }
        //    return categoryList;
        //}

        public DataTable GetAllCategories()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Dùng self-join để lấy tên của Parent
                SqlCommand cmd = new SqlCommand(@"
                SELECT C1.CategoryID, C1.CategoryName, C1.ParentID, C2.CategoryName as ParentCategoryName 
                FROM Categories C1 
                LEFT JOIN Categories C2 ON C1.ParentID = C2.CategoryID", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }

        public bool IsCategoryInUse(int categoryId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Products WHERE CategoryID = @CategoryID", con);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                con.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        // Hàm thêm mới
        public void AddCategory(string categoryName, object parentId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Categories (CategoryName, ParentID) VALUES (@Name, @ParentID)", con);
                cmd.Parameters.AddWithValue("@Name", categoryName);
                cmd.Parameters.AddWithValue("@ParentID", parentId ?? DBNull.Value); // Xử lý nếu không có cha
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Hàm cập nhật
        public void UpdateCategory(int categoryId, string categoryName, object parentId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Categories SET CategoryName = @Name, ParentID = @ParentID WHERE CategoryID = @ID", con);
                cmd.Parameters.AddWithValue("@ID", categoryId);
                cmd.Parameters.AddWithValue("@Name", categoryName);
                cmd.Parameters.AddWithValue("@ParentID", parentId ?? DBNull.Value);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Hàm xóa
        public void DeleteCategory(int categoryId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Categories WHERE CategoryID = @ID", con);
                cmd.Parameters.AddWithValue("@ID", categoryId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public string GetCategoryNameById(int categoryId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT CategoryName FROM Categories WHERE CategoryID = @CategoryID", con);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                con.Open();
                // ExecuteScalar sẽ trả về giá trị của cột đầu tiên, dòng đầu tiên
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : string.Empty;
            }
        }
    }
}