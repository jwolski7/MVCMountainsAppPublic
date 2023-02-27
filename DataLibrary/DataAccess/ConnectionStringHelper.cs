using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLibrary.DataAccess
{
    public class ConnectionStringHelper
    {
        // This does get updated automatically.
        public static string CnnVal(IConfiguration _config)
        {
            return _config.GetConnectionString("Name of the database goes in here.");
        }
    }
}
