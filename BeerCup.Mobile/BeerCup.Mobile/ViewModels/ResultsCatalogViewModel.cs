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
    public class ResultsCatalogViewModel : ViewModelBase
    {
        private readonly IBattleDataService _battleDataService;
        private ObservableCollection<Battle> _finishedBattles;

        public ResultsCatalogViewModel(INavigationService navigationService, IBattleDataService battleDataService) : base(navigationService)
        {
            _battleDataService = battleDataService;
        }

        public ObservableCollection<Battle> FinishedBattles
        {
            get => _finishedBattles;
            set
            {
                _finishedBattles = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object data)
        {
            FinishedBattles = (await _battleDataService.GetFinishedBattles()).ToObservableCollection();
        }
    }
}
