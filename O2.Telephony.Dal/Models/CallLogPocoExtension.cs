using O2.Telephony.Models;

namespace O2.Telephony.Dal.Models
{
    public static class CallLogPocoExtension
    {
        internal static CallLog ToModel(this CallLogPoco callPoco)
        {
            if (callPoco == null)
            {
                return null;
            }

            return new CallLog
                       {
                           TelephonyCallId = callPoco.TelephonyCallId,
                           ParentTelephonyCallId = callPoco.ParentTelephonyCallId,
                           TelephonyAccountId = callPoco.TelephonyAccountId,
                           Username = callPoco.Username,
                           SalesRepName = callPoco.SalesRepName,
                           DivisionNumber = callPoco.DivisionNumber,
                           IsEnterprise = callPoco.IsEnterprise,
                           ProductType = (ProductTypeCode)callPoco.ProductType,
                           SessionType = (CallSessionType)callPoco.SessionType,
                           CampaignId = callPoco.CampaignId,
                           CampaignName = callPoco.CampaignName,
                           Status = ConvertCallStatus(callPoco.Status),
                           DurationTotalSeconds = callPoco.DurationTotalSeconds,
                           DurationRoundedMinutes = callPoco.DurationRoundedMinutes,
                           DateUtc = callPoco.DateUtc,
                           StartTimeUtc = callPoco.StartTimeUtc,
                           EndTimeUtc = callPoco.EndTimeUtc,
                           LocalTimeZoneAbbr = callPoco.LocalTimeZoneAbbr,
                           LocalTimeTotalOffset = callPoco.LocalTimeTotalOffset,
                           DateLocal = callPoco.DateLocal,
                           StartTimeLocal = callPoco.StartTimeLocal,
                           EndTimeLocal = callPoco.EndTimeLocal,
                           RecordType = callPoco.RecordType.HasValue ? (CallRecordType) callPoco.RecordType.Value : CallRecordType.Unknown,
                           RecordId = callPoco.RecordId,
                           RecordName = callPoco.RecordName,
                           FromPhone = callPoco.FromPhone,
                           ToPhone = callPoco.ToPhone,
                           Created = callPoco.Created,
                           Updated = callPoco.Updated,
                           IsFinal = callPoco.IsFinal,
                       };
        }

        internal static CallStatusType ConvertCallStatus(string callStatus)
        {
            switch (callStatus.ToLower())
            {
                case "beforequeued":
                    return CallStatusType.BeforeQueued;
                case "queued":
                    return CallStatusType.Queued;
                case "ringing":
                    return CallStatusType.Ringing;
                case "inprogress":
                    return CallStatusType.InProgress;
                case "completed":
                    return CallStatusType.Completed;
                case "busy":
                    return CallStatusType.Busy;
                case "failed":
                    return CallStatusType.Failed;
                case "noanswer":
                    return CallStatusType.NoAnswer;
                case "canceled":
                    return CallStatusType.Canceled;
                default:
                    return CallStatusType.Unknown;
            }
        }
    }
}
