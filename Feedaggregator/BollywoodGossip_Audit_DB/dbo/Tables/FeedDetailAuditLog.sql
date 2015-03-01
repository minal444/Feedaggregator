CREATE TABLE [dbo].[FeedDetailAuditLog] (
    [FeedDetailAuditLogID] BIGINT   NOT NULL,
    [DeviceConfigID]       BIGINT   NULL,
    [FeedID]               BIGINT   NULL,
    [CreatedDate]          DATETIME NULL,
    [ViewOnWeb]            BIT      NULL,
    CONSTRAINT [PK_FeedDetailAuditLog] PRIMARY KEY CLUSTERED ([FeedDetailAuditLogID] ASC),
    CONSTRAINT [FK_FeedDetailAuditLog_DeviceConfig] FOREIGN KEY ([DeviceConfigID]) REFERENCES [dbo].[DeviceConfig] ([DeviceConfigID]),
    CONSTRAINT [FK_FeedDetailAuditLog_Feeds] FOREIGN KEY ([FeedID]) REFERENCES [dbo].[Feeds] ([FeedID])
);

