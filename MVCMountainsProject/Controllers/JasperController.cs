using DataLibrary.ApiAccess;
using DataLibrary.BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCMountainsProject.Models;

namespace MVCMountainsProject.Controllers
{
    public class JasperController : Controller
    {

        private readonly IConfiguration _config;

        public JasperController(IConfiguration config)
        {
            _config = config;
        }

        // GET: JasperController
        public ActionResult Index()
        {
            int test = WeatherProcessor.CheckDay(_config);

            if (test == 0)
                ApiHandler();

            var data = WeatherProcessor.LoadWeather(_config);
            List<WeatherModel> weather = new List<WeatherModel>();

            foreach (var row in data.ToList())
            {
                weather.Add(new WeatherModel
                {
                    Date = row.Date,
                    Snowfall = row.Snowfall,
                    Forecast = row.Forecast
                });
            }

            return View(weather);
        }

        public List<Object> GetChartJSON()
        {
            List<Object> chart = WeatherProcessor.ChartDataList(_config);

            return chart;
        }

        public void ApiHandler()
        {
            ApiProcessor api = new ApiProcessor();
            WeatherApiModel apiModel = api.getData();
            WeatherModel weatherModel = new WeatherModel();

            foreach (forecastday forc in apiModel.forecast.forecastday)
            {
                weatherModel.Date = forc.date;
                weatherModel.Forecast = forc.day.condition.text;
                weatherModel.Snowfall = forc.day.totalprecip_mm;
            }

            int recordsCreated = WeatherProcessor.CreateWeather(_config,
                weatherModel.Forecast,
                weatherModel.Snowfall,
                weatherModel.Date);
        }
    }
}
