using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using O2.Telephony.Dal.Interfaces;
using O2.Telephony.Dal.Models;
using O2.Telephony.Dal.PetaPoco;
using O2.Telephony.Models;

namespace O2.Telephony.Dal.Imp
{
    public class CreditDal : BaseDal, ICreditDal
    {
        #region Constructors
        public CreditDal() : base(LogManager.GetCurrentClassLogger())
        {
            Logger.Trace("Instantiated");
        }
        #endregion

        #region Create
        public CreditTransaction Create(Guid accountId, Guid? callId, TransactionType type, string username, string processedBy, int actualSeconds, string orderId)
        {
            Logger.Debug(
                $"Create({accountId}, {callId?.ToString() ?? "null"}, {type}, {username}, {processedBy}, {actualSeconds}, {orderId})");

            int seconds = Math.Abs(actualSeconds);
            int transactionMinutes = seconds / 60;
            transactionMinutes += (seconds%60 != 0 ? 1 : 0);

            if (actualSeconds < 0)
                transactionMinutes *= -1;

            var ct = new CreditTransaction
            {
                ActualSeconds = actualSeconds,
                CreateDateUtc = DateTime.UtcNow,
                OrderId = orderId,
                ProcessedBy = processedBy,
                TelephonyAccountId = accountId,
                TelephonyCallId = callId,
                TransactionTimeMinutes = transactionMinutes,
                TransactionType = type,
                Username = username
            };

            using (var db = new Database(TelephonyConnection))
            {
                try
                {
                    var result = db.Insert(new CreditTransactionPoco(ct));
                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        ct.Id = id;
                    }

                    Logger.Trace($"poco Insert CreditTransaction result: {result}");

                    return ct; // result.ToModel();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Error poco Insert CreditTransaction: {Format(db)}");
                    throw;
                }
            }
        }
        #endregion

        #region Read
        public CreditTransaction Read(int id)
        {
            Logger.Debug($"Read({id})");

            var sql = new Sql("WHERE id = @0", id);

            using (var db = new Database(TelephonyConnection))
            {
                try
                {
                    //Use FirstOrDefault here.  Don't throw an error for not found.  Just return the null object
                    Logger.Trace($"db.FirstOrDefault: {Format(sql)}");
                    return db.FirstOrDefault<CreditTransactionPoco>(sql).ToModel();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Error db.FirstOrDefault: {Format(db)}");
                    throw;
                }
            }
        }

        public CreditSummary ReadSummary(Guid accountId)
        {
            Logger.Debug($"ReadSummary({accountId})");
            
            var sql = new Sql(
                    @"SELECT SUM(CASE WHEN CreateDateUTC >= DATEADD(month, -1, getutcdate()) AND TransactionType = @0 THEN ABS(TransactionTimeMinutes) ELSE 0 END) AS MinutesUsedPast30, 
                        SUM(CASE WHEN CreateDateUTC >= DATEADD(year, -1, getutcdate()) AND TransactionType = @0 THEN ABS(TransactionTimeMinutes) ELSE 0 END) AS MinutesUsedPastYear,
                        SUM(CASE WHEN TransactionType = @0 THEN ABS(TransactionTimeMinutes) ELSE 0 END) AS MinutesUsed,
                        SUM(TransactionTimeMinutes) as MinutesAvailable,
                        TelephonyAccountId FROM CreditTransactions
                        WHERE TelephonyAccountId = @1
                        GROUP BY TelephonyAccountId", (byte)TransactionType.Used, accountId);

            using (var db = new Database(TelephonyConnection))
            {
                try
                {
                    //Use FirstOrDefault here.  Don't throw an error for not found.  Just return the null object
                    Logger.Trace($"db.FirstOrDefault: {Format(sql)}");
                    var result = db.FirstOrDefault<CreditSummary>(sql);
                    if (result == null)
                        return new CreditSummary
                        {
                            TelephonyAccountId = accountId,
                            MinutesAvailable = 0,
                            MinutesUsed = 0,
                            MinutesUsedPast30 = 0,
                            MinutesUsedPastYear = 0
                        };

                    return result;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Error db.FirstOrDefault: {Format(db)}");
                    throw;
                }
            }
        }

        public int ReadAvailable(Guid accountId)
        {
            Logger.Debug($"ReadAvailable({accountId})");

            var sql = new Sql(
                "SELECT COALESCE(SUM(TransactionTimeMinutes), 0) AS TotalTime FROM CreditTransactions WHERE TelephonyAccountId = @0", accountId);

            using (var db = new Database(TelephonyConnection))
            {
                try
                {
                    Logger.Trace($"db.ExecuteScalar: {Format(sql)}");
                    return db.ExecuteScalar<int>(sql);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Error db.ExecuteScalar: {Format(db)}");
                    throw;
                }
            }
        }

        public IEnumerable<CreditTransaction> ReadTransactions(Guid accountId)
        {
            Logger.Debug($"ReadTransactions({accountId})");

            var sql = new Sql(
                "SELECT * FROM CreditTransactions WHERE TelephonyAccountId = @0 ORDER BY CreateDateUtc DESC", accountId);

            using (var db = new Database(TelephonyConnection))
            {
                try
                {
                    Logger.Trace($"db.Fetch: {Format(sql)}");
                    var results = db.Fetch<CreditTransactionPoco>(sql);
                    return results.Select(x => x.ToModel());
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Error db.Fetch: {Format(db)}");
                    throw;
                }
            }
        }
        #endregion
    }
}
