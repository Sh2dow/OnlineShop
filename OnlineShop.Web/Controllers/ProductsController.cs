using System.Net;
using System.Web.Mvc;
using OnlineShop.Models;
using OnlineShop.BL.Services;
using OnlineShop.BL.Services.Interfaces;

namespace OnlineShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private ILocalService localService;

        public ProductsController()
        {
            localService = new LocalService();
        }

        // GET: Products
        [HttpGet]
        public ActionResult Index(string param)
        {
            if (string.IsNullOrEmpty(param))
                return View(localService.GetAllProducts());
            long parseid;
            if (long.TryParse(param, out parseid))
                return View(localService.GetProductsByCategory(param));
            else
                return View(localService.GetProductsByKeyword(param));
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
