using BeerCup.Mobile.Contracts.Services.Data;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BeerCup.Mobile.Services.General
{
    public class GeolocationService : IGeolocationService
    {
        private const double maxAllowedDistanceFromBattleInKm = 0.1;
        private readonly IBattleDataService _battleDataService;

        public GeolocationService(IBattleDataService battleDataService)
        {
            _battleDataService = battleDataService;
        }

        public async Task<BattlePlace> GetBattlePlace()
        {
            Location location = await GetMyLocation();

            return new BattlePlace
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
        }

        public async Task<bool> IsUserOnBattlePlace(Battle runningBattle)
        {
            Location userLocation = await GetMyLocation();
            var battleLocation = new Location(runningBattle.Place.Latitude, runningBattle.Place.Longitude);

            var userDistanceFromBattlePlace = Location.CalculateDistance(battleLocation, userLocation, DistanceUnits.Kilometers);
            if (userDistanceFromBattlePlace < maxAllowedDistanceFromBattleInKm)
                return true;
            else
                return false;
        }

        private async Task<Location> GetMyLocation()
        {
            Location location;
            try
            {
                location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Problem with getting geolocation. {ex.Message}");
                throw;
            }

            return location;
        }
    }
}
