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
            string categoriesJson = Business.Category.GetCategories();
            if (!string.IsNullOrWhiteSpace(categoriesJson))
            {
                categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(categoriesJson);
            }

            return categories;
        }
    }
}