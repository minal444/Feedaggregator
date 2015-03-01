CREATE TABLE [dbo].[SourceDestinationColumnMapping] (
    [SourceColumnId]      INT           NOT NULL,
    [DestinationColumnId] INT           NOT NULL,
    [CreatedBy]           VARCHAR (100) NOT NULL,
    [CreatedDate]         DATETIME      NOT NULL,
    [UpdatedBy]           VARCHAR (100) NULL,
    [UpdatedDate]         DATETIME      NULL,
    CONSTRAINT [PK_SourceDestinationColumnMapping] PRIMARY KEY CLUSTERED ([SourceColumnId] ASC, [DestinationColumnId] ASC),
    CONSTRAINT [FK_SourceDestinationColumnMapping_DestinationColumns] FOREIGN KEY ([DestinationColumnId]) REFERENCES [dbo].[DestinationColumns] ([DestinationColumnId]),
    CONSTRAINT [FK_SourceDestinationColumnMapping_SourceColumns] FOREIGN KEY ([SourceColumnId]) REFERENCES [dbo].[SourceColumns] ([SourceColumnId])
);

