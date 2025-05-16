using Ecommerce_CubicTaks_.Application.Contract;
using Ecommerce_CubicTaks_.Application.Service;
using Ecommerce_CubicTaks_.Context;
using Ecommerce_CubicTaks_.Infrastructure;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace Ecommerce_CubicTaks_.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<ApplicationDbContext>(new HierarchicalLifetimeManager());

            // Repositories
            container.RegisterType<ICustomerRepository, CustomerRepository>();
            container.RegisterType<ICustomerRepository, CustomerRepository>();
            container.RegisterType<IOrderRepository, OrderRepository>();
            // register all your components with the container here
            container.RegisterType<ICustomerRepository, CustomerRepository>(new HierarchicalLifetimeManager());

            // e.g. container.RegisterType<ITestService, TestService>();


            // Register your services here:
            container.RegisterType<ICustomerService, CustomerService>(new HierarchicalLifetimeManager());

            // Set Web API Dependency Resolver
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}