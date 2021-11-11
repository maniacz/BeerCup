using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Bootstrap;
using BeerCup.Mobile.Contracts.Services.General;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BeerCup.Mobile.Views;
using BeerCup.Mobile.ViewModels;

namespace BeerCup.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            InitializeApp();

            InitializeNavigation();

            InitializeDebug();
            //MainPage = new MainPage();

            //MainPage = new BattleView();
        }

        private async void InitializeDebug()
        {
            var authenticationService = AppContainer.Resolve<IAuthenticationService>();
            var navigationService = AppContainer.Resolve<INavigationService>();

            //var response = await authenticationService.Authenticate("uq", "pass");
            var response = await authenticationService.Authenticate("uqy", "pass");

            await navigationService.NavigateToAsync<ManageBreweriesViewModel>();

            //var response = await authenticationService.Register("kamo", "pass", "kamo@gmail.com", "A002");
        }

        private void InitializeNavigation()
        {
            var navigationService = AppContainer.Resolve<INavigationService>();
            navigationService.InitializeAsync();
        }

        private void InitializeApp()
        {
            AppContainer.RegisterDependencies();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
