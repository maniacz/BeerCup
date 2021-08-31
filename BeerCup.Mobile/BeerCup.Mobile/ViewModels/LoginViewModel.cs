using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.ViewModels.Base;
using BeerCup.Mobile.Services.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace BeerCup.Mobile.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;

        private string _username;
        private string _password;

        public LoginViewModel(IAuthenticationService authenticationService, INavigationService navigationService)
            : base(navigationService)
        {
            _authenticationService = authenticationService;
        }

        public ICommand LoginCommand => new Command(OnLogin);

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private async void OnLogin()
        {
            bool test = false;
            //todo: connectionService
            //var authenticationResponse = await _authenticationService.Authenticate(Username, Password);
            //if (authenticationResponse.IsAuthenticated)
            //{
            //    await _navigationService.NavigateToAsync<MainViewModel>();
            //}

        }
    }
}
