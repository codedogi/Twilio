using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2.Telephony.Models.OutboundCall
{
	public class OutboundCallStatusOptions
	{
		public Guid TelephonyAccountId { get; set; }
		public Guid TelephonyCallId { get; set; }
		public string FromNumber { get; set; }
		public string ToNumber { get; set; }
		public DateTime? CallStartTime { get; set; }
	}
}
