using Autofac;
using BeerCup.Contracts.Repository;
using BeerCup.Contracts.Services.Data;
using BeerCup.Contracts.Services.General;
//using BeerCup.DataAccess;
using BeerCup.Repository;
using BeerCup.Services.Data;
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
            //builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)); //todo: Czy to jest potrzebne?
            builder.RegisterType(typeof(AuthenticationService)).As(typeof(IAuthenticationService));

            //services - general
            builder.RegisterType(typeof(NavigationService)).As(typeof(INavigationService));

            //General
            builder.RegisterType(typeof(GenericRepository)).As(typeof(IGenericRepository));

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
