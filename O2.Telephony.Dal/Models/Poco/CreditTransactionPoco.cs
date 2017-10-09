using System;
using O2.Telephony.Dal.PetaPoco;

namespace O2.Telephony.Dal.Models
{
    [TableName("CreditTransactions")]
    [PrimaryKey("Id", autoIncrement=true)]
    internal partial class CreditTransactionPoco
    {
        public int Id { get; set; }
        public Guid TelephonyAccountId { get; set; }
        public Guid? TelephonyCallId { get; set; }
        public byte TransactionType { get; set; }
        public string Username { get; set; }
        public string ProcessedBy { get; set; }
        public int ActualSeconds { get; set; }
        public int TransactionTimeMinutes { get; set; }
        public string OrderId { get; set; }
        public DateTime CreateDateUtc { get; set; }
    }
}
