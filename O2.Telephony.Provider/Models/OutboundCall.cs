using System;

namespace O2.Telephony.Provider.Models
{
	public class OutboundCall
	{
		#region Public Properties

		public Guid Id { get; set; }
		public Guid AccountId { get; set; }
		public string CallStatus { get; set; }
		public DateTime Created { get; set; }
		public DateTime? StartTime { get; set; }
		public int? Duration { get; set; }
	
		#endregion

		#region Public Methods

	    public override string ToString()
	    {
	        return string.Format("[{0}] Id: {1}, AccountId: {2}, CallStatus: {3}, Created: {4}", 
                GetType().FullName, Id, AccountId, CallStatus, Created);
	    }

	    #endregion

	}
}
