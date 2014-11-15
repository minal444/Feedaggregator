using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Feedaggregator.DataLayer;
using Feedaggregator.EntityLayer;
namespace Feedaggregator.BusinessLayer
{
    public class FeedSourceDTO
    {
        /// <summary>
        /// Get List of Patient
        /// </summary>
        /// <param name="SearchCriteria"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<FeedSourceEntity> GetList()
        {
            try
            {
                List<FeedSourceEntity> myCollection = FeedSourceDAL.GetList();

                return myCollection;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
