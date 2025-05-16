using Ecommerce_CubicTaks_.Model.Model;
using Ecommerce_CubicTaks_.Model.Model.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("ECommerceConnection") { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Composite key for join table
            modelBuilder.Entity<CustomerOrder>()
                .HasKey(co => new { co.CustomerId, co.OrderId });

            modelBuilder.Entity<CustomerOrder>()
                .HasRequired(co => co.Customer)
                .WithMany(c => c.CustomerOrders)
                .HasForeignKey(co => co.CustomerId);

            modelBuilder.Entity<CustomerOrder>()
                .HasRequired(co => co.Order)
                .WithMany(o => o.CustomerOrders)
                .HasForeignKey(co => co.OrderId);
       

            base.OnModelCreating(modelBuilder);
        }
    }
}
