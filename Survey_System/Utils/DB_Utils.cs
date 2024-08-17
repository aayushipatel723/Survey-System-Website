using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Survey_System.Utils
{
    public class DB_Utils
    {
        private static string connectionStr = ConfigurationManager.ConnectionStrings["SurveyConString"].ConnectionString;
        public static string getConnectionString()
        {
            return connectionStr;
        }
    }
}