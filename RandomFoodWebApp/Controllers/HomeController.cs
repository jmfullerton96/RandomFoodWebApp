using Microsoft.AspNetCore.Mvc;
using RandomFoodWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
//using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace RandomFoodWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetRestaraunt(int radius)
        {
            var restaurant = FetchRestaraunt(radius);

            return View(restaurant.Result);
        }

        static async Task<Restaurant> FetchRestaraunt(int distanceRadius, CancellationToken ct = default(CancellationToken))
        {
            int meters = distanceRadius * 1609; // max = 40000 - about 25 miles per yelp api documentation
            Restaurant restaurant = new Restaurant();

            try
            {
                Random rand = new Random();
                int offset = rand.Next(0, GetNumberOfRestaurants(meters).Result); //returns random number between 0 - Total Number of restaurants

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "qiBVhYijqdobDDIjY7KIhk0wDSkgkDzO7u8oSb8jkLLpnGWZYokUJmXzS1zR_GeeueSW5Q7QgkX_40ayO3APTTETQ3O9UAAyBLmJxNJq6EdNaLO0GXkwM5kmPpO3YnYx");
                    var uri = String.Format("https://api.yelp.com/v3/businesses/search?categories=restaurants&location=64056&open_now=true&radius={0}&limit=1&offset={1}", meters, offset);
                    using (var response = await client.GetAsync(uri)) // See this page for API Examples: https://spectralops.io/blog/yelp-api-guide/
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var readResponse = await response.Content.ReadAsStringAsync();
                            var jObj = JObject.Parse(readResponse);
                            var businesses = jObj.GetValue("businesses");
                            restaurant = JsonConvert.DeserializeObject<IEnumerable<Restaurant>>(businesses.ToString()).First();

                            Console.WriteLine("Some val");
                        }
                        else
                        {
                            string errorResponse = await response.Content.ReadAsStringAsync();
                            var errorJson = JsonConvert.DeserializeObject(errorResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return restaurant;
        }

        static async Task<int> GetNumberOfRestaurants(int radiusMeters)
        {
            int totalRestaurants = 0;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "qiBVhYijqdobDDIjY7KIhk0wDSkgkDzO7u8oSb8jkLLpnGWZYokUJmXzS1zR_GeeueSW5Q7QgkX_40ayO3APTTETQ3O9UAAyBLmJxNJq6EdNaLO0GXkwM5kmPpO3YnYx");

                    using (var response = await client.GetAsync(String.Format("https://api.yelp.com/v3/businesses/search?categories=restaurants&location=64056&open_now=true&radius={0}", radiusMeters))) // See this page for API Examples: https://spectralops.io/blog/yelp-api-guide/
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var readResponse = await response.Content.ReadAsStringAsync();
                            var jObj = JObject.Parse(readResponse);
                            totalRestaurants = (int)jObj.GetValue("total");

                            if (totalRestaurants < 1)
                            {
                                // TODO add some kind of error handling, will cause an error creating the offset if this method returns 0
                                // Maybe automatically bump the radius?
                            }
                        }
                        else
                        {
                            string errorResponse = await response.Content.ReadAsStringAsync();
                            var errorJson = JsonConvert.DeserializeObject(errorResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return totalRestaurants;
        }
    }
}
