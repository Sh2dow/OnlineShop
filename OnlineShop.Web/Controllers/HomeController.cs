using System.Web.Mvc;
using OnlineShop.DL.Context;
using OnlineShop.Models;

namespace OnlineShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private ProductContext db = new ProductContext();

        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductViews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductViews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,seller,condition,ItemWebUrl,Cost,itemLocation,EbayItemID")] Product productView)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(productView);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productView);
        }
        
    }
}