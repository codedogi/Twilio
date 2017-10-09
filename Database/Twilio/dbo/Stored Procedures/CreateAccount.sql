-- =============================================
-- Description:	Create an account as a child of the parent
-- =============================================
CREATE PROCEDURE [dbo].[CreateAccount]
	@telephonyId uniqueidentifier,
	@telephonyParentId uniqueidentifier,
	@twilioId char(34),
	@twilioToken varchar(34)
AS
BEGIN
	set nocount on

	declare @ErrorMessage VARCHAR(500),
		    @ErrorSev     INT,
			@ErrorState   INT

	declare @Result hierarchyid
	declare @parentNode hierarchyid 
	declare @maxChildNode hierarchyid
	declare @now datetime
	declare @activeStatus tinyint

	-- start transaction
	begin transaction
	begin try

		--set variables
		select @now = GetDate();
		select @activeStatus = 1;
		select @parentNode = node from account where TelephonyId = @telephonyParentId
		select @maxChildNode = max(node) from account where node.GetAncestor(1) = @parentNode

		-- insert subscribe data, email
		insert into Account (TelephonyId, Node, TwilioId, TwilioToken, Status, Created, Updated)
			values (@telephonyId, @parentNode.GetDescendant(@maxChildNode, NULL), @twilioId, @twilioToken, @activeStatus, @now, @now);
		
		-- Commit the transaction.
		commit transaction
	
		--return account just created
		select TelephonyId, Node.ToString() as Node, NodeLevel, TwilioId, TwilioToken, Status, Created, Updated from Account where TelephonyId = @telephonyId;

	end try
	begin catch
		print error_line()
		print error_message()

		-- something failed, rollback
		rollback transaction

		SELECT @ErrorMessage = Error_message(),
			   @ErrorSev = Error_severity(),
			   @ErrorState = Error_state()
 
		RAISERROR (@ErrorMessage,
				   @ErrorSev,
				   @ErrorState)

	end catch

END

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[CreateAccount] TO [db_sp_execute]
    AS [dbo];

