namespace O2.Telephony.Models.CallerId
{
    public class UpdateCallerIdResult<T> : UpdateCallerIdResult
    {
        #region Public Properties

        public T Value { get; set; }

        #endregion

        #region Constructors

        public UpdateCallerIdResult(CallerIdResultCode code, string message = null)
            : base(code, message)
        {
            if (code == CallerIdResultCode.InvalidParameter)
            {
                ErrorMessage = string.Format("Invalid parameter {0}", message);
            }
        }

        public UpdateCallerIdResult(T value)
        {
            Value = value;
        }

        #endregion
    }
}
