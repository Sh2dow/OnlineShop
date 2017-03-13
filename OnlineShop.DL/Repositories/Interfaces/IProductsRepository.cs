using OnlineShop.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.DL.Repositories
{
    public interface IProductsRepository : IDisposable
    {
        void AddProduct(StoreItem product);
        void AddProducts(IEnumerable<StoreItem> products);
        StoreItem Get(StoreItem detachedModel);
        void UpdateProduct(StoreItem product);
        void RemoveProduct(StoreItem product);
        IEnumerable<StoreItem> GetAllProducts();
        IEnumerable<StoreItem> GetProductsByCategory(string categoryId);
        IEnumerable<StoreItem> GetProductsByKeyword(string keyword);
        StoreItem GetProductById(string id);
        void Save();

    }
}
