using COSMETICS_WEB.App_Code.DAL;
using COSMETICS_WEB.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.BLL
{
    public class BrandBLL
    {
        private BrandDAO dao = new BrandDAO();
        public DataTable GetAllBrands()
        {
            return dao.GetAllBrands();
        }

        public void AddBrand(Brand brand)
        {
            dao.AddBrand(brand);
        }

        public void UpdateBrand(Brand brand)
        {
            dao.UpdateBrand(brand);
        }

        public bool DeleteBrand(int brandId)
        {
            if (dao.IsBrandInUse(brandId))
            {
                return false;
            }
            dao.DeleteBrand(brandId);
            return true;
        }

        public string GetBrandNameById(int brandId)
        {
            return dao.GetBrandNameById(brandId);
        }

        public void UpdateBrandLogo(int brandId, string logoPath)
        {
            dao.UpdateBrandLogo(brandId, logoPath);
        }

    }
}