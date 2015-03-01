--2397
--select dbo.fn_GetReletedFeeds (2397)
--select * from FeedsMapping where ReletedFeedID in (2402,2395)
--select * from FeedsMapping where FeedID in (1816,2395,2402)
--select * from feeds where id in (1816,2395,2402)
CREATE FUNCTION [dbo].[fn_GetReletedFeeds](@FeedId bigint)
Returns XML
BEGIN
	Declare @readmoreText varchar(100)
	set @readmoreText  = '...[Read More]'
	
	DECLARE @XML as XML
	--declare @XMLTABLE table ( xmlData Varchar(max))
	--insert into @XMLTABLE 
	SELECT 
       @XML = 
	(SELECT *
	FROM 
	(
	SELECT ISNULL(Feed.FeedID, '') as id  
	,ISNULL(Feed.Title, '') as title  
	,ISNULL(Feed.ImageURL, '') as imageurl  
	,ltrim(substring(ISNULL(Feed.Description, ''),0,150)) --adding case for description if blank
		+ CASE
			When len(ltrim(rtrim(ISNULL(Feed.Description,''))))>10 THEN @readmoreText 
			ELSE '' END  as description  
	,ISNULL(Feed.PublishDate, '') as publishdate  
	,ISNULL(Feed.UTCPublishDate, '') as UTCPublishDate  
	--,dbo.fn_GetTimeDiffference(Feed.PublishDate, getutcdate()) as pubtime  
	,ISNULL(source.SourceName, '') as sourcename  
	--,ISNULL(source.SourceURL, '') as sourceurl  
	,ISNULL(source.LogoURL, '') as logourl  
	,'From: '+ ISNULL(source.SourceName, '') + ' ' + dbo.fn_GetTimeDiffference(Feed.UTCPublishDate, getutcdate()) + ' ago' as sourcefrom
	,ROW_NUMBER() OVER (ORDER BY Feed.UTCPublishDate DESC) AS RowNum
	FROM FeedsMapping As FeedMapping
	Inner Join Feeds As Feed  on FeedMapping.FeedID= Feed.FeedID
	INNER JOIN FeedSource as source on Feed.Source = source.FeedSourceID  
	Where ISNull(Feed.Active,1) = 1
	AND FeedMapping.ReletedFeedID = @FeedId and FeedMapping.FeedID != @FeedId
	) 
	AS Feed
	ORDER BY Feed.UTCPublishDate DESC
	FOR XML AUTO, ROOT('Feeds'),TYPE, ELEMENTS  
	)
	--Select @XML = xmlData from @XMLTABLE
	return @XML
END

