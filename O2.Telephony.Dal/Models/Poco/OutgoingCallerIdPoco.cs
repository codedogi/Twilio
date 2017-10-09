// ReSharper disable CheckNamespace
using System;
using O2.Telephony.Dal.PetaPoco;
using O2.Telephony.Models.CallerId;

namespace O2.Telephony.Dal.Models
{
	[TableName("OutgoingCallerId")]
	[PrimaryKey("Id", autoIncrement = false)]
	internal partial class OutgoingCallerIdPoco
	{
		public Guid Id { get; set; }
		public Guid AccountId { get; set; }
		public byte Status { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Updated { get; set; }
		public DateTime? Deleted { get; set; }

		public OutgoingCallerIdPoco()
		{ }

		internal OutgoingCallerIdPoco(CallerId callerId)
		{
			Id = callerId.Id;
			AccountId = callerId.AccountId;
			Status = (byte) callerId.Status;
			Created = callerId.Created;
			Updated = callerId.Updated;
		}

		internal CallerId ToModel()
		{
			return new CallerId
			{
				Id = Id,
				AccountId = AccountId,
				Status = (CallerIdStatus)Status,
				Created = Created,
				Updated = Updated,
			};

		}

		public override string ToString()
		{
			return string.Format(
					"[{0}] Id: {1}, AccountId: {2}, PhoneNumber: {3}, Status: {4}, Created: {5}, Updated: {6}, Deleted: {7}",
					GetType().FullName, 
					Id, 
					AccountId,
					Status.ToString(),
					Created, 
					Updated != null ? Updated.ToString() : "<null>",
					Deleted != null ? Deleted.ToString() : "<null>");
		}
	}
}
// ReSharper restore CheckNamespace
