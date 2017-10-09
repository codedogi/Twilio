CREATE TABLE [dbo].[Account] (
    [Id]        UNIQUEIDENTIFIER    NOT NULL,
    [Node]      [sys].[hierarchyid] NOT NULL,
    [NodeLevel] AS                  ([Node].[GetLevel]()),
    [Status]    TINYINT             NOT NULL,
    [Created]   DATETIME            NOT NULL,
    [Updated]   DATETIME            NOT NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Account_Node]
    ON [dbo].[Account]([Node] ASC);

