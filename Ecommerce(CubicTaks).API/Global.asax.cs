using Autofac.Core;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.WebApi;
using Ecommerce_CubicTaks_.Application.Contract;
using Ecommerce_CubicTaks_.Infrastructure;
using Ecommerce_CubicTaks_.Application.Service;
using Ecommerce_CubicTaks_.Context;
using AutoMapper;
using Ecommerce_CubicTaks_.Application.Mapper;
using Microsoft.Win32;
using FluentValidation.WebApi;
using Ecommerce_CubicTaks_.API.FluentValidation;
using Ecommerce_CubicTaks_.Application.Service.Jwt;

namespace Ecommerce_CubicTaks_.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        //protected void Application_Start()
        //{
        //    GlobalConfiguration.Configure(WebApiConfig.Register);

        //    AreaRegistration.RegisterAllAreas();


        //    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);
        //    BundleConfig.RegisterBundles(BundleTable.Bundles);
        //    //UnityConfig.RegisterComponents();
        //}
        protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;
            // ✅ Register FluentValidation (auto-discovers validators)
            // ✅ Manually assign the validator factory
            FluentValidationModelValidatorProvider.Configure(config, provider =>
            {
                provider.ValidatorFactory = new CustomValidatorFactory();
            }); AreaRegistration.RegisterAllAreas();
            //  Register routes and other ASP.NET MVC
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // 1. Create the container builder
            var builder = new ContainerBuilder();

            // 2. Register your Web API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // 3. Register dependencies
            builder.RegisterType<ApplicationDbContext>();
         
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<CustomerOrderRepository>().As<ICustomerOrderRepository>();
            builder.RegisterType<testserice>().As<Itestserice>();
            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<JwtService>().As<IJwtService>().SingleInstance();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();

            // 3. AutoMapper registration
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>(); // Replace with your profile
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.RegisterInstance(mapper).As<IMapper>().SingleInstance();

            // 4. Build the container
            var container = builder.Build();
            // 5. Set the Web API dependency resolver
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // 6. Register routes and other Web API config
            GlobalConfiguration.Configure(WebApiConfig.Register);
           


        }
    }
    }
