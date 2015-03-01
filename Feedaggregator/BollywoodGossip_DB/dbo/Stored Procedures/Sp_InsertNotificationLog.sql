CREATE proc [dbo].[Sp_InsertNotificationLog]  
(
	@DeviceID varchar(500),
	@NotificationLogMessage varchar(max) = ''
)
AS  
BEGIN  

	Declare @DeviceConfigID int
	Set @DeviceConfigID = 0
	select @DeviceConfigID = ISNUll(DeviceConfigID,0) from DeviceConfig (nolock) where DeviceID = @DeviceID
	Insert into  NotificationLog (DeviceConfigID, CreatedDate, NotificationLogMessage) 
							values(@DeviceConfigID,GETUTCDATE(),@NotificationLogMessage)
END

