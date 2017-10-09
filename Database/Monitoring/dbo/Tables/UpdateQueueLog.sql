CREATE TABLE [dbo].[UpdateQueueLog] (
    [Id]                    BIGINT         IDENTITY (1, 1) NOT NULL,
    [Time]                  DATETIME2 (7)  NOT NULL,
    [Level]                 NVARCHAR (16)  NOT NULL,
    [Message]               NVARCHAR (MAX) NOT NULL,
    [Success]               BIT            CONSTRAINT [DF_UpdateTransactionLog_Success] DEFAULT ((1)) NULL,
    [UpdateTransactionType] NVARCHAR (64)  NULL,
    [ThinkAccountId]        INT            NULL,
    [ThinkContactId]        INT            NULL,
    [OldUserName]           NVARCHAR (128) NULL,
    [NewUserName]           NVARCHAR (128) NULL,
    [Machine]               NVARCHAR (64)  NOT NULL,
    [Logger]                NVARCHAR (128) NOT NULL,
    [StackTrace]            NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UpdateTransactionLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

