namespace O2.Telephony.Models
{
    public class TelephonyResult<T> : TelephonyResult
    {
        #region Public Properties

        public T Value { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override string ToString()
        {
            return string.Format("[{0} - ResultCode: {1}, ErrorMessage: {2}]", GetType().FullName, ResultCode.ToString(), ErrorMessage);
        }

        public TelephonyResult(TelephonyResultCode code, string message = null)
            : base(code, message)
        {
            if (code == TelephonyResultCode.InvalidParameter)
            {
                ErrorMessage = string.Format("Invalid parameter {0}", message);
            }
        }

        #endregion

        #region Constructors

		public TelephonyResult()
		{
		}

        public TelephonyResult(T value)
        {
            Value = value;
        }

        #endregion Constructors
    }
}
