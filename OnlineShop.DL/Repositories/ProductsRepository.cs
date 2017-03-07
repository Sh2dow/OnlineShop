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

        public void AddProducts(IEnumerable<ItemFinal> products)
        {
            if (products != null)
            {
                foreach (var product in products)
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
                _db.SaveChanges();
            }
        }

        ItemFinal Get(ItemFinal detachedModel)
        {
            using (var context = new ProductContext())
            {
                return context.Products.Single(x => x.ItemID == detachedModel.ItemID);
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
