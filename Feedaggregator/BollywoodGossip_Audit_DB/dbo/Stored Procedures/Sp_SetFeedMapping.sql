CREATE proc [dbo].[Sp_SetFeedMapping]  
(
	@FeedID bigint,
	@SearchedFeedID bigint
)
AS  
BEGIN  

	--if Searchedfeedid is exists change its mapping to new feed id
	IF EXISTS (SELECT 1 FROM FeedsMapping (nolock) where FeedID = @SearchedFeedID)
	BEGIN
		UPDATE FeedsMapping set ReletedFeedID = @FeedID where FeedID = @SearchedFeedID
	END
	ELSE -- else add new item
	BEGIN
		INSERT INTO FeedsMapping (FeedID, ReletedFeedID) values (@FeedID,@FeedID)
	END
END

