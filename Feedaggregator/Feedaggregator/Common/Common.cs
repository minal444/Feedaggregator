using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Feedaggregator
{
    public static class Common
    {
        public static string BGCon = Convert.ToString(ConfigurationManager.ConnectionStrings["BollyWoodGossipCon"]);
        public static string GenerateSearchIndex = Convert.ToString(ConfigurationManager.AppSettings["GenerateSearchIndex"]);
        public static string DefaultTimezone = Convert.ToString(ConfigurationManager.AppSettings["DefaultTimezone"]);
        public static string SaveXML = Convert.ToString(ConfigurationManager.AppSettings["SaveXML"]);
        
    }
}
