using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BeerCup.Mobile.ViewModels
{
    public class AdminPanelViewModel : ViewModelBase
    {
        private readonly IAdminPanelDataService _adminPanelDataService;

        private bool _battleStartAllowed;

        public AdminPanelViewModel(INavigationService navigationService, IAdminPanelDataService adminPanelDataService) : base(navigationService)
        {
            _adminPanelDataService = adminPanelDataService;
            BattleStartAllowed = true;
        }

        public ICommand StartBattleCommand => new Command(OnStartBattle);

        public bool BattleStartAllowed 
        {
            get => _battleStartAllowed;
            set
            {
                _battleStartAllowed = value;
                OnPropertyChanged();
            }
        }

        private void OnStartBattle()
        {
            var startedBattle = _adminPanelDataService.StartBattle();
            if (startedBattle != null)
            {
                BattleStartAllowed = false;
            }
        }
    }
}
