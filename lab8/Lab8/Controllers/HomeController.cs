using Lab8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lab8.Controllers
{
    public class HomeController : Controller
    {
        List<Country> countries = new List<Country>();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(string firstCountry, string secondCountry, int value)
        {
            await LoadJson();
            float firstDev = 0, secondDev = 0;
            string firstCurr = "", secondCurr = "";
            foreach (var item in countries)
            {
                if (item.name == firstCountry)
                {
                    firstDev = item.devize;
                    firstCurr = item.currency;
                }
                else if (item.name == secondCountry)
                {
                    secondDev = item.devize;
                    secondCurr = item.currency;
                }
            }
            
            double total = (firstDev / secondDev) * value;

            string data = ($"{value} {firstCurr} equals to {total.ToString("0.##")} {secondCurr}");
            countries.Add(new Country { name = data });
            return View(countries);
            //return Content($"{value} {firstCurr} equals to {total.ToString("0.##")} {secondCurr}");
        }
        public async Task LoadJson()
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync("https://api.hnb.hr/tecajn/v1");

            countries = JsonConvert.DeserializeObject<List<Country>>(response);
        }

        public async Task<IActionResult> IndexAsync()
        {
            await LoadJson();
            return View(countries);
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
