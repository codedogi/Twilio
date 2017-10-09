
CREATE VIEW [dbo].[EventCodeFilter]
AS
SELECT DISTINCT TOP (100) PERCENT dbo.aspnet_WebEvent_Events.EventCode, dbo.EventCodes.Name, dbo.EventCodes.Description
FROM         dbo.aspnet_WebEvent_Events INNER JOIN
                      dbo.EventCodes ON dbo.aspnet_WebEvent_Events.EventCode = dbo.EventCodes.WebEventCode
ORDER BY dbo.EventCodes.Name