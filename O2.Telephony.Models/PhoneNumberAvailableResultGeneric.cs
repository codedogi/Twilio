namespace O2.Telephony.Models
{
    public class PhoneNumberAvailableResult<T> : PhoneNumberAvailableResult
    {
        #region Public Properties

        public T Value { get; set; }

        #endregion

        #region Constructors

        public PhoneNumberAvailableResult(PhoneNumberAvailableResultCode code, string message = null)
            : base(code, message)
        {
            if (code == PhoneNumberAvailableResultCode.InvalidParameter)
            {
                ErrorMessage = string.Format("Invalid parameter {0}", message);
            }
        }

        public PhoneNumberAvailableResult(T value)
        {
            Value = value;
        }

        #endregion
    }
}
