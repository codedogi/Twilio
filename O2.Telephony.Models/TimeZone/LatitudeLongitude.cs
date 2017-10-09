namespace O2.Telephony.Models.TimeZone
{
    public class LatitudeLongitude
    {
        //private
        private const string FormatStringLatitude = "##.########";
        private const string FormatStringLongitude = "###.########";
        private decimal _latitude;
        private decimal _longitude;

        //Public Properties
        public decimal Latitude
        {
            get { return _latitude; }

            //trim to 8 decimal postions
            set { decimal.TryParse(value.ToString(FormatStringLatitude), out _latitude); }
        }

        public decimal Longitude
        {
            get { return _longitude; }

            //trim to 8 decimal postions
            set { decimal.TryParse(value.ToString(FormatStringLongitude), out _longitude); }
        }
        
        public string GetLocation()
        {
            return string.Format("{0},{1}", Latitude, Longitude);
        }

        public bool IsValidLocation()
        {
            if (_latitude == decimal.MaxValue || _longitude == decimal.MaxValue)
            {
                return false;
            }

            return true;
        }

        //override
        public override string ToString()
        {
            return string.Format("[{0}] Latitude: {1}, Longitude: {2}", GetType().FullName, Latitude, Longitude);
        }
    }
}
