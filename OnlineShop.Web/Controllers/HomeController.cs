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
        public ActionResult SaveProducts(string Str)
        {

            string appID = ConfigurationManager.AppSettings["AppID"];
            //string findingServerAddress = ConfigurationManager.AppSettings["FindingServerAddress"];
            //var url = findingServerAddress + "shopping?version=957&appid=" + appID + "&callname=FindPopularItems&categoryId=" + Str + "&ResponseEncodingType=JSON";
            //grabService.GrabItemsByCategory(productView);
            return RedirectToAction("Index", "Products");
        }

        [HttpPost]
        public ActionResult SaveProducts(Json json)
        {
                //if (ModelState.IsValid)
                //{
                    // Get AppID and ServerAddress from Web.config
                    string appID = ConfigurationManager.AppSettings["AppID"];
                    string findingServerAddress = ConfigurationManager.AppSettings["FindingServerAddress"];
            //Take by QueryKeywords
            //var url = findingServerAddress + "shopping?version=713&appid=" + appID + "&callname=FindPopularItems&QueryKeywords=" + productView.Title + "&ResponseEncodingType=JSON";
            //Take by categoryId
            //var url = findingServerAddress + "shopping?version=957&appid=" + appID + "&callname=FindPopularItems&categoryId=" + productView.Title + "&ResponseEncodingType=JSON";
            grabService.GrabJson(json);

            //}
            return RedirectToAction("Index", "Products");
        }

    }
}