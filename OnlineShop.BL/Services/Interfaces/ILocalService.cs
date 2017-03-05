using OnlineShop.Models;
using System.Collections.Generic;

namespace OnlineShop.BL.Services.Interfaces
{
    public interface ILocalService
    {
        Item GetProductById(int id);
        IEnumerable<Item> GetAllProducts();
        IEnumerable<Item> GetProductsByKeyword(string keyword);
        IEnumerable<Item> GetProductsByCategory(int categoryId);
    }
}
