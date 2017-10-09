namespace O2.Telephony.Models
{
    public class PhoneNumberResult : BaseResult
    {
        //public properties
        public PhoneNumberResultCode ResultCode { get; set; }

        public override bool HasError
        {
            get { return ResultCode != PhoneNumberResultCode.Success; }
        }

        //constructors
        public PhoneNumberResult()
        {
            ResultCode = PhoneNumberResultCode.Success;
        }

        public PhoneNumberResult(PhoneNumberResultCode code, string message = null)
        {
            ResultCode = code;

            if (code == PhoneNumberResultCode.InvalidParameter)
            {
                ErrorMessage = string.Format("Invalid parameter {0}", message);
            }
            else
            {
                ErrorMessage = message;
            }
        }
    }
}
