using Ecommerce_CubicTaks_.Application.Contract;
using Ecommerce_CubicTaks_.Application.Service;
using Ecommerce_CubicTaks_.Context;
using Ecommerce_CubicTaks_.Infrastructure;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Ecommerce_CubicTaks_.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Register DbContext
            container.RegisterType<ApplicationDbContext>();

            // Repositories
            container.RegisterType<ICustomerRepository, CustomerRepository>();
            container.RegisterType<IOrderRepository, OrderRepository>();
            container.RegisterType<ICustomerOrderRepository, CustomerOrderRepository>();

            // Services
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<IOrderService, OrderService>();

            // AutoMapper (optional if you're using profiles)
            // container.RegisterInstance<IMapper>(AutoMapperConfiguration.Initialize());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}