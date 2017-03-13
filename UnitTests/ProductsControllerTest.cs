using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineShop.Web.Controllers;
using OnlineShop.Models;
using System.Web.Mvc;
using Moq;
using OnlineShop.DL.Repositories;
using System.Collections.Generic;
using OnlineShop.BL.Services.Interfaces;

namespace OnlineShopTests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SearchItemsByCategory()
        {
            // Arrange
        }

        [TestMethod]
        public void SearchItemsByKeyword()
        {
            // Arrange
        }
    }

    [TestClass]
    public class ProductsControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            ProductsController controller = new ProductsController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexViewModelNotNull()
        {
            // Arrange
            var mock = new Mock<ILocalService>();
            mock.Setup(a => a.GetAllProducts()).Returns(new List<StoreItem>());
            ProductsController controller = new ProductsController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void IndexViewBagMessage()
        {
            // Arrange
            var mock = new Mock<ILocalService>();
            mock.Setup(a => a.GetAllProducts()).Returns(new List<StoreItem>() { new StoreItem() });
            ProductsController controller = new ProductsController(mock.Object);
            string expected = "There's 1 object in the db";

            // Act
            ViewResult result = controller.Index() as ViewResult;
            string actual = result.ViewBag.Message as string;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDetailsView()
        {
            var controller = new ProductsController();
            var result = controller.Details("2") as ViewResult;
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void TestDetailsViewData()
        {
            var controller = new ProductsController();
            var result = controller.Details("2") as ViewResult;
            var product = (StoreItem)result.ViewData.Model;
            Assert.AreEqual("Laptop", product.Title);
        }

        [TestMethod]
        public void TestDetailsRedirect()
        {
            var controller = new ProductsController();
            var result = (RedirectToRouteResult)controller.Details("-1");
            Assert.AreEqual("Index", result.RouteValues["action"]);

        }

        //[TestMethod]
        //public void Index_Get_RetrievesAllItemsFromRepository()
        //{
        //    // Arrange
        //    StoreItem contact1 = GetContactNamed(1, "Orlando", "Gee");
        //    StoreItem contact2 = GetContactNamed(2, "Keith", "Harris");
        //    GrabService grabservice = new InMemoryContactRepository();
        //    repository.Add(contact1);
        //    repository.Add(contact2);
        //    var controller = GetHomeController(repository);

        //    // Act
        //    var result = controller.Index();

        //    // Assert
        //    var model = (IEnumerable<Contact>)result.ViewData.Model;
        //    CollectionAssert.Contains(model.ToList(), contact1);
        //    CollectionAssert.Contains(model.ToList(), contact1);
        //}
    }

    /*  [TestClass]
      public class UnitTest1
      {
          [TestMethod]
          public void GetAllProducts_ShouldReturnAllProducts()
          {
              var testProducts = GetTestProducts();
              var controller = new ProductsController(testProducts);

              var result = controller.GetAllProducts() as List<StoreItem>;
              Assert.AreEqual(testProducts.Count, result.Count);
          }

          [TestMethod]
          public async Task GetAllProductsAsync_ShouldReturnAllProducts()
          {
              var testProducts = GetTestProducts();
              var controller = new SimpleProductController(testProducts);

              var result = await controller.GetAllProductsAsync() as List<StoreItem>;
              Assert.AreEqual(testProducts.Count, result.Count);
          }

          [TestMethod]
          public void GetProduct_ShouldReturnCorrectProduct()
          {
              var testProducts = GetTestProducts();
              var controller = new SimpleProductController(testProducts);

              var result = controller.GetProduct(4) as OkNegotiatedContentResult<StoreItem>;
              Assert.IsNotNull(result);
              Assert.AreEqual(testProducts[3].Name, result.Content.Name);
          }

          [TestMethod]
          public async Task GetProductAsync_ShouldReturnCorrectProduct()
          {
              var testProducts = GetTestProducts();
              var controller = new SimpleProductController(testProducts);

              var result = await controller.GetProductAsync(4) as OkNegotiatedContentResult<Product>;
              Assert.IsNotNull(result);
              Assert.AreEqual(testProducts[3].Name, result.Content.Name);
          }

          [TestMethod]
          public void GetProduct_ShouldNotFindProduct()
          {
              var controller = new SimpleProductController(GetTestProducts());

              var result = controller.GetProduct(999);
              Assert.IsInstanceOfType(result, typeof(NotFoundResult));
          }

          private List<StoreItem> GetTestProducts()
          {
              var testProducts = new List<Product>();
              testProducts.Add(new Product { Id = 1, Name = "Demo1", Price = 1 });
              testProducts.Add(new Product { Id = 2, Name = "Demo2", Price = 3.75M });
              testProducts.Add(new Product { Id = 3, Name = "Demo3", Price = 16.99M });
              testProducts.Add(new Product { Id = 4, Name = "Demo4", Price = 11.00M });

              return testProducts;
      }
          }*/
}
