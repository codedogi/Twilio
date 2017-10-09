namespace O2.Telephony.Models
{
	abstract public class BaseResult
	{
		public string ErrorMessage { get; set; }
		public abstract bool HasError { get; }
	}
}
