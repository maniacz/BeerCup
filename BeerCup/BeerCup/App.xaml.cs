using BeerCup.Bootstrap;
using BeerCup.Contracts.Services.General;
using BeerCup.Services;
using BeerCup.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeerCup
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            InitializeApp();

            InitializeNavigation();

            //todo: wyjebać
            //DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();
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
