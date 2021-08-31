using Autofac;
using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Services.Data;
using BeerCup.Mobile.Services.General;
//using BeerCup.DataAccess;
using BeerCup.Mobile.Repository;
using BeerCup.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Bootstrap
{
    public class AppContainer
    {
        public static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //ViewModels
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<LoginViewModel>();

            //services - data
            //builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)); //todo: Czy to jest potrzebne?
            builder.RegisterType(typeof(AuthenticationService)).As(typeof(IAuthenticationService));

            //services - general
            builder.RegisterType(typeof(NavigationService)).As(typeof(INavigationService));
            builder.RegisterType(typeof(SettingsService)).As(typeof(ISettingsService));

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
