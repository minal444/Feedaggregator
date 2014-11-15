using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace SVIMPH.Shared.Common
{
    class Common
    {
        public static string ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["ErrorLogCon"]);
    }
}
