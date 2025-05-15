namespace Ecommerce_CubicTaks_.Context.Migrations
{
    using Ecommerce_CubicTaks_.Model.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Ecommerce_CubicTaks_.Context.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Ecommerce_CubicTaks_.Context.ApplicationDbContext context)
        {
            // Seed Customers
            context.Customers.AddOrUpdate(
                c => c.Email,
                new Customer { FirstName = "John", LastName = "Doe", Address = "123 Main St", Email = "john@example.com", Phone = "01000111222" },
                new Customer { FirstName = "Jane", LastName = "Smith", Address = "456 Side St", Email = "jane@example.com", Phone = "01000999888" },
                new Customer { FirstName = "Abanob", LastName = "Smith", Address = "456 Side St", Email = "Abanob@example.com", Phone = "01000999888" }
            );
            context.SaveChanges();

            // Get actual customer records with IDs
            var customer1 = context.Customers.FirstOrDefault(c => c.Email == "john@example.com");
            var customer2 = context.Customers.FirstOrDefault(c => c.Email == "jane@example.com");
            var customer3 = context.Customers.FirstOrDefault(c => c.Email == "Abanob@example.com");

            // Seed Orders
            context.Orders.AddOrUpdate(
                o => o.TotalAmount, // Not a good unique key, but fine for seeding example
                new Order { OrderDate = DateTime.Now, TotalAmount = 1500 },
                new Order { OrderDate = DateTime.Now.AddDays(-1), TotalAmount = 950 }
            );
            context.SaveChanges();

            // Get actual orders with IDs
            var order1 = context.Orders.FirstOrDefault(o => o.TotalAmount == 1500);
            var order2 = context.Orders.FirstOrDefault(o => o.TotalAmount == 950);

            // Seed Join Table
            context.CustomerOrders.AddOrUpdate(
                co => new { co.CustomerId, co.OrderId },
                new CustomerOrder { CustomerId = customer1.Id, OrderId = order1.Id },
                new CustomerOrder { CustomerId = customer2.Id, OrderId = order2.Id },
                new CustomerOrder { CustomerId = customer3.Id, OrderId = order2.Id }
            );
            context.SaveChanges();
        }

    }
}
