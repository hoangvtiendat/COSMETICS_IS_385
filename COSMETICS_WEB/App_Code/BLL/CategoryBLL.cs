using COSMETICS_WEB.App_Code.DAL;
using COSMETICS_WEB.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace COSMETICS_WEB.App_Code.BLL
{
    public class CategoryBLL
    {
        private CategoryDAO dao = new CategoryDAO();
        public DataTable GetAllCategories()
        {
            return dao.GetAllCategories();
        }

        public void AddCategory(string categoryName, object parentId)
        {
            dao.AddCategory(categoryName, parentId);
        }

        public void UpdateCategory(int categoryId, string categoryName, object parentId)
        {
            dao.UpdateCategory(categoryId, categoryName, parentId);
        }

        // Logic xóa an toàn
        public bool DeleteCategory(int categoryId)
        {
            if (dao.IsCategoryInUse(categoryId))
            {
                return false; // Trả về false nếu đang được dùng
            }
            dao.DeleteCategory(categoryId);
            return true; // Trả về true nếu xóa thành công
        }

        public string GetCategoryNameById(int categoryId)
        {
            return dao.GetCategoryNameById(categoryId);
        }
    }
}