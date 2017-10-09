using O2.Telephony.Models;

namespace O2.Telephony.Dal.Models
{
    internal partial class CallLogPoco : BasePoco
    {
        public CallLogPoco(){}

        internal CallLogPoco(CallLog call)
        {
            TelephonyCallId = call.TelephonyCallId;
            ParentTelephonyCallId = call.ParentTelephonyCallId;
            TelephonyAccountId = call.TelephonyAccountId;
            Username = call.Username;
            SalesRepName = call.SalesRepName;
            DivisionNumber = call.DivisionNumber;
            IsEnterprise = call.IsEnterprise;
            ProductType = (byte) call.ProductType;
            SessionType = (byte) call.SessionType;
            CampaignId = call.CampaignId;
            CampaignName = call.CampaignName;
            Status = call.Status.ToString();
            DurationTotalSeconds = call.DurationTotalSeconds;
            DurationRoundedMinutes = call.DurationRoundedMinutes;
            DateUtc = call.DateUtc;
            StartTimeUtc = call.StartTimeUtc;
            EndTimeUtc = call.EndTimeUtc;
            LocalTimeZoneAbbr = call.LocalTimeZoneAbbr;
            LocalTimeTotalOffset = call.LocalTimeTotalOffset;
            DateLocal = call.DateLocal;
            StartTimeLocal = call.StartTimeLocal;
            EndTimeLocal = call.EndTimeLocal;
            RecordType = (byte) call.RecordType;
            RecordId = call.RecordId;
            RecordName = call.RecordName;
            FromPhone = call.FromPhone;
            ToPhone = call.ToPhone;
            Created = call.Created;
            Updated = call.Updated;
            IsFinal = call.IsFinal;
        }

        public override string ToString()
        {
            return string.Format("[{0}] TelephonyCallId: {1}, ParentTelephonyCallId: {2}, TelephonyAccountId: {3}, Username: {4}, Status: {5}",
                                 GetType().FullName, TelephonyCallId, ParentTelephonyCallId, TelephonyAccountId, Username, Status);
        }
    }
}
