using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Data;
using Webshop.Model;

namespace Webshop.Business
{
    public static class Category
    {
        #region Public
        public static string GetCategories()
        {
            var dataCategories = Data.CategoryManager.GetCategories();
            var categories = new List<Model.Category>();
            if (dataCategories != null)
                dataCategories.ForEach(p => categories.Add(MapDataCategoryToModelCategory(p)));

            string categoriesJson = JsonConvert.SerializeObject(categories);

            return categoriesJson;
        }

        internal static List<Model.Category> GetCategoriesByProductId(int productId)
        {
            List<Model.Category> categories = new List<Model.Category>();
            List<Data.ProductCategory> dataProductCategories = Data.CategoryManager.GetCategoriesByProductId(productId);
            List<Data.Category> dataCategories = CategoryManager.GetCategories();
            foreach (var dataCategory in dataProductCategories)
            {
                var category = dataCategories.Where(c => c.Id == dataCategory.CategoryId).FirstOrDefault();
                if (category != null)
                    categories.Add(MapDataCategoryToModelCategory(category));
            }

            categories = categories.OrderBy(c => c.Name).ToList();
            return categories;

        }

        #endregion

        #region Private

        private static Model.Category MapDataCategoryToModelCategory(Data.Category datacategory)
        {
            Model.Category category = new Model.Category();
            if (datacategory != null)
            {
                category.Id = datacategory.Id;
                category.Name = datacategory.Name;
            }
            return category;
        }

        #endregion


    }
}
