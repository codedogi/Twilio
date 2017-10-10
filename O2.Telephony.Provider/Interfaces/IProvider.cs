using System;
using System.Collections.Generic;
using O2.Telephony.Models;
using O2.Telephony.Provider.Models;
using O2.Telephony.Provider.Models.CallerId;
using AccountResult = O2.Telephony.Provider.Models.AccountResult;
using CallLog = O2.Telephony.Provider.Models.CallLog;
using PauseCallOptions = O2.Telephony.Provider.Models.PauseCallOptions;
using PhoneNumberAvailable = O2.Telephony.Provider.Models.PhoneNumberAvailable;
using PhoneNumberAvailableFilterLocal = O2.Telephony.Provider.Models.PhoneNumberAvailableFilterLocal;
using PhoneNumberAvailableFilterTollFree = O2.Telephony.Provider.Models.PhoneNumberAvailableFilterTollFree;
using PhoneNumberOption = O2.Telephony.Provider.Models.PhoneNumberOption;
using PhoneNumberResult = O2.Telephony.Provider.Models.PhoneNumberResult;
using TelephonyResult = O2.Telephony.Provider.Models.TelephonyResult;
using TwilioApi = Twilio;

namespace O2.Telephony.Provider.Interfaces
{
    public interface IProviderLogic
    {
        //create
        AccountResult CreateAccount(Guid accountId, Guid parentAccountId);
        PhoneNumberResult CreateIncomingPhoneNumber(Guid accountId, PhoneNumberOption option);

        //read
        Models.PhoneNumberAvailableResult<IEnumerable<PhoneNumberAvailable>> GetAvailableLocalPhoneNumbers(Guid accountId, PhoneNumberAvailableFilterLocal filter);
        Models.PhoneNumberAvailableResult<IEnumerable<PhoneNumberAvailable>> GetAvailableTollFreePhoneNumbers(Guid accountId, PhoneNumberAvailableFilterTollFree filter);

        //update
        //delete
        //process
		Models.TelephonyResult<OutboundCall> GetOutboundCallStatus(Guid accountId, Guid callId);
		Models.TelephonyResult<OutboundCall> GetOutboundChildCallStatus(Guid telephonyAccountId, Guid telephonyCallId, string toPhoneNumber, string fromPhoneNumber, DateTime? callStartTime);
        TelephonyResult MakeOutboundCall(Guid accountId, OutboundCallOption option);
        Models.TelephonyResult<CallLog> MakeSingleOutboundCall(Guid accountId, OutboundCallOption option);
        Models.TelephonyResult<CallLog> MakeMultipleOutboundCallParent(Guid accountId, OutboundCallOption option);
        Models.TelephonyResult<OutboundCall> MakeMultipleOutboundCallChild(Guid accountId, OutboundCallOption option);
        TwilioApi.TwiML.TwilioResponse ConnectOutboundCall(Guid accountid, string phone);

        TwilioApi.TwiML.TwilioResponse CallChildStartSingle(Guid accountid, Guid telephonyCallId, string phoneTo, string callerId, string say);
        TwilioApi.TwiML.TwilioResponse CallChildStartMultiple(Guid accountid, Guid telephonyCallId, string phoneTo, string callerId, string say);
        TwilioApi.TwiML.TwilioResponse CallChildEndSingle(Guid accountId, Guid telephonyCallId, string dialSid);
        TwilioApi.TwiML.TwilioResponse CallChildEndMultiple(Guid accountId, Guid telephonyCallId, string dialSid);
        TwilioApi.TwiML.TwilioResponse CallParentExtendPause(Guid accountId, Guid telephonyCallId, string dialSid, string data);
        TwilioApi.TwiML.TwilioResponse CallParentDisconnect(Guid accountid, Guid telephonyCallId, string dialSid);
        TwilioApi.TwiML.TwilioResponse EmptyResponse();
        void CallParentEnd(Guid accountId, Guid telephonyCallId, string callSid);

        TelephonyResult EndSingleOutboundCall(Guid accountId, Guid telephonyCallId);
        TelephonyResult EndParentMultipleOutboundCall(Guid accountId, Guid telephonyCallId);
        TelephonyResult EndChildMultipleOutboundCall(Guid accountId, Guid telephonyCallId);
		TelephonyResult ExtendPauseOutboundCall(PauseCallOptions options);
        TelephonyResult DisconnectParentCall(Guid accountId, Guid telephonyCallId);

        string VerifyTwilio(string twilioId, string twilioToken);
        CallLog GetCallLog(Guid telephonyAccountid, string callSid);

		#region Caller ID

	    Models.TelephonyResult<IEnumerable<CallerId>> GetOutgoingCallerIds(Guid accountId, string phoneNumber = null);
		Models.TelephonyResult<CallerId> GetOutgoingCallerId(Guid accountId, Guid callerIdId);
        Models.TelephonyResult<CallerId> GetCurrentOutgoingCallerId(Guid accountId);
		AddCallerIdResult AddOutgoingCallerId(AddCallerIdOptions options);
		RemoveCallerIdResult RemoveOutgoingCallerId(Guid telephonyAccountId, Guid telephonyCallerIdId, string providerId = null);
	    UpdateCallerIdResult UpdateOutgoingCallerId(UpdateCallerIdOptions options);
	    UpdateCallerIdResult UpdateOutgoingCallerIdVerificationStatus(UpdateCallerIdVerificationOptions options);
		CallerIdVerificationStatusResult GetOutgoingCallerIdVerificationStatus(Guid telephonyCallerIdId);
		Models.TelephonyResult<Models.PageOfT<CallerIdSearchResult>> SearchForOutgoingCallerIds(Guid accountId, string phoneNumber, string friendlyName, int? page, int? perPage);
	    #endregion

    }
}
