namespace O2.Telephony.Provider.Models
{
    public class TelephonyResult<T> : TelephonyResult
    {
        #region Public Properties

        public T Value { get; set; }

        #endregion

        #region Constructors

        public TelephonyResult(TelephonyResultCode code, string message = null)
            : base(code, message)
        {
            //if (code == TelephonyResultCode.InvalidParameter)
            //{
            //    ErrorMessage = string.Format("Invalid parameter {0}", message);
            //}
        }

        public TelephonyResult(T value)
        {
            Value = value;
        }

		public TelephonyResult()
		{

		}

        #endregion
    }
}
