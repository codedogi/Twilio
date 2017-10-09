using System;
using System.Text;
using NLog;
using O2.Telephony.Dal.PetaPoco;
using ILogger = NLog.ILogger;

namespace O2.Telephony.Dal.Imp
{
    public class BaseDal
    {
        #region Fields
        protected static ILogger Logger;

        protected const string TelephonyConnection = "Telephony";
        protected const string MonitoringConnection = "Monitoring";
        #endregion Fields

        #region Constructors
        public BaseDal(Logger logger)
        {
            Logger = logger ?? LogManager.GetCurrentClassLogger();
        }
        #endregion

        #region internal Format for log methods
        /// <summary>
        /// Format sql and arguments into a string
        /// </summary>
        /// <param name="sql">PetaPoco sql</param>
        /// <returns>string</returns>
        internal string Format(Sql sql)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append($"Sql: [{(string.IsNullOrWhiteSpace(sql.SQL) ? "n/a" : sql.SQL)}]");
                sb.Append($" Args: [{Format(sql.Arguments)}]");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                const string message = "Error with Format(Sql)";
                Logger.Error(ex, message);
                return message;
            }
        }

        /// <summary>
        /// Format last sql, last comand and last arguments into a string
        /// </summary>
        /// <param name="db">PetaPoco database</param>
        /// <returns>string</returns>
        internal string Format(Database db)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append($"Sql: [{(string.IsNullOrWhiteSpace(db.LastSQL) ? "n/a" : db.LastSQL)}]");
                sb.Append($" Command: [{(string.IsNullOrWhiteSpace(db.LastCommand) ? "n/a" : db.LastCommand)}]");
                sb.Append($" Args: [{Format(db.LastArgs)}]");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                const string message = "Error with Format(Database)";
                Logger.Error(ex, message);
                return message;
            }
        }

        /// <summary>
        /// Format arguments object array into a string
        /// </summary>
        /// <param name="arguments">array of objects</param>
        /// <returns>string</returns>
        private string Format(object[] arguments)
        {
            try
            {
                if (arguments.Length == 0)
                {
                    return "n/a";
                }

                var sb = new StringBuilder();

                for (int i = 0; i < arguments.Length; i++)
                {
                    sb.Append($"{i}: {arguments[i]} ");
                }

                //return string with extra space at the end removed
                return sb.ToString(0, sb.Length - 1);
            }
            catch (Exception ex)
            {
                const string message = "Error with Format(Arguments)";
                Logger.Error(ex, message);
                return message;
            }
        }
        #endregion
    }
}
