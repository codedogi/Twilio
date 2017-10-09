using System;
using System.Collections.Generic;
using O2.Telephony.Models;
using O2.Telephony.Models.OutboundCall;

namespace O2.Telephony.Dal.Interfaces
{
	/// <summary>
	/// Interface for Phone Data
	/// </summary>
	public interface IPhoneDal
	{

		#region Caller ID

		Guid CreateOutgoingCallerId(Guid accountId, Guid callerIdId);
		bool DeleteOutgoingCallerId(Guid callerIdId);

		#endregion

		#region Outgoing Calls

		Guid CreateOutgoingCall(OutboundCall outboundCall);
		OutboundCall ReadOutgoingCall(Guid callId);

		#endregion

        #region Call

	    void Create(CallLog callLog);
	    void Update(CallLog callLog, IEnumerable<string> columns);
	    CallLog ReadCallLog(Guid telephonyCallId);
        PageOfT<CallLog> ReadCallLogPaged(Guid telephonyAccountId, bool childOnly, int page = 1, int itemsPerPage = 25, bool? isFinal = null, DateTime? fromDate = null,
	                                      DateTime? toDate = null, string status = null);
		CallLog ReadLastOutgoingParentCallForAccount(Guid telephonyAccountId);


		#endregion
	}
}
