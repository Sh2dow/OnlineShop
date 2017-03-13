using OnlineShop.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.DL.Repositories
{
    public interface IProductsRepository : IDisposable
    {
        Exception ExceptionToThrow { get; set; }
        void AddProduct(StoreItem product);
        void AddProducts(IEnumerable<StoreItem> products);
        void UpdateProduct(StoreItem product);
        void RemoveProduct(StoreItem product);
        IEnumerable<StoreItem> GetAllProducts();
        IEnumerable<StoreItem> GetProductsByCategory(string categoryId);
        IEnumerable<StoreItem> GetProductsByKeyword(string keyword);
        StoreItem GetProductById(string id);
        void Save();

    }
}
