using System;

namespace O2.Telephony.Models.CallerId
{
	public class CallerId
	{
		#region Public Properties

		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Updated { get; set; }
		public string FriendlyName { get; set; }
		public Guid AccountId { get; set; }
		public string PhoneNumber { get; set; }
		public CallerIdStatus Status { get; set; }

		#endregion

		#region Public Methods

		public override string ToString()
		{
			return string.Format("[{0}] Id: {1}, Created: {2}, Updated: {3}, FriendlyName: {4}, AccountId: {5}, PhoneNumber: {6}, Status: {7}", 
				GetType().FullName,
				Id,
				Created,
				Updated.HasValue ? Updated.ToString() : "<null>",
				FriendlyName,
				AccountId,
				PhoneNumber,
				Status.ToString());
		}

		#endregion
	}
}
