using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.Web.Models;
using Webshop.Web.Utilities;

namespace Webshop.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            var product = new ProductViewModel();
            product.Categories = CategoryManager.GetCategories();
            return View(product);
        }

        // GET: Product/Create
        public ActionResult List()
        {
            var product = new ProductViewModel();
            List<ProductViewModel> products =  ProductManager.GetProducts();
            return View(products);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductViewModel product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProductManager.Save(product);
                    return RedirectToAction("List");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(product);

        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductViewModel product = new ProductViewModel();
            try
            {
               product = ProductManager.GetProductById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(product);
        }

        

        [HttpPost]
        public ActionResult SetPersistence(bool persistOnDataBase)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProductManager.PersistData = persistOnDataBase;
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "Storage has not benn changed, check errors" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, responseText = "Storage changed successfully!" }, JsonRequestBehavior.AllowGet);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProductViewModel product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.Categories = product.Categories.Where(c => c.Checked).ToList();
                    ProductManager.Update(id, product);
                    return RedirectToAction("List");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            ProductViewModel product = new ProductViewModel();
            try
            {
                product = ProductManager.GetProductById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ProductViewModel product)
        {
            try
            {
                ProductManager.Delete(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction("List");
        }
    }
}
