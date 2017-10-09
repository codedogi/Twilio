namespace O2.Telephony.Models.CallerId
{
    public class UpdateCallerIdResult : BaseResult
    {
        #region Public Properties

        public CallerIdResultCode ResultCode { get; set; }

        public override bool HasError
        {
            get { return ResultCode != CallerIdResultCode.Success; }
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return string.Format("[{0}]", GetType().FullName);
        }

        #endregion

        #region Constructors

        public UpdateCallerIdResult()
        {
            ResultCode = CallerIdResultCode.Success;
        }

        public UpdateCallerIdResult(CallerIdResultCode code, string message = null)
        {
            ResultCode = code;
            ErrorMessage = message;
        }

        #endregion

    }
}
