using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace NotificationCenter
{
    public static class Common
    {
        public static string BGCon = Convert.ToString(ConfigurationManager.ConnectionStrings["BollyWoodGossipCon"]);
        public static string GoogleAPIKey = Convert.ToString(ConfigurationManager.AppSettings["GoogleAPIKey"]);
    }
}
