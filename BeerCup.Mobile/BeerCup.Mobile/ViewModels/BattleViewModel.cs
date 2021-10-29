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
        private readonly IVotingDataService _votingDataService;
        private readonly IBattleDataService _battleDataService;
        private readonly IGeolocationService _geolocationService;

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
                    //todo: wykorzystać serwis, żeby zlikwodować coupling do view
                    await Application.Current.MainPage.DisplayAlert("Głosowanie", "Możesz wybrać max 2 piwa", "OK");
                    return;
                }
            }
        }

        public BattleViewModel(IVotingDataService votingDataService, INavigationService navigationService, IBattleDataService battleDataService, IGeolocationService geolocationService)
            : base(navigationService)
        {
            _votingDataService = votingDataService;
            _battleDataService = battleDataService;
            _geolocationService = geolocationService;

            LoadStartingBeers();
        }

        public override async Task InitializeAsync(object data)
        {
            var runningBattle = await _battleDataService.GetCurrentRunningBattle();
            if (runningBattle == null)
            {
                await Application.Current.MainPage.DisplayAlert("Bitwa", "Nie odbywa się teraz żadna bitwa", "OK");
                await _navigationService.PopToRootAsync();
                return;
            }

            if (!_geolocationService.IsUserOnBattlePlace(runningBattle).Result)
            {
                await Application.Current.MainPage.DisplayAlert("Bitwa", "Aby zagłosować musisz udać się na miejsce bitwy", "OK");
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
