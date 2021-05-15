using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CodeHome
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Product>().HasKey(t => t.Id);
            
            modelBuilder.Entity<Models.Order>().HasKey(t => t.Id);
            modelBuilder.Entity<Models.Order>().HasMany(t => t.Items).WithOne(t => t.Order);
            modelBuilder.Entity<Models.Order>().HasOne(t => t.Register).WithOne(t => t.Order).IsRequired();


            modelBuilder.Entity<Models.OrderItem>().HasKey(t=> t.Id);
            modelBuilder.Entity<Models.OrderItem>().HasOne(t => t.Order);
            modelBuilder.Entity<Models.OrderItem>().HasOne(t => t.Product);


            modelBuilder.Entity<Models.Register>().HasKey(t=> t.Id);
            modelBuilder.Entity<Models.Register>().HasOne(t => t.Order);
        }
    }
}
