--[Sp_GetSettingsForDevice] test
CREATE Proc [dbo].[Sp_GetSettingsForDevice]
(
	@DeviceID Varchar(500)
)
As 
Begin

	Select 
		DeviceConfigID, 
		DeviceID, 
		[Notification],
		Version,
		DeviceManufacturer,
		DeviceModel,
		OsVersion,
		CarrierName,
		NetworkType
		from DeviceConfig (nolock)
		Where DeviceID = @DeviceID
	FOR XML AUTO, ROOT('settings'),TYPE, ELEMENTS  

End

