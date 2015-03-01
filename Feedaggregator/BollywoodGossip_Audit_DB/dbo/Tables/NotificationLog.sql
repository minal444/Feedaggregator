CREATE TABLE [dbo].[NotificationLog] (
    [NotificationLogID]      BIGINT        NOT NULL,
    [DeviceConfigID]         BIGINT        NOT NULL,
    [CreatedDate]            DATETIME      NULL,
    [NotificationLogMessage] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_NotificationLog] PRIMARY KEY CLUSTERED ([NotificationLogID] ASC),
    CONSTRAINT [FK_NotificationLog_DeviceConfig] FOREIGN KEY ([DeviceConfigID]) REFERENCES [dbo].[DeviceConfig] ([DeviceConfigID])
);

