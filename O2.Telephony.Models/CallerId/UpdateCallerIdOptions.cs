using System;

namespace O2.Telephony.Models.CallerId
{
	public class UpdateCallerIdOptions
	{
		#region Public Properties

		public Guid TelephonyAccountId { get; set; }
		public Guid TelephonyCallerIdId { get; set; }
		public string FriendlyName { get; set; }

		#endregion

		#region Public Methods

		public override string ToString()
		{
			return string.Format("[{0}] TelephonyAccountId: {1}, TelephonyCallerIdId: {2}, FriendlyName: {3}", 
				GetType().FullName, 
				TelephonyAccountId,
				TelephonyCallerIdId,
				FriendlyName);
		}

		#endregion
	}
}
