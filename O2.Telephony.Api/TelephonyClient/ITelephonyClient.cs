using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using O2.Telephony.Models;
using O2.Telephony.Models.CallerId;
using O2.Telephony.Models.OutboundCall;
using O2.Telephony.Models.TimeZone;

namespace O2.Telephony.Api
{
	/// <summary>
	/// Telephony Interface
	/// </summary>
	public interface ITelephonyClient
	{
		#region Account

		Account GetAccount(Guid accountId);
	    Guid CreateAccount(Guid parentAccountId);

		#endregion

		#region Phone Numbers

		IEnumerable<PhoneNumberAvailable> FindLocalPhoneNumbers(Guid accountId, PhoneNumberAvailableFilterLocal filter);
		PhoneNumberResult CreatePhoneNumber(Guid accountId, PhoneNumberOption option);

		#endregion

		#region Caller ID

		IEnumerable<CallerId> GetCallerIds(Guid accountId, string phoneNumber = null);
        CallerId GetCurrentCallerId(Guid accountId);
		CallerId GetCallerId(Guid accountId, Guid callerIdId);
		AddCallerIdResult AddCallerId(Guid accountId, AddCallerIdOptions options);
		UpdateCallerIdResult UpdateCallerId(UpdateCallerIdOptions options);
		RemoveCallerIdResult RemoveCallerId(Guid accountId, Guid callerIdId, string providerId = null);
		CallerIdVerificationStatusResult GetCallerIdVerificationStatus(Guid telephonyAccountId, Guid telephonyCallerIdId);
		PageOfT<CallerIdSearchResult> SearchForCallerIds(Guid accountId, string phoneNumber, string friendlyName, int? page = 1, int? perPage = 25);

		#endregion

		#region Outbound Calling

		OutboundCallResult MakeSingleOutboundCall(OutboundCallOptions options);
	    OutboundCallResult MakeMultipleOutboundCallParent(OutboundCallOptions options);
	    OutboundCallResult MakeMultipleOutboundCallChild(OutboundCallOptions options);
	    TelephonyResult EndSingleOutboundCall(Guid telephonyAccountId, Guid telephonyCallId);
	    TelephonyResult EndParentMultipleOutboundCall(Guid telephonyAccountId, Guid telephonyCallId);
        TelephonyResult EndChildMultipleOutboundCall(Guid telephonyAccountId, Guid telephonyCallId);
	    TelephonyResult ExtendCallPause(PauseCallOptions options);
	    TelephonyResult DisconnectParentCall(Guid telephonyAccountId, Guid telephonyCallId);
        OutboundCallResult GetOutboundCallStatus(Guid telephonyAccountId, Guid telephonyCallId);
        OutboundCallResult GetOutboundChildCallStatus(Guid telephonyAccountId, Guid telephonyCallId, string toPhoneNumber, string fromPhoneNumber, DateTime? callStartTime);
	    string GetLocalTime(string latitude, string longitude);
	    LatitudeLongitude GetGeoLatLng(string zipCode);
        TelephonyResult<PageOfT<CallLog>> GetOutboundCallLogPaged(Guid telephonyAccountId, int page = 1, int itemsPerPage = 25, bool? isFinal = null, bool? childOnly = null);
		TelephonyResult<string> GetLastOutboundParentCallStatus(Guid telephonyAccountId);
        string GetTimeZoneDescription(string zipCode);

		#endregion

        #region Credit Logic

        CreditTransaction AdjustCredits(Guid telephonyAccountId, AdjustmentType type, string username, string processedBy,
	        int minutes, string orderId);

        CreditTransaction DeductCredits(Guid telephonyAccountId, Guid? telephonyCallId, string username, string processedBy, int seconds,
	        string orderId);

	    CreditTransaction Read(int id);

        CreditSummary ReadSummary(Guid telephonyAccountId);

        int ReadAvailable(Guid telephonyAccountId);

	    IEnumerable<CreditTransaction> ReadTransactions(Guid telephonyAccountId);

	    #endregion
	}
}
