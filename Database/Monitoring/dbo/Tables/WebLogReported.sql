CREATE TABLE [dbo].[WebLogReported] (
    [Id]           INT              IDENTITY (1, 1) NOT NULL,
    [WebLogGuid]   UNIQUEIDENTIFIER NOT NULL,
    [ReportedDate] DATETIME         NOT NULL,
    CONSTRAINT [PK_WebLogGuid] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IDX_WebLogReported_WebLogGuid]
    ON [dbo].[WebLogReported]([WebLogGuid] ASC) WITH (FILLFACTOR = 80);

