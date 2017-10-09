CREATE TABLE [dbo].[NLAErrorLog] (
    [Id]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [Time]       DATETIME2 (7)  NOT NULL,
    [Level]      VARCHAR (10)   NOT NULL,
    [Machine]    VARCHAR (50)   NOT NULL,
    [Logger]     VARCHAR (128)  NOT NULL,
    [Message]    NVARCHAR (MAX) NOT NULL,
    [StackTrace] VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_WebLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

