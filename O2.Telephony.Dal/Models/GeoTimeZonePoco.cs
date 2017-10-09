using O2.Telephony.Models.TimeZone;

namespace O2.Telephony.Dal.Models
{
    internal partial class GeoTimeZonePoco : BasePoco
    {
        //constructors
        public GeoTimeZonePoco()
        {
        }

        internal GeoTimeZonePoco(GeoTimeZone geoTimeZone)
        {
            Id = geoTimeZone.Id;
            Latitude = geoTimeZone.Latitude;
            Longitude = geoTimeZone.Longitude;
            OffsetUtcDst = geoTimeZone.OffsetSecondsUtcDaylight;
            OffsetUtcRaw = geoTimeZone.OffsetSecondsUtcRaw;
            TimeZoneAbbrStandard = geoTimeZone.TimeZoneAbbrStandard;
            TimeZoneAbbrDaylight = geoTimeZone.TimeZoneAbbrDaylight;
            TimeZoneId = geoTimeZone.TimeZoneId;
            TimeZoneNameStandard = geoTimeZone.TimeZoneNameStandard;
            TimeZoneNameDaylight = geoTimeZone.TimeZoneNameDaylight;
            Created = geoTimeZone.Created;
            Updated = geoTimeZone.Updated;
        }

        //override
        public override string ToString()
        {
            return
                string.Format(
                    "[{0}] Id: {1}, TimeZoneAbbrStandard: {2}, TimeZoneAbbrStandard: {3}, TimeZoneId: {4}, TimeZoneNameStandard: {5}, " +
                    "TimeZoneNameDaylight: {6}, Created: {7}, Updated: {8}", GetType().FullName, Id, TimeZoneAbbrStandard, TimeZoneAbbrDaylight,
                    TimeZoneId, TimeZoneNameStandard, TimeZoneNameDaylight, Created, Updated);
        }
    }
}
