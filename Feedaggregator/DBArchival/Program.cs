using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SVIMPH.Shared.DataLayer;
using System.Data;

namespace DBArchival
{
    class Program
    {
         
        static void Main(string[] args)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Common.AuditCon, CommandType.StoredProcedure, "SP_SyncDB");
            }
            catch (Exception ex)
            {
                //throw;
            }
            
        }
    }
}
