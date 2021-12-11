using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BeerCup.Mobile.ViewModels
{
    public class EditBreweryViewModel : ViewModelBase
    {
        private readonly IAdminPanelDataService _adminPanelDataService;
        private readonly IDialogService _dialogService;
        private Brewery _brewery;

        public EditBreweryViewModel(INavigationService navigationService, IAdminPanelDataService adminPanelDataService, IDialogService dialogService) : base(navigationService)
        {
            _adminPanelDataService = adminPanelDataService;
            _dialogService = dialogService;
        }

        public ICommand DeleteBreweryTapped => new Command(OnDeleteBrewery);
        public ICommand SaveEditBreweryTapped => new Command(OnSaveEditBrewery);

        public Brewery Brewery
        {
            get => _brewery;
            set
            {
                _brewery = value;
                OnPropertyChanged();
            }
        }

        private async void OnSaveEditBrewery()
        {
            var modifiedBrewery = await _adminPanelDataService.EditBreweryName(this.Brewery);
            if (modifiedBrewery != null)
            {
                MessagingCenter.Send(this, MessagingConstants.BreweryNameChanged, modifiedBrewery);
                await _navigationService.NavigateBackAsync();
            }
        }

        private async void OnDeleteBrewery()
        {
            if (await _dialogService.Confirm($"Czy na pewno chcesz usunąć browar {Brewery.Name}?", "Usuwanie browaru", "Tak", "Nie"))
            {
                var removedBrewery = await _adminPanelDataService.DeleteBrewery(this.Brewery);
                if (removedBrewery != null)
                {
                    MessagingCenter.Send<EditBreweryViewModel, Brewery>(this, MessagingConstants.BreweryDeleted, removedBrewery);
                    _dialogService.ShowToast($"Usunięto browar {Brewery.Name}");
                    await _navigationService.NavigateBackAsync();
                }
            }
        }

        public override async Task InitializeAsync(object data)
        {
            var brewery = (Brewery)data;
            Brewery = brewery;
        }
    }
}
