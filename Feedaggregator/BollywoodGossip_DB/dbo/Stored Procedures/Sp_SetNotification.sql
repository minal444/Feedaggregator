CREATE PROC [dbo].[Sp_SetNotification]  
(
	@DeviceID varchar(500),
	@Flag bit	
)
AS  
BEGIN  
 	IF EXISTS(SELECT 1 from DeviceConfig (nolock) where DeviceID = @DeviceID)
 	BEGIN
 		UPDATE DeviceConfig SET [Notification] = @Flag 
 		, UpdatedDate = GETUTCDATE()
 		where DeviceID = @DeviceID
 	END
 	--removing else condition as no this method will call only for update
 	--ELSE
 	--BEGIN
 	--	INSERT into DeviceConfig (DeviceID,[Notification]) values (@DeviceID,@Flag)
 	--END
END

