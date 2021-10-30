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
    public class ScheduleCatalogViewModel : ViewModelBase
    {
        private ObservableCollection<Battle> _battles;
        private readonly IBattleDataService _battleDataService;

        public ScheduleCatalogViewModel(INavigationService navigationService, IBattleDataService battleDataService) : base(navigationService)
        {
            _battleDataService = battleDataService;
        }

        public ICommand BattleTappedCommand => new Command<Battle>(OnBattleTapped);

        public ObservableCollection<Battle> Battles
        {
            get => _battles;
            set
            {
                _battles = value;
                OnPropertyChanged();
            }
        }

        private void OnBattleTapped(Battle selectedBattle)
        {
            throw new NotImplementedException();
        }

        public override async Task InitializeAsync(object data)
        {
            Battles = (await _battleDataService.GetAllBattles()).ToObservableCollection();
        }
    }
}
