using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.Telephony.Models.Credits
{
    public class AdjustmentOptions
    {
        public AdjustmentType AdjustmentType { get; set; }
        public string Username { get; set; }
        public string ProcessedBy { get; set; }
        public int Minutes { get; set; }
        public string OrderId { get; set; }
    }
}
