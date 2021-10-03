using BeerCup.Mobile.Bootstrap;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeerCup.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterView : ContentPage
    {
        public RegisterView()
        {
            InitializeComponent();
            //todo: wywalić
            //var authenticationService = AppContainer.Resolve<IAuthenticationService>();
            //var navigationService = AppContainer.Resolve<INavigationService>();
            //var settingService = AppContainer.Resolve<ISettingsService>();
            //this.BindingContext = new RegisterViewModel(authenticationService, navigationService, settingService);
        }
    }
}