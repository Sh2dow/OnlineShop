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

        public void UpdateProduct(ItemFinal product)
        {
            repo.UpdateProduct(product);
            repo.Save();
        }

        public void RemoveProduct(ItemFinal product)
        {
            repo.RemoveProduct(product);
            repo.Save();
        }

        public IEnumerable<ItemFinal> GetAllProducts()
        {
            return repo.GetAllProducts();
        }

        public ItemFinal GetProductById(int productId)
        {
            return repo.GetProductById(productId);
        }

        public IEnumerable<ItemFinal> GetProductsByCategory(int categoryId)
        {
            return repo.GetProductsByCategory(categoryId);
        }

        public IEnumerable<ItemFinal> GetProductsByKeyword(string keyword)
        {
            return repo.GetProductsByKeyword(keyword);
        }
    }
}