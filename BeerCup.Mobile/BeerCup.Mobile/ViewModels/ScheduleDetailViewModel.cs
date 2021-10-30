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

namespace BeerCup.Mobile.ViewModels
{
    public class ScheduleDetailViewModel : ViewModelBase
    {
        private readonly IBattleDataService _battleDataService;
        private ObservableCollection<Brewery> _breweries;
        private Battle _selectedBattle;

        public ScheduleDetailViewModel(INavigationService navigationService, IBattleDataService battleDataService) : base(navigationService)
        {
            _battleDataService = battleDataService;
        }

        public ObservableCollection<Brewery> Breweries
        {
            get => _breweries;
            set
            {
                _breweries = value;
                OnPropertyChanged();
            }
        }

        public Battle SelectedBattle
        {
            get => _selectedBattle;
            set
            {
                _selectedBattle = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object data)
        {
            SelectedBattle = (Battle)data;
            Breweries = (await _battleDataService.GetBreweriesFromBattle(SelectedBattle.Id)).ToObservableCollection();
        }
    }
}
