using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Constants
{
    public static class ApiConstants
    {
        public const string BaseApiUrl = "http://10.0.2.2/";
        public const string AuthenticateEndpoint = "BeerCup.WebAPI/Users/authenticate";
        public const string BeersEndpoint = "BeerCup.WebAPI/Beers";
        public const string BattlesEndpoint = "BeerCup.WebAPI/Battles";
        public const string BeerFromBattleEndpoint = "BeerCup.WebAPI/Beers/frombattle";
        public const string UserBattleVotesEndpoint = "BeerCup.WebAPI/Battles/UserVotes";
    }
}
