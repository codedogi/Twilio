using System;

namespace O2.Telephony.Provider.Models.CallerId
{
	public class RemoveCallerIdResult : BaseResult<CallerIdResultCode>
	{
		#region Public Properties

		public override bool HasError
		{
			get { return ResultCode != CallerIdResultCode.Success;  }
		}

		#endregion

		#region Public Methods

		public override string ToString()
		{
			return string.Format("[{0}]", GetType().FullName);
		}

		#endregion

		#region Constructors
		
		public RemoveCallerIdResult()
		{
			
		}

		public RemoveCallerIdResult(CallerIdResultCode code, string message = null)
		{
			ResultCode = code;
			ErrorMessage = message;
		}

		#endregion

	}
}
