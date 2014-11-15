using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVIMPH.Shared.DataLayer;
using SVIMPH.Shared.EntityLayer;
namespace SVIMPH.Shared.BusinessLayer
{
    public  class ErrorLogDTO
    {
        public static void Insert(ErrorLogEntity entity)
        {
            try
            {
                ErrorLogDAL.Insert(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
