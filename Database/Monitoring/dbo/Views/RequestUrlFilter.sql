
CREATE VIEW [dbo].[RequestUrlFilter]
AS
SELECT DISTINCT RequestUrl
FROM         dbo.aspnet_WebEvent_Events