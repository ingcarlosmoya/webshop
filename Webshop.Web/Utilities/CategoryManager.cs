using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Webshop.Web.Models;

namespace Webshop.Web.Utilities
{
    public static class CategoryManager
    {
        internal static List<CategoryViewModel> GetCategories()
        {
            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            if (ProductManager.PersistData)
            {

                string categoriesJson = Business.Category.GetCategories();
                if (!string.IsNullOrWhiteSpace(categoriesJson))
                {
                    categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(categoriesJson);
                }
            }

            else
            {
                categories.Add(new CategoryViewModel() { Id = 1, Name = "Food" });
                categories.Add(new CategoryViewModel() { Id = 2, Name = "Electronics" });
                categories.Add(new CategoryViewModel() { Id = 3, Name = "Health" });
            }
            return categories;
        }
    }
}