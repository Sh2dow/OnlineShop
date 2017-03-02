using System;
using System.Collections.Generic;
using OnlineShop.Models;

namespace OnlineShop.BL.Services.Interfaces
{
    interface IGrabService
    {
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
    }
}
