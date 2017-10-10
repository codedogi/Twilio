namespace O2.Telephony.Provider.Models
{
    public class PhoneNumberResult
    {
        #region Public Properties

        public PhoneNumberResultCode ResultCode { get; set; }

        public bool HasError
        {
            get { return ResultCode != PhoneNumberResultCode.Success; }
        }

        public string ErrorMessage { get; set; }

        #endregion Public Properties

        #region Constructors

        public PhoneNumberResult()
        {
            ResultCode = PhoneNumberResultCode.Success;
        }

        public PhoneNumberResult(PhoneNumberResultCode code, string message = null)
        {
            ResultCode = code;

            if (code == PhoneNumberResultCode.InvalidParameter)
            {
                ErrorMessage = string.Format("Invalid parameter {0}", message);
            }
            else
            {
                ErrorMessage = message;
            }
        }

        #endregion Constructors
    }
}
