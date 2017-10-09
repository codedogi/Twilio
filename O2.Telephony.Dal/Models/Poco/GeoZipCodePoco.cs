// ReSharper disable CheckNamespace
using System;
using O2.Telephony.Dal.PetaPoco;

namespace O2.Telephony.Dal.Models
{
    [TableName("GeoZipCode")]
    [PrimaryKey("ZipCode", autoIncrement = false)]
    internal partial class GeoZipCodePoco
    {
        public string ZipCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
// ReSharper restore CheckNamespace
