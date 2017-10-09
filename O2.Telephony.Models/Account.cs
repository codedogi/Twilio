using System;

namespace O2.Telephony.Models
{
    public class Account
    {
        #region Public Properties

        public Guid Id { get; set; }
        public string Node { get; set; }
        public Int16 NodeLevel { get; set; }
        public AccountStatusCode Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return
                string.Format(
                    "[{0}] Id: {1}, Node: {2}, NodeLevel: {3}, Status: {4}, Created: {5}, Updated: {6}", GetType().FullName, Id, Node, NodeLevel,
                    Status, Created, Updated);
        }

        #endregion
    }
}
