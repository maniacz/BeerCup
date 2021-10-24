using AutoMapper;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
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
        private bool _battleEndAllowed;
        private Battle _runningBattle;

        public AdminPanelViewModel(INavigationService navigationService, IAdminPanelDataService adminPanelDataService, IGeolocationService geolocationService, IMapper mapper) 
            : base(navigationService)
        {
            _adminPanelDataService = adminPanelDataService;
            _geolocationService = geolocationService;
            _mapper = mapper;
            BattleStartAllowed = true;
            BattleEndAllowed = false;
        }

        public ICommand StartBattleCommand => new Command(OnStartBattle);
        public ICommand EndBattleCommand => new Command(OnEndBattle);


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

        private async void OnStartBattle()
        {
            var battlePlace = await _geolocationService.GetBattlePlace();
            _runningBattle = await _adminPanelDataService.StartBattle(battlePlace);
            if (_runningBattle != null)
            {
                BattleStartAllowed = false;
                BattleEndAllowed = true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Bitwa", "Nie udało się wystartować bitwy!", "OK");
            }
        }

        private async void OnEndBattle()
        {
            var endedBattle = await _adminPanelDataService.EndBattle(_runningBattle);
            if (endedBattle != null)
            {
                BattleStartAllowed = true;
                BattleEndAllowed = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Bitwa", "Nie udało się zakończyć bitwy!", "OK");
            }
        }
    }
}
