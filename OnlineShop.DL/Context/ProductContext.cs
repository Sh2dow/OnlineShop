﻿using System.Data.Entity;
using OnlineShop.Models;

namespace OnlineShop.DL.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("ProductContext")
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
