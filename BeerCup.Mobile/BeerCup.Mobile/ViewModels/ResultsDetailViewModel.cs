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
using System.Linq;

namespace BeerCup.Mobile.ViewModels
{
    public class ResultsDetailViewModel : ViewModelBase
    {
        private readonly IBattleDataService _battleDataService;
        private ObservableCollection<Result> _battleResults;
        private Battle _selectedBattle;

        public ResultsDetailViewModel(INavigationService navigationService, IBattleDataService battleDataService) : base(navigationService)
        {
            _battleDataService = battleDataService;
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
            BattleResults = (await _battleDataService.GetBattleResults(SelectedBattle.Id)).ToObservableCollection();
            var userVotesInBattle = await _battleDataService.GetBattleUserVotes(SelectedBattle.Id);

            foreach (var result in BattleResults)
            {
                if (userVotesInBattle.Where(uv => uv.BeerId == result.BeerId).Any())
                {
                    result.UserVotedFor = true;
                }
            }
            //BattleResults = (await GetFakeBattleResultsAsync()).ToObservableCollection();
        }



        private async Task<List<Result>> GetFakeBattleResultsAsync()
        {
            return new List<Result>
            {
                new Result(1, "BroGar", 30, "60.0"),
                //new Result(2, "Venom", 10, 20.0M),
                //new Result(3, "Kazamat", 5, 10.0M),
                //new Result(4, "Hołda", 3, 6.0M),
                //new Result(5, "Bastion", 2, 4.0M)
            };
        }

    }
}
