using System;
using System.Collections.Generic;
using NLog;
using O2.Telephony.Dal.Interfaces;
using O2.Telephony.Logic.Interfaces;
using O2.Telephony.Models;

namespace O2.Telephony.Logic
{
    public class CreditLogic : BaseLogic, ICreditLogic
    {
        private readonly IAccountDal _accountDal;
        private readonly ICreditDal _creditDal;

        public CreditLogic(IAccountDal accountDal, ICreditDal creditDal)
            : base(LogManager.GetCurrentClassLogger())
        {
            Logger.Trace("Instantiated");

            _accountDal = accountDal;
            _creditDal = creditDal;
        }

        public CreditResult<CreditTransaction> AdjustCredits(Guid accountId, AdjustmentType type, string username, string processedBy, int minutes, string orderId)
        {
            try
            {
                Logger.Debug($"AdjustCredits({accountId}, {type}, {username}, {processedBy}, {minutes}, {orderId})");

                if (accountId == Guid.Empty)
                {
                    Logger.Trace("CreditResultCode.InvalidParameter, accountId");
                    return new CreditResult<CreditTransaction>(CreditResultCode.InvalidParameter, "accountId");
                }

                if (string.IsNullOrEmpty(username))
                {
                    Logger.Trace("CreditResultCode.InvalidParameter, username");
                    return new CreditResult<CreditTransaction>(CreditResultCode.InvalidParameter, "username");
                }

                if (string.IsNullOrEmpty(processedBy))
                {
                    Logger.Trace("CreditResultCode.InvalidParameter, processedBy");
                    return new CreditResult<CreditTransaction>(CreditResultCode.InvalidParameter, "processedBy");
                }

                if (minutes == 0)
                {
                    Logger.Trace("CreditResultCode.InvalidParameter, minutes");
                    return new CreditResult<CreditTransaction>(CreditResultCode.InvalidParameter, "minutes");
                }

                Account account = _accountDal.Read(accountId);

                if (account == null)
                {
                    Logger.Trace("CreditResultCode.AccountNotFound, accountId");
                    return new CreditResult<CreditTransaction>(CreditResultCode.AccountNotFound, "accountId");
                }

                TransactionType transType;
                switch (type)
                {
                    case AdjustmentType.ManualAllocation:
                        transType = TransactionType.ManualAllocation;
                        break;
                    case AdjustmentType.UserPurchased:
                        transType = TransactionType.UserPurchased;
                        break;
                    case AdjustmentType.QueueAllocation:
                        transType = TransactionType.QueueAllocation;
                        break;
                    case AdjustmentType.ExpiredAllocation:
                        transType = TransactionType.ExpiredAllocation;
                        break;
                    default:
                        transType = TransactionType.AutoAllocation;
                        break;
                }

                var ct = _creditDal.Create(accountId, null, transType, username, processedBy, minutes*60, orderId);

                return new CreditResult<CreditTransaction>(ct);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, 
                    $"Error in AdjustCredits({accountId}, {type}, {username}, {processedBy}, {minutes}, {orderId})");
                return new CreditResult<CreditTransaction>(CreditResultCode.Error, ex.Message);
            }
        }

