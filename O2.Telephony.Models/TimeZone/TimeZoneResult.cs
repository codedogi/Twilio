namespace O2.Telephony.Models.TimeZone
{
    public class TimeZoneResult
    {
        #region Public Properties
        
        public double DaylightOffset { get; set; }
        public double StandardOffset { get; set; }
        public string Status { get; set; }
        public string TimeZoneId { get; set; }
        public string TimeZoneNameStandard { get; set; }
        public string TimeZoneNameDaylight { get; set; }

        #endregion
    }
}
