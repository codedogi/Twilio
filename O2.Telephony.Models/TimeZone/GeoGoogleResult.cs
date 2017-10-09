using System.Collections.Generic;

namespace O2.Telephony.Models.TimeZone
{
    public class GeoGoogleResult
    {
        #region Public Properties
        
        // ReSharper disable InconsistentNaming
        public List<GeoGoogleGeometry> results { get; set; }  
        public string status { get; set; }
        // ReSharper restore InconsistentNaming

        #endregion
    }
}
