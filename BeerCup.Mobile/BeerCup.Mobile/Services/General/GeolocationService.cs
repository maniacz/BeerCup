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
        public async Task<BattlePlace> GetBattlePlace()
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

            return new BattlePlace
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
        }
    }
}
