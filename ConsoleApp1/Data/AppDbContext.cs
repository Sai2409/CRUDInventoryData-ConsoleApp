﻿
using ECommerceInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceInventory.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductStockAvailability> ProductStockAvailabilities { get; set; }

    }
}
