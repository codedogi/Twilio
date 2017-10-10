using System;

namespace O2.Telephony.Provider.Models.CallerId
{
	public class CallerIdSearchResult
	{
		#region Public Properties

		public Guid? Id { get; set; }
		public Guid? AccountId { get; set; }
		public string FriendlyName { get; set; }
		public string PhoneNumber { get; set; }
		public string ProviderId { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }

		#endregion

		#region Public Methods


		#endregion
	}
}
