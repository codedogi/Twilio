namespace O2.Telephony.Models.CallerId
{
    public class RemoveCallerIdResult<T> : RemoveCallerIdResult
    {
        #region Public Properties

        public T Value { get; set; }

        #endregion

        #region Constructors

        public RemoveCallerIdResult(CallerIdResultCode code, string message = null)
            : base(code, message)
        {
            if (code == CallerIdResultCode.InvalidParameter)
            {
                ErrorMessage = string.Format("Invalid parameter {0}", message);
            }
        }

        public RemoveCallerIdResult(T value)
        {
            Value = value;
        }

        #endregion
    }
}
