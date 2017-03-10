using OnlineShop.Models;
using System.Collections.Generic;

namespace OnlineShop.BL.Services.Interfaces
{
    public interface ILocalService
    {
        LocalItem GetProductById(string id);
        IEnumerable<LocalItem> GetAllProducts();
        IEnumerable<LocalItem> GetProductsByKeyword(string keyword);
        IEnumerable<LocalItem> GetProductsByCategory(long categoryId);
    }
}
