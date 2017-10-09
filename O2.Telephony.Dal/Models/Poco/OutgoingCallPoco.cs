// ReSharper disable CheckNamespace
using System;
using O2.Telephony.Dal.PetaPoco;
using O2.Telephony.Models.OutboundCall;

namespace O2.Telephony.Dal.Models
{
	[TableName("OutgoingCalls")]
	[PrimaryKey("Id", autoIncrement = false)]
	internal partial class OutgoingCallPoco
	{
		public Guid Id { get; set; }
		public Guid AccountId { get; set; }
		public DateTime Created { get; set; }

		public OutgoingCallPoco()
		{ }

		internal OutgoingCallPoco(OutboundCall callerId)
		{
			Id = callerId.Id;
			AccountId = callerId.AccountId;
			Created = callerId.Created;
		}

		internal OutboundCall ToModel()
		{
			return new OutboundCall
			{
				Id = Id,
				AccountId = AccountId,
				Created = Created,
			};

		}

		public override string ToString()
		{
			return string.Format(
				"[{0}] Id: {1}, AccountId: {2}, Created: {3}",
				GetType().FullName,
				Id,
				AccountId,
				Created);
		}
	}
}
// ReSharper restore CheckNamespace
