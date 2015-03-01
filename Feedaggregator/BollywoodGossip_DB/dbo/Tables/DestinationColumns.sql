CREATE TABLE [dbo].[DestinationColumns] (
    [DestinationColumnId]   INT           NOT NULL,
    [DestinationColumnName] NVARCHAR (50) NOT NULL,
    [CreatedDate]           DATETIME      NULL,
    [UpdatedDate]           DATETIME      NULL,
    CONSTRAINT [PK_DestinationColumns] PRIMARY KEY CLUSTERED ([DestinationColumnId] ASC)
);

