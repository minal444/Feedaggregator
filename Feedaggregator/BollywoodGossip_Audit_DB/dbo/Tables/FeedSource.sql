CREATE TABLE [dbo].[FeedSource] (
    [FeedSourceID]         INT            NOT NULL,
    [SourceName]           VARCHAR (100)  NOT NULL,
    [SourceURL]            VARCHAR (1000) NOT NULL,
    [Active]               BIT            NOT NULL,
    [StartDate]            DATETIME       NOT NULL,
    [EndDate]              DATETIME       NULL,
    [CreatedBy]            VARCHAR (100)  NOT NULL,
    [CreatedDate]          DATETIME       NOT NULL,
    [UpdatedBy]            VARCHAR (100)  NULL,
    [UpdatedDate]          DATETIME       NULL,
    [SiteURL]              VARCHAR (1000) NULL,
    [LogoURL]              VARCHAR (1000) NULL,
    [Imagesource]          VARCHAR (100)  NULL,
    [ImageNameSpace]       VARCHAR (1000) NULL,
    [DescriptionNameSpace] VARCHAR (500)  NULL,
    [Timezone]             VARCHAR (50)   NULL,
    CONSTRAINT [PK_FeedSource] PRIMARY KEY CLUSTERED ([FeedSourceID] ASC)
);

