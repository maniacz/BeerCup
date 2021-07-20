using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.Components.OpenWeather
{
    public class WeatherConnector : IWeatherConnector
    {
        private readonly RestClient restClient;
        private readonly string baseUrl = "http://api.openweathermap.org/";
        private readonly string apiKey = "454ce3ea3a7d8598716092669a3f3db6";

        public WeatherConnector()
        {
            this.restClient = new RestClient(baseUrl);
        }

        public async Task<Weather> Fetch(string city)
        {
            var request = new RestRequest("data/2.5/weather", Method.GET);
            request.AddParameter("appid", this.apiKey);
            request.AddParameter("q", city);

            var queryResult = await restClient.ExecuteAsync(request);
            var weather = JsonConvert.DeserializeObject<Weather>(queryResult.Content);
            return weather;
        }
    }
}
