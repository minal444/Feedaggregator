CREATE TABLE [dbo].[FeedsMapping] (
    [FeedsMappingID] INT    IDENTITY (1, 1) NOT NULL,
    [FeedID]         BIGINT NOT NULL,
    [ReletedFeedID]  BIGINT NOT NULL,
    CONSTRAINT [PK_FeedsMapping] PRIMARY KEY CLUSTERED ([FeedsMappingID] ASC),
    CONSTRAINT [FK_FeedsMapping_Feeds] FOREIGN KEY ([FeedID]) REFERENCES [dbo].[Feeds] ([FeedID]),
    CONSTRAINT [FK_FeedsMapping_Feeds1] FOREIGN KEY ([ReletedFeedID]) REFERENCES [dbo].[Feeds] ([FeedID])
);

