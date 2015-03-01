-- Exec Sp_DeleteFeed 5
CREATE Proc [dbo].[Sp_DeleteFeed]
(
	@FeedId bigint,
	@Output int out
)
as
Begin
	SET @Output = 0
	Begin Tran DeleteFeed
	
	Begin Try
		--Delete from Mappting table
		Delete from BollywoodGossip.dbo.FeedsMapping where FeedID = @FeedId
		
		--Delete from Mappting table wwhith relationa id
		Delete from BollywoodGossip.dbo.FeedsMapping where ReletedFeedID = @FeedId
		
		--Delete from Audit Log table
		Delete from BollywoodGossip.dbo.FeedDetailAuditLog where FeedID = @FeedId

		--Delete from Main Feed Table
		Delete from BollywoodGossip.dbo.Feeds where FeedID = @FeedId
		Commit Tran DeleteFeed
		SET @Output = 1
	End Try
	Begin Catch
	Rollback Tran DeleteFeed;
			Insert into ErrorLog   (ErrorDescription
								,Exception
								,Stacktrace
								,ErrorMetadata
								,CreatedDate
								,UpdateDate
								,ErrorSource)
		values ('Error while deleting FeedID:'+ convert(varchar(50),@FeedId),ERROR_MESSAGE(),null,@FeedId,GETUTCDATE(),GETUTCDATE(),'[DeleteFeed]')
	End Catch
End


