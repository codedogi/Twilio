CREATE TABLE [dbo].[OutgoingCalls] (
    [TelephonyCallId]    UNIQUEIDENTIFIER NOT NULL,
    [TelephonyAccountId] UNIQUEIDENTIFIER NOT NULL,
    [AccountSid]         CHAR (34)        NOT NULL,
    [ToPhoneNumber]      CHAR (12)        NOT NULL,
    [FromPhoneNumber]    CHAR (12)        NOT NULL,
    [CallSid]            CHAR (34)        NOT NULL,
    [CallStatus]         CHAR (16)        NULL,
    [Created]            DATETIME         NOT NULL,
    [Updated]            DATETIME         NULL,
    [Deleted]            DATETIME         NULL,
    CONSTRAINT [PK_OutgoingCalls] PRIMARY KEY CLUSTERED ([TelephonyCallId] ASC) WITH (FILLFACTOR = 90)
);

