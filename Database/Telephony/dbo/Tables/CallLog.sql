CREATE TABLE [dbo].[CallLog] (
    [TelephonyCallId]        UNIQUEIDENTIFIER NOT NULL,
    [ParentTelephonyCallId]  UNIQUEIDENTIFIER NULL,
    [TelephonyAccountId]     UNIQUEIDENTIFIER NOT NULL,
    [Username]               VARCHAR (256)    NOT NULL,
    [SalesRepName]           VARCHAR (101)    NULL,
    [DivisionNumber]         NVARCHAR (64)    NOT NULL,
    [IsEnterprise]           BIT              NOT NULL,
    [ProductType]            TINYINT          NOT NULL,
    [SessionType]            TINYINT          NOT NULL,
    [CampaignId]             INT              NULL,
    [CampaignName]           VARCHAR (128)    NULL,
    [Status]                 VARCHAR (12)     NOT NULL,
    [DurationTotalSeconds]   INT              NOT NULL,
    [DurationRoundedMinutes] INT              NOT NULL,
    [DateUTC]                DATE             NOT NULL,
    [StartTimeUTC]           DATETIME         NULL,
    [EndTimeUTC]             DATETIME         NULL,
    [LocalTimeZoneAbbr]      VARCHAR (6)      NOT NULL,
    [LocalTimeTotalOffset]   FLOAT (53)       CONSTRAINT [DF_CallLog_LocalTimeTotalOffset] DEFAULT ((0)) NOT NULL,
    [DateLocal]              DATE             NOT NULL,
    [StartTimeLocal]         DATETIME         NULL,
    [EndTimeLocal]           DATETIME         NULL,
    [RecordType]             TINYINT          NOT NULL,
    [RecordId]               VARCHAR (15)     NULL,
    [RecordName]             NVARCHAR (50)    NOT NULL,
    [FromPhone]              VARCHAR (12)     NOT NULL,
    [ToPhone]                VARCHAR (12)     NOT NULL,
    [Created]                DATETIME         NOT NULL,
    [Updated]                DATETIME         NOT NULL,
    [IsFinal]                BIT              NOT NULL,
    CONSTRAINT [PK_Call] PRIMARY KEY CLUSTERED ([TelephonyCallId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [Idx_TelephonyAccountId]
    ON [dbo].[CallLog]([TelephonyAccountId] ASC);


GO
CREATE NONCLUSTERED INDEX [Idx_Created]
    ON [dbo].[CallLog]([Created] ASC);

