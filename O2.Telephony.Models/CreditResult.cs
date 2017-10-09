
namespace O2.Telephony.Models
{
    public class CreditResult : BaseResult
    {
        public CreditResultCode ResultCode { get; set; }

        public override bool HasError
        {
            get { return ResultCode != CreditResultCode.Success; }
        }

        public CreditResult()
        {
            ResultCode = CreditResultCode.Success;
        }

        public CreditResult(CreditResultCode code, string message = null)
        {
            ResultCode = code;
            ErrorMessage = message;
        }
    }
}
