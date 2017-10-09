CREATE TABLE [dbo].[Account] (
    [TelephonyId] UNIQUEIDENTIFIER    NOT NULL,
    [Node]        [sys].[hierarchyid] NOT NULL,
    [NodeLevel]   AS                  ([Node].[GetLevel]()),
    [TwilioId]    CHAR (34)           NOT NULL,
    [TwilioToken] VARCHAR (34)        NOT NULL,
    [Status]      TINYINT             NOT NULL,
    [Created]     DATETIME            NOT NULL,
    [Updated]     DATETIME            NOT NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY NONCLUSTERED ([TelephonyId] ASC)
);

