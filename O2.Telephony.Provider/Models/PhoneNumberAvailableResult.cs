namespace O2.Telephony.Provider.Models
{
    public class PhoneNumberAvailableResult
    {
        #region Public Properties

        public PhoneNumberAvailableResultCode ResultCode { get; set; }

        public bool HasError
        {
            get { return ResultCode != PhoneNumberAvailableResultCode.Success; }
        }

        public string ErrorMessage { get; set; }

        #endregion Public Properties

        #region Constructors

        public PhoneNumberAvailableResult()
        {
            ResultCode = PhoneNumberAvailableResultCode.Success;
        }

        public PhoneNumberAvailableResult(PhoneNumberAvailableResultCode code, string message = null)
        {
            ResultCode = code;
            ErrorMessage = message;
        }

        #endregion Constructors
    }
}
