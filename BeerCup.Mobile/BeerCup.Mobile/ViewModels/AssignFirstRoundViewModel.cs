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
    public class AssignFirstRoundViewModel : ViewModelBase
    {
        private ObservableCollection<Battle> _firstRoundBattles;
        private ObservableCollection<Brewery> _breweries;
        private Battle _selectedBattle;
        private readonly IBattleDataService _battleDataService;

        public AssignFirstRoundViewModel(INavigationService navigationService, IBattleDataService battleDataService) : base(navigationService)
        {
            _battleDataService = battleDataService;
        }

        public ICommand SelectedBattleChangedCommand => new Command<Battle>(OnSelectedBattleChanged);

        public ICommand AddBreweryTappedCommand => new Command(OnAddBreweryTapped);

        public ObservableCollection<Battle> FirstRoundBattles
        {
            get => _firstRoundBattles;
            set
            {
                _firstRoundBattles = value;
                OnPropertyChanged();
            }
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
                OnSelectedBattleChanged(_selectedBattle);
            }
        }

        private async void OnSelectedBattleChanged(Battle selectedBattle)
        {
            Breweries = (await _battleDataService.GetBreweriesFromBattle(selectedBattle.BattleNo)).ToObservableCollection();
        }

        private void OnAddBreweryTapped(object obj)
        {
            throw new NotImplementedException();
        }

        public override async Task InitializeAsync(object data)
        {
            FirstRoundBattles = (await _battleDataService.GetFirstRoundBattles()).ToObservableCollection();
        }
    }
}
