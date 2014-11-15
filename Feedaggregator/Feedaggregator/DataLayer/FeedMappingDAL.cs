using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
////using System.Threading.Tasks;
using Feedaggregator.EntityLayer;
using System.Data.SqlClient;
using System.Data;

namespace Feedaggregator.DataLayer
{
    public class FeedMappingDAL
    {

        /// <summary>Returns a list with ContactPerson objects.</summary>
        /// <returns>A ContactPersonCollection with the ContactPerson objects.</returns>
        public static List<FeedMappingEntity> GetList(int feedSourceId)
        {
            try
            {
                List<FeedMappingEntity> tempList = new List<FeedMappingEntity>();
                SqlParameter paramFeedSourceId;
                paramFeedSourceId = new SqlParameter("@FeedSourceId", feedSourceId);

                using (SqlDataReader readerFeedMapping = SqlHelper.ExecuteReader(Feedaggregator.Common.BGCon, CommandType.StoredProcedure, "Sp_GetFeedMappings", paramFeedSourceId))
                {
                    if (readerFeedMapping.HasRows)
                    {
                        tempList = new List<FeedMappingEntity>();
                        while (readerFeedMapping.Read())
                        {
                            tempList.Add(FillDataRecord(readerFeedMapping));
                        }
                    }
                    readerFeedMapping.Close();
                }


                return tempList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static FeedMappingEntity FillDataRecord(IDataRecord dataRecordLogin)
        {
            try
            {
                var feedMappingEntity = new FeedMappingEntity()
                {
                    SourceColumnId = dataRecordLogin.GetInt32(dataRecordLogin.GetOrdinal("SourceColumnId")),
                    SourceColumnName = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("SourceColumnName")),
                    DestinationColumnId = dataRecordLogin.GetInt32(dataRecordLogin.GetOrdinal("DestinationColumnId")),
                    DestinationColumnName = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("DestinationColumnName")),
                   
                    FeedSourceId = dataRecordLogin.GetInt32(dataRecordLogin.GetOrdinal("FeedSourceId")),
                    FeedSourceName = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("FeedSourceName")),
                };

                return feedMappingEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SetFeedMapping(long FeedID, long SearchedFeedID)
        {
            try
            {
                SqlParameter paramFeedId,paramSearchedFeedID;
                paramFeedId = new SqlParameter("@FeedID", FeedID);
                paramSearchedFeedID = new SqlParameter("@SearchedFeedID", SearchedFeedID);
                SqlParameter[] arrParam = {paramFeedId,paramSearchedFeedID};
                SqlHelper.ExecuteNonQuery(Common.BGCon, CommandType.StoredProcedure, "Sp_SetFeedMapping", arrParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
