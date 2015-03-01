CREATE proc [dbo].[SP_GetDevicesforNotification]
(
	@DeviceID varchar(500) = Null
)
AS  
BEGIN  
	Declare @RequestedMaxFeedID bigint

	--SELECT Top 1 @RequestedMaxFeedID =Feed.FeedID
	--	FROM Feeds (nolock) Feed
	--	INNER JOIN FeedSource (nolock) as source on Feed.Source = source.FeedSourceID  
	--	Where ISNull(Feed.Active,1) = 1 AND source.Active= 1
	--	ORDER BY PublishDate DESC 
		
	SELECT Top 1 @RequestedMaxFeedID =Feed.FeedID  
	  FROM Feeds (nolock) Feed  
	  INNER JOIN FeedSource (nolock) as source on Feed.Source = source.FeedSourceID    
	  Where ISNull(Feed.Active,1) = 1 AND source.Active= 1  
	  ORDER BY UTCPublishDate DESC   
	  	
		
	--SELECT *,(@RequestedMaxFeedID - MaxFeedID) as NewNumber FROM DeviceConfig 
	--where ISNULL([Notification],1) = 1
	--AND ISNULL(MaxFeedID,0)<@RequestedMaxFeedID
	
	
	SELECT *
	,
	CASE WHEN (SELECT COUNT(FeedID) FROM Feeds (nolock) WHERE FeedID<=@RequestedMaxFeedID AND FeedID > ISNULL(MaxFeedID,0) AND Active=1) > 99 Then '99+'
	ELSE (SELECT convert(varchar(5),(COUNT(FeedID))) FROM Feeds (nolock) WHERE FeedID<=@RequestedMaxFeedID AND FeedID > ISNULL(MaxFeedID,0) AND Active=1) END + ' New stories available!' 
	as NewNumber 
	FROM DeviceConfig (nolock)
	where ISNULL([Notification],1) = 1 AND ACTIVE = 1 AND
	ISNULL(MaxFeedID,0)<@RequestedMaxFeedID
	
	
 
END

