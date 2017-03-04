using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.BL.Services.Interfaces
{
    interface IFillService
    {
        Product GetProductById(int id);

        IEnumerable<Product> GetAllProducts();
    }
}
