using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RandomFoodWebApp.Models
{
    public class SearchParameters
    {
        // Amount of distance willing to travel - radius
        [Required(ErrorMessage = "Please enter the distance you're willing to travel")]
        [Display(Name = "Radius willing to travel")]
        public int DistanceRadius { get; set; }

        // Use zipcode if location is not on
        public int ZipCode { get; set; }

        public void OnPost()
        {
            // See here for retrieving data from the model https://www.learnrazorpages.com/razor-pages/model-binding
            //var dist = Request.Form["DistanceRadius"];
            Console.WriteLine(DistanceRadius);
            //ViewData["confirmation"] = $"{DistanceRadius}, is the distance";
        }
    }
}