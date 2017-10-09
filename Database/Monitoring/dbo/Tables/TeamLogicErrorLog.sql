CREATE TABLE [dbo].[TeamLogicErrorLog] (
    [Id]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [Time]       DATETIME2 (7) NOT NULL,
    [Level]      VARCHAR (10)  NOT NULL,
    [Message]    VARCHAR (MAX) NOT NULL,
    [Machine]    VARCHAR (64)  NOT NULL,
    [Logger]     VARCHAR (128) NOT NULL,
    [StackTrace] VARCHAR (MAX) NULL,
    [Exception]  VARCHAR (MAX) NULL,
    CONSTRAINT [PK_TeamLogicLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

