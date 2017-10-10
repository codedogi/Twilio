using System;

namespace O2.Telephony.Provider.Models.CallerId
{
	public class CallerId
	{
		#region Public Properties

		public Guid Id { get; set; }
		public Guid AccountId { get; set; }
		public string FriendlyName { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }

		#endregion

		#region Public Methods

		public override string ToString()
		{
			return string.Format("[{0}] Id: {1}, AccountSid: {2}, DateCreated: {3}, DateUpdated: {4}, FriendlyName: {5}, PhoneNumber: {6}", 
				GetType().FullName,
				Id,
				AccountId,
				DateCreated,
				DateUpdated.HasValue ? DateUpdated.ToString() : "<null>",
				FriendlyName,
				PhoneNumber);
		}

		#endregion
	}
}
