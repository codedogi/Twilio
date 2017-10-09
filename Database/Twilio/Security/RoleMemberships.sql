EXECUTE sp_addrolemember @rolename = N'db_owner', @membername = N'ConsoleUser';


GO
EXECUTE sp_addrolemember @rolename = N'db_datareader', @membername = N'TwilioUser';


GO
EXECUTE sp_addrolemember @rolename = N'db_datawriter', @membername = N'TwilioUser';


GO
EXECUTE sp_addrolemember @rolename = N'db_datareader', @membername = N'SqlCompareDev';


GO
EXECUTE sp_addrolemember @rolename = N'db_ddladmin', @membername = N'SqlCompareDev';


GO
EXECUTE sp_addrolemember @rolename = N'db_datawriter', @membername = N'SqlCompareDev';

