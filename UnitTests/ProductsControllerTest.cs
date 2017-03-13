using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineShop.Web.Controllers;
using OnlineShop.Models;
using System.Web.Mvc;
using Moq;
using OnlineShop.DL.Repositories;
using System.Collections.Generic;
using OnlineShop.BL.Services.Interfaces;
using System;
using System.Net;
using OnlineShop.BL.Services;

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
            var mock = new Mock<IGrabService>();
            var categoryId = "0";
            HomeController controller = new HomeController(mock.Object);
            // act
            RedirectToRouteResult result = controller.SearchItemsByCategory(categoryId) as RedirectToRouteResult;
            // assert
            mock.Verify(a => a.GrabTopItemsByCategory(categoryId));
        }

        [TestMethod]
        public void SearchItemsByKeyword()
        {
            // Arrange
            var mock = new Mock<IGrabService>();
            var keyword = "iphone";
            HomeController controller = new HomeController(mock.Object);
            // act
            RedirectToRouteResult result = controller.SearchItemsByKeyword(keyword) as RedirectToRouteResult;
            // assert
            mock.Verify(a => a.GrabTopItemsByKeyword(keyword));
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
        public void DetailsViewModelNotNull()
        {
            // Arrange
            var mock = new Mock<ILocalService>();
            var id = "1";
            mock.Setup(a => a.GetProductById(id)).Returns(new StoreItem());
            ProductsController controller = new ProductsController(mock.Object);
            // Act
            ViewResult result = controller.Details(id) as ViewResult;
            // Assert
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Details_Verify()
        {
            // arrange
            var mock = new Mock<ILocalService>();
            const string id = "111242493709";
            ProductsController controller = new ProductsController(mock.Object);
            // act
            RedirectToRouteResult result = controller.Details(id) as RedirectToRouteResult;
            // assert
            mock.Verify(a => a.GetProductById(id));
        }

        [TestMethod]
        public void GetProductFromRepository()
        {
            const string id = "111242493709";
            var mock = new Mock<IProductsRepository>();
            mock.Setup(x => x.GetProductById(id)).Returns(new StoreItem());
            var repository = mock.Object;
            var service = new LocalService(repository);
            var result = service.GetProductById(id);
            Assert.IsNotNull(result);
        }



        /// <summary>
        /// Instance of a controller for testing things that use controller methods i.e. controller.TryValidateModel(model)
        /// </summary>
        public class ModelStateTestController : Controller
        {
            public ModelStateTestController()
            {
                ControllerContext = (new Mock<ControllerContext>()).Object;
            }

            public bool TestTryValidateModel(object model)
            {
                return TryValidateModel(model);
            }
        }

        [TestMethod]
        public void ModelState_validations_are_thrown()
        {
            // Arrange
            var controller = new ModelStateTestController();
            var testitem = new StoreItem
            {
                ItemID = null, //This is a required property and so this value is invalid
                PrimaryCategoryID = null //This is a required property and so this value is invalid
            };

            // Act
            var result = controller.TestTryValidateModel(testitem);

            // Assert
            Assert.IsFalse(result);

            var modelState = controller.ModelState;

            Assert.AreEqual(2, modelState.Keys.Count);

            Assert.IsTrue(modelState.Keys.Contains("ItemID"));
            Assert.IsTrue(modelState["ItemID"].Errors.Count == 1);
            Assert.AreEqual("Требуется поле id.", modelState["ItemID"].Errors[0].ErrorMessage);

            Assert.IsTrue(modelState.Keys.Contains("PrimaryCategoryID"));
            Assert.IsTrue(modelState["PrimaryCategoryID"].Errors.Count == 1);
            Assert.AreEqual("Требуется поле PrimaryCategoryID.", modelState["PrimaryCategoryID"].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void RepositoryThrowsException()
        {
            // Arrange
            var testrepo = new ProductsRepository();
            Exception exception = new Exception();
            testrepo.ExceptionToThrow = exception;
            var controller = new ProductsController(new LocalService(testrepo));
            var grabsvc = new Mock<IGrabService>();
            var testitem = GetTestItem();
            var result = (ViewResult)controller.Details(testitem.ItemID);
            Assert.AreEqual("", result.ViewName);
            ModelState modelState = result.ViewData.ModelState[""];
        }

        StoreItem GetTestItem()
        {
            return new StoreItem()
            {
                ItemID = "111242493709",
                PrimaryCategoryID = "37943",
                Price = 7.99,
                EndTime = Convert.ToDateTime("2017-04-08T05:02:21.000Z"),
                Title = "Wooden Box Handmade Trinket Storage Keepsake Jewelry Name Card Holder Gift"
            };
        }

       
}
