using O2.Telephony.Models;

namespace O2.Telephony.Dal.Models
{
    public static class CreditTransactionPocoExtension
    {
        internal static CreditTransaction ToModel(this CreditTransactionPoco ct)
        {
            if (ct == null)
                return null;

            return new CreditTransaction
            {
                ActualSeconds = ct.ActualSeconds,
                CreateDateUtc = ct.CreateDateUtc,
                Id = ct.Id,
                OrderId = ct.OrderId,
                ProcessedBy = ct.ProcessedBy,
                TelephonyAccountId = ct.TelephonyAccountId,
                TelephonyCallId = ct.TelephonyCallId,
                TransactionTimeMinutes = ct.TransactionTimeMinutes,
                TransactionType = (TransactionType) ct.TransactionType,
                Username = ct.Username
            };
        }
    }
}
