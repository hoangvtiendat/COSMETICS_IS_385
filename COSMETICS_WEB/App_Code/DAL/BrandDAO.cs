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
    public class BrandDAO
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        //public List<Brand> GetAllBrands()
        //{
        //    List<Brand> brandList = new List<Brand>();
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("SELECT BrandID, BrandName, LogoPath FROM Brands", con);
        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();
        //        while (rdr.Read())
        //        {
        //            Brand brand = new Brand();
        //            brand.BrandID = Convert.ToInt32(rdr["BrandID"]);
        //            brand.BrandName = rdr["BrandName"].ToString();
        //            brand.LogoPath = rdr["LogoPath"]?.ToString(); // Dùng ?. để tránh lỗi nếu LogoPath là NULL
        //            brandList.Add(brand);
        //        }
        //    }
        //    return brandList;
        //}

       

        public DataTable GetAllBrands()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT BrandID, BrandName, LogoPath, Description FROM Brands", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }

        public bool IsBrandInUse(int brandId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Products WHERE BrandID = @BrandID", con);
                cmd.Parameters.AddWithValue("@BrandID", brandId);
                con.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public void AddBrand(Brand brand)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Brands (BrandName, Description, LogoPath) VALUES (@Name, @Desc, @Logo)", con);
                cmd.Parameters.AddWithValue("@Name", brand.BrandName);
                cmd.Parameters.AddWithValue("@Desc", brand.Description);
                cmd.Parameters.AddWithValue("@Logo", brand.LogoPath);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateBrand(Brand brand)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Brands SET BrandName = @Name, Description = @Desc WHERE BrandID = @ID", con);
                cmd.Parameters.AddWithValue("@ID", brand.BrandID);
                cmd.Parameters.AddWithValue("@Name", brand.BrandName);
                cmd.Parameters.AddWithValue("@Desc", brand.Description);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteBrand(int brandId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Brands WHERE BrandID = @ID", con);
                cmd.Parameters.AddWithValue("@ID", brandId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public string GetBrandNameById(int brandId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT BrandName FROM Brands WHERE BrandID = @BrandID", con);
                cmd.Parameters.AddWithValue("@BrandID", brandId);
                con.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : string.Empty;
            }
        }
        public void UpdateBrandLogo(int brandId, string logoPath)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Brands SET LogoPath = @LogoPath WHERE BrandID = @ID", con);
                cmd.Parameters.AddWithValue("@ID", brandId);
                cmd.Parameters.AddWithValue("@LogoPath", logoPath);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}