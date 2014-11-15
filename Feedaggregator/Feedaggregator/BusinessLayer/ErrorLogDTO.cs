using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feedaggregator.BusinessLayer
{
    class ErrorLogDTO
    {
        public static void Insert()
        {
            try
            {
                //FeedItemDAL.Insert(ref entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
