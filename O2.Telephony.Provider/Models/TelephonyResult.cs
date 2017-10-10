namespace O2.Telephony.Provider.Models
{
    public class TelephonyResult
    {
        #region Public Properties

        public TelephonyResultCode ResultCode { get; set; }

        public bool HasError
        {
            get { return ResultCode != TelephonyResultCode.Success; }
        }

        public string ErrorMessage { get; set; }

        #endregion Public Properties

        #region Constructors

        public TelephonyResult()
        {
            ResultCode = TelephonyResultCode.Success;
        }

        public TelephonyResult(TelephonyResultCode code, string message = null)
        {
            ResultCode = code;

            if (code == TelephonyResultCode.InvalidParameter)
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
