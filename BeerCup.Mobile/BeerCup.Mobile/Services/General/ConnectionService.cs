using BeerCup.Mobile.Contracts.Services.General;
using Xamarin.Essentials;

namespace BeerCup.Mobile.Services.General
{
    public class ConnectionService : IConnectionService
    {
        public bool IsConnected()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                return true;
            else
                return false;
        }
    }
}
