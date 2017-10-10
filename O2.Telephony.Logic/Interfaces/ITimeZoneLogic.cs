using O2.Telephony.Models.TimeZone;

namespace O2.Telephony.Logic.Interfaces
{
    public interface ITimeZoneLogic
    {
        //create

        //read
        string LocalTime(string latitudeCommaLongitude);
        LatitudeLongitude ZipToGeo(string zipCode);
        GeoTimeZone ZipToTimeZone(string zipCode);
        string Description(string zipCode);

        //update
        //delete
        //process
    }
}
