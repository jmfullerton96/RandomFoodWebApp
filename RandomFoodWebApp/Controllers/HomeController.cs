using Microsoft.AspNetCore.Mvc;
using RandomFoodWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Http;
using System.Web.MVC;
using System.Threading;
using System.Threading.Tasks;

namespace RandomFoodWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(int radius) // I get the radius from the form here
        {
            //Console.WriteLine(radius);
            var restaraunt = GetRestaraunt(radius);

            //return View(restaraunt);
            return View();
        }

        static async Task<Restaraunt> GetRestaraunt(int distanceRadius, CancellationToken ct = default(CancellationToken))
        {
            HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue { MediaType = })
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "sDDOtdTz9T69XOQZpPUYMZwjd2oMP-wWhru7EwE10XZ-pdNL5AQpyKyDYgU9L0TnHVB-5zfmhXEVCXH5_xK5V4HIrng4f3o8tnFNYzXX113_E1QxAKQd7e39sHeJYXYx");
            //client.DefaultRequestHeaders.Add("Authorization: Bearer", "sDDOtdTz9T69XOQZpPUYMZwjd2oMP-wWhru7EwE10XZ-pdNL5AQpyKyDYgU9L0TnHVB-5zfmhXEVCXH5_xK5V4HIrng4f3o8tnFNYzXX113_E1QxAKQd7e39sHeJYXYx"); // Can add to headers like this
            var response = await client.GetAsync("https://api.yelp.com/v3/businesses/search", ct);
            
            if (response.IsSuccessStatusCode)
            {
                // TODO
            }

            Restaraunt restaraunt = new Restaraunt
            {
                // TODO populate w/ data from API Call
            };

            return restaraunt;
        }
    }
}
