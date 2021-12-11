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
    public class EditBattleViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IAdminPanelDataService _adminPanelDataService;
        private readonly IBattleDataService _battleDataService;
        private int _battleNo;
        private string _battleName;
        private string _battleStyle;
        private string _pubName;
        private DateTime _battleDate;

        public EditBattleViewModel(INavigationService navigationService, IDialogService dialogService, IAdminPanelDataService adminPanelDataService, IBattleDataService battleDataService) : base(navigationService)
        {
            _dialogService = dialogService;
            _adminPanelDataService = adminPanelDataService;
            _battleDataService = battleDataService;
        }

        public ICommand SaveEditBattleTappedCommand => new Command(OnSaveEditBattleTapped);

        public int BattleNo
        {
            get => _battleNo;
            set
            {
                _battleNo = value;
                OnPropertyChanged();
            }
        }

        public string BattleName
        {
            get => _battleName;
            set
            {
                _battleName = value;
                OnPropertyChanged();
            }
        }

        public string BattleStyle
        {
            get => _battleStyle;
            set
            {
                _battleStyle = value;
                OnPropertyChanged();
            }
        }
        public string PubName
        {
            get => _pubName;
            set
            {
                _pubName = value;
                OnPropertyChanged();
            }
        }

        public DateTime BattleDate
        {
            get => _battleDate;
            set
            {
                _battleDate = value;
                OnPropertyChanged();
            }
        }

        private async void OnSaveEditBattleTapped()
        {
            var battle = new Battle
            {
                BattleNo = this.BattleNo,
                BattleName = this.BattleName,
                Style = BattleStyle,
                PubName = this.PubName,
                Date = BattleDate
            };

            //todo: DEBUG
            //var battle = new Battle
            //{
            //    BattleNo = 17,
            //    BattleName = "Bitwa 17",
            //    Style = "Pastry Sour",
            //    PubName = "Absu",
            //    Date = DateTime.Today
            //};

            var editedBattle = await _adminPanelDataService.SaveEditBattle(battle);

            if (editedBattle != null)
                await _navigationService.NavigateToAsync<ManageBattlesViewModel>();
            else
                await _dialogService.ShowDialog("Nie udało się dodać bitwy!", "Dodawanie bitwy", "OK");
        }

        public override async Task InitializeAsync(object data)
        {
            var selectedBattle = (Battle)data;
            var battle = await _battleDataService.GetBattleByBattleNo(selectedBattle.BattleNo);
            BattleNo = battle.BattleNo;
            BattleName = battle.BattleName;
            BattleStyle = battle.Style;
            PubName = battle.PubName;
            BattleDate = battle.Date.Value;
        }
    }
}
