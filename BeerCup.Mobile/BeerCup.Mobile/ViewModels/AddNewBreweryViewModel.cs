using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BeerCup.Mobile.ViewModels
{
    public class AddNewBreweryViewModel : ViewModelBase
    {
        private readonly IAdminPanelDataService _adminPanelDataService;
        private string _breweryName;

        public AddNewBreweryViewModel(INavigationService navigationService, IAdminPanelDataService adminPanelDataService) : base(navigationService)
        {
            _adminPanelDataService = adminPanelDataService;
        }

        public ICommand AddBreweryTapped => new Command(OnAddBrewery);

        public string BreweryName
        {
            get => _breweryName;
            set
            {
                _breweryName = value;
                OnPropertyChanged();
            }
        }

        private async void OnAddBrewery(object obj)
        {
            var newBrewery = new Brewery { Name = BreweryName };
            var addedBrewery = await _adminPanelDataService.AddNewBrewery(newBrewery);
            if (addedBrewery != null)
            {
                MessagingCenter.Send(this, MessagingConstants.BreweryAdded, addedBrewery);
                await _navigationService.NavigateBackAsync();
            }
            //todo: dodać obsługę w przypadku nieudanego dodania browaru, fluent validation WebAPI zwraca np. nazwa nie może być pusta itp.
        }
    }
}
