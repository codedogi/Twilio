using System;

namespace O2.Telephony.Provider.Models.CallerId
{
	public class UpdateCallerIdVerificationOptions
	{
		#region Public Properties

		public Guid TelephonyAccountId { get; set; }
		public Guid TelephonyCallerIdId { get; set; }
		public string CallerIdSid { get; set; }
		public string Status { get; set; }
		public string Callback { get; set; }

		#endregion

		#region Public Methods

		public override string ToString()
		{
			return string.Format("[{0}] TelephonyAccountId: {1}, TelephonyCallerIdId: {2}, CallerIdSid: {3}, Status: {4}, Callback: {5}", 
				GetType().FullName, 
				TelephonyAccountId,
				TelephonyCallerIdId,
				CallerIdSid,
				Status,
				Callback);
		}

		#endregion
	}
}
