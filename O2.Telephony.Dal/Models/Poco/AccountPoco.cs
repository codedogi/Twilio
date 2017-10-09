// ReSharper disable CheckNamespace
using System;
using O2.Telephony.Dal.PetaPoco;

namespace O2.Telephony.Dal.Models
{
    [TableName("Account")]
    [PrimaryKey("Id", autoIncrement = false)]
    internal partial class AccountPoco
    {
        public Guid Id { get; set; }
        public string Node { get; set; }
        [ResultColumn]
        public Int16 NodeLevel { get; set; }
        public byte Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
// ReSharper restore CheckNamespace
