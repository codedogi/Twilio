namespace O2.Telephony.Models.TimeZone
{
    public class TimeZoneGoogleResult
    {
        #region Public Properties
        
        // ReSharper disable InconsistentNaming
        public double dstOffset { get; set; }
        public double rawOffset { get; set; }
        public string status { get; set; }
        public string timeZoneId { get; set; }
        public string timeZoneName { get; set; }
        // ReSharper restore InconsistentNaming

        #endregion
    }
}
