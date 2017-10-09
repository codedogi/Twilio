CREATE TABLE [dbo].[PurchaseRecordsMigration] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [Time]            DATETIME2 (7)  NOT NULL,
    [Level]           VARCHAR (10)   NOT NULL,
    [Message]         NVARCHAR (MAX) NOT NULL,
    [OriginAccountId] INT            NULL,
    [Machine]         VARCHAR (50)   NOT NULL,
    [Logger]          VARCHAR (128)  NOT NULL,
    [StackTrace]      VARCHAR (MAX)  NULL,
    [Status]          NVARCHAR (50)  NULL,
    CONSTRAINT [PK_PurchaseRecordsMigration] PRIMARY KEY CLUSTERED ([Id] ASC)
);

