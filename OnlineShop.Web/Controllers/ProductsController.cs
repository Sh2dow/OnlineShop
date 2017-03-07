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
            //var content = fillService.GetAllProducts().Select(s => new
            //{
            //    s.ItemID,
            //    s.Title,
            //    s.ViewItemURLForNaturalSearch,
            //    s.Price,
            //    s.PrimaryCategoryID,
            //    s.PrimaryCategoryName,
            //    s.Image
            //});
            //List<ItemFinal> items = content.Select(item => new ItemFinal()
            //{
            //    ItemID = item.ItemID,
            //    Title = item.Title,
            //    Price = item.Price,
            //    ViewItemURLForNaturalSearch = item.ViewItemURLForNaturalSearch,
            //    PrimaryCategoryID = item.PrimaryCategoryID,
            //    PrimaryCategoryName = item.PrimaryCategoryName,
            //    Image = item.Image,
            //}).ToList();

            var model = fillService.GetAllProducts();
            return View(model);
        }

        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productView = fillService.GetProductById(id);
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
            var productView = fillService.GetProductById(id);
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
        public ActionResult Edit(ItemFinal productView)
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
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemFinal productView = fillService.GetProductById(id);
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
            ItemFinal productView = fillService.GetProductById(id);
            fillService.RemoveProduct(productView);
            return RedirectToAction("Index");
        }
    }
}
