using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SnacksStore.Data.Model;

namespace SnacksStore.Data
{
    public class SnacksStoreContext : DbContext
    {
        //Entities
        public DbSet<Rol> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPriceLog> ProductPriceLogs { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseProducts> PurchaseProducts { get; set; }


        public SnacksStoreContext(DbContextOptions<SnacksStoreContext> options) : base(options){ }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}
