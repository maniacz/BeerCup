using AutoMapper;
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
        private readonly IGeolocationService _geolocationService;
        private readonly IMapper _mapper;
        private bool _battleStartAllowed;

        public AdminPanelViewModel(INavigationService navigationService, IAdminPanelDataService adminPanelDataService, IGeolocationService geolocationService, IMapper mapper) 
            : base(navigationService)
        {
            _adminPanelDataService = adminPanelDataService;
            _geolocationService = geolocationService;
            _mapper = mapper;
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

        private async void OnStartBattle()
        {
            var battlePlace = await _geolocationService.GetBattlePlace();
            var startedBattle = await _adminPanelDataService.StartBattle(battlePlace);
            if (startedBattle != null)
            {
                BattleStartAllowed = false;
            }
        }
    }
}
