using System;
using System.Collections.Generic;
using O2.Telephony.Models;

namespace O2.Telephony.Dal.Interfaces
{
    public interface ICreditDal
    {
        CreditTransaction Create(Guid accountId, Guid? callId, TransactionType type, string username, string processedBy,
            int actualSeconds, string orderId);

        CreditTransaction Read(int id);

        CreditSummary ReadSummary(Guid accountId);

        int ReadAvailable(Guid accountId);

        IEnumerable<CreditTransaction> ReadTransactions(Guid accountId);
    }
}
