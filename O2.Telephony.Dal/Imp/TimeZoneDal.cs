using System;
using System.Collections.Generic;
using NLog;
using O2.Telephony.Dal.Interfaces;
using O2.Telephony.Dal.Models;
using O2.Telephony.Dal.PetaPoco;
using O2.Telephony.Models.TimeZone;

namespace O2.Telephony.Dal.Imp
{
    /// <summary>
    /// Data access layer for time zone info
    /// </summary>
    public class TimeZoneDal : BaseDal, ITimeZoneDal
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public TimeZoneDal()
            : base(LogManager.GetCurrentClassLogger())
        {
            Logger.Trace("Instantiated");
        }
        #endregion

        //Create
        #region Create

        /// <summary>
        /// Create time zone info
        /// </summary>
        /// <param name="geoTimeZone">Zip geo time zone data</param>
        /// <returns>id of record created</returns>
        public int Create(GeoTimeZone geoTimeZone)
        {
            Logger.Debug($"Create({geoTimeZone})");

            using (var db = new Database(TelephonyConnection))
            {
                try
                {
                    var poco = new GeoTimeZonePoco(geoTimeZone);

                    var id = db.Insert(poco);
                    var recordId = int.Parse(id.ToString());

                    Logger.Trace($"db.Insert success: {recordId}");

                    return recordId;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Error db.Insert: {Format(db)}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Create geo zip code info
        /// </summary>
        /// <param name="geoZipCode">Zip code with lat/lng coordinates</param>
        public void Create(GeoZipCode geoZipCode)
        {
            Logger.Debug($"Create({geoZipCode})");

            using (var db = new Database(TelephonyConnection))
            {
                try
                {
                    var poco = new GeoZipCodePoco(geoZipCode);

                    db.Insert(poco);
                    Logger.Trace($"db.Insert success: {geoZipCode.ZipCode}");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Error db.Insert: {Format(db)}");
                    throw;
                }
            }
        }
        #endregion

        //Read
        #region Read
        /// <summary>
        /// Read time zone info
        /// </summary>
        /// <param name="latitude">latitude</param>
        /// <param name="longitude">longitude</param>
        /// <returns>Time zone info</returns>
        public GeoTimeZone Read(decimal latitude, decimal longitude)
        {
            Logger.Debug($"Read({latitude}, {longitude})");

            var sql = new Sql().Append("select * from GeoTimeZone where latitude = @0 and longitude = @1", latitude, longitude);

            using (var db = new Database(TelephonyConnection))
            {
                try
                {
                    Logger.Trace($"db.SingleOrDefault: {Format(sql)}");
                    return db.SingleOrDefault<GeoTimeZonePoco>(sql).ToModel();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Error db.SingleOrDefault: {Format(db)}");
                    throw;
                }
            }
        }

        public GeoZipCode Read(string zipCode)
        {
            Logger.Debug($"Read({zipCode})");

            var sql = new Sql().Append("select * from GeoZipCode where ZipCode = @0", zipCode);

            using (var db = new Database(TelephonyConnection))
            {
                try
                {
                    Logger.Trace($"db.SingleOrDefault: {Format(sql)}");
                    return db.SingleOrDefault<GeoZipCodePoco>(sql).ToModel();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Error db.SingleOrDefault: {Format(db)}");
                    throw;
                }
            }
        }
        #endregion

        //Update
        public void Update(GeoTimeZone geoTimeZone, IEnumerable<string> columns)
        {
            Logger.Debug($"Update({geoTimeZone}, {columns})");

            using (var db = new Database(TelephonyConnection))
            {
                try
                {
                    db.Update(new GeoTimeZonePoco(geoTimeZone), columns);
                    Logger.Trace($"db.Update: {Format(db)}");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Error db.Update: {Format(db)}");
                    throw;
                }
            }
        }

        //Delete
        //Process
    }
}
