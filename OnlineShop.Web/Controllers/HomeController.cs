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
            return View();
        }

        [HttpGet]
        public ActionResult SaveProducts(string param)
        {
            long n;
            if (long.TryParse(param, out n))
                grabService.GrabItemsByCategory(n);
            else
                grabService.GrabItemsByKeyword(param);
            return RedirectToAction("Index", "Products");
        }
    }
}