CREATE TABLE [dbo].[TelephonyServiceLog] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [LogDate]         DATETIME2 (7)  NOT NULL,
    [Level]           VARCHAR (10)   NOT NULL,
    [Machine]         VARCHAR (1024) NOT NULL,
    [Application]     VARCHAR (512)  NOT NULL,
    [Logger]          VARCHAR (128)  NOT NULL,
    [IP]              VARCHAR (16)   NULL,
    [Url]             VARCHAR (1024) NULL,
    [RouteData]       VARCHAR (512)  NULL,
    [ServerVariables] VARCHAR (MAX)  NULL,
    [Form]            VARCHAR (MAX)  NULL,
    [Cookie]          VARCHAR (MAX)  NULL,
    [Header]          VARCHAR (MAX)  NULL,
    [Message]         VARCHAR (MAX)  NULL,
    [StackTrace]      VARCHAR (MAX)  NULL,
    [Exception]       VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_TelephonyServiceLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

