using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.Telephony.Models.OutboundCall
{
	public class OutboundCall
	{
		#region Public Properties

		public Guid Id { get; set; }
		public Guid AccountId { get; set; }
		public DateTime Created { get; set; }

		#endregion

		#region Public Methods

		public override string ToString()
		{
			return string.Format("[{0}] Id: {1}, AccountId: {2}, Created: {3}",
				GetType().FullName,
				Id,
				AccountId,
				Created);
		}

		#endregion

	}
}
