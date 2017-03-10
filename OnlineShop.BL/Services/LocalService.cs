using System.Collections.Generic;
using OnlineShop.Models;
using OnlineShop.DL;
using OnlineShop.BL.Services.Interfaces;
using System;

namespace OnlineShop.BL.Services
{
    public class LocalService : ILocalService
    {
        private ProductsRepository repo;

        public LocalService()
        {
            repo = new ProductsRepository();
        }

        public void UpdateProduct(LocalItem product)
        {
            repo.UpdateProduct(product);
            repo.Save();
        }

        public void RemoveProduct(LocalItem product)
        {
            repo.RemoveProduct(product);
            repo.Save();
        }

        public IEnumerable<LocalItem> GetAllProducts()
        {
            return repo.GetAllProducts();
        }

        public LocalItem GetProductById(string productId)
        {
            return repo.GetProductById(productId);
        }

        public IEnumerable<LocalItem> GetProductsByCategory(long categoryId)
        {
            return repo.GetProductsByCategory(categoryId);
        }

        public IEnumerable<LocalItem> GetProductsByKeyword(string keyword)
        {
            return repo.GetProductsByKeyword(keyword);
        }
    }
}