CREATE TABLE [dbo].[OutgoingCallerId] (
    [TelephonyCallerIdId] UNIQUEIDENTIFIER NOT NULL,
    [TelephonyAccountId]  UNIQUEIDENTIFIER NOT NULL,
    [AccountSid]          CHAR (34)        NOT NULL,
    [PhoneNumber]         CHAR (12)        NOT NULL,
    [FriendlyName]        CHAR (64)        NULL,
    [ValidationCode]      CHAR (6)         NOT NULL,
    [CallSid]             CHAR (34)        NULL,
    [VerificationStatus]  CHAR (10)        NULL,
    [Sid]                 CHAR (34)        NULL,
    [Callback]            VARCHAR (MAX)    NULL,
    [Created]             DATETIME         NOT NULL,
    [Updated]             DATETIME         NULL,
    [Deleted]             DATETIME         NULL,
    [Timestamp]           ROWVERSION       NOT NULL,
    CONSTRAINT [PK_OutgoingCallerId_1] PRIMARY KEY CLUSTERED ([TelephonyCallerIdId] ASC)
);

