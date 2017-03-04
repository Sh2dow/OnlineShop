using System;
using System.Collections.Generic;
using OnlineShop.Models;

namespace OnlineShop.BL.Services.Interfaces
{
    interface IGrabService
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
    }
}
