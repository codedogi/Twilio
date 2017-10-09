CREATE TABLE [dbo].[UserModulesLog] (
    [Id]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [MessageId]      VARCHAR (36)   NOT NULL,
    [Time]           DATETIME2 (7)  NOT NULL,
    [Level]          VARCHAR (10)   NOT NULL,
    [Message]        NVARCHAR (MAX) NOT NULL,
    [UserThinkId]    INT            NOT NULL,
    [AccountThinkId] INT            NOT NULL,
    [AccountId]      INT            NULL,
    [ModuleCode]     VARCHAR (100)  NULL,
    [StartDate]      DATETIME2 (7)  NULL,
    [EndDate]        DATETIME2 (7)  NULL,
    [Status]         NVARCHAR (50)  NULL,
    [Machine]        VARCHAR (50)   NOT NULL,
    [Logger]         VARCHAR (128)  NOT NULL,
    [StackTrace]     VARCHAR (MAX)  NULL,
    [Success]        BIT            CONSTRAINT [DF_UserModulesLog_Success] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_UserModulesLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

