using OnlineShop.Models;
using System.Collections.Generic;

namespace OnlineShop.BL.Services.Interfaces
{
    public interface ILocalService
    {
        StoreItem GetProductById(string id);
        IEnumerable<StoreItem> GetAllProducts();
        IEnumerable<StoreItem> GetProductsByKeyword(string keyword);
        IEnumerable<StoreItem> GetProductsByCategory(string categoryId);
        void UpdateProduct(StoreItem product);
        void RemoveProduct(StoreItem productView);
    }
}
