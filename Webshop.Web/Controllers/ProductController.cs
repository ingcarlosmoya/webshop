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

            return RedirectToAction("Index", "Home");
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

        // GET: Product/List
        public ActionResult List()
        {
            var product = new ProductViewModel();
            List<ProductViewModel> products = ProductManager.GetProducts();
            return View(products);
        }

        // GET: Product/SetPersistence
        public ActionResult SetPersistence()
        {
            try
            {
                return Json(new { success = true, responseText = ProductManager.PersistData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "Storage has not been changed, check errors" + ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductViewModel product = new ProductViewModel();
            try
            {
                if (ModelState.IsValid)
                    product = ProductManager.GetProductById(id);
                else
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(product);
        }

        //POST: Product/SetPersistence
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
                return Json(new { success = false, responseText = "Storage has not been changed, check errors" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, responseText = "Storage changed successfully!" }, JsonRequestBehavior.AllowGet);
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
                if (ModelState.IsValid)
                    product = ProductManager.GetProductById(id);
                else
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
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
