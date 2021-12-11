using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.MultiSelectListView;

namespace BeerCup.Mobile.ViewModels
{
    public class BattleViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IVotingDataService _votingDataService;
        private readonly IBattleDataService _battleDataService;
        private readonly IGeolocationService _geolocationService;

        public BattleViewModel(INavigationService navigationService, IDialogService dialogService, IVotingDataService votingDataService, IBattleDataService battleDataService, IGeolocationService geolocationService)
            : base(navigationService)
        {
            _dialogService = dialogService;
            _votingDataService = votingDataService;
            _battleDataService = battleDataService;
            _geolocationService = geolocationService;

            LoadStartingBeers();
        }

        public MultiSelectObservableCollection<Beer> Beers { get; set; }
        public ICommand BeerTappedCommand => new Command(OnBeerTappedCommand);
        public ICommand VoteCommand => new Command(OnVoteCommand);

        private async void OnVoteCommand()
        {
            if (Beers.SelectedItems.Count() == 2)
            {
                try
                {
                    await _votingDataService.SendVotes(Beers.SelectedItems);
                }
                catch (Exception)
                {

                    throw;
                }

                await _navigationService.PopToRootAsync();
            }
        }

        private async void OnBeerTappedCommand(object tappedObject)
        {
            if (tappedObject is Beer selectedBeer)
            {
                if (Beers.SelectedItems.Count() > 2)
                {
                    var tappedBeer = Beers.Where(si => si.Data.AssignedNumberInBattle == selectedBeer.AssignedNumberInBattle).FirstOrDefault();
                    tappedBeer.IsSelected = false;
                    await _dialogService.ShowDialog("Możesz wybrać max 2 piwa", "Głosowanie", "OK");
                    return;
                }
            }
        }

        public override async Task InitializeAsync(object data)
        {
            var runningBattle = await _battleDataService.GetCurrentRunningBattle();
            if (runningBattle == null)
            {
                await _dialogService.ShowDialog("Nie odbywa się teraz żadna bitwa", "Bitwa", "OK");
                await _navigationService.PopToRootAsync();
                return;
            }

            if (!_geolocationService.IsUserOnBattlePlace(runningBattle).Result)
            {
                await _dialogService.ShowDialog("Aby zagłosować musisz udać się na miejsce bitwy", "Bitwa", "OK");
                await _navigationService.PopToRootAsync();
                return;
            }
        }

        private void LoadStartingBeers()
        {
            Beers = new MultiSelectObservableCollection<Beer>();
            var startingBreweriesCount = GetStartingBreweriesCount();

            for (int i = 0; i < startingBreweriesCount; i++)
            {
                Beers.Add(new Beer
                {
                    AssignedNumberInBattle = i + 1,
                });
            }
        }

        private int GetStartingBreweriesCount()
        {
            return 5;
        }
    }
}
