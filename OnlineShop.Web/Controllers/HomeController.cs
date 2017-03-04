using System.Web.Mvc;
using OnlineShop.DL;
using OnlineShop.Models;
using System.Configuration;
using OnlineShop.BL;

namespace OnlineShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private GrabService grabService;

        public ActionResult Index()
        {
            return View();
        }


        //[HttpGet]
        //public ActionResult AddBook(int authorId)
        //{
        //    return View(new Book { AuthorId = authorId });
        //}

        //[HttpPost]
        //public ActionResult AddBook(Book book)
        //{
        //    _bookService.AddBook(book);

        //    return RedirectToAction("Index", new { authorId = book.AuthorId });
        //}

        // GET: ProductViews/Create
        [HttpGet]
        public ActionResult SaveProducts()
        {
            return View("Products");
        }

        //POST: ProductViews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProducts(Product productView)
        {
            if (ModelState.IsValid)
            {
                // Get AppID and ServerAddress from Web.config
                string appID = ConfigurationManager.AppSettings["AppID"];
                string findingServerAddress = ConfigurationManager.AppSettings["FindingServerAddress"];
                
                //Take by QueryKeywords
                //var url = findingServerAddress + "shopping?version=713&appid=" + appID + "&callname=FindPopularItems&QueryKeywords=" + productView.Title + "&ResponseEncodingType=JSON";

                //Take by categoryId
                //var url = findingServerAddress + "shopping?version=957&appid=" + appID + "&callname=FindPopularItems&categoryId=" + productView.Title + "&ResponseEncodingType=JSON";

                grabService.AddProduct(productView);
                return RedirectToAction("Products");
            }
            return View();
        }
        
    }
}