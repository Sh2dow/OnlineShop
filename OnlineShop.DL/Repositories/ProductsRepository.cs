using OnlineShop.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using System.Diagnostics;

namespace OnlineShop.DL
{
    public class ProductsRepository
    {
        private ProductContext _db;

        public ProductsRepository()
        {
            _db = new ProductContext();
        }

        public void AddProduct(StoreItem product)
        {
            if (_db.Products.Any(e => e.ItemID == product.ItemID))
            {
                Debug.Print("Updating entity id: " + product.ItemID);
                _db.Products.Attach(product);
                _db.Entry(product).State = EntityState.Modified;
            }
            else
            {
                _db.Products.Attach(product);
                _db.Products.Add(product);
            }
        }

        public void AddProducts(IEnumerable<StoreItem> products)
        {
            if (products != null)
            {
                foreach (var product in products)
                {
                    AddProduct(product);
                }
            }
        }

        StoreItem Get(StoreItem detachedModel)
        {
            using (var context = new ProductContext())
            {
                return context.Products.Single(x => x.ItemID == detachedModel.ItemID);
            }
        }

        public void UpdateProduct(StoreItem product)
        {
            _db.Products.Attach(product);
            _db.Entry(product).State = EntityState.Modified;
        }

        public void RemoveProduct(StoreItem product)
        {
            if (product != null)
            {
                _db.Products.Remove(product);
            }
        }

        public IEnumerable<StoreItem> GetAllProducts()
        {
            return _db.Products;
        }

        public IEnumerable<StoreItem> GetProductsByCategory(long categoryId)
        {
            return _db.Products.Where(product => product.PrimaryCategoryID.Equals(categoryId));
        }

        public IEnumerable<StoreItem> GetProductsByKeyword(string keyword)
        {
            return _db.Products.Where(product => product.Title.Contains(keyword));
        }

        public StoreItem GetProductById(string id)
        {
            return _db.Products.FirstOrDefault(product => product.ItemID == id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
