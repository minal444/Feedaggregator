CREATE TABLE [dbo].[Feedback] (
    [FeedbackID]     BIGINT        NOT NULL,
    [Name]           VARCHAR (200) NULL,
    [Email]          VARCHAR (200) NULL,
    [Category]       VARCHAR (100) NULL,
    [Feedback]       VARCHAR (MAX) NULL,
    [Response]       BIT           NULL,
    [CreatedDate]    DATETIME      NULL,
    [DeviceConfigID] BIGINT        NULL,
    CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED ([FeedbackID] ASC)
);

