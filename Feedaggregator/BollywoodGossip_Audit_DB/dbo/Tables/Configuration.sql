CREATE TABLE [dbo].[Configuration] (
    [ConfigurationID]    INT           NOT NULL,
    [ConfigurationName]  VARCHAR (50)  NOT NULL,
    [ConfigurationValue] VARCHAR (100) NOT NULL,
    [CreatedBy]          VARCHAR (50)  NOT NULL,
    [CreatedDate]        DATETIME      NOT NULL,
    [UpdatedBy]          VARCHAR (50)  NULL,
    [UpdatedDate]        DATETIME      NULL,
    CONSTRAINT [PK_Configuration] PRIMARY KEY CLUSTERED ([ConfigurationID] ASC)
);

