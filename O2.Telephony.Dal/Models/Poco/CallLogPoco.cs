// ReSharper disable CheckNamespace
using System;
using O2.Telephony.Dal.PetaPoco;

namespace O2.Telephony.Dal.Models
{
	[TableName("CallLog")]
	[PrimaryKey("TelephonyCallId", autoIncrement = false)]
	internal partial class CallLogPoco
	{
		public Guid TelephonyCallId { get; set; }
        public Guid? ParentTelephonyCallId { get; set; }
        public Guid TelephonyAccountId { get; set; }
        public string Username { get; set; }
        public string SalesRepName { get; set; }
        public string DivisionNumber { get; set; }
        public bool IsEnterprise { get; set; }
        public byte ProductType { get; set; }
        public byte SessionType { get; set; }
        public int? CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string Status { get; set; }
        public int DurationTotalSeconds { get; set; }
        public int DurationRoundedMinutes { get; set; }
        public DateTime DateUtc { get; set; }
        public DateTime? StartTimeUtc { get; set; }
        public DateTime? EndTimeUtc { get; set; }
        public string LocalTimeZoneAbbr { get; set; }
	    public double LocalTimeTotalOffset { get; set; }
        public DateTime DateLocal { get; set; }
        public DateTime? StartTimeLocal { get; set; }
        public DateTime? EndTimeLocal { get; set; }
        public byte? RecordType { get; set; }
        public string RecordId { get; set; }
        public string RecordName { get; set; }
        public string FromPhone { get; set; }
        public string ToPhone { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool IsFinal { get; set; }
	}
}
// ReSharper restore CheckNamespace
