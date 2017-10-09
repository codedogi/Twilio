using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.Telephony.Models
{
    public class CreditSummary
    {
        public Guid TelephonyAccountId { get; set; }
        public int MinutesAvailable { get; set; }
        public int MinutesUsed { get; set; }
        public int MinutesUsedPast30 { get; set; }
        public int MinutesUsedPastYear { get; set; }
    }
}
