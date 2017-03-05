using System.Web.Mvc;
using OnlineShop.Models;
using System.Configuration;
using System.Collections.Generic;
using OnlineShop.BL;
using OnlineShop.BL.Services.Interfaces;
using Newtonsoft.Json;

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
            var model = new ItemByCategory();
            return View(model);
        }

        [HttpGet]
        public ActionResult SaveProducts(string param)
        {
            if (ModelState.IsValid)
            {
                int n;
                if (int.TryParse(param, out n))
                    grabService.GrabItemsByCategory(n);
                else
                    grabService.GrabItemsByKeyword(param);
            }
            return RedirectToAction("Index", "Products");
        }

        //[HttpPost]
        //public ActionResult SaveProducts()
        //{
        //    return RedirectToAction("Index", "Products");
        //}

    }
}