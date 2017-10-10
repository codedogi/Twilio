using System;
using System.Configuration;
using NLog;
using O2.Telephony.Dal.Interfaces;
using O2.Telephony.Logic.Interfaces;
using O2.Telephony.Models;
using O2.Telephony.Provider.Interfaces;

namespace O2.Telephony.Logic
{
    public class AccountLogic : BaseLogic, IAccountLogic
    {
        private readonly IAccountDal _accountDal;
        private readonly IProviderLogic _providerLogic;

        public AccountLogic(IAccountDal accountDal, IProviderLogic providerLogic) : base(LogManager.GetCurrentClassLogger())
        {
            Logger.Trace("Instantiated");
            _accountDal = accountDal;
            _providerLogic = providerLogic;
        }

        public AccountResult<Account> Create(Guid? parentId)
        {
            try
            {
                Logger.Debug($"Create({parentId})");

                //Determine parentAccountId
                Guid parentAccountId;

                if (parentId.HasValue)
                {
                    if (parentId == Guid.Empty)
                    {
                        Logger.Trace("AccountResultCode.InvalidParameter, parentId");
                        return new AccountResult<Account>(AccountResultCode.InvalidParameter, "parentId");
                    }

                    parentAccountId = parentId.Value;
                }
                else
                {
                    if (!Guid.TryParse(ConfigurationManager.AppSettings["TelephonyAccountRootNode"], out parentAccountId))
                    {
                        Logger.Trace("AccountResultCode.RootNodeNotFound, TelephonyAccountRootNode");
                        return new AccountResult<Account>(AccountResultCode.RootNodeNotFound,
                                                          "Guid failed to parse the TelephonyAccountRootNode key value");
                    }
                }

                Account account;

                try
                {
                    //get parent account
                    Account parentAccount = _accountDal.Read(parentAccountId);

                    if (parentAccount == null)
                    {
                        Logger.Trace("AccountResultCode.ParentAccountNotFound, parentAccountId");
                        return new AccountResult<Account>(AccountResultCode.ParentAccountNotFound, "Parent account not found");
                    }

                    //create telephony account
                    account = _accountDal.Create(Guid.NewGuid(), parentAccountId);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Database error in Create({parentId})");
                    return new AccountResult<Account>(AccountResultCode.DatabaseError, ex.Message);
                }

                //create provider account
                Provider.Models.AccountResult result = _providerLogic.CreateAccount(account.Id, parentAccountId);

                if (result.HasError)
                {
                    Logger.Trace($"AccountResultCode.ProviderError, providerMessage: {result.ErrorMessage}");
                    return new AccountResult<Account>(AccountResultCode.ProviderError,
                        $"Provider failed to create account, AccountId: {account.Id}, ParentAccountId: {parentAccountId}");
                }

                return new AccountResult<Account>(account);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error in Create({parentId})");
                return new AccountResult<Account>(AccountResultCode.Error, ex.Message);
            }
        }

        public AccountResult<Account> Get(Guid accountId)
        {
            Logger.Debug($"Get({accountId})");

            try
            {
                //validate
                if (accountId == Guid.Empty)
                {
                    Logger.Trace("AccountResultCode.InvalidParameter, accountId");
                    return new AccountResult<Account>(AccountResultCode.InvalidParameter, "accountId");
                }

                //get telephony account
                Account account = _accountDal.Read(accountId);

                if (account == null)
                {
                    Logger.Trace("AccountResultCode.AccountNotFound, accountId");
                    return new AccountResult<Account>(AccountResultCode.AccountNotFound, "Account not found");
                }

                return new AccountResult<Account>(account);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error in Get({accountId})");
                return new AccountResult<Account>(AccountResultCode.DatabaseError, ex.Message);
            }            
        }
    }
}
