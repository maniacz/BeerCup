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
using Xamarin.Forms.MultiSelectListView;

namespace BeerCup.Mobile.ViewModels
{
    public class ManageBattlesViewModel : ViewModelBase
    {
        private ObservableCollection<Battle> _battles;
        private readonly IBattleDataService _battleDataService;

        public ManageBattlesViewModel(INavigationService navigationService, IBattleDataService battleDataService) : base(navigationService)
        {
            _battleDataService = battleDataService;
        }

        public ICommand BattleTappedCommand => new Command<Battle>(OnBattleTapped);


        public ICommand AddBattleTappedCommand => new Command(OnAddBattleTapped);

        public ObservableCollection<Battle> Battles
        {
            get => _battles;
            set
            {
                _battles = value;
                OnPropertyChanged();
            }
        }

        private async void OnBattleTapped(Battle selectedBattle)
        {
            await _navigationService.NavigateToAsync<EditBattleViewModel>(selectedBattle);
        }

        private async void OnAddBattleTapped()
        {
            await _navigationService.NavigateToAsync<AddNewBattleViewModel>();
        }

        public override async Task InitializeAsync(object data)
        {
            Battles = (await _battleDataService.GetAllBattles()).ToObservableCollection();
        }
    }
}
