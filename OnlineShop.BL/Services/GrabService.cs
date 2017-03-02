using OnlineShop.BL.Services.Interfaces;
using System;
using System.Collections.Generic;
using OnlineShop.Models;

namespace OnlineShop.BL.Services
{
    public class GrabService : IGrabService
    {
        public IEnumerable<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            throw new NotImplementedException();
        }
    }
}