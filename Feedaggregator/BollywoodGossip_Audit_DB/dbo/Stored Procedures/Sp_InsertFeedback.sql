Create Proc [dbo].[Sp_InsertFeedback]
(
	@Name Varchar(200),
	@Email Varchar(200),
	@Category Varchar(100),
	@Feedback Varchar(Max),
	@Response Bit,
	@DeviceId Varchar(500)
)
as
Begin
	Declare @DeviceConfigId bigint
	Select @DeviceConfigId = DeviceConfigID From DeviceConfig (nolock) Where DeviceID = @DeviceId

	INSERT INTO [Feedback]
			   ([Name]
			   ,[Email]
			   ,[Category]
			   ,[Feedback]
			   ,[Response]
			   ,[CreatedDate]
			   ,[DeviceConfigID])
		 VALUES
			   (
				@Name,
				@Email,
				@Category,
				@Feedback,
				@Response,
				GETUTCDATE(),
				@DeviceConfigId
			   )

End

