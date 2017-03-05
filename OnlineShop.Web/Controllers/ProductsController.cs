using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OnlineShop.Models;
using OnlineShop.BL.Services;

namespace OnlineShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private LocalService fillService;

        public ProductsController()
        {
            fillService = new LocalService();
        }

        // GET: Products
        //public ActionResult Index(int? categoryId)
        //{
        //    if (categoryId != null)
        //        return View(fillService.GetProductsByCategory((int)categoryId).ToList());
        //    else
        //        return View(fillService.GetAllProducts().ToList());
        //}
        public ActionResult Index()
        {
            var items = fillService.GetAllProducts().ToList();
            return View(items);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item productView = fillService.GetProductById((int)id);
            if (productView == null)
            {
                return HttpNotFound();
            }
            return View(productView);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productView = fillService.GetProductById((int)id);
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
        public ActionResult Edit(Item productView)
        {
            if (ModelState.IsValid)
            {
                fillService.UpdateProduct(productView);
                //fillService.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productView);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item productView = fillService.GetProductById((int)id);
            if (productView == null)
            {
                return HttpNotFound();
            }
            return View(productView);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item productView = fillService.GetProductById((int)id);
            fillService.RemoveProduct(productView);
            return RedirectToAction("Index");
        }
    }
}
