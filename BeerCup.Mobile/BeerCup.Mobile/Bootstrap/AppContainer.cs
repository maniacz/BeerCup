using Autofac;
using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Services.Data;
using BeerCup.Mobile.Services.General;
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
            builder.RegisterType<MenuViewModel>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<HomeViewModel>();
            builder.RegisterType<BattleViewModel>();
            builder.RegisterType<BreweryStatsViewModel>();
            builder.RegisterType<RegisterViewModel>();
            builder.RegisterType<AdminPanelViewModel>();
            builder.RegisterType<ResultsCatalogViewModel>();
            builder.RegisterType<ResultsDetailViewModel>();
            builder.RegisterType<ScheduleCatalogViewModel>();
            builder.RegisterType<ScheduleDetailViewModel>();
            builder.RegisterType<ManageBattlesViewModel>();
            builder.RegisterType<AddNewBattleViewModel>();
            builder.RegisterType<EditBattleViewModel>();
            builder.RegisterType<ManageBreweriesViewModel>();
            builder.RegisterType<EditBreweryViewModel>();
            builder.RegisterType<AddNewBreweryViewModel>();
            builder.RegisterType<AssignFirstRoundViewModel>();
            builder.RegisterType<AddBreweryToFirstRoundBattleViewModel>();

            //services - data
            builder.RegisterType(typeof(AuthenticationService)).As(typeof(IAuthenticationService));
            builder.RegisterType(typeof(VotingDataService)).As(typeof(IVotingDataService));
            builder.RegisterType(typeof(AdminPanelDataService)).As(typeof(IAdminPanelDataService));
            builder.RegisterType(typeof(BattleDataService)).As(typeof(IBattleDataService));
            builder.RegisterType(typeof(BreweryDataService)).As(typeof(IBreweryDataService));
            builder.RegisterType(typeof(BeerDataService)).As(typeof(IBeerDataService));

            //services - general
            builder.RegisterType(typeof(NavigationService)).As(typeof(INavigationService));
            builder.RegisterType(typeof(SettingsService)).As(typeof(ISettingsService)).SingleInstance();
            builder.RegisterType(typeof(GeolocationService)).As(typeof(IGeolocationService));
            builder.RegisterType(typeof(DialogService)).As(typeof(IDialogService));

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
