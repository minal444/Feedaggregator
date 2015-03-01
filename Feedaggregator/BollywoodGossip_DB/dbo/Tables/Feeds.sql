CREATE TABLE [dbo].[Feeds] (
    [FeedID]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [Title]          NVARCHAR (MAX) NULL,
    [Description]    NVARCHAR (MAX) NULL,
    [ImageURL]       VARCHAR (MAX)  NULL,
    [PublishDate]    DATETIME       NULL,
    [RedirectURL]    VARCHAR (MAX)  NULL,
    [Source]         INT            NULL,
    [CreatedBy]      VARCHAR (100)  NULL,
    [CreatedDate]    DATETIME       NULL,
    [UpdatedBy]      VARCHAR (100)  NULL,
    [UpdatedDate]    DATETIME       NULL,
    [SourceXML]      VARCHAR (MAX)  NULL,
    [Active]         BIT            NULL,
    [UTCPublishDate] DATETIME       NULL,
    CONSTRAINT [PK_Feeds] PRIMARY KEY CLUSTERED ([FeedID] ASC),
    CONSTRAINT [FK_Feeds_FeedSource] FOREIGN KEY ([Source]) REFERENCES [dbo].[FeedSource] ([FeedSourceID])
);

