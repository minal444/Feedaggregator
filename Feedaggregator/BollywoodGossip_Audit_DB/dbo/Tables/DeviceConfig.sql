CREATE TABLE [dbo].[DeviceConfig] (
    [DeviceConfigID]     BIGINT        NOT NULL,
    [DeviceID]           VARCHAR (500) NULL,
    [Notification]       BIT           NULL,
    [MaxFeedID]          BIGINT        NULL,
    [Version]            VARCHAR (100) NULL,
    [CreatedDate]        DATETIME      NULL,
    [UpdatedDate]        DATETIME      NULL,
    [DeviceManufacturer] VARCHAR (300) NULL,
    [DeviceModel]        VARCHAR (100) NULL,
    [OsVersion]          VARCHAR (100) NULL,
    [CarrierName]        VARCHAR (200) NULL,
    [NetworkType]        VARCHAR (100) NULL,
    [Active]             BIT           NULL,
    CONSTRAINT [PK_DeviceConfig] PRIMARY KEY CLUSTERED ([DeviceConfigID] ASC)
);

