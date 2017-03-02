using OnlineShop.DL.Context;
using OnlineShop.Models;
using System.Data.Entity;
using System.Linq;

namespace OnlineShop.DL.Repositories
{
    class ProductsRepository
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

        public Product GetProductById(int id)
        {
            return _db.Products.FirstOrDefault(product => product.Id == id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
