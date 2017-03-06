using OnlineShop.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace OnlineShop.DL
{
    public class ProductsRepository
    {
        private ProductContext _db;

        public ProductsRepository()
        {
            _db = new ProductContext();
        }

        public void AddProducts(IEnumerable<ItemFinal> products)
        {
            if (products != null)
            {
                _db.Products.AddRange(products);
            }
        }

        public void UpdateProduct(ItemFinal product)
        {
            _db.Products.Attach(product);
            _db.Entry(product).State = EntityState.Modified;
        }

        public void RemoveProduct(ItemFinal product)
        {
            if (product != null)
            {
                _db.Products.Remove(product);
            }
        }

        public IEnumerable<ItemFinal> GetAllProducts()
        {
            return _db.Products;
        }

        public IEnumerable<ItemFinal> GetProductsByCategory(long categoryId)
        {
            return _db.Products.Where(product => product.PrimaryCategoryID.Equals(categoryId));
        }

        public IEnumerable<ItemFinal> GetProductsByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemFinal> GetProductsByCategory(string keyword)
        {
            return _db.Products.Where(product => product.Title.Contains(keyword));
        }
        

        public ItemFinal GetProductById(string id)
        {
            return _db.Products.FirstOrDefault(product => product.ItemID == id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
