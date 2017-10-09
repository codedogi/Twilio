using System.Collections.Generic;
using O2.Telephony.Models.TimeZone;

namespace O2.Telephony.Dal.Interfaces
{
    /// <summary>
    /// Interface for TimeZone
    /// </summary>
    public interface ITimeZoneDal
    {
        //create
        int Create(GeoTimeZone geoTimeZone);
        void Create(GeoZipCode geoZipCode);

        //read
        GeoTimeZone Read(decimal latitude, decimal longitude);
        GeoZipCode Read(string zipCode);

        //update
        void Update(GeoTimeZone geoTimeZone, IEnumerable<string> columns);

        //delete
        //process

    }
}
