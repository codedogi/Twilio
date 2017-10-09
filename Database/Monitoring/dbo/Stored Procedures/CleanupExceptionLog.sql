-- ======================================
-- Author:		Venu Bandi
-- Create date: 8/5/2013
-- Description:	Deletes Exceptions older than a month from Weblog and aspnet_webevents table.
-- ======================================
CREATE PROCEDURE [dbo].CleanupExceptionLog AS

set nocount on

declare @timeout datetime = DATEADD(month, -1, GETDATE())				-- 30 days timeout

DELETE FROM dbo.WebLog WHERE LogDate < @timeout
DELETE FROM dbo.aspnet_WebEvent_Events WHERE EventTime < @timeout