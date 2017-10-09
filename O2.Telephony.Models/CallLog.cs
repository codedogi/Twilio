using System;

namespace O2.Telephony.Models
{
    public class CallLog
    {
        #region Public Properties

        public Guid TelephonyCallId { get; set; }
        public Guid? ParentTelephonyCallId { get; set; }
        public Guid TelephonyAccountId { get; set; }
        public string Username { get; set; }
        public string SalesRepName { get; set; }
        public string DivisionNumber { get; set; }
        public bool IsEnterprise { get; set; }
        public ProductTypeCode ProductType { get; set; }
        public CallSessionType SessionType { get; set; }
        public int? CampaignId { get; set; }
        public string CampaignName { get; set; }
        public CallStatusType Status { get; set; }
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
        public CallRecordType RecordType { get; set; }
        public string RecordId { get; set; }
        public string RecordName { get; set; }
        public string FromPhone { get; set; }
        public string ToPhone { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool IsFinal { get; set; }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return string.Format("[{0}] TelephonyCallId: {1}, ParentTelephonyCallId: {2}, TelephonyAccountId: {3}, Username: {4}, Status: {5}",
                                 GetType().FullName, TelephonyCallId, ParentTelephonyCallId, TelephonyAccountId, Username, Status);
        }

        #endregion
    }
}
