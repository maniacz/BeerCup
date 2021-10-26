﻿using AutoMapper;
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
        private readonly IAdminPanelDataService _adminPanelDataService;
        private readonly IBattleDataService _battleDataService;
        private readonly IGeolocationService _geolocationService;
        private bool _battleStartAllowed;
        private bool _battleEndAllowed;
        private bool _publishResultsAllowed;
        private Battle _runningBattle;
        private string _startButtonText;

        public AdminPanelViewModel(INavigationService navigationService, IAdminPanelDataService adminPanelDataService, IBattleDataService battleDataService, IGeolocationService geolocationService)
            : base(navigationService)
        {
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

        public override async Task InitializeAsync(object data)
        {
            var todaysBattle = await _battleDataService.GetTodaysBattle();
            if (todaysBattle != null)
            {
                StartButtonText = $"Wystartuj bitwę w stylu {todaysBattle.Style}";
            }
            else
            {
                StartButtonText = "Nie ma dzisiaj zaplanowanej bitwy";
                BattleStartAllowed = false;
                BattleEndAllowed = false;
                PublishResultsAllowed = false;
                return;
            }

            _runningBattle = await _battleDataService.GetCurrentRunningBattle();
            if (_runningBattle != null)
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
                PublishResultsAllowed = true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Bitwa", "Nie udało się zakończyć bitwy!", "OK");
            }
        }
        private async void OnPublishResult(object obj)
        {
            var battlePublished = await _adminPanelDataService.PublishResults(_runningBattle);
            if (battlePublished != null)
            {
                BattleStartAllowed = false;
                BattleEndAllowed = false;
                PublishResultsAllowed = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Bitwa", "Nie udało się ogłosić wyników bitwy!", "OK");
            }
        }
    }
}
