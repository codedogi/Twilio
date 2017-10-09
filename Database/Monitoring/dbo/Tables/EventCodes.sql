CREATE TABLE [dbo].[EventCodes] (
    [WebEventCode] INT            NOT NULL,
    [Name]         NVARCHAR (100) NOT NULL,
    [Description]  NVARCHAR (250) NULL,
    CONSTRAINT [PK_EventCodes] PRIMARY KEY CLUSTERED ([WebEventCode] ASC)
);

