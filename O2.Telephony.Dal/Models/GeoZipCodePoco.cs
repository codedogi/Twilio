using O2.Telephony.Models.TimeZone;

namespace O2.Telephony.Dal.Models
{
    internal partial class GeoZipCodePoco : BasePoco
    {
        //constructors
        public GeoZipCodePoco()
        {
        }

        internal GeoZipCodePoco(GeoZipCode geoZipCode)
        {
            ZipCode = geoZipCode.ZipCode;
            Latitude = geoZipCode.Latitude;
            Longitude = geoZipCode.Longitude;
            Created = geoZipCode.Created;
            Updated = geoZipCode.Updated;
        }

        //override
        public override string ToString()
        {
            return string.Format("[{0}] ZipCode: {1}, Latitude: {2}, Longitude: {3}, Created: {4}, Updated: {5}", GetType().FullName, ZipCode,
                                 Latitude, Longitude, Created, Updated);
        }
    }
}
