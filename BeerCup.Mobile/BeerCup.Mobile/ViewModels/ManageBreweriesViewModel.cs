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
    public class ManageBreweriesViewModel : ViewModelBase
    {
        private readonly IAdminPanelDataService _adminPanelDataService;
        private ObservableCollection<Brewery> _breweries;

        public ManageBreweriesViewModel(INavigationService navigationService, IAdminPanelDataService adminPanelDataService) : base(navigationService)
        {
            _adminPanelDataService = adminPanelDataService;
        }

        public ICommand BreweryTappedCommand => new Command(OnBreweryTapped);


        public ObservableCollection<Brewery> Breweries
        {
            get => _breweries;
            set
            {
                _breweries = value;
                OnPropertyChanged();
            }
        }

        private async void OnBreweryTapped(object obj)
        {
            var brewery = (Brewery)obj;
            //todo: jakiś pop-up z pytaniem czy chcesz usunąć czy zmienić nazwę browaru i odpowiedni flow programu
            await _adminPanelDataService.DeleteBrewery(brewery);
        }

        public override async Task InitializeAsync(object data)
        {
            Breweries = (await _adminPanelDataService.GetAllBreweries()).ToObservableCollection();
        }
    }
}
