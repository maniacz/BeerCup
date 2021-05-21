using BeerCup.Bootstrap;
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

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
