namespace O2.Telephony.Models
{
    public class AccountResult<T> : AccountResult
    {
        #region Public Properties
        public T Value { get; set; }
        #endregion

        #region Constructors
        public AccountResult(AccountResultCode code, string message = null)
            : base(code, message)
        {
            if (code == AccountResultCode.InvalidParameter)
            {
                ErrorMessage = string.Format("Invalid parameter {0}", message);
            }
        }

        public AccountResult(T value)
        {
            Value = value;
        }
        #endregion
    }
}
