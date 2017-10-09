CREATE TABLE [dbo].[QueueSuspendModule] (
    [Id]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [Time]       DATETIME2 (7)  NOT NULL,
    [Level]      VARCHAR (10)   NOT NULL,
    [Message]    NVARCHAR (MAX) NOT NULL,
    [MessageId]  VARCHAR (36)   NOT NULL,
    [OrderId]    INT            NOT NULL,
    [ModuleCode] VARCHAR (50)   NOT NULL,
    [CustomerId] INT            NULL,
    [LicenseId]  VARCHAR (20)   NULL,
    [Machine]    NVARCHAR (50)  NOT NULL,
    [Logger]     NVARCHAR (200) NOT NULL,
    [StackTrace] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_QueueSuspendModule] PRIMARY KEY CLUSTERED ([Id] ASC)
);

