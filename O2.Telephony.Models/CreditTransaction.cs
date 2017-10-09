using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.Telephony.Models
{
    public class CreditTransaction
    {
        #region Properties
        public int Id { get; set; }
        public Guid TelephonyAccountId { get; set; }
        public Guid? TelephonyCallId { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Username { get; set; }
        public string ProcessedBy { get; set; }
        public int ActualSeconds { get; set; }
        public int TransactionTimeMinutes { get; set; }
        public string OrderId { get; set; }
        public DateTime CreateDateUtc { get; set; }
        #endregion Properties

        #region Methods
        public override string ToString()
        {
            return string.Format("[{0}] TelephonyAccountId: {1}, TransactionType: {2}, Username: {3}, ProcessedBy {4}, ActualSeconds: {5}, TransactionTimeMinutes: {6}, Order Id: {7}",
                GetType().FullName, TelephonyAccountId, TransactionType, Username, ProcessedBy, ActualSeconds, TransactionTimeMinutes, OrderId);
        }
        #endregion Methods
    }
}
