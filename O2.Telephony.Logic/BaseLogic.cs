using NLog;
using ILogger = NLog.ILogger;

namespace O2.Telephony.Logic
{
    public class BaseLogic
    {
        #region Fields
        protected static ILogger Logger;
        #endregion Fields

        #region Constructors
        public BaseLogic(Logger logger)
        {
            Logger = logger ?? LogManager.GetCurrentClassLogger();
        }
        #endregion
    }
}
