using System;

namespace O2.Telephony.Provider.Models.CallerId
{
	public class CallerIdVerificationStatusResult : BaseResult<CallerIdResultCode>
	{
		#region Public Properties

		public Guid TelephonyAccountId { get; set; }
		public Guid TelephonyCallerIdId { get; set; }
		public bool ReceivedVerification { get; set; }
		public bool VerificationStatus { get; set; }

		public override bool HasError
		{
			get { return ResultCode != CallerIdResultCode.Success;  }
		}

		#endregion

		#region Public Methods

		public override string ToString()
		{
			return string.Format("[{0}], ReceivedVerification: {1}, VerificationStatus: {2}", GetType().FullName, ReceivedVerification, VerificationStatus);
		}

		#endregion

		#region Constructors
		
		public CallerIdVerificationStatusResult()
		{
			
		}

		public CallerIdVerificationStatusResult(CallerIdResultCode code, string message = null)
		{
			ResultCode = code;
			ErrorMessage = message;
		}

		#endregion

	}
}
