using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVIMPH.Shared.EntityLayer;
using System.Data;
using System.Data.SqlClient;

namespace SVIMPH.Shared.DataLayer
{
    public class ErrorLogDAL
    {
        public static void Insert(ErrorLogEntity entity)
        {
            try
            {
                //Patient_Id, 
                SqlParameter paramErrorDescription = new SqlParameter("@ErrorDescription", SqlDbType.VarChar);
                paramErrorDescription.Direction = ParameterDirection.Input;
                paramErrorDescription.Value = entity.ErrorDescription;

                SqlParameter paramException = new SqlParameter("@Exception", SqlDbType.NVarChar);
                paramException.Direction = ParameterDirection.Input;
                paramException.Value = entity.Exception;

                SqlParameter paramStacktrace = new SqlParameter("@Stacktrace", SqlDbType.NVarChar);
                paramStacktrace.Direction = ParameterDirection.Input;
                paramStacktrace.Value = entity.Stacktrace;

                SqlParameter paramErrorMetadata = new SqlParameter("@ErrorMetadata", SqlDbType.NVarChar);
                paramErrorMetadata.Direction = ParameterDirection.Input;
                paramErrorMetadata.Value = entity.ErrorMetadata;

                SqlParameter paramErrorSource = new SqlParameter("@ErrorSource", SqlDbType.NVarChar);
                paramErrorSource.Direction = ParameterDirection.Input;
                paramErrorSource.Value = entity.ErrorSource;

                SqlHelper.ExecuteNonQuery(SVIMPH.Shared.Common.Common.ConnectionString, CommandType.StoredProcedure, "Sp_InsertErrorLog", paramErrorDescription, paramErrorMetadata, paramErrorSource, paramException, paramStacktrace);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
