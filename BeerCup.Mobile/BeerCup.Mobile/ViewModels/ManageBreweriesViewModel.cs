using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Extensions;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BeerCup.Mobile.ViewModels
{
    public class ManageBreweriesViewModel : ViewModelBase
    {
        private readonly IAdminPanelDataService _adminPanelDataService;
        private ObservableCollection<Brewery> _breweries;

        public ManageBreweriesViewModel(INavigationService navigationService, IAdminPanelDataService adminPanelDataService) : base(navigationService)
        {
            _adminPanelDataService = adminPanelDataService;
            InitializeMessenger();
        }

        public ICommand BreweryTappedCommand => new Command(OnBreweryTapped);
        public ICommand AddBreweryTappedCommand => new Command(OnAddBreweryTapped);

        public ObservableCollection<Brewery> Breweries
        {
            get => _breweries;
            set
            {
                _breweries = value;
                OnPropertyChanged();
            }
        }

        public void InitializeMessenger()
        {
            //todo: jeśli finalnie obiekty typu payload jak np. deletedBrewery nie będą używane w obsługujących metodach to je wywal stąd
            MessagingCenter.Subscribe<EditBreweryViewModel, Brewery>(this, MessagingConstants.BreweryDeleted,
                (editBreweryViewModel, deletedBrewery) => OnBreweryDeletedReceived(deletedBrewery));
            MessagingCenter.Subscribe<EditBreweryViewModel, Brewery>(this, MessagingConstants.BreweryNameChanged,
                (editBreweryViewModel, modifiedBrewery) => OnBreweryNameChangedReceived(modifiedBrewery));
            MessagingCenter.Subscribe<AddNewBreweryViewModel, Brewery>(this, MessagingConstants.BreweryAdded,
                (addNewBreweryViewModel, addedBrewery) => OnBreweryAddedReceived(addedBrewery));
        }


        private async void OnBreweryNameChangedReceived(Brewery modifiedBrewery)
        {
            await RefreshBreweriesList();
        }

        private async void OnBreweryDeletedReceived(Brewery deletedBrewery)
        {
            await RefreshBreweriesList();
        }

        private async void OnBreweryAddedReceived(Brewery addedBrewery)
        {
            await RefreshBreweriesList();
        }

        private async Task RefreshBreweriesList()
        {
            Breweries = (await _adminPanelDataService.GetAllBreweries()).ToObservableCollection();
        }

        private async void OnBreweryTapped(object obj)
        {
            var brewery = (Brewery)obj;
            await _navigationService.NavigateToAsync<EditBreweryViewModel>(brewery);
        }

        private async void OnAddBreweryTapped()
        {
            await _navigationService.NavigateToAsync<AddNewBreweryViewModel>();
        }

        public override async Task InitializeAsync(object data)
        {
            Breweries = (await _adminPanelDataService.GetAllBreweries()).ToObservableCollection();
        }
    }
}
