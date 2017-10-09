namespace O2.Telephony.Models
{
    public class TelephonyResult : BaseResult
    {
        //public properties
        public TelephonyResultCode ResultCode { get; set; }

        public override bool HasError
        {
            get { return ResultCode != TelephonyResultCode.Success; }
        }

        //constructor
        public TelephonyResult()
        {
            ResultCode = TelephonyResultCode.Success;
        }

        public TelephonyResult(TelephonyResultCode code, string message = null)
        {
            ResultCode = code;
            ErrorMessage = message;
        }

        //public methods
        public override string ToString()
        {
            return string.Format("[{0} - ResultCode: {1}, ErrorMessage: {2}]", GetType().FullName, ResultCode, ErrorMessage);
        }
    }
}
