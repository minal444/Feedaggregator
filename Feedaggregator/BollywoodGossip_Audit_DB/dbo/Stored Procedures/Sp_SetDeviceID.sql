CREATE PROC [dbo].[Sp_SetDeviceID]    
(  
 @newDeviceID varchar(500),  
 @oldDeviceID varchar(500),  
 @appVersionNumber varchar(100),
 @deviceManufacturer varchar(300),
 @deviceModel varchar(100),
 @osVersion varchar(100),
 @carrierName varchar(200),
 @networkType varchar(100)
)  
AS    
BEGIN    
if(@newDeviceID = '')  
 return -- do nothing if new device is blank  
IF(@oldDeviceID = '')--first time registration  
 BEGIN  
  --to handle case where user uninstall and install app  
  IF EXISTS(SELECT 1 from DeviceConfig (nolock) where DeviceID = @newDeviceID)   
   BEGIN  
    --device exists already, update with notification = 1 and version  
    UPDATE DeviceConfig SET [Notification] = 1, [Version] = @appVersionNumber ,
							DeviceManufacturer = @deviceManufacturer, 
						 DeviceModel = @deviceModel,
						 OsVersion = @osVersion,
						 CarrierName= @carrierName,
						 NetworkType = @networkType,
						 UpdatedDate = GETUTCDATE(),
						 Active = 1
    where DeviceID = @newDeviceID  
   END  
   ELSE  
   BEGIN  
    --adding device first time  
    INSERT into DeviceConfig (DeviceID,[Notification],[Version],DeviceManufacturer, DeviceModel, OsVersion, CarrierName , NetworkType, CreatedDate, ACTIVE) 
    values (@newDeviceID,1,@appVersionNumber,@deviceManufacturer, @deviceModel, @osVersion, @carrierName , @networkType, GETUTCDATE(),1)   
   END  
 END  
ELSE --update version number change device id, no change in notification  
 BEGIN  
  UPDATE DeviceConfig set DeviceID = @newDeviceID, [Version] = @appVersionNumber ,
						  DeviceManufacturer = @deviceManufacturer, 
						 DeviceModel = @deviceModel,
						 OsVersion = @osVersion,
						 CarrierName= @carrierName,
						 NetworkType = @networkType,
						UpdatedDate = GETUTCDATE(),
						Active = 1
						 where DeviceID = @oldDeviceID 
 END    
END

