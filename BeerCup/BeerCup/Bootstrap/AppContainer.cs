using Autofac;
using BeerCup.Contracts.Services.General;
using BeerCup.DataAccess;
using BeerCup.Services.General;
using BeerCup.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Bootstrap
{
    public class AppContainer
    {
        public static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //ViewModels
            builder.RegisterType<MainViewModel>();

            //services - data
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));

            //services - general
            builder.RegisterType(typeof(NavigationService)).As(typeof(INavigationService));

            _container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public static object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }
    }
}
