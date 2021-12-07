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
        private readonly IBreweryDataService _breweryDataService;
        private ObservableCollection<Brewery> _notAssignedBreweries;

        public AddBreweryToFirstRoundBattleViewModel(INavigationService navigationService, IBreweryDataService breweryDataService) 
            : base(navigationService)
        {
            _breweryDataService = breweryDataService;
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

        private async void OnCancelTapped(object obj)
        {
            await _navigationService.NavigateBackAsync();
        }

        private void OnAddBreweryTapped(object obj)
        {
            throw new NotImplementedException();
        }

        public override async Task InitializeAsync(object data)
        {
            NotAssignedBreweries = (await _breweryDataService.GetAllNotAssignedInFirstRoundBreweries()).ToObservableCollection();
        }
    }
}
