namespace O2.Telephony.Models
{
    public class AccountResult : BaseResult
    {
        //public properties
        public AccountResultCode ResultCode { get; set; }

        public override bool HasError
        {
            get { return ResultCode != AccountResultCode.Success; }
        }

        //constructors
        public AccountResult()
        {
            ResultCode = AccountResultCode.Success;
        }

        public AccountResult(AccountResultCode code, string message = null)
        {
            ResultCode = code;
            ErrorMessage = message;
        }
    }
}
