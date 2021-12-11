using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
using BeerCup.Mobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BeerCup.Mobile.ViewModels
{
    public class AdminPanelViewModel : ViewModelBase
    {
        private const string PublishBatteResultsText = "Ogłoś wyniki bitwy";
        private const string HideBattleResultsText = "Ukryj wyniki bitwy";
        private readonly IDialogService _dialogService;
        private readonly IAdminPanelDataService _adminPanelDataService;
        private readonly IBattleDataService _battleDataService;
        private readonly IGeolocationService _geolocationService;
        private bool _battleStartAllowed;
        private bool _battleEndAllowed;
        private bool _publishResultsAllowed;
        private string _startButtonText;
        private string _publishButtonText;
        private Battle _todaysBattle;

        public AdminPanelViewModel(INavigationService navigationService, IDialogService dialogService, IAdminPanelDataService adminPanelDataService, IBattleDataService battleDataService, IGeolocationService geolocationService)
            : base(navigationService)
        {
            _dialogService = dialogService;
            _adminPanelDataService = adminPanelDataService;
            _battleDataService = battleDataService;
            _geolocationService = geolocationService;
            BattleStartAllowed = true;
            BattleEndAllowed = false;
            PublishResultsAllowed = false;
        }

        public ICommand StartBattleCommand => new Command(OnStartBattle);
        public ICommand EndBattleCommand => new Command(OnEndBattle);
        public ICommand PublishResultsCommand => new Command(OnPublishResult);


        public bool BattleStartAllowed
        {
            get => _battleStartAllowed;
            set
            {
                _battleStartAllowed = value;
                OnPropertyChanged();
            }
        }

        public bool BattleEndAllowed
        {
            get => _battleEndAllowed;
            set
            {
                _battleEndAllowed = value;
                OnPropertyChanged();
            }
        }

        public bool PublishResultsAllowed
        {
            get => _publishResultsAllowed;
            set
            {
                _publishResultsAllowed = value;
                OnPropertyChanged();
            }
        }

        public string StartButtonText
        {
            get => _startButtonText;
            set
            {
                _startButtonText = value;
                OnPropertyChanged();
            }
        }

        public string PublishButtonText
        {
            get => _publishButtonText;
            set
            {
                _publishButtonText = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object data)
        {
            _todaysBattle = await _battleDataService.GetTodaysBattle();
            if (_todaysBattle != null)
            {
                StartButtonText = $"Wystartuj bitwę w stylu {_todaysBattle.Style}";
                PublishButtonText = PublishBatteResultsText;
            }
            else
            {
                StartButtonText = "Nie ma dzisiaj zaplanowanej bitwy";
                BattleStartAllowed = false;
                BattleEndAllowed = false;
                PublishResultsAllowed = false;
                return;
            }

            var runningBattle = await _battleDataService.GetCurrentRunningBattle();
            if (runningBattle != null)
            {
                BattleStartAllowed = false;
                BattleEndAllowed = true;
            }
            else
            {
                BattleStartAllowed = true;
                BattleEndAllowed = false;
            }
        }

        private async void OnStartBattle()
        {
            var battlePlace = await _geolocationService.GetBattlePlace();
            var runningBattle = await _adminPanelDataService.StartBattle(battlePlace);
            if (runningBattle != null)
            {
                BattleStartAllowed = false;
                BattleEndAllowed = true;
            }
            else
            {
                await _dialogService.ShowDialog("Nie udało się wystartować bitwy!", "Bitwa", "OK");
            }
        }
        private async void OnEndBattle()
        {
            var endedBattle = await _adminPanelDataService.EndBattle(_todaysBattle);
            if (endedBattle != null)
            {
                BattleStartAllowed = true;
                BattleEndAllowed = false;
                PublishResultsAllowed = true;
            }
            else
            {
                await _dialogService.ShowDialog("Nie udało się zakończyć bitwy!", "Bitwa", "OK");
            }
        }
        private async void OnPublishResult(object obj)
        {
            await PublishResults();

            if (! await _adminPanelDataService.IsWinnersAlreadyPromotedToNextRound(_todaysBattle))
            {
                await _adminPanelDataService.PromoteWinnersToFollowingBattles(_todaysBattle);
            }
        }

        private async Task PublishResults()
        {
            var todaysBattle = await _battleDataService.GetTodaysBattle();
            if (todaysBattle != null)
            {
                if (!todaysBattle.ResultsPublished)
                    await TogglePublishBattleResults(true);
                else
                    await TogglePublishBattleResults(false);
            }
        }

        private async Task TogglePublishBattleResults(bool publishResults)
        {
            Battle battleWithResultsPublishStateChanged;

            if (publishResults)
                battleWithResultsPublishStateChanged = await _adminPanelDataService.PublishResults(_todaysBattle);
            else
                battleWithResultsPublishStateChanged = await _adminPanelDataService.HideResults(_todaysBattle);

            if (battleWithResultsPublishStateChanged != null)
            {
                BattleStartAllowed = false;
                BattleEndAllowed = false;

                if (publishResults)
                    PublishButtonText = HideBattleResultsText;
                else
                    PublishButtonText = PublishBatteResultsText;
            }
            else
            {
                if (publishResults)
                    await _dialogService.ShowDialog("Nie udało się ogłosić wyników bitwy!", "Bitwa", "OK");
                else
                    await _dialogService.ShowDialog("Nie udało się ukryć wyników bitwy!", "Bitwa", "OK");
            }
        }
    }
}
