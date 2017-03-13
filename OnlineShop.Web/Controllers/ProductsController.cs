using System.Net;
using System.Web.Mvc;
using OnlineShop.Models;
using OnlineShop.BL.Services;
using OnlineShop.BL.Services.Interfaces;
using OnlineShop.DL.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private ILocalService localService;

        public ProductsController()
        {
            localService = new LocalService();
        }

        public ProductsController(ILocalService l)
        {
            localService = l;
        }

        // GET: Products
        [HttpGet]
        public ActionResult Index(string param = "")
        {
            IEnumerable<StoreItem> model;
            if (string.IsNullOrEmpty(param))
            {
                model = localService.GetAllProducts();
                if (model.Count() > 0)
                    ViewBag.Message = string.Format("There's {0} object in the db", model.Count());
            }
            long parseid;
            if (long.TryParse(param, out parseid))
                model = localService.GetProductsByCategory(param);
            else
                model = localService.GetProductsByKeyword(param);
            return View(model);
        }

        public ActionResult GetItemsByCategory(long? id)
        {
            return RedirectToAction("Index", new { param = id });
        }

        public ActionResult GetProductsByKeyword(string keyword)
        {
            return RedirectToAction("Index", new { param = keyword });
        }

        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productView = localService.GetProductById(id);
            if (productView == null)
            {
                return HttpNotFound();
            }
            return View(productView);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productView = localService.GetProductById(id);
            if (productView == null)
            {
                return HttpNotFound();
            }
            return View(productView);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StoreItem product)
        {
            if (ModelState.IsValid)
            {
                localService.UpdateProduct(product);
                //fillService.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreItem productView = localService.GetProductById(id);
            if (productView == null)
            {
                return HttpNotFound();
            }
            return View(productView);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            StoreItem productView = localService.GetProductById(id);
            localService.RemoveProduct(productView);
            return RedirectToAction("Index");
        }
    }
}
