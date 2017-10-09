CREATE TABLE [dbo].[ServiceEvents] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [EventTime] DATETIME      NULL,
    [Message]   VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ServiceEvents] PRIMARY KEY CLUSTERED ([Id] ASC)
);

