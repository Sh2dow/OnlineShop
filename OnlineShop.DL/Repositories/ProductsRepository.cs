using OnlineShop.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using OnlineShop.DL.Context;

namespace OnlineShop.DL.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private ProductContext _db;

        public Exception ExceptionToThrow { get; set; }

        public ProductsRepository()
        {
            _db = new ProductContext();
        }

        public void AddProduct(StoreItem product)
        {
            if (_db.Products.Any(e => e.ItemID == product.ItemID))
            {
                _db.Products.Attach(product);
                _db.Entry(product).State = EntityState.Modified;
            }
            else
            {
                //_db.Products.Attach(product);
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

        public void UpdateProduct(StoreItem product)
        {
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

        public IEnumerable<StoreItem> GetProductsByCategory(string categoryId)
        {
            var result = _db.Products.Where(product => product.PrimaryCategoryID.Contains(categoryId));
            return (result.Count() == 0 ? GetAllProducts() : result);
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

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
