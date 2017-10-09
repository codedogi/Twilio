namespace O2.Telephony.Models
{
    public class PhoneNumberAvailableFilterTollFree
    {
        #region Public Properties

        public string Contains { get; set; }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return
                string.Format(
                    "[{0}] Contains: {1}", GetType().FullName, Contains);
        }

        #endregion

    }
}
