using OnlineShop.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OnlineShop.DL
{
    public class ProductsRepository
    {
        private ProductContext _db;

        public ProductsRepository()
        {
            _db = new ProductContext();
        }

        public void AddProduct(Product product)
        {
            if (product != null)
            {
                _db.Products.Add(product);
            }
        }

        public void UpdateProduct(Product product)
        {
            _db.Products.Attach(product);
            _db.Entry(product).State = EntityState.Modified;
        }

        public void RemoveProduct(Product product)
        {
            if (product != null)
            {
                _db.Products.Remove(product);
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _db.Products;
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _db.Products.Where(product => product.PrimaryCategoryID.Equals(categoryId));
        }

        public Product GetProductById(int id)
        {
            return _db.Products.FirstOrDefault(product => product.ItemID == id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
