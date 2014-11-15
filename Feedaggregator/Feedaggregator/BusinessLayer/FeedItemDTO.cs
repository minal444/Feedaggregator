using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
////using System.Threading.Tasks;
using Feedaggregator.DataLayer;
using Feedaggregator.EntityLayer;
using System.ComponentModel;
namespace Feedaggregator.BusinessLayer
{
    public class FeedItemDTO
    {
        /// <summary>
        /// Get List of Patient
        /// </summary>
        /// <param name="SearchCriteria"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static bool IsUpdatedRequired(DateTime publishDate)
        {
            bool IsUpdateRequired = FeedItemDAL.IsUpdatedRequired(publishDate);

            return IsUpdateRequired;
        }

        public static void Insert(ref FeedItemEntity entity)
        {
            try
            {
                FeedItemDAL.Insert(ref entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
