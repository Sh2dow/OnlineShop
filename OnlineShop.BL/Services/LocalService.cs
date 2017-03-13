using System.Collections.Generic;
using OnlineShop.Models;
using OnlineShop.DL.Repositories;
using OnlineShop.BL.Services.Interfaces;

namespace OnlineShop.BL.Services
{
    public class LocalService : ILocalService
    {
        private IProductsRepository repo;

        public LocalService(IProductsRepository r)
        {
            repo = r;
        }
        public LocalService()
        {
            repo = new ProductsRepository();
        }

        public void UpdateProduct(StoreItem product)
        {
            repo.UpdateProduct(product);
            repo.Save();
        }

        public void RemoveProduct(StoreItem product)
        {
            repo.RemoveProduct(product);
            repo.Save();
        }

        public IEnumerable<StoreItem> GetAllProducts()
        {
            return repo.GetAllProducts();
        }

        public StoreItem GetProductById(string productId)
        {
            var item = repo.GetProductById(productId);
            return new GrabService().ExpandItem(item);
        }

        public IEnumerable<StoreItem> GetProductsByCategory(string categoryId)
        {
            return repo.GetProductsByCategory(categoryId);
        }

        public IEnumerable<StoreItem> GetProductsByKeyword(string keyword)
        {
            return repo.GetProductsByKeyword(keyword);
        }
    }
}