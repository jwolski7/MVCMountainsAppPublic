using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAccess
    {
        public static IEnumerable<WeatherModel> LoadData<WeatherModel>(IConfiguration _config, string sql)
        {
            using IDbConnection connection = new SqlConnection(ConnectionStringHelper.CnnVal(_config));
            {
                var weather = connection.Query<WeatherModel>(sql);
                return weather;
            }
        }

        public static int SaveData<WeatherModel>(IConfiguration _config, string sql, WeatherModel data) 
        {
            using IDbConnection connection = new SqlConnection(ConnectionStringHelper.CnnVal(_config));
            {  
                return connection.Execute(sql, data);
            }
        }

        public static List<T> GetListData<T>(IConfiguration _config, string sql)
        {
            using IDbConnection connection = new SqlConnection(ConnectionStringHelper.CnnVal(_config));
            {
                var weather = connection.Query<T>(sql);
                
                return weather.ToList();
            }
        }
    }
}
