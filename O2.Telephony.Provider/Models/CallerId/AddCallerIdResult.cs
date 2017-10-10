using System;

namespace O2.Telephony.Provider.Models.CallerId
{
	public class AddCallerIdResult : BaseResult<CallerIdResultCode>
	{
		#region Public Properties

		public Guid TelephonyCallerIdId { get; set; }
		public string ValidationCode { get; set; }

		public override bool HasError
		{
			get { return ResultCode != CallerIdResultCode.Success; }
		}

		#endregion

		#region Public Methods

		public override string ToString()
		{
			return string.Format("[{0}] ValidationCode: {1}, TelephonyCallerIdId: {2}", GetType().FullName, ValidationCode, TelephonyCallerIdId);
		}

		#endregion

		#region Constructors

		public AddCallerIdResult()
		{

		}

		public AddCallerIdResult(CallerIdResultCode code, string message = null)
		{
			ResultCode = code;
			ErrorMessage = message;
		}

		#endregion
	}
}
