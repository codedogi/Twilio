CREATE PROCEDURE dbo.ReportExceptions 
	@WebLogGuid UNIQUEIDENTIFIER
AS
BEGIN
	INSERT INTO dbo.WebLogReported (WebLogGuid, ReportedDate) 
	VALUES (@WebLogGuid, CURRENT_TIMESTAMP)
END