CREATE TABLE [dbo].[JsLog] (
    [Id]              BIGINT          IDENTITY (1, 1) NOT NULL,
    [LogDate]         DATETIME2 (7)   NOT NULL,
    [Level]           VARCHAR (10)    NOT NULL,
    [UserId]          INT             NULL,
    [UserName]        VARCHAR (256)   NULL,
    [Message]         NVARCHAR (MAX)  NOT NULL,
    [Machine]         NVARCHAR (1024) NOT NULL,
    [Application]     VARCHAR (512)   NOT NULL,
    [Logger]          VARCHAR (128)   NOT NULL,
    [IP]              CHAR (16)       NULL,
    [ServerVariables] VARCHAR (MAX)   NULL,
    [Cookie]          NVARCHAR (MAX)  NULL,
    [Header]          NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_DialerLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

