using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Data
{
    public static class CategoryManager
    {
        public static List<Category> GetCategories()
        {
            try
            {
                List<Category> categories;
                using (var context = new WebshopEntities())
                {
                    categories = context.Category.ToList();
                }

                return categories;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<ProductCategory> GetCategoriesByProductId(int productId)
        {
            try
            {
                List<ProductCategory> productCategories;
                using (var context = new WebshopEntities())
                {
                    productCategories = context.ProductCategory.Where(p => p.ProductId == productId).ToList();
                }

                return productCategories;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void SaveProductCategories(List<ProductCategory> productCategories)
        {
            try
            {
                using (var context = new WebshopEntities())
                {
                    context.ProductCategory.AddRange(productCategories);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteProductCategoriesByProductId(int productId)
        {
            try
            {
                using (var context = new WebshopEntities())
                {
                    var productCategories = context.ProductCategory.Where(p => p.ProductId == productId).ToList();
                    context.ProductCategory.RemoveRange(productCategories);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
