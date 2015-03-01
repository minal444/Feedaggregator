--[Sp_GetFeedMappings] 6
CREATE Proc [dbo].[Sp_GetFeedMappings]
(
	@FeedSourceId Int
)
As 
Begin
select 
	fs.FeedSourceID as FeedSourceId, fs.SourceName as FeedSourceName, sc.SourceColumnId, sc.SourceColumnName, 
	dc.DestinationColumnId, dc.DestinationColumnName from 
	SourceDestinationColumnMapping (nolock) sdcm 
inner join SourceColumns (nolock) sc on sc.SourceColumnId = sdcm.SourceColumnId
inner join DestinationColumns (nolock) dc on dc.DestinationColumnId = sdcm.DestinationColumnId
inner join FeedSource (nolock) fs on fs.FeedSourceID = sc.FeedSourceId
Where fs.FeedSourceID = @FeedSourceId
End

