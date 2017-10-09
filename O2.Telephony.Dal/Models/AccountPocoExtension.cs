using O2.Telephony.Models;

namespace O2.Telephony.Dal.Models
{
    public static class AccountPocoExtension
    {
        internal static Account ToModel(this AccountPoco accountPoco)
        {
            if (accountPoco == null)
            {
                return null;
            }

            return new Account
            {
                Id = accountPoco.Id,
                Node = accountPoco.Node,
                NodeLevel = accountPoco.NodeLevel,
                Status = (AccountStatusCode)accountPoco.Status,
                Created = accountPoco.Created,
                Updated = accountPoco.Updated
            };

        }
    }
}
