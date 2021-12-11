using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Extensions;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BeerCup.Mobile.ViewModels
{
    public class AddBreweryToFirstRoundBattleViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IBreweryDataService _breweryDataService;
        private readonly IBeerDataService _beerDataService;
        private ObservableCollection<Brewery> _notAssignedBreweries;
        private Battle _battle;
        private Brewery _selectedBrewery;

        public AddBreweryToFirstRoundBattleViewModel(INavigationService navigationService, IDialogService dialogService, IBreweryDataService breweryDataService, IBeerDataService beerDataService) 
            : base(navigationService)
        {
            _dialogService = dialogService;
            _breweryDataService = breweryDataService;
            _beerDataService = beerDataService;
        }

        public ICommand AddBreweryTappedCommand => new Command(OnAddBreweryTapped);

        public ICommand CancelTappedCommand => new Command(OnCancelTapped);

        public ObservableCollection<Brewery> NotAssignedBreweries 
        {
            get => _notAssignedBreweries;
            set
            {
                _notAssignedBreweries = value;
                OnPropertyChanged();
            }
        }

        public Brewery SelectedBrewery
        { 
            get => _selectedBrewery;
            set 
            {
                _selectedBrewery = value;
                OnPropertyChanged();
            }
        }

        private async void OnCancelTapped(object obj)
        {
            await _navigationService.NavigateBackAsync();
        }

        private async void OnAddBreweryTapped()
        {
            var breweryId = SelectedBrewery.BreweryId;
            var battleId = _battle.Id;
            var addedBeer = await _beerDataService.RegisterBeerInFirstRound(battleId, breweryId);

            if (addedBeer != null)
            {
                MessagingCenter.Send(this, MessagingConstants.BreweryAssignedInFirstRound, _battle);
                await _navigationService.NavigateBackAsync();
            }
            else
            {
                await _dialogService.ShowDialog("Nie udało się zarejestrować browaru do pierwszej rundy!", "Rejestracja Browaru", "OK");
            }
        }

        public override async Task InitializeAsync(object data)
        {
            _battle = (Battle)data;
            NotAssignedBreweries = (await _breweryDataService.GetAllNotAssignedInFirstRoundBreweries()).ToObservableCollection();
        }
    }
}