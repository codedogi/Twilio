CREATE ROLE [db_sp_execute]
    AUTHORIZATION [dbo];


GO
EXECUTE sp_addrolemember @rolename = N'db_sp_execute', @membername = N'TelephonyUser';

