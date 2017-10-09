CREATE TABLE [dbo].[UserCreditsLog] (
    [Id]             BIGINT          IDENTITY (1, 1) NOT NULL,
    [MessageId]      VARCHAR (36)    NOT NULL,
    [Time]           DATETIME2 (7)   NOT NULL,
    [Level]          VARCHAR (10)    NOT NULL,
    [Message]        NVARCHAR (MAX)  NOT NULL,
    [UserThinkId]    INT             NOT NULL,
    [AccountThinkId] INT             NOT NULL,
    [AccountId]      INT             NULL,
    [ModuleCode]     VARCHAR (100)   NULL,
    [Credits]        INT             NULL,
    [PricePerCredit] DECIMAL (18, 2) NULL,
    [Machine]        VARCHAR (50)    NOT NULL,
    [Logger]         VARCHAR (128)   NOT NULL,
    [StackTrace]     VARCHAR (MAX)   NULL,
    [Success]        BIT             CONSTRAINT [DF_UserCreditsLog_Success] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_UserCreditsLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

