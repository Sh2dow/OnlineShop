using System.Collections.Generic;
using OnlineShop.Models;
using OnlineShop.DL;
using OnlineShop.BL.Services.Interfaces;

namespace OnlineShop.BL.Services
{
    public class LocalService : ILocalService
    {
        private ProductsRepository repo;

        public LocalService()
        {
            repo = new ProductsRepository();
        }

        public void UpdateProduct(Item product)
        {
            repo.UpdateProduct(product);
            repo.Save();
        }

        public void RemoveProduct(Item product)
        {
            repo.RemoveProduct(product);
            repo.Save();
        }

        public IEnumerable<Item> GetAllProducts()
        {
            return repo.GetAllProducts();
        }

        public Item GetProductById(int productId)
        {
            return repo.GetProductById(productId);
        }

        public IEnumerable<Item> GetProductsByCategory(int categoryId)
        {
            return repo.GetProductsByCategory(categoryId);
        }

        public IEnumerable<Item> GetProductsByKeyword(string keyword)
        {
            return repo.GetProductsByKeyword(keyword);
        }
    }
}