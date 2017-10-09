using System;

namespace O2.Telephony.Models.TimeZone
{
    public class GeoTimeZone
    {
        //Public Properties
        public int Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public double OffsetSecondsUtcDaylight { get; set; }
        public double OffsetSecondsUtcRaw { get; set; }
        public string TimeZoneAbbrStandard { get; set; }
        public string TimeZoneAbbrDaylight { get; set; }
        public string TimeZoneId { get; set; }
        public string TimeZoneNameStandard { get; set; }
        public string TimeZoneNameDaylight { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        //override
        public override string ToString()
        {
            return
                string.Format(
                    "[{0}] Id: {1}, TimeZoneStandardAbbr: {2}, TimeZoneDaylightAbbr: {3}, TimeZoneId: {4}, TimeZoneNameStandard: {5}, " +
                    "TimeZoneNameDaylight: {6}, Created: {7}, Updated: {8}", GetType().FullName, Id, TimeZoneAbbrStandard, TimeZoneAbbrDaylight,
                    TimeZoneId, TimeZoneNameStandard, TimeZoneNameDaylight, Created, Updated);
        }
    }
}
