using O2.Telephony.Models;

namespace O2.Telephony.Dal.Models
{
    internal partial class AccountPoco : BasePoco
    {
        public AccountPoco()
        {}

        internal AccountPoco(Account account)
        {
            Id = account.Id;
            Node = account.Node;
            NodeLevel = account.NodeLevel;
            Status = (byte) account.Status;
            Created = account.Created;
            Updated = account.Updated;
        }

        public override string ToString()
        {
            return
                string.Format(
                    "[{0}] Id: {1}, Node: {2}, NodeLevel: {3}, Status: {4}, Created: {5}, Updated: {6}", GetType().FullName, Id, Node, NodeLevel,
                    Status, Created, Updated);
        }
    }
}
