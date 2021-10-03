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
using AutoMapper.Contrib.Autofac.DependencyInjection;
using BeerCup.Mobile.Mappings;

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
            builder.RegisterType<MenuViewModel>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<HomeViewModel>();
            builder.RegisterType<BattleViewModel>();
            builder.RegisterType<BreweryStatsViewModel>();
            builder.RegisterType<RegisterViewModel>();


            //services - data
            builder.RegisterType(typeof(AuthenticationService)).As(typeof(IAuthenticationService));
            builder.RegisterType(typeof(VotingDataService)).As(typeof(IVotingDataService));

            //services - general
            builder.RegisterType(typeof(NavigationService)).As(typeof(INavigationService));
            builder.RegisterType(typeof(SettingsService)).As(typeof(ISettingsService)).SingleInstance();

            //General
            builder.RegisterType(typeof(GenericRepository)).As(typeof(IGenericRepository));

            //AutoMapper
            builder.RegisterAutoMapper(typeof(BeersProfile).Assembly);

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
