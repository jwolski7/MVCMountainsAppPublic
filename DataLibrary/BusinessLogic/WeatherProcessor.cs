using DataLibrary.DataAccess;
using DataLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class WeatherProcessor
    {
        public static int CreateWeather(IConfiguration _config, string forecast, double snowfall, string date)
        {
            WeatherModel data = new WeatherModel
            {
                Forecast = forecast,
                Snowfall = snowfall,
                Date = date
            };

            string sql = @"INSERT INTO JasperWeather (Snowfall, Forecast, Date)
                            values (@Snowfall, @Forecast, @Date);";

            return SqlDataAccess.SaveData(_config, sql, data);
        }

        public static IEnumerable<WeatherModel> LoadWeather(IConfiguration _config)
        {
            string sql = @"SELECT * FROM JasperWeather ORDER BY Date;";


            return SqlDataAccess.LoadData<WeatherModel>(_config, sql);
        }

        public static List<Object> ChartDataList(IConfiguration _config)
        {
            string sql = @"SELECT ID, Snowfall, Forecast, Date FROM JasperWeather ORDER BY Date;";

            List<Object> data = new List<Object>();
            data.Add(new object[]
            {
                "Month", "Date", "Snow(mm)", "Year"
            });

            List<WeatherModel> list = SqlDataAccess.GetListData<WeatherModel>(_config, sql);
            double totalSnowfall = 0;
            int weekCounter = 0, count = 0, dif = 0;
            string dayHolder = "";

            foreach (var row in list)
            {
                totalSnowfall += row.Snowfall;
                count++;
                if (count == 1)
                {
                    dayHolder = row.Date.Substring(8, 2);
                    dif = DateTime.DaysInMonth(Int32.Parse(row.Date.Substring(0, 4)), 
                                                Int32.Parse(row.Date.Substring(5, 2))) % 7; 
                }
                else if (count == 7 & weekCounter != 3 )
                {
                    data.Add(new object[]
                    {
                        GetMonth(row.Date.Substring(5, 2)),
                        GetMonth(row.Date.Substring(5, 2)) + ". " + dayHolder + "-" + row.Date.Substring(8,2), 
                        totalSnowfall, 
                        row.Date.Substring(0, 4)
                    });
                    weekCounter++;
                    count= 0;
                    totalSnowfall = 0;
                }
                else if (count == (7 + dif))
                {
                    data.Add(new object[]
                    {
                        GetMonth(row.Date.Substring(5, 2)),
                        GetMonth(row.Date.Substring(5, 2)) + ". " + dayHolder + "-" + row.Date.Substring(8,2), 
                        totalSnowfall, 
                        row.Date.Substring(0, 4)
                    });
                    count = 0;
                    totalSnowfall = 0;
                    dif = 0;
                    weekCounter = 0;
                }
            }
            return data;
        }

        public static int CheckDay(IConfiguration _config)
        {
            string today = DateTime.Today.ToString("yyyy-MM-dd");
            string sql = @"SELECT * FROM JasperWeather WHERE Date = '" + today + "';";

            List<WeatherModel> list = SqlDataAccess.GetListData<WeatherModel>(_config, sql);
            
            return list.Count;
        }

        public static string GetMonth(string month)
        {
            Dictionary<string, string> monthList = new Dictionary<string, string>{ 
                                                   {"01", "Jan" }, {"02","Feb" }, 
                                                   {"03" , "Mar"}, {"04" , "Apr"},
                                                   {"05" , "May"}, {"11" , "Nov"}, 
                                                   {"12" , "Dec"} };
            return monthList[month];
        }
    }
}
