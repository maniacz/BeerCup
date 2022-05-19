using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace BeerCup.Mobile.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IDialogService _dialogService;
        private readonly ISettingsService _settingsService;


        private string _username;
        private string _password;

        public LoginViewModel(IAuthenticationService authenticationService, INavigationService navigationService, IDialogService dialogService, ISettingsService settingsService)
            : base(navigationService)
        {
            _authenticationService = authenticationService;
            _dialogService = dialogService;
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
            if (authenticationResponse?.Data?.IsAuthenticated == true)
            {
                _settingsService.UserNameSetting = authenticationResponse.Data.Username;
                _settingsService.UserRoleSetting = authenticationResponse.Data.Role;
                _settingsService.UserIdSetting = authenticationResponse.Data.UserId;

                await _navigationService.NavigateToAsync<MainViewModel>(authenticationResponse.Data.Role);
            }
            else
            {
                if (authenticationResponse.Error != null)
                {
                    string authenticationFailReason;
                    switch (authenticationResponse.Error)
                    {
                        case ApiErrorResponseConstants.Unauthorized:
                            authenticationFailReason = "Użytkownik nieautoryzowany";
                            break;
                        case ApiErrorResponseConstants.ServiceUnavailable:
                            authenticationFailReason = "Serwis niedostępny";
                            break;
                        default:
                            authenticationFailReason = "Coś poszło nie tak";
                            break;
                    }

                    await _dialogService.ShowDialog(authenticationFailReason, "Logowanie", "OK");
                    return;
                }
                await _dialogService.ShowDialog("Nie zalogowano", "Logowanie", "OK");
            }
        }

        private void OnRegister()
        {
            _navigationService.NavigateToAsync<RegisterViewModel>();
        }
    }
}
