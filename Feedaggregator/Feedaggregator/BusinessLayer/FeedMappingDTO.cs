using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
////using System.Threading.Tasks;
using System.ComponentModel;
using Feedaggregator.DataLayer;
using Feedaggregator.EntityLayer;
namespace Feedaggregator.BusinessLayer
{
    class FeedMappingDTO
    {
        /// <summary>
        /// Get List of Patient
        /// </summary>
        /// <param name="SearchCriteria"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<FeedMappingEntity> GetList(int feedSourceId)
        {
            try
            {
                List<FeedMappingEntity> myCollection = FeedMappingDAL.GetList(feedSourceId);

                return myCollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
