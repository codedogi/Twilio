using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using O2.Telephony.Models;

namespace O2.Telephony.Dal.Models
{
    internal partial class CreditTransactionPoco : BasePoco
    {
        public CreditTransactionPoco() { }

        internal CreditTransactionPoco(CreditTransaction ct)
        {
            ActualSeconds = ct.ActualSeconds;
            CreateDateUtc = ct.CreateDateUtc;
            Id = ct.Id;
            OrderId = ct.OrderId;
            ProcessedBy = ct.ProcessedBy;
            TelephonyAccountId = ct.TelephonyAccountId;
            TelephonyCallId = ct.TelephonyCallId;
            TransactionTimeMinutes = ct.TransactionTimeMinutes;
            TransactionType = (byte) ct.TransactionType;
            Username = ct.Username;
        }

        public override string ToString()
        {
            return string.Format("[{0}] TelephonyAccountId: {1}, TransactionType: {2}, Username: {3}, ProcessedBy {4}, ActualSeconds: {5}, TransactionTimeMinutes: {6}, Order Id: {7}",
                GetType().FullName, TelephonyAccountId, TransactionType, Username, ProcessedBy, ActualSeconds, TransactionTimeMinutes, OrderId);
        }
    }
}
