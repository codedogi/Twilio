namespace O2.Telephony.Provider.Models
{
    public class AccountResult
    {
        #region Public Properties
        public AccountResultCode ResultCode { get; set; }
        public bool HasError { get { return ResultCode != AccountResultCode.Success; } }
        public string ErrorMessage { get; set; }
        #endregion Public Properties

        #region Constructors
        public AccountResult()
        {
            ResultCode = AccountResultCode.Success;
        }

        public AccountResult(AccountResultCode code, string message = null)
        {
            ResultCode = code;
            ErrorMessage = message;
        }
        #endregion Constructors
    }
}
