using OnlineShop.BL.Services.Interfaces;
using System;
using System.Collections.Generic;
using OnlineShop.Models;
using OnlineShop.DL;

namespace OnlineShop.BL
{
    public class GrabService : IGrabService
    {
        private ProductsRepository repo;

        public GrabService()
        {
            repo = new ProductsRepository();
        }

        public void AddProduct(Product product)
        {
            repo.AddProduct(product);
            repo.Save();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            throw new NotImplementedException();
        }
    }
}