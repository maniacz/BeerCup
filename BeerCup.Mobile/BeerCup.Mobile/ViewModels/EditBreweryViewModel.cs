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
        private Brewery _brewery;

        public EditBreweryViewModel(INavigationService navigationService, IAdminPanelDataService adminPanelDataService) : base(navigationService)
        {
            _adminPanelDataService = adminPanelDataService;
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

        private void OnSaveEditBrewery()
        {
            throw new NotImplementedException();
        }

        private async void OnDeleteBrewery()
        {
            var removedBrewery = await _adminPanelDataService.DeleteBrewery(this.Brewery);
            if (removedBrewery != null)
            {
                //todo: tu zrobić zamiast alertu okno typu "Czy na pewno chcesz usunąć browar ###?" i logikę do tego niżej
                await Application.Current.MainPage.DisplayAlert("BeerCup", $"Usunięto z turnieju browar {this.Brewery.Name}", "OK");
                //todo: _navigationService.NavigateBack
                await _navigationService.NavigateToAsync<ManageBreweriesViewModel>();
            }
        }

        public override async Task InitializeAsync(object data)
        {
            var brewery = (Brewery)data;
            Brewery = brewery;
        }
    }
}
