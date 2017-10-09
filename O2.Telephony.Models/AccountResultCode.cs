namespace O2.Telephony.Models
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
        RootNodeNotFound
    }
}
