CREATE TABLE [dbo].[ErrorLog] (
    [ErrorID]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [ErrorDescription] VARCHAR (200)  NULL,
    [Exception]        NVARCHAR (MAX) NULL,
    [Stacktrace]       NVARCHAR (MAX) NULL,
    [ErrorMetadata]    NVARCHAR (500) NULL,
    [CreatedDate]      DATETIME       NULL,
    [UpdateDate]       DATETIME       NULL,
    [ErrorSource]      VARCHAR (50)   NULL,
    CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED ([ErrorID] ASC)
);

