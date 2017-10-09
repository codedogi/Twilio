CREATE TABLE [dbo].[GeoZipCode] (
    [ZipCode]   VARCHAR (20)    NOT NULL,
    [Latitude]  DECIMAL (10, 8) NOT NULL,
    [Longitude] DECIMAL (11, 8) NOT NULL,
    [Created]   DATETIME        NOT NULL,
    [Updated]   DATETIME        NOT NULL,
    CONSTRAINT [PK_GeoZipCode] PRIMARY KEY CLUSTERED ([ZipCode] ASC)
);

