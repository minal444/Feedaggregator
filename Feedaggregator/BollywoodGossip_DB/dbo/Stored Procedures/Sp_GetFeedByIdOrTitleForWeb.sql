
--[Sp_GetFeedByIdOrTitleForWeb] 1745
CREATE PROC [dbo].[Sp_GetFeedByIdOrTitleForWeb]  
(  
 @ID BIGINT = null,  
 @Title Varchar(max) = null
)  
AS  
BEGIN  
 SELECT   
  ISNULL(Feed.FeedID, '') as FeedID  
  ,ISNULL(Feed.Title, '') as title  
  ,ISNULL(Feed.ImageURL, '') as imageurl  
  ,LTRIM(REPLACE(ISNULL(Feed.Description, ''),'Social Publishing:    lable_on','')) as description  
  ,ISNULL(Feed.PublishDate, '') as publishdate  
  ,ISNULL(Feed.RedirectURL, '') as redirecturl  
  ,dbo.fn_GetTimeDiffference(Feed.PublishDate, getutcdate()) as pubtime  
  ,ISNULL(Source.SourceName, '') as sourcefrom  
 FROM Feeds (nolock) as Feed   
 INNER JOIN FeedSource (nolock) Source on Feed.Source = Source.FeedSourceID  
 WHERE ((@ID IS NOT Null AND Feed.FeedID = @ID) OR @Id is null)  
   AND ((@Title IS NOT Null AND Title = @Title) OR @Title is null)  
  --FOR XML AUTO, ROOT('Feeds'),TYPE, ELEMENTS  
 
END



