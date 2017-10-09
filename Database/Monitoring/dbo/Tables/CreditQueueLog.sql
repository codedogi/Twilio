CREATE TABLE [dbo].[CreditQueueLog] (
    [Id]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [Time]             DATETIME2 (7)  NOT NULL,
    [Level]            VARCHAR (10)   NOT NULL,
    [Message]          NVARCHAR (MAX) NOT NULL,
    [Success]          BIT            CONSTRAINT [DF_CreditQueueLog_Success] DEFAULT ((1)) NULL,
    [ThinkId]          INT            NULL,
    [OriginAccountId]  INT            NULL,
    [NumberOfCredits]  INT            NULL,
    [ModuleCode]       VARCHAR (100)  NULL,
    [NumberOfSegments] TINYINT        NULL,
    [Frequency]        TINYINT        NULL,
    [FrequencyType]    TINYINT        NULL,
    [Machine]          VARCHAR (50)   NOT NULL,
    [Logger]           VARCHAR (128)  NOT NULL,
    [StackTrace]       VARCHAR (MAX)  NULL,
    [Quantity]         INT            NULL,
    [ThinkOrderId]     INT            NULL,
    [Status]           NVARCHAR (50)  NULL,
    CONSTRAINT [PK_CreditQueueLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

