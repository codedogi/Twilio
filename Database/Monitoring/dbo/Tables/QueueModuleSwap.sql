CREATE TABLE [dbo].[QueueModuleSwap] (
    [Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [Time]          DATETIME2 (7)  NOT NULL,
    [Level]         VARCHAR (10)   NOT NULL,
    [Message]       NVARCHAR (MAX) NOT NULL,
    [MessageId]     VARCHAR (36)   NOT NULL,
    [OrderId]       INT            NOT NULL,
    [OldModuleCode] VARCHAR (50)   NOT NULL,
    [NewModuleCode] VARCHAR (50)   NOT NULL,
    [StartDate]     DATETIME2 (7)  NOT NULL,
    [EndDate]       DATETIME2 (7)  NOT NULL,
    [CustomerId]    INT            NULL,
    [OldLicenseKey] VARCHAR (20)   NULL,
    [NewLicenseKey] VARCHAR (20)   NULL,
    [Machine]       NVARCHAR (50)  NOT NULL,
    [Logger]        NVARCHAR (200) NOT NULL,
    [StackTrace]    NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_QueueModuleSwap] PRIMARY KEY CLUSTERED ([Id] ASC)
);

