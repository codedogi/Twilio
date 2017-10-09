namespace O2.Telephony.Models
{
	public enum CallStatusType : byte
	{
		Unknown,
        BeforeQueued,
		Queued,
		Ringing,
        InProgress,
        Completed,
        Busy,
        Failed,
        NoAnswer,
        Canceled
	}
}
