using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomPass.Models;

namespace RandomPass.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            //Create a string with all numbers and letters
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            //creat a array with 14 letters
            var stringChars = new char[14];
            Random random = new Random();
            //loop over the 14 chars
            for (int i = 0; i < stringChars.Length; i++)
            {
                //put a random value inside
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            //if value null turn to 0 
            if(HttpContext.Session.GetInt32("count") == null)
            {
                HttpContext.Session.SetInt32("count", 0);
            }

            int count = (int)HttpContext.Session.GetInt32("count");
            
            if (count == 0)
            {   
                count++;
                HttpContext.Session.SetInt32("count", count);
            }

            RandomPasscode passcode = new RandomPasscode()
            {
                Passcode = new string(stringChars),
                Count = (int)count
            };


            return View("Index", passcode);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
