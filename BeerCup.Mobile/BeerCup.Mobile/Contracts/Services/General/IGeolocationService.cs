using BeerCup.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BeerCup.Mobile.Contracts.Services.General
{
    public interface IGeolocationService
    {
        Task<BattlePlace> GetBattlePlace();

        Task<bool> IsUserOnBattlePlace(Battle runningBattle);
    }
}