        public CreditResult<CreditTransaction> DeductCredits(Guid accountId, Guid? callId, string username, string processedBy, int seconds, string orderId)
        {
            try
            {
                Logger.Debug(
                    $"DeductCredits({accountId}, {callId?.ToString() ?? "null"}, {username}, {processedBy}, {seconds}, {orderId})");

                if (accountId == Guid.Empty)
                {
                    Logger.Trace("CreditResultCode.InvalidParameter, accountId");
                    return new CreditResult<CreditTransaction>(CreditResultCode.InvalidParameter, "accountId");
                }

                if (seconds <= 0)
                {
                    Logger.Trace("CreditResultCode.InvalidParameter, seconds");
                    return new CreditResult<CreditTransaction>(CreditResultCode.InvalidParameter, "seconds");
                }

                if (string.IsNullOrEmpty(username))
                {
                    Logger.Trace("CreditResultCode.InvalidParameter, username");
                    return new CreditResult<CreditTransaction>(CreditResultCode.InvalidParameter, "username");
                }

                if (string.IsNullOrEmpty(processedBy))
                {
                    Logger.Trace("CreditResultCode.InvalidParameter, processedBy");
                    return new CreditResult<CreditTransaction>(CreditResultCode.InvalidParameter, "processedBy");
                }

                Account account = _accountDal.Read(accountId);

                if (account == null)
                {
                    Logger.Trace("CreditResultCode.AccountNotFound, accountId");
                    return new CreditResult<CreditTransaction>(CreditResultCode.AccountNotFound, "accountId");
                }

                var ct = _creditDal.Create(accountId, null, TransactionType.Used, username, processedBy, -seconds, orderId);

                return new CreditResult<CreditTransaction>(ct);
            }
            catch (Exception ex)
            {
                Logger.Error(ex,
                    $"Error in DeductCredits({accountId}, {callId?.ToString() ?? "null"}, {username}, {processedBy}, {seconds}, {orderId})");
                return new CreditResult<CreditTransaction>(CreditResultCode.Error, ex.Message);
            }
        }

        public CreditResult<CreditTransaction> Read(int id)
        {
            try
            {
                Logger.Debug($"Read({id})");

                var ct = _creditDal.Read(id);

                return new CreditResult<CreditTransaction>(ct);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error in Read({id})");
                return new CreditResult<CreditTransaction>(CreditResultCode.Error, ex.Message);
            }
        }

        public CreditResult<CreditSummary> ReadSummary(Guid accountId)
        {
            try
            {
                Logger.Debug($"ReadSummary({accountId})");

                if (accountId == Guid.Empty)
                {
                    Logger.Trace("CreditResultCode.InvalidParameter, accountId");
                    return new CreditResult<CreditSummary>(CreditResultCode.InvalidParameter, "accountId");
                }

                Account account = _accountDal.Read(accountId);

                if (account == null)
                {
                    Logger.Trace("CreditResultCode.AccountNotFound, accountId");
                    return new CreditResult<CreditSummary>(CreditResultCode.AccountNotFound, "accountId");
                }

                var ct = _creditDal.ReadSummary(accountId);

                return new CreditResult<CreditSummary>(ct);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"ReadSummary({accountId})");
                return new CreditResult<CreditSummary>(CreditResultCode.Error, ex.Message);
            }
        }

        public CreditResult<int> ReadAvailable(Guid accountId)
        {
            try
            {
                Logger.Debug($"ReadAvailable({accountId})");

                if (accountId == Guid.Empty)
                {
                    Logger.Trace("CreditResultCode.InvalidParameter, accountId");
                    return new CreditResult<int>(CreditResultCode.InvalidParameter, "accountId");
                }

                var account = _accountDal.Read(accountId);

                if (account == null)
                {
                    Logger.Trace("CreditResultCode.AccountNotFound, accountId");
                    return new CreditResult<int>(CreditResultCode.AccountNotFound, "accountId");
                }

                var ct = _creditDal.ReadAvailable(accountId);

                return new CreditResult<int>(ct);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"ReadAvailable({accountId})");
                return new CreditResult<int>(CreditResultCode.Error, ex.Message);
            }
        }

        public CreditResult<IEnumerable<CreditTransaction>> ReadTransactions(Guid accountId)
        {
            try
            {
                Logger.Debug($"ReadTransactions({accountId})");

                if (accountId == Guid.Empty)
                {
                    Logger.Trace("CreditResultCode.InvalidParameter, accountId");
                    return new CreditResult<IEnumerable<CreditTransaction>>(CreditResultCode.InvalidParameter, "accountId");
                }

                Account account = _accountDal.Read(accountId);

                if (account == null)
                {
                    Logger.Trace("CreditResultCode.AccountNotFound, accountId");
                    return new CreditResult<IEnumerable<CreditTransaction>>(CreditResultCode.AccountNotFound, "accountId");
                }

                var ct = _creditDal.ReadTransactions(accountId);

                return new CreditResult<IEnumerable<CreditTransaction>>(ct);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"ReadTransactions({accountId})");
                return new CreditResult<IEnumerable<CreditTransaction>>(CreditResultCode.Error, ex.Message);
            }
        }
    }
}
