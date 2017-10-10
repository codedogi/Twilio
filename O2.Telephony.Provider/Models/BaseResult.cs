namespace O2.Telephony.Provider.Models
{
	abstract public class BaseResult<TResultCode>
	{
		#region Public Properties

		public TResultCode ResultCode { get; set; }

		public string ErrorMessage { get; set; }

		public abstract bool HasError { get; }

		#endregion Public Properties

		#region Constructors

		#endregion Constructors
	}
}
