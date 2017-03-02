using System.Data.Entity;
using OnlineShop.Models;

namespace OnlineShop.DL.Context
{
    class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductContext() : base("ProductContext")
        {
        }
    }
}
