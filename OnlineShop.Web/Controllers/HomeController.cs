using System.Web.Mvc;
using OnlineShop.BL;
using OnlineShop.BL.Services.Interfaces;
using System.Net;

namespace OnlineShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IGrabService grabService;

        public HomeController()
        {
            grabService = new GrabService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SearchItemsByCategory(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            grabService.GrabTopItemsByCategory((long)id);
            return RedirectToAction("GetItemsByCategory", "Products", new { id });
        }

        [HttpGet]
        public ActionResult SearchItemsByKeyword(string keyword)
        {
            if (keyword == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            grabService.GrabTopItemsByKeyword(keyword);
            return RedirectToAction("GetProductsByKeyword", "Products", new { keyword });
        }
    }
}