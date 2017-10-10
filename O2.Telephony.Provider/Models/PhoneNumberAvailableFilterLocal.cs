namespace O2.Telephony.Provider.Models
{
    public class PhoneNumberAvailableFilterLocal
    {
        #region Public Properties

        public string AreaCode { get; set; }
        public string Contains { get; set; }
        public string InPostalCode { get; set; }
        public string NearNumber { get; set; }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return
                string.Format(
                    "[{0}] AreaCode: {1}, Contains: {2}, InPostalCode: {3}, NearNumber: {4}", GetType().FullName, AreaCode, Contains, InPostalCode,
                    NearNumber);
        }

        #endregion

    }
}
