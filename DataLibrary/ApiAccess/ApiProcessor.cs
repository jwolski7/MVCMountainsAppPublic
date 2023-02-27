using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.ApiAccess
{
    public class ApiProcessor
    {
        public static string structureURL()
        {
            // Api data retrieval information goes here.

            string final = "";
            
            return final;
        }
        public WeatherApiModel getData() 
        {
            var weatherApiData = Task<WeatherApiModel>.Run(async () =>
            {
                WeatherApiModel apiData = new WeatherApiModel();
                string path = structureURL();
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(path),
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    apiData = (WeatherApiModel)JsonConvert.DeserializeObject(body, typeof(WeatherApiModel));
                }

                return apiData;
            });
            
            return weatherApiData.Result; 
        } 
    }
}
