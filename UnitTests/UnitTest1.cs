using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineShop.Web.Controllers;
using System.Collections.Generic;
using OnlineShop.Models;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void GetShouldReturnAllStoreItems()
        {
            // Arrange
            var controller = new HomeController();
            // Act
            var actionResult = controller.SearchItemsByCategory(null);
            // Assert
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<StoreItem>>;
            Assert.IsNotNull(response);
            var books = response.Content;
            Assert.AreEqual(4, books.Count());
        }

        [TestMethod]
        public void GetWithIdItShouldReturnThatBook()
        {
            var controller = new BooksController();
            var actionResult = controller.Get(1);
            var response = actionResult as OkNegotiatedContentResult<Book>;
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Content.Id);
        }

        [TestMethod]
        public void PostShouldAddBook()
        {
            var controller = new BooksController();
            var actionResult = controller.Post(new Book
            {
                Title = "C# 5.0 in a Nutshell",
                Author = "Joseph Albahari"
            });
            var response = actionResult as CreatedAtRouteNegotiatedContentResult<Book>;
            Assert.IsNotNull(response);
            Assert.AreEqual("DefaultApi", response.RouteName);
            Assert.AreEqual(response.Content.Id, response.RouteValues["Id"]);
        }

        [TestMethod]
        public void PutShouldUpdateBook()
        {
            var controller = new BooksController();
            var book = new Book { Id = 5, Title = "CLR via C#", Author = "Jeffrey Richter" };
            var actionResult = controller.Put(book.Id, book);
            var response = actionResult as OkNegotiatedContentResult<Book>;
            Assert.IsNotNull(response);
            var newBook = response.Content;
            Assert.AreEqual(5, newBook.Id);
            Assert.AreEqual("CLR via C#", newBook.Title);
            Assert.AreEqual("Jeffrey Richter", newBook.Author);
        }
    }
}
