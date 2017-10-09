using O2.Telephony.Models.TimeZone;

namespace O2.Telephony.Dal.Models
{
    public static class GeoZipCodePocoExtension
    {
        internal static GeoZipCode ToModel(this GeoZipCodePoco geoZipCodePoco)
        {
            if (geoZipCodePoco == null)
            {
                return null;
            }

            return new GeoZipCode()
            {
                ZipCode = geoZipCodePoco.ZipCode,
                Latitude = geoZipCodePoco.Latitude,
                Longitude = geoZipCodePoco.Longitude,
                Created = geoZipCodePoco.Created,
                Updated = geoZipCodePoco.Updated
            };

        }
    }
}
