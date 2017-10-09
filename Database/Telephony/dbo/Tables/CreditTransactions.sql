CREATE TABLE [dbo].[CreditTransactions] (
    [Id]                     INT              IDENTITY (1, 1) NOT NULL,
    [TelephonyAccountId]     UNIQUEIDENTIFIER NOT NULL,
    [TelephonyCallId]        UNIQUEIDENTIFIER NULL,
    [TransactionType]        TINYINT          NOT NULL,
    [Username]               VARCHAR (256)    NOT NULL,
    [ProcessedBy]            VARCHAR (256)    NOT NULL,
    [ActualSeconds]          INT              NOT NULL,
    [TransactionTimeMinutes] INT              NOT NULL,
    [OrderId]                VARCHAR (256)    NULL,
    [CreateDateUTC]          DATETIME         CONSTRAINT [DF_CreditTransactions_CreateDateUTC] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_CreditTransactions] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_CreditTransactions]
    ON [dbo].[CreditTransactions]([TelephonyAccountId] ASC, [CreateDateUTC] DESC, [TransactionType] ASC)
    INCLUDE([TransactionTimeMinutes]);

