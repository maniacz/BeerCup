using Newtonsoft.Json;

namespace BeerCup.ApplicationServices.Components.OpenWeather
{
    public class Weather
    {
        public string Name { get; set; }

        [JsonProperty("main")]
        public MainData Main { get; set; }
    }
}