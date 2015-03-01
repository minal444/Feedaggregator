--[Sp_GetNewFeedsCount] 6313
CREATE PROC [dbo].[Sp_GetNewFeedsCount]
(
	@ID BIGINT
)
AS
BEGIN
	SELECT 
		COUNT(FeedID) as NewCount
	FROM Feeds (nolock) as [Count] 
	WHERE FeedID > @ID ANd Active= 1
		FOR XML AUTO, ROOT('Counts'),TYPE, ELEMENTS
END

