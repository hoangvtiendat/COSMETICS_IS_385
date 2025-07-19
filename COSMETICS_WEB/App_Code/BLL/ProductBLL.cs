using COSMETICS_WEB.App_Code.DAL;
using COSMETICS_WEB.App_Code.Models;
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

        public List<Product> GetProductsByBrandID(int brandId)
        {
            return dao.GetProductsByBrandID(brandId);
        }

        public Product GetProductById(int productId)
        {
            return dao.GetProductById(productId);
        }

        public List<ProductImage> GetProductImages(int productId)
        {
            return dao.GetProductImages(productId);
        }

       
        public void DeleteProductImage(int imageId)
        {
            dao.DeleteProductImage(imageId);
        }
        
        public int AddProduct(Product product)
        {
            return dao.AddProduct(product);
        }

       
        public void AddProductImages(int productId, List<string> imagePaths)
        {
            dao.AddProductImages(productId, imagePaths);
        }
   
        public void DeleteProduct(int productId)
        {
            dao.DeleteProduct(productId);
        }

        public void UpdateProduct(Product product)
        {
            dao.UpdateProduct(product);
        }

        public void UpdateMainImage(int productId, string newImagePath)
        {
            dao.UpdateMainImage(productId, newImagePath);
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            return dao.SearchProducts(searchTerm);
        }
    }
}