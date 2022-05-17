using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace BeerCup.Mobile.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsService _settingsService;

        private string _username;
        private string _password;
        private string _email;
        private string _accessCode;

        public RegisterViewModel(IAuthenticationService authenticationService, INavigationService navigationService, ISettingsService settingsService) : base(navigationService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;
        }

        public ICommand RegisterCommand => new Command(OnRegister);
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

        public string Email
        {
            get => _email;
            set
            {
                //todo: walidacja adresu mailowego
                _email = value;
                OnPropertyChanged();
            }
        }

        public string AccessCode
        {
            get => _accessCode;
            set
            {
                //todo: walidacja access kodu
                _accessCode = value;
                OnPropertyChanged();
            }
        }

        private async void OnRegister()
        {
            //todo: connectionService
            //todo: fluent validation czy wszystkie pola są wypełnione

            //todo: checking if e-mail already registered
            //todo: DEBUG values
            Username = "ehe";
            Password = "fedf";
            Email = "kamo@wp.pl";
            AccessCode = "A003";

            var registrationResponse = await _authenticationService.Register(Username, Password, Email, AccessCode);
            if (string.IsNullOrEmpty(registrationResponse.Error))
            {
                if (registrationResponse.Data != null && registrationResponse.Data.IsAuthenticated)
                {
                    //todo: await _dialogService.ShowDialog("Registration successful", "Message", "OK");
                    _settingsService.UserNameSetting = registrationResponse.Data.Username;
                    _settingsService.UserRoleSetting = registrationResponse.Data.Role;
                    _settingsService.UserIdSetting = registrationResponse.Data.UserId;

                    await _navigationService.NavigateToAsync<MainViewModel>(registrationResponse.Data.Role);
                }
                else
                {
                    //todo: _dialogService.ShowDialog("Rejestracja nieudana", "Nie autoryzowano", "OK");
                }
            }
            else
            {
                switch (registrationResponse.Error)
                {
                    case ApiErrorResponseConstants.NotValidAccessCode:
                        //todo: _dialogService.ShowDialog("Rejestracja nieudana", "Nieprawidłowy kod dostępu", "OK");
                        break;
                    case ApiErrorResponseConstants.UserAlreadyExists:
                        //todo: _dialogService.ShowDialog("Rejestracja nieudana", "Taki użytkownik już istnieje", "OK");
                        break;
                    default:
                        break;
                }
            }
        }
        private void OnLogin()
        {
            _navigationService.NavigateToAsync<LoginViewModel>();
        }
    }
}
