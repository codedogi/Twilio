using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.Telephony.Models
{
    public enum TransactionType : byte
    {
        UserPurchased,
        AutoAllocation,
        ManualAllocation,
        QueueAllocation,
        Used,
        ExpiredAllocation
    }
}
