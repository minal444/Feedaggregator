CREATE TABLE [dbo].[SourceColumns] (
    [SourceColumnId]   INT           NOT NULL,
    [SourceColumnName] VARCHAR (100) NOT NULL,
    [FeedSourceId]     INT           NOT NULL,
    [CreatedBy]        VARCHAR (100) NOT NULL,
    [CreatedDate]      DATETIME      NOT NULL,
    [UpdatedBy]        VARCHAR (100) NULL,
    [UpdatedDate]      DATETIME      NULL,
    CONSTRAINT [PK_SourceColumns] PRIMARY KEY CLUSTERED ([SourceColumnId] ASC)
);

