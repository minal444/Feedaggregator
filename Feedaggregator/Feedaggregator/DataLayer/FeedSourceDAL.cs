using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Feedaggregator.EntityLayer;
using System.Data.SqlClient;
using System.Data;
namespace Feedaggregator.DataLayer
{
    class FeedSourceDAL
    {

        /// <summary>Returns a list with ContactPerson objects.</summary>
        /// <returns>A ContactPersonCollection with the ContactPerson objects.</returns>
        public static List<FeedSourceEntity> GetList()
        {
            try
            {
                List<FeedSourceEntity> tempList = new List<FeedSourceEntity>();

                using (SqlDataReader readerPatient = SqlHelper.ExecuteReader(Feedaggregator.Common.BGCon, CommandType.StoredProcedure, "Sp_GetFeedSource"))
                {
                    if (readerPatient.HasRows)
                    {
                        tempList = new List<FeedSourceEntity>();
                        while (readerPatient.Read())
                        {
                            tempList.Add(FillDataRecord(readerPatient));
                        }
                    }
                    readerPatient.Close();
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
        private static FeedSourceEntity FillDataRecord(IDataRecord dataRecordLogin)
        {
            try
            {
                var feedSourceEntity = new FeedSourceEntity()
                {
                    SourceName= dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("SourceName")),
                    SourceURL = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("SourceURL")),
                    Id = dataRecordLogin.GetInt32(dataRecordLogin.GetOrdinal("Id")),
                    SiteURL = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("SiteURL")),
                    ImageSource = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("ImageSource")),
                    ImageNameSpace = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("ImageNameSpace")),
                    DescriptionNameSpace = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("DescriptionNameSpace")),
                    Timezone = dataRecordLogin.GetString(dataRecordLogin.GetOrdinal("Timezone"))
                };

                return feedSourceEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
