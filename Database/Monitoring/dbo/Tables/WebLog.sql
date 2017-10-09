CREATE TABLE [dbo].[WebLog] (
    [Id]              BIGINT           IDENTITY (1, 1) NOT NULL,
    [LogDate]         DATETIME2 (7)    NOT NULL,
    [Level]           VARCHAR (10)     NOT NULL,
    [Machine]         NVARCHAR (1024)  NOT NULL,
    [Application]     VARCHAR (512)    NOT NULL,
    [Logger]          VARCHAR (128)    NOT NULL,
    [IP]              CHAR (16)        NULL,
    [Url]             NVARCHAR (1024)  NULL,
    [RouteData]       NVARCHAR (512)   NULL,
    [ServerVariables] VARCHAR (MAX)    NULL,
    [Form]            VARCHAR (MAX)    NULL,
    [Cookie]          NVARCHAR (MAX)   NULL,
    [Header]          NVARCHAR (MAX)   NULL,
    [Message]         NVARCHAR (MAX)   NOT NULL,
    [StackTrace]      VARCHAR (MAX)    NULL,
    [Exception]       VARCHAR (MAX)    NULL,
    [WebLogGuid]      UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_WebLog_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_WebLog_Application]
    ON [dbo].[WebLog]([Application] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_WebLog_Logger]
    ON [dbo].[WebLog]([Logger] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_WebLog_Level]
    ON [dbo].[WebLog]([Level] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_WebLog_LogDate]
    ON [dbo].[WebLog]([LogDate] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_WebLog_Machine]
    ON [dbo].[WebLog]([Machine] ASC);

