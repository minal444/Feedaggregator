CREATE PROC [dbo].[Sp_InsertFeedDetailAuditLog]  
(  
 @FeedId BIGINT,
 @DeviceId VARCHAR(500),
 @ViewOnWeb BIT
)  
AS  
BEGIN  
	Declare @DeviceConfigID int  
	SET @DeviceConfigID = 0  
	Select @DeviceConfigID = ISNULL(DeviceConfigID,0) from DeviceConfig (nolock) where DeviceID = @DeviceID  
	IF(@DeviceConfigID != 0)  
		INSERT INTO FeedDetailAuditLog (DeviceConfigID,FeedID,CreatedDate,ViewOnWeb) VALUES(@DeviceConfigID, @FeedId, GETUTCDATE(),@ViewOnWeb)  
END

