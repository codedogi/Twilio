CREATE TABLE [dbo].[E7Log] (
    [Id]               BIGINT        IDENTITY (1, 1) NOT NULL,
    [Time]             DATETIME2 (7) NOT NULL,
    [Level]            VARCHAR (10)  NOT NULL,
    [Message]          VARCHAR (MAX) NOT NULL,
    [Machine]          VARCHAR (50)  NOT NULL,
    [Logger]           VARCHAR (256) NOT NULL,
    [StackTrace]       VARCHAR (MAX) NULL,
    [E7RequestTransId] BIGINT        NULL,
    [E7ResultTransId]  BIGINT        NULL,
    [E7DataId]         BIGINT        NULL
);

