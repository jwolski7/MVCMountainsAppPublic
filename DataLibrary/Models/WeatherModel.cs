using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class WeatherModel
    {
        public int ID { get; set; }
        public string? Forecast { get; set; }
        public double Snowfall { get; set; }
        public string? Date { get; set; }
    }
}
