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
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            //var authenticationServise = AppContainer.Resolve<IAuthenticationService>();
            //var navigationService = AppContainer.Resolve<INavigationService>();
            //BindingContext = new LoginViewModel(authenticationServise, navigationService);
        }
    }
}