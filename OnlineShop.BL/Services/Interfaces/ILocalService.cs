using OnlineShop.Models;
using System.Collections.Generic;

namespace OnlineShop.BL.Services.Interfaces
{
    public interface ILocalService
    {
        ItemFinal GetProductById(int id);
        IEnumerable<ItemFinal> GetAllProducts();
        IEnumerable<ItemFinal> GetProductsByKeyword(string keyword);
        IEnumerable<ItemFinal> GetProductsByCategory(int categoryId);
    }
}
