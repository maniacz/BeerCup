using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Bootstrap;
using BeerCup.Mobile.Contracts.Services.General;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeerCup.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            InitializeApp();

            InitializeNavigation();

            //InitializeDebug();

            //MainPage = new MainPage();
        }

        private async void InitializeDebug()
        {
            var authenticationService = AppContainer.Resolve<IAuthenticationService>();
            var response = await authenticationService.Authenticate("uq", "pass");
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
