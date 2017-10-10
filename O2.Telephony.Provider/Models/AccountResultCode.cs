namespace O2.Telephony.Provider.Models
{
    public enum AccountResultCode
    {
        Unknown,
        Success,
        Error,
        InvalidParameter,
        DatabaseError,
        ProviderError,
        AccountNotFound,
        ParentAccountNotFound,
        AccountAlreadyExists
    }
}
