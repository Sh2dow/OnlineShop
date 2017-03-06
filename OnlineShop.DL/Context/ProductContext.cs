using System.Data.Entity;
using OnlineShop.Models;

namespace OnlineShop.DL
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("ProductContext")
        {
        }
        public DbSet<ItemFinal> Products { get; set; }
    }
}
