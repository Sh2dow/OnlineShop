using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OnlineShop.Models;
using OnlineShop.BL.Services;
using System.Collections.Generic;

namespace OnlineShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private LocalService localService;

        public ProductsController()
        {
            localService = new LocalService();
        }

        // GET: Products
        [HttpGet]
        public ActionResult Index(string param)
        {
            long i;
            if (long.TryParse(param, out i))
            {
                return View(localService.GetProductsByCategory(long.Parse(param)).ToList());
            }
            else
            {
                if (string.IsNullOrEmpty(param))
                    return View(localService.GetAllProducts());
                else
                    return View(localService.GetProductsByKeyword(param.ToString()));
            }
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
        public ActionResult Edit(LocalItem productView)
        {
            if (ModelState.IsValid)
            {
                localService.UpdateProduct(productView);
                //fillService.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productView);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocalItem productView = localService.GetProductById(id);
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
            LocalItem productView = localService.GetProductById(id);
            localService.RemoveProduct(productView);
            return RedirectToAction("Index");
        }
    }
}
