using System;

namespace O2.Telephony.Models.TimeZone
{
    public class GeoZipCode
    {
        //Public Properties
        public string ZipCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        //override
        public override string ToString()
        {
            return
                string.Format("[{0}] ZipCode: {1}, Latitude: {2}, Longitude: {3}, Created: {4}, Updated: {5}", GetType().FullName, ZipCode, Latitude,
                              Longitude, Created, Updated);
        }
    }
}
