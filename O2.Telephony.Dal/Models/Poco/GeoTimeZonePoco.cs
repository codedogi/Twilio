// ReSharper disable CheckNamespace
using System;
using O2.Telephony.Dal.PetaPoco;

namespace O2.Telephony.Dal.Models
{
    [TableName("GeoTimeZone")]
    [PrimaryKey("Id", autoIncrement = true)]
    internal partial class GeoTimeZonePoco
    {
        public int Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public double OffsetUtcDst { get; set; }
        public double OffsetUtcRaw { get; set; }
        public string TimeZoneAbbrStandard { get; set; }
        public string TimeZoneAbbrDaylight { get; set; }
        public string TimeZoneId { get; set; }
        public string TimeZoneNameStandard { get; set; }
        public string TimeZoneNameDaylight { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
// ReSharper restore CheckNamespace
