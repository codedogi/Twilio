using O2.Telephony.Models.TimeZone;

namespace O2.Telephony.Dal.Models
{
    public static class GeoTimeZonePocoExtension
    {
        internal static GeoTimeZone ToModel(this GeoTimeZonePoco geoTimeZonePoco)
        {
            if (geoTimeZonePoco == null)
            {
                return null;
            }

            return new GeoTimeZone()
            {
                Id = geoTimeZonePoco.Id,
                Latitude = geoTimeZonePoco.Latitude,
                Longitude = geoTimeZonePoco.Longitude,
                OffsetSecondsUtcDaylight = geoTimeZonePoco.OffsetUtcDst,
                OffsetSecondsUtcRaw = geoTimeZonePoco.OffsetUtcRaw,
                TimeZoneAbbrStandard = geoTimeZonePoco.TimeZoneAbbrStandard,
                TimeZoneAbbrDaylight = geoTimeZonePoco.TimeZoneAbbrDaylight,
                TimeZoneId = geoTimeZonePoco.TimeZoneId,
                TimeZoneNameStandard = geoTimeZonePoco.TimeZoneNameStandard,
                TimeZoneNameDaylight = geoTimeZonePoco.TimeZoneNameDaylight,
                Created = geoTimeZonePoco.Created,
                Updated = geoTimeZonePoco.Updated
            };

        }
    }
}
