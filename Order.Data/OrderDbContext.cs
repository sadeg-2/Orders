using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Orders.Data.Models;
using OrderF = Orders.Data.Models.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;

namespace Orders.API.Data
{
    public class OrderDbContext : IdentityDbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
        
            base.OnModelCreating(builder);
            builder.Entity<OrderItem>().HasKey(x => new { x.OrderId, x.MealId });
            //builder.Entity<BaseEntity>().HasQueryFilter(x => !x.IsDelete);
            //builder.Entity<User>().HasQueryFilter(o =>  !o.IsDelete);

        }


        public DbSet<Resturant> Resturants { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<OrderF> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
