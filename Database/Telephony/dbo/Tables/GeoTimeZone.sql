CREATE TABLE [dbo].[GeoTimeZone] (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [Latitude]             DECIMAL (10, 8) NOT NULL,
    [Longitude]            DECIMAL (11, 8) NOT NULL,
    [OffsetUtcDst]         FLOAT (53)      NOT NULL,
    [OffsetUtcRaw]         FLOAT (53)      NOT NULL,
    [TimeZoneAbbrStandard] VARCHAR (6)     NOT NULL,
    [TimeZoneAbbrDaylight] VARCHAR (6)     NOT NULL,
    [TimeZoneId]           NVARCHAR (100)  NOT NULL,
    [TimeZoneNameStandard] NVARCHAR (200)  NOT NULL,
    [TimeZoneNameDaylight] NVARCHAR (200)  NOT NULL,
    [Created]              DATETIME        NOT NULL,
    [Updated]              DATETIME        NOT NULL,
    CONSTRAINT [PK_GeoTimeZone] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_GeoTimeZone_LatLng]
    ON [dbo].[GeoTimeZone]([Latitude] ASC, [Longitude] ASC);

