using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.MultiSelectListView;

namespace BeerCup.Mobile.ViewModels
{
    public class BattleViewModel : ViewModelBase
    {
        private readonly IVotingDataService _votingDataService;

        public MultiSelectObservableCollection<Beer> Beers { get; set; }
        public ICommand BeerTappedCommand => new Command(OnBeerTappedCommand);
        public ICommand VoteCommand => new Command(OnVoteCommand);

        private void OnVoteCommand()
        {
            if (Beers.SelectedItems.Count() == 2)
            {
                try
                {
                    _votingDataService.SendVotes(Beers.SelectedItems);
                }
                catch (Exception)
                {

                    throw;
                }

                //_navigationService.NavigateToAsync<HomeViewModel>();
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

        public BattleViewModel(IVotingDataService votingDataService, INavigationService navigationService)
            : base(navigationService)
        {
            Beers = new MultiSelectObservableCollection<Beer>();
            LoadStartingBeers();
            _votingDataService = votingDataService;
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
