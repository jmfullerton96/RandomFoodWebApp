using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFoodWebApp.Models
{
    public class Restaurant
    {
        public Restaurant()
        {

        }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("location.display_address")]
        public string Address { get; set; }
        public string DistanceFromCurrentLocation { get; set; }
        public string DurationOfTravelFromCurrentLocation { get; set; }
    }
}
