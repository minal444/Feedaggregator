using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using Feedaggregator.EntityLayer;
namespace Feedaggregator.DataLayer
{
    class FeedItemDAL
    {
        /// <summary>Returns a list with ContactPerson objects.</summary>
        /// <returns>A ContactPersonCollection with the ContactPerson objects.</returns>
        public static bool IsUpdatedRequired(DateTime publishDate)
        {
            try
            {
                //List<FeedSourceEntity> tempList = new List<FeedSourceEntity>();
                SqlParameter paramPublishDate;
                paramPublishDate = new SqlParameter("@PublishDate", publishDate);
                bool IsUpdatedRequired = false;  
                IsUpdatedRequired= Convert.ToBoolean(SqlHelper.ExecuteScalar(Feedaggregator.Common.BGCon, CommandType.StoredProcedure, "Sp_CheckIfUpdateRequired", paramPublishDate));

                return IsUpdatedRequired;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>Returns a list with ContactPerson objects.</summary>
        /// <returns>A ContactPersonCollection with the ContactPerson objects.</returns>
        public static void Insert(ref FeedItemEntity entity)
        {
            try
            {
                //Patient_Id, 
                SqlParameter paramTitle= new SqlParameter("@Title", SqlDbType.NVarChar);
                paramTitle.Direction = ParameterDirection.Input;
                paramTitle.Value = entity.Title;

                SqlParameter paramRedirectURL= new SqlParameter("@RedirectURL", SqlDbType.VarChar);
                paramRedirectURL.Direction = ParameterDirection.Input;
                paramRedirectURL.Value = entity.RedirectURL;

                SqlParameter paramSource= new SqlParameter("@Source", SqlDbType.Int);
                paramSource.Direction = ParameterDirection.Input;
                paramSource.Value = entity.Source;

                SqlParameter paramImageURL = new SqlParameter("@ImageURL", SqlDbType.VarChar);
                paramImageURL.Direction = ParameterDirection.Input;
                paramImageURL.Value = entity.ImageURL;

                SqlParameter paramPublishDate = new SqlParameter("@PublishDate", SqlDbType.DateTime);
                paramPublishDate.Direction = ParameterDirection.Input;
                paramPublishDate.Value = entity.PublishDate;

                SqlParameter paramDescription = new SqlParameter("@Description", SqlDbType.NVarChar);
                paramDescription.Direction = ParameterDirection.Input;
                paramDescription.Value = entity.Description;

                SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
                paramCreatedBy.Direction = ParameterDirection.Input;
                paramCreatedBy.Value = "Minal P";

                SqlParameter paramActive = new SqlParameter("@Active", SqlDbType.Bit);
                paramActive.Direction = ParameterDirection.Input;
                paramActive.Value = entity.Active;

                SqlParameter paramFeedSourceXML = new SqlParameter("@FeedSourceXML", SqlDbType.VarChar);
                paramFeedSourceXML.Direction = ParameterDirection.Input;
                paramFeedSourceXML.Value = entity.FeedSourceXML;

                SqlParameter paramUTCPublishDate = new SqlParameter("@UTCPublishDate", SqlDbType.DateTime);
                paramUTCPublishDate.Direction = ParameterDirection.Input;
                paramUTCPublishDate.Value = entity.UTCPublishDate;

                object identity = SqlHelper.ExecuteScalar(Feedaggregator.Common.BGCon, CommandType.StoredProcedure, "Sp_InsertFeed", paramTitle, paramRedirectURL, paramSource, paramImageURL, paramPublishDate, paramDescription, paramCreatedBy, paramActive, paramFeedSourceXML, paramUTCPublishDate);
                entity.Id = Convert.ToInt64(identity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>Returns a list with ContactPerson objects.</summary>
        /// <returns>A ContactPersonCollection with the ContactPerson objects.</returns>
        //public static bool GetMappings()
        //{
        //    try
        //    {
        //        List<FeedSourceEntity> tempList = new List<FeedSourceEntity>();

        //        using (SqlDataReader readerPatient = SqlHelper.ExecuteReader(Feedaggregator.Common.BGCon, CommandType.StoredProcedure, "Sp_GetFeedSource"))
        //        {
        //            if (readerPatient.HasRows)
        //            {
        //                tempList = new List<FeedSourceEntity>();
        //                while (readerPatient.Read())
        //                {
        //                    tempList.Add(FillDataRecord(readerPatient));
        //                }
        //            }
        //            readerPatient.Close();
        //        }


        //        return tempList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}
