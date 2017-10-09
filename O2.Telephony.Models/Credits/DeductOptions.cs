using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.Telephony.Models.Credits
{
    public class DeductOptions
    {
        public Guid? TelephonyCallId { get; set; }
        public string Username { get; set; }
        public string ProcessedBy { get; set; }
        public int Seconds { get; set; }
        public string OrderId { get; set; }
    }
}
