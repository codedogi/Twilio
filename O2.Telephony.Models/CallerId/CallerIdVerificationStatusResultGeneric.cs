namespace O2.Telephony.Models.CallerId
{
    public class CallerIdVerificationStatusResult<T> : CallerIdVerificationStatusResult
    {
        #region Public Properties

        public T Value { get; set; }

        #endregion

        #region Constructors

        public CallerIdVerificationStatusResult(CallerIdResultCode code, string message = null)
            : base(code, message)
        {
            if (code == CallerIdResultCode.InvalidParameter)
            {
                ErrorMessage = string.Format("Invalid parameter {0}", message);
            }
        }

        public CallerIdVerificationStatusResult(T value)
        {
            Value = value;
        }

        #endregion
    }
}
