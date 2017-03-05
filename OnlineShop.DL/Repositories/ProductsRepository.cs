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

        public void AddProduct(Item product)
        {
            if (product != null)
            {
                _db.Products.Add(product);
            }
        }

        public void UpdateProduct(Item product)
        {
            _db.Products.Attach(product);
            _db.Entry(product).State = EntityState.Modified;
        }

        public void RemoveProduct(Item product)
        {
            if (product != null)
            {
                _db.Products.Remove(product);
            }
        }

        public IEnumerable<Item> GetAllProducts()
        {
            return _db.Products;
        }

        public IEnumerable<Item> GetProductsByCategory(int categoryId)
        {
            return _db.Products.Where(product => product.PrimaryCategoryID.Equals(categoryId));
        }

        public IEnumerable<Item> GetProductsByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetProductsByCategory(string keyword)
        {
            return _db.Products.Where(product => product.Title.Contains(keyword));
        }
        

        public Item GetProductById(int id)
        {
            return _db.Products.FirstOrDefault(product => product.ItemID == id.ToString());
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
