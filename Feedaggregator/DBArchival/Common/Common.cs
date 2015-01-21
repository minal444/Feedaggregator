using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBArchival
{
    public static class Common
    {
        public static string AuditCon = Convert.ToString(ConfigurationManager.ConnectionStrings["AuditDBConnection"]);
        public static string directoryPath = ConfigurationManager.AppSettings["SearchIndexDirectoryPath"];
    }
}
