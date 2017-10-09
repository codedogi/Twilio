using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2.Telephony.Models.OutboundCall
{
	public class OutboundCallResult
	{
		public Guid TelephonyCallId { get; set; }
		public Guid TelephonyAccountId { get; set; }
		public string CallStatus { get; set; }
		public DateTime? StartTime { get; set; }
		public int? Duration { get; set; }
	}
}
