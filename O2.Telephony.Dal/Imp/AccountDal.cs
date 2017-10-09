using System;
using NLog;
using O2.Telephony.Dal.Interfaces;
using O2.Telephony.Dal.Models;
using O2.Telephony.Models;
using O2.Telephony.Dal.PetaPoco;

namespace O2.Telephony.Dal.Imp
{
    /// <summary>
    /// Data access layer for accounts
    /// </summary>
    public class AccountDal : BaseDal, IAccountDal
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public AccountDal()
            : base(LogManager.GetCurrentClassLogger())
        {
            Logger.Trace("Instantiated");
        }
        #endregion

        #region Create
        /// <summary>
        /// Create an account
        /// </summary>
        /// <param name="accountId">Account id to create</param>
        /// <param name="parentId">Parent account id to create account under</param>
        /// <returns>Account created</returns>
        public Account Create(Guid accountId, Guid parentId)
        {
            Logger.Debug($"Create({accountId}, {parentId})");

            using (var db = new Database(TelephonyConnection))
            {
                try
                {
                    var sql = new Sql().Append("execute CreateAccount @accountId, @parentId", new {accountId, parentId});
                    var account = db.Single<AccountPoco>(sql);

                    Logger.Trace($"db.Single execute CreateAccount result: {account}");

                    return account.ToModel();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Error db.Single: {Format(db)}");
                    throw;
                }
            }
        }
        #endregion

        #region Read
        /// <summary>
        /// Read an account
        /// </summary>
        /// <param name="id">Id to read by</param>
        /// <returns>Account read</returns>
        public Account Read(Guid id)
        {
            Logger.Debug($"Read({id})");

            var sql = new Sql().Append("select Id, Node.ToString() as Node, NodeLevel, Status, Created, Updated from Account where id = @0", id);

            using (var db = new Database(TelephonyConnection))
            {
                try
                {
                    Logger.Trace($"db.SingleOrDefault: {Format(sql)}");
                    return db.SingleOrDefault<AccountPoco>(sql).ToModel();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Error db.SingleOrDefault: {Format(db)}");
                    throw;
                }
            }
        }
        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion

        #region Process
        #endregion
    }
}
