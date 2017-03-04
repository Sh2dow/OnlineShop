using OnlineShop.BL.Services.Interfaces;
using System;
using System.Collections.Generic;
using OnlineShop.Models;
using OnlineShop.DL;

namespace OnlineShop.BL.Services
{
    public class FillService : IFillService
    {
        private ProductsRepository repo;

        public FillService()
        {
            repo = new ProductsRepository();
        }

        public void UpdateProduct(Product product)
        {
            repo.UpdateProduct(product);
            repo.Save();
        }

        public void RemoveProduct(Product product)
        {
            repo.RemoveProduct(product);
            repo.Save();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return repo.GetAllProducts();
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return repo.GetProductsByCategory(categoryId);
        }

        public Product GetProductById(int productId)
        {
            return repo.GetProductById(productId);
        }
    }
}