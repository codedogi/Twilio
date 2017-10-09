namespace O2.Telephony.Models.CallerId
{
    public class AddCallerIdResult<T> : AddCallerIdResult
    {
        #region Public Properties

        public T Value { get; set; }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return string.Format("[{0}] ValidationCode: {1}, TelephonyCallerIdId: {2}", GetType().FullName, ValidationCode, TelephonyCallerIdId);
        }

        #endregion

        #region Constructors

        public AddCallerIdResult(CallerIdResultCode code, string message = null)
            : base(code, message)
        {
            if (code == CallerIdResultCode.InvalidParameter)
            {
                ErrorMessage = string.Format("Invalid parameter {0}", message);
            }
        }

        public AddCallerIdResult(T value)
        {
            Value = value;
        }

        #endregion
    }
}
