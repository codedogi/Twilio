using System;
using System.Collections.Generic;
using O2.Telephony.Models;

namespace O2.Telephony.Logic.Interfaces
{
    public interface ICreditLogic
    {
        /// <summary>
        /// Adjusts the specified account identifier. As of now you can add/remove credits by minutes only
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="type">The debit type.</param>
        /// <param name="username">The username.</param>
        /// <param name="processedBy">The proccessed by.</param>
        /// <param name="minutes">The minutes.</param>
        /// <param name="orderId">The order identifier.</param>
        /// <returns></returns>
        CreditResult<CreditTransaction> AdjustCredits(Guid accountId, AdjustmentType type, string username,
            string processedBy, int minutes, string orderId);

        /// <summary>
        /// Deducts Credits from the specified account identifier. Processed in seconds, the seconds will be converted to minutes and rounded up to the nearest minute.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="callId">The call identifier.</param>
        /// <param name="username">The username.</param>
        /// <param name="processedBy">The processed by.</param>
        /// <param name="seconds">The seconds.</param>
        /// <param name="orderId">The order identifier.</param>
        /// <returns></returns>
        CreditResult<CreditTransaction> DeductCredits(Guid accountId, Guid? callId, string username, string processedBy,
            int seconds, string orderId);

        /// <summary>
        /// Reads the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        CreditResult<CreditTransaction> Read(int id);

        /// <summary>
        /// Reads the summary.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <returns></returns>
        CreditResult<CreditSummary> ReadSummary(Guid accountId);

        /// <summary>
        /// Reads the transactions.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <returns></returns>
        CreditResult<IEnumerable<CreditTransaction>> ReadTransactions(Guid accountId);

        /// <summary>
        /// Reads the available credits for the account.
        /// </summary>
        /// <param name="accountId">telephony account id</param>
        /// <returns>credit result containing the amount of credits available which can be negative</returns>
        CreditResult<int> ReadAvailable(Guid accountId);
    }
}
