using System;
using System.Collections.Generic;

namespace O2.Telephony.Models.OutboundCall
{
	public class OutboundCallOptions
	{
		public Guid TelephonyCallId { get; set; }
		public Guid TelephonyAccountId { get; set; }
		public string FromNumber { get; set; }
		public string ToNumber { get; set; }
		public int? NoAnswerTimeout { get; set; }
		public List<string> Say { get; set; }
		public bool HangupIfMachine { get; set; }
        
        public string Username { get; set; }
        public string SalesRepName { get; set; }
        public string DivisionNumber { get; set; }
        public bool IsEnterprise { get; set; }
        public ProductTypeCode ProductType { get; set; }
        public int? CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string TimeZoneZip { get; set; }
        public CallRecordType RecordType { get; set; }
        public string RecordId { get; set; }
        public string RecordName { get; set; }
	}
}
