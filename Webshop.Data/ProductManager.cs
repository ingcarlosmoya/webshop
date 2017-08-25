using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Data
{
    public static class ProductManager
    {

        public static void Delete(int id)
        {
            try
            {
                using (var context = new WebshopEntities())
                {
                    var dataProduct = context.Product.Where(p => p.Id == id).FirstOrDefault();
                    if (dataProduct != null)
                    {
                        context.Product.Remove(dataProduct);
                        context.SaveChanges();
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<Product> GetProducts()
        {
            try
            {
                List<Product> products;
                using (var context = new WebshopEntities())
                {
                    products = context.Product.ToList();
                }

                return products;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static Product GetProductById(int id)
        {
            try
            {
                Product product;
                using (var context = new WebshopEntities())
                {
                    product = context.Product.Where(p => p.Id == id).FirstOrDefault();
                }

                return product;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Save(Product product)
        {
            try
            {
                using (var context = new WebshopEntities())
                {
                    context.Product.Add(product);
                    context.SaveChanges();
                    return product.Id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Update(int id, Product product)
        {
            try
            {
                using (var context = new WebshopEntities())
                {
                    var dataProduct = context.Product.Where(p => p.Id == id).FirstOrDefault();
                    if (dataProduct != null)
                    {
                        dataProduct.Description = product.Description;
                        dataProduct.Name = dataProduct.Name;
                        dataProduct.UnitPrice = dataProduct.UnitPrice;
                        dataProduct.UpdateDateTime = DateTime.Now;
                        context.SaveChanges();
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
