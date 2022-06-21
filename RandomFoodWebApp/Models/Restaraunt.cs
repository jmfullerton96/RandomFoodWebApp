using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFoodWebApp.Models
{
    public class Restaraunt
    {
        public Restaraunt()
        {

        }

        public string Name { get; set; }
        public string Address { get; set; }
        public string DistanceFromCurrentLocation { get; set; }
        public string DurationOfTravelFromCurrentLocation { get; set; }
    }
}
