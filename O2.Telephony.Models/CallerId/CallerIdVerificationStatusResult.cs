using System;

namespace O2.Telephony.Models.CallerId
{
    public class CallerIdVerificationStatusResult : BaseResult
    {
        #region Public Properties

        public CallerIdResultCode ResultCode { get; set; }
        public Guid TelephonyAccountId { get; set; }
        public Guid TelephonyCallerIdId { get; set; }
        public bool ReceivedVerification { get; set; }
        public bool VerificationStatus { get; set; }

        public override bool HasError
        {
            get { return ResultCode != CallerIdResultCode.Success; }
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return string.Format("[{0}], ReceivedVerification: {1}, VerificationStatus: {2}", GetType().FullName, ReceivedVerification,
                                 VerificationStatus);
        }

        #endregion

        #region Constructors

        public CallerIdVerificationStatusResult()
        {
            ResultCode = CallerIdResultCode.Success;
        }

        public CallerIdVerificationStatusResult(CallerIdResultCode code, string message = null)
        {
            ResultCode = code;
            ErrorMessage = message;
        }

        #endregion

    }
}
