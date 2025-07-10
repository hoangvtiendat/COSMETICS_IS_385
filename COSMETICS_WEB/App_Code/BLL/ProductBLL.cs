using COSMETICS_WEB.App_Code.DAL;
using COSMETICS_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.BLL
{
    public class ProductBLL
    {
        private ProductDAO dao = new ProductDAO();

        public List<Product> GetAllProducts()
        {
            return dao.GetAllProducts();
        }
        public List<Product> GetProductsByCategoryID(int categoryId)
        {
            return dao.GetProductsByCategoryID(categoryId);
        }

        public List<Product> GetFeaturedProducts(int count)
        {
            return dao.GetFeaturedProducts(count);
        }
    }
}