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
    public static class Product
    {


        #region Private

        private static List<ProductCategory> GetDataProductCategories(Model.Product product, int productId)
        {
            List<Data.ProductCategory> productCategories = new List<Data.ProductCategory>();

            foreach (var category in product.Categories)
            {
                var productCategory = new Data.ProductCategory();
                var date = DateTime.Now;
                productCategory.CategoryId = category.Id;
                productCategory.ProductId = productId;
                productCategory.CreateDateTime = date;
                productCategory.UpdateDateTime = date;

                productCategories.Add(productCategory);
            }

            return productCategories;
        }

        private static Data.Product MapModelProductToDataProduct(Model.Product product)
        {
            Data.Product dataProduct = new Data.Product();
            dataProduct.Description = product.Description;
            dataProduct.Name = product.Name;
            dataProduct.Serial = product.Serial;
            dataProduct.UnitPrice = product.UnitPrice;

            return dataProduct;
        }

        private static Model.Product MapDataProductToModelProduct(Data.Product dataProduct)
        {
            Model.Product product = new Model.Product();
            product.Description = dataProduct.Description;
            product.Name = dataProduct.Name;
            product.Serial = dataProduct.Serial;
            product.UnitPrice = dataProduct.UnitPrice;
            product.Id = dataProduct.Id;

            return product;
        }

        private static void SaveProductCategories(Model.Product product, int productId)
        {
            List<ProductCategory> productCategories = GetDataProductCategories(product, productId);

            Data.CategoryManager.SaveProductCategories(productCategories);
        }

        private static Model.Product SetProduct(Data.Product dataProduct)
        {
            var product = MapDataProductToModelProduct(dataProduct);
            product.Categories = Category.GetCategoriesByProductId(product.Id);
            return product;
        }

        private static void UpdateProductCategories(int productId, Model.Product product)
        {
            List<ProductCategory> productCategories = GetDataProductCategories(product, productId);
            Data.CategoryManager.DeleteProductCategoriesByProductId(productId);
            Data.CategoryManager.SaveProductCategories(productCategories);
        }

        #endregion



        #region Public

        public static void Delete(int id)
        {
            Data.CategoryManager.DeleteProductCategoriesByProductId(id);
            Data.ProductManager.Delete(id);

        }

        public static string GenerateSerial()
        {
            string serial = string.Format("{0}{1}{2}", DateTime.Now.DayOfYear, DateTime.Now.Year, DateTime.Now.TimeOfDay);
            serial = serial.Replace(".", string.Empty);
            serial = serial.Replace(":", string.Empty);
            return serial;
        }

        public static string GetProducts()
        {
            var dataProducts = Data.ProductManager.GetProducts();
            var products = new List<Model.Product>();
            if (dataProducts != null)
            {
                dataProducts.ForEach(p => products.Add(SetProduct(p)));
            }

            string productsJson = JsonConvert.SerializeObject(products);

            return productsJson;
        }

        public static string GetProductById(int id)
        {
            Data.Product dataProduct = Data.ProductManager.GetProductById(id);
            Model.Product product = SetProduct(dataProduct);
            string productJson = JsonConvert.SerializeObject(product);

            return productJson;
        }
  
        public static void Save(string productJson)
        {
            Model.Product product = JsonConvert.DeserializeObject<Model.Product>(productJson);
            product.Serial = GenerateSerial();
            Data.Product dataProduct = MapModelProductToDataProduct(product);
            var date = DateTime.Now;
            dataProduct.CreateDateTime = date;
            dataProduct.UpdateDateTime = date;
            int productId = Data.ProductManager.Save(dataProduct);
            SaveProductCategories(product, productId);
        }

        public static void Update(int id, string productJson)
        {
            Model.Product product = JsonConvert.DeserializeObject<Model.Product>(productJson);
            Data.Product dataProduct = MapModelProductToDataProduct(product);
            Data.ProductManager.Update(id, dataProduct);

            UpdateProductCategories(id, product);

        }

        #endregion


    }
}
