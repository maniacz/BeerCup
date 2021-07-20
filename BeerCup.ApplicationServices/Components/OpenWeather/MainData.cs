using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.ApplicationServices.Components.OpenWeather
{
    public class MainData
    {
        [JsonProperty("humidity")]
        public float Humidity { get; set; }
    }
}
