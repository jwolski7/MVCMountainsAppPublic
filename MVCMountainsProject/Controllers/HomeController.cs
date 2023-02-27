using Microsoft.AspNetCore.Mvc;
using MVCMountainsProject.Models;
using System.Diagnostics;
using DataLibrary.BusinessLogic;
using DataLibrary.ApiAccess;


namespace MVCMountainsProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
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

        [HttpGet]
        public async Task<ActionResult<List<WeatherModel>>> GetAllWeather()
        {
            var weather = WeatherProcessor.LoadWeather(_config);

            return Ok(weather);
        }
    }
}