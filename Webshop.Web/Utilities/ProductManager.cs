using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webshop.Web.Models;

namespace Webshop.Web.Utilities
{
    public static class ProductManager
    {

        public static bool PersistData
        {
            get
            {
                bool persistData = false;
                if (HttpContext.Current.Session["PersistData"] != null)
                    bool.TryParse(HttpContext.Current.Session["PersistData"].ToString(), out persistData);

                return persistData;
            }
            set
            {
                HttpContext.Current.Session["PersistData"] = value;
            }
        }

        private static List<ProductViewModel> Products
        {
            get
            {
                if (HttpContext.Current.Session["Products"] == null)
                    HttpContext.Current.Session["Products"] = new List<ProductViewModel>();

                return (List<ProductViewModel>)HttpContext.Current.Session["Products"];
            }
            set
            {
                HttpContext.Current.Session["Products"] = value;
            }
        }

        internal static List<ProductViewModel> GetProducts()
        {

            if (PersistData)
            {
                string productJson = Business.Product.GetProducts();

                List<ProductViewModel> products =
                    JsonConvert.DeserializeObject<List<ProductViewModel>>(productJson);

                return products;
            }
            else
            {
                return Products;
            }
        }

        internal static void Save(ProductViewModel product)
        {

            if (PersistData)
            {
                product.Categories = product.Categories.Where(c => c.Checked).ToList();
                Business.Product.Save(JsonConvert.SerializeObject(product));
            }
            else
            {
                product.Id = GenerateId(Products);
                product.Serial = Business.Product.GenerateSerial();
                Products.Add(product);
                Products = Products.OrderBy(p => p.Name).ToList();
            }
        }

        internal static ProductViewModel GetProductById(int id)
        {
            ProductViewModel product = new ProductViewModel();

            if (PersistData)
            {
                string productJson = Business.Product.GetProductById(id);
                if (!string.IsNullOrWhiteSpace(productJson))
                    product = JsonConvert.DeserializeObject<ProductViewModel>(productJson);
                List<CategoryViewModel> categories = GetSelectedCategories(product);
                product.Categories = categories;

            }
            else
                product = Products.Where(p => p.Id == id).FirstOrDefault();

            return product;
        }

        private static List<CategoryViewModel> GetSelectedCategories(ProductViewModel product)
        {
            List<CategoryViewModel> categories = CategoryManager.GetCategories();
            List<int> selectedCategories = product.Categories.Select(c => c.Id).ToList();
            foreach (var category in categories)
            {
                if (selectedCategories.Contains(category.Id))
                    category.Checked = true;
            }

            return categories;
        }

        internal static void Update(int id, ProductViewModel product)
        {

            if (PersistData)
            {
                product.Categories = product.Categories.Where(c => c.Checked).ToList();
                Business.Product.Update(id, JsonConvert.SerializeObject(product));
            }
            else
            {
                ProductViewModel oldProduct = Products.Where(p => p.Id == id).FirstOrDefault();
                if (oldProduct != null)
                {
                    oldProduct.Name = product.Name;
                    oldProduct.Description = product.Description;
                    oldProduct.UnitPrice = product.UnitPrice;
                    oldProduct.Categories = GetSelectedCategories(product);
                }
                Products = Products.OrderBy(p => p.Name).ToList();
            }
        }

        private static int GenerateId(List<ProductViewModel> products)
        {
            int id = 0;

            if (products.Count > 0)
                id = products.OrderByDescending(p => p.Serial).FirstOrDefault().Id;

            return id + 1;
        }

        internal static void Delete(int id)
        {
            if (PersistData)
            {
                Business.Product.Delete(id);
            }
            else
            {
                ProductViewModel product = Products.Where(p => p.Id == id).FirstOrDefault();
                if (product != null)
                    Products.Remove(product);
            }
        }
    }
}