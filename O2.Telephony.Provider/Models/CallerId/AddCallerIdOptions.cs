using System;

namespace O2.Telephony.Provider.Models.CallerId
{
	public class AddCallerIdOptions
	{
		#region Public Properties

		public Guid TelephonyAccountId { get; set; }
		public Guid TelephonyCallerIdId { get; set; }
		public string PhoneNumberToVerify { get; set; }
		public string FriendlyName { get; set; }
		public int VerifyDelay { get; set; }

		#endregion

		#region Public Methods

		public override string ToString()
		{
			return string.Format("[{0}] TelephonyAccountId: {1}, TelephonyCallerIdId: {2}, PhoneNumberToVerify: {3}, FriendlyName: {4}, VerifyDelay: {5}", 
				GetType().FullName,
				TelephonyAccountId,
				TelephonyCallerIdId,
				PhoneNumberToVerify,
				FriendlyName,
				VerifyDelay);
		}

		#endregion
	}
}
