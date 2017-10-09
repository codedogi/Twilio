CREATE TABLE [dbo].[AutoDataMetadataLog] (
    [Id]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [LogDateTime] DATETIME2 (7)   NOT NULL,
    [Level]       VARCHAR (10)    NOT NULL,
    [Machine]     NVARCHAR (1024) NOT NULL,
    [Application] VARCHAR (512)   NOT NULL,
    [Logger]      VARCHAR (128)   NOT NULL,
    [Message]     NVARCHAR (MAX)  NOT NULL,
    [StackTrace]  VARCHAR (MAX)   NULL,
    CONSTRAINT [PK_AutoDataMetadataLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

