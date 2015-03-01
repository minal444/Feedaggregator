CREATE PROC [dbo].[Sp_GetFeedSource]  
AS  
BEGIN  
	 SELECT   
	  FeedSourceID as ID,   
	  SourceName,   
	  SourceURL,  
	  SiteURL,  
	  isnull(ImageSource,'') as ImageSource,  
	  isnull(ImageNameSpace,'') as ImageNameSpace,  
	  isnull(DescriptionNameSpace,'') as DescriptionNameSpace ,
	  ISNULL(Timezone,'') as Timezone 
	 FROM FeedSource (nolock)  
	 WHERE Active=1 AND (StartDate <= GETUTCDATE() AND ( EndDate >= GETUTCDATE() OR EndDate IS NULL))  
END

