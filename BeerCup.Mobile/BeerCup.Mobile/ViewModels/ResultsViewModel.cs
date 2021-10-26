using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.ViewModels.Base;
using BeerCup.Mobile.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using BeerCup.Mobile.Contracts.Services.Data;

namespace BeerCup.Mobile.ViewModels
{
    public class ResultsViewModel : ViewModelBase
    {
        private readonly IBattleDataService _battleDataService;
        private ObservableCollection<Result> _battleResults;

        public ResultsViewModel(INavigationService navigationService, IBattleDataService battleDataService) : base(navigationService)
        {
            _battleDataService = battleDataService;
            //todo: dodać data service
        }

        public ObservableCollection<Result> BattleResults
        {
            get => _battleResults;
            set
            {
                _battleResults = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object data)
        {
            //BattleResults = (await GetFakeBattleResultsAsync()).ToObservableCollection();
            BattleResults = (await _battleDataService.GetBattleResults(1)).ToObservableCollection();
        }



        private async Task<List<Result>> GetFakeBattleResultsAsync()
        {
            return new List<Result>
            {
                new Result(1, "BroGar", 30, 60.0),
                new Result(2, "Venom", 10, 20.0),
                new Result(3, "Kazamat", 5, 10.0),
                new Result(4, "Hołda", 3, 6.0),
                new Result(5, "Bastion", 2, 4.0)
            };
        }

    }
}
