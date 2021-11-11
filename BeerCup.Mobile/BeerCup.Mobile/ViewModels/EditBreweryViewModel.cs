using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BeerCup.Mobile.ViewModels
{
    public class EditBreweryViewModel : ViewModelBase
    {
        public EditBreweryViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public ICommand DeleteBreweryTapped => new Command(OnDeleteBrewery);

        public ICommand SaveEditBreweryTapped => new Command(OnSaveEditBrewery);

        private void OnSaveEditBrewery(object obj)
        {
            throw new NotImplementedException();
        }

        private void OnDeleteBrewery(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
