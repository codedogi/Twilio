using System;
using System.Collections.Generic;
using O2.Telephony.Models;
using O2.Telephony.Models.CallerId;
using O2.Telephony.Models.OutboundCall;

namespace O2.Telephony.Logic.Interfaces
{
	public interface IPhoneLogic
	{
		//create
		PhoneNumberResult CreateIncomingPhoneNumber(Guid accountId, PhoneNumberOption option);

		//read
		PhoneNumberAvailableResult<IEnumerable<PhoneNumberAvailable>> GetAvailableLocalPhoneNumbers(Guid accountId,
																									PhoneNumberAvailableFilterLocal filter);

		PhoneNumberAvailableResult<IEnumerable<PhoneNumberAvailable>> GetAvailableTollFreePhoneNumbers(Guid accountId,
																									   PhoneNumberAvailableFilterTollFree filter);

        TelephonyResult<PageOfT<CallLog>> GetOutboundCallLogPaged(Guid telephonyAccountId, bool childOnly, int page = 1, int itemsPerPage = 25, bool? isFinal = null);

	    CallLog GetCallLog(Guid telephonyCallId);

		//update
	    CallLog UpdateCallLog(Guid telephonyCallId, string callSid);

		//delete
		//process
		TelephonyResult<string> GetLastOutboundCallStatusForAccount(Guid telephonyAccountId);

		#region Outbound Calling

		TelephonyResult<OutboundCallResult> MakeSingleOutboundCall(OutboundCallOptions options);
		TelephonyResult<OutboundCallResult> GetOutboundCallStatus(Guid telephonyAccountId, Guid telephonyCallId);
		TelephonyResult<OutboundCallResult> GetOutboundCallChildStatus(OutboundCallStatusOptions options);
	    TelephonyResult EndSingleOutboundCall(Guid telephonyAccountId, Guid telephonyCallId);
        TelephonyResult EndChildMultipleOutboundCall(Guid telephonyAccountId, Guid telephonyCallId);
        TelephonyResult DisconnectParentCall(Guid telephonyAccountId, Guid telephonyCallId);

		#endregion
        
		#region Caller ID

		TelephonyResult<IEnumerable<CallerId>> GetOutgoingCallerIds(Guid accountId, string phoneNumber = null);
		TelephonyResult<CallerId> GetOutgoingCallerId(Guid accountId, Guid callerIdId);
	    TelephonyResult<CallerId> GetCurrentOutgoingCallerId(Guid accountId);
        AddCallerIdResult AddOutgoingCallerId(AddCallerIdOptions options);
		RemoveCallerIdResult RemoveOutgoingCallerId(Guid accountId, Guid callerIdId, string providerId = null);
		UpdateCallerIdResult UpdateOutgoingCallerId(UpdateCallerIdOptions options);
		CallerIdVerificationStatusResult GetOutgoingCallerIdVerificationStatus(Guid telephonyCallerIdId);
		TelephonyResult<PageOfT<CallerIdSearchResult>> SearchForOutgoingCallerIds(Guid accountId, string phoneNumber, string friendlyName, int? page, int? perPage);

		#endregion

	}
}
