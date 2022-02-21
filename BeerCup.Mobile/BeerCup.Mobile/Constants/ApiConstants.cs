namespace BeerCup.Mobile.Constants
{
    public static class ApiConstants
    {
        public const string BaseApiUrl = "http://10.0.2.2/";
        public const string AuthenticateEndpoint = "BeerCup.WebAPI/Users/authenticate";
        public const string RegisterEndpoint = "BeerCup.WebAPI/Users/register";
        public const string BeersEndpoint = "BeerCup.WebAPI/Beers";
        public const string BattlesEndpoint = "BeerCup.WebAPI/Battles";
        public const string BreweriesEndpoint = "BeerCup.WebAPI/Breweries";
        public const string BeerFromBattleEndpoint = "BeerCup.WebAPI/Beers/frombattle";
        public const string UserBattleVotesEndpoint = "BeerCup.WebAPI/Battles/{battleId}/{userId}";
        public const string AdminPanelEndpoint = "BeerCup.WebAPI/BattleHandling";
        public const string AwardDrawingEndpoint = "BeerCup.WebAPI/BattleHandling/AwardDrawing/";
        public const string AwardDrawingWithPaperVotesEndpoint = "BeerCup.WebAPI/BattleHandling/AwardDrawingForAll/";
    }
}
