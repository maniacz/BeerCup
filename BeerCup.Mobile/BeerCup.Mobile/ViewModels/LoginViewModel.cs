using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.ViewModels.Base;
using BeerCup.Mobile.Services.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using System;

namespace BeerCup.Mobile.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsService _settingsService;


        private string _username;
        private string _password;

        public LoginViewModel(IAuthenticationService authenticationService, INavigationService navigationService, ISettingsService settingsService)
            : base(navigationService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;
        }

        public ICommand LoginCommand => new Command(OnLogin);
        public ICommand RegisterCommand => new Command(OnRegister);



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
            //todo: DEBUG - wywalić
            Username = "uq";
            Password = "pass";

            //todo: connectionService
            var authenticationResponse = await _authenticationService.Authenticate(Username, Password);
            if (authenticationResponse.IsAuthenticated)
            {
                _settingsService.UserNameSetting = authenticationResponse.User.Username;
                _settingsService.UserRoleSetting = authenticationResponse.User.Role;
                _settingsService.UserIdSetting = authenticationResponse.User.UserId;
                //todo: finalnie musi się jakoś id bitwy ustawiać
                _settingsService.BattleIdSetting = 1;

                if (authenticationResponse.User.Role == Enums.UserRole.Voter)
                {
                    await _navigationService.NavigateToAsync<MainViewModel>(authenticationResponse.User.Role);
                }





                await _navigationService.NavigateToAsync<MainViewModel>();
            }
            else
            {
                //todo: dodać dialogService, który zwróci jakiś pop up z info z niewłaściwym logowaniem
            }
        }

        private void OnRegister()
        {
            _navigationService.NavigateToAsync<RegisterViewModel>();
        }
    }
}
