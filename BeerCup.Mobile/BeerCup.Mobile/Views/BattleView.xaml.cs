﻿using BeerCup.Mobile.Bootstrap;
using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeerCup.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BattleView : ContentPage
    {
        public BattleView()
        {
            InitializeComponent();
            //todo: wywalić poniższe
            var navigationService = AppContainer.Resolve<INavigationService>();
            var votingDataService = AppContainer.Resolve<IVotingDataService>();
            BindingContext = new BattleViewModel(votingDataService, navigationService);
        }
    }
}