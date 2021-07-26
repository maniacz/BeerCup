using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly string baseUrl;
        private readonly string apiKey;
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        public WeatherConnector(IConfiguration configuration, ILogger<WeatherConnector> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.baseUrl = this.configuration.GetSection("OpenWeather").GetValue("BaseUrl", "");
            this.apiKey = this.configuration.GetSection("OpenWeather").GetValue("ApiKey", "");
            this.restClient = new RestClient(baseUrl);
        }

        public async Task<Weather> Fetch(string city)
        {
            var request = new RestRequest("data/2.5/weather", Method.GET);
            
            request.AddParameter("appid", apiKey);
            request.AddParameter("q", city);

            var queryResult = await restClient.ExecuteAsync(request);
            this.logger.LogInformation("Fetch weather request returned: {0}", queryResult.StatusCode);
            if (!queryResult.IsSuccessful)
            {
                this.logger.LogError("Exception in: ", queryResult.ErrorException);
            }

            var weather = JsonConvert.DeserializeObject<Weather>(queryResult.Content);
            logger.LogInformation("Current weather in {0}: Humidity = {1}", weather.Name, weather.Main.Humidity);
            return weather;
        }
    }
}
