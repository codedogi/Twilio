using System;
using System.Collections.Generic;

namespace O2.Telephony.Models
{
    public class PauseCallOptions
    {
		public Guid TelephonyCallId { get; set; }
		public Guid TelephonyAccountId { get; set; }
		public int SecondsToPause { get; set; }
		public string SayBeforePause { get; set; }
		public string SayAfterPause { get; set; }
		public bool Hangup { get; set; }
	}
}
