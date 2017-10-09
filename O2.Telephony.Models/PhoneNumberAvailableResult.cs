namespace O2.Telephony.Models
{
    public class PhoneNumberAvailableResult : BaseResult
    {
        //public properties
        public PhoneNumberAvailableResultCode ResultCode { get; set; }

        public override bool HasError
        {
            get { return ResultCode != PhoneNumberAvailableResultCode.Success; }
        }

        //constructors
        public PhoneNumberAvailableResult()
        {
            ResultCode = PhoneNumberAvailableResultCode.Success;
        }

        public PhoneNumberAvailableResult(PhoneNumberAvailableResultCode code, string message = null)
        {
            ResultCode = code;
            ErrorMessage = message;
        }
    }
}
