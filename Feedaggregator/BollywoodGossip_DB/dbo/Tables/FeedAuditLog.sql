CREATE TABLE [dbo].[FeedAuditLog] (
    [FeedAuditLogID] BIGINT   IDENTITY (1, 1) NOT NULL,
    [DeviceConfigID] BIGINT   NULL,
    [PageNumber]     INT      NOT NULL,
    [CreatedDate]    DATETIME NULL,
    CONSTRAINT [PK_FeedAuditLog] PRIMARY KEY CLUSTERED ([FeedAuditLogID] ASC),
    CONSTRAINT [FK_FeedAuditLog_DeviceConfig] FOREIGN KEY ([DeviceConfigID]) REFERENCES [dbo].[DeviceConfig] ([DeviceConfigID])
);

