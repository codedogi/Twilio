CREATE TABLE [dbo].[OutgoingCallerId] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [AccountId] UNIQUEIDENTIFIER NOT NULL,
    [Status]    TINYINT          NOT NULL,
    [Created]   DATETIME         NOT NULL,
    [Updated]   DATETIME         NULL,
    [Deleted]   DATETIME         NULL,
    CONSTRAINT [PK_OutgoingCallerId] PRIMARY KEY CLUSTERED ([Id] ASC)
);

