namespace O2.Telephony.Provider.Models
{
    public enum TelephonyResultCode
    {
        Unknown,
        Success,
        Error,
        InvalidParameter,
        DatabaseError,
        ProviderError,
        AccountNotFound,
		CallerIdNotFound,
		CallNotFound,
    }
}
