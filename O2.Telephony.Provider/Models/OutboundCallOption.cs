using System;
using System.Collections.Generic;

namespace O2.Telephony.Provider.Models
{
    /// <summary>
    /// Available options to include when initiating a phone call
    /// </summary>
    public class OutboundCallOption
    {
        /// <summary>
        /// Infogroup Id (ABI Number) for record
        /// </summary>
        public string RecordId { get; set; }

        /// <summary>
        /// The phone number to use as the caller id. Format with a '+' and country code e.g., +16175551212 (E.164 format). Must be a Twilio number or a valid outgoing caller id for your account.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// The number to call formatted with a '+' and country code e.g., +16175551212 (E.164 format). Twilio will also accept unformatted US numbers e.g., (415) 555-1212, 415-555-1212.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// The integer number of seconds that Twilio should allow the phone to ring before assuming there is no answer. Default is 60 seconds, the maximum is 999 seconds. Note, you could set this to a low value, such as 15, to hangup before reaching an answering machine or voicemail.
        /// </summary>
        public int? Timeout { get; set; }

		public Guid TelephonyCallId { get; set; }
        public Guid TelephonyCallIdChild { get; set; }
        public Guid TelephonyAccountId { get; set; }
        public List<string> Say { get; set; }
        public CallSessionType CallSessionType { get; set; }
        public bool HangupIfMachine { get; set; }
    }
}
