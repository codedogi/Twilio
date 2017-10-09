CREATE TABLE [dbo].[NewsFeedLog] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Source]      VARCHAR (50)   NOT NULL,
    [SearchType]  VARCHAR (50)   NOT NULL,
    [Keywords]    VARCHAR (1024) NULL,
    [StoryId]     VARCHAR (1024) NULL,
    [ResultCount] INT            CONSTRAINT [DF_NewsFeedLog_ResultCount] DEFAULT ((0)) NOT NULL,
    [DateCreated] DATETIME       CONSTRAINT [DF_NewsFeedLog_DateSearched] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_NewsFeedLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

