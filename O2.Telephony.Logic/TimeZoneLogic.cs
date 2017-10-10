using System;
using System.Configuration;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using NLog;
using Newtonsoft.Json;
using O2.Telephony.Dal.Interfaces;
using O2.Telephony.Logic.Interfaces;
using O2.Telephony.Models.TimeZone;
using RestSharp;

namespace O2.Telephony.Logic
{
    public class TimeZoneLogic : BaseLogic, ITimeZoneLogic
    {
        private readonly ITimeZoneDal _timeZoneDal;

        #region Constructors
        public TimeZoneLogic(ITimeZoneDal timeZoneDal) : base(LogManager.GetCurrentClassLogger())
        {
            Logger.Trace("Instantiated");
            _timeZoneDal = timeZoneDal;
        }
        #endregion

        #region Public Methods
        public string LocalTime(string latitudeCommaLongitude)
        {
            try
            {
                if (latitudeCommaLongitude == "0,0")
                {
                    //no geo info return empty string
                    return string.Empty;
                }

                //get lat/lng from string
                var latlng = SplitLatitudeLongitude(latitudeCommaLongitude);

                //get time zone from geo
                var geoTimeZone = GeoToTimeZone(latlng);

                if (geoTimeZone == null)
                {
                    return string.Empty;
                }

                var now = DateTime.Now;
                var nowUtc = now.ToUniversalTime();
                string timeZoneAbbr;

                if (now.IsDaylightSavingTime())
                {
                    nowUtc = nowUtc.AddSeconds(geoTimeZone.OffsetSecondsUtcRaw + geoTimeZone.OffsetSecondsUtcDaylight);
                    timeZoneAbbr = geoTimeZone.TimeZoneAbbrDaylight;
                }
                else
                {
                    nowUtc = nowUtc.AddSeconds(geoTimeZone.OffsetSecondsUtcRaw);
                    timeZoneAbbr = geoTimeZone.TimeZoneAbbrStandard;
                }

                //return time zone
                return $"{nowUtc:t} {timeZoneAbbr}";
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return string.Empty;
            }
        }

        public LatitudeLongitude ZipToGeo(string zipCode)
        {
            try
            {
                //get geo from local
                var geoInfo = GetGeoFromLocal(zipCode);

                if (geoInfo == null)
                {
                    if (TelephonyGeoLatLngUseGoogle == false)
                    {
                        //Google check is shut off in app.config
                    }
                    else
                    {
                        geoInfo = GetGeoFromGoogle(zipCode);

                        if (geoInfo != null)
                        {
                            geoInfo = AddResultToGeoZipCode(geoInfo);
                        }
                    }
                }

                if (geoInfo == null)
                {
                    return new LatitudeLongitude {Latitude = decimal.MaxValue, Longitude = decimal.MaxValue};
                }

                return new LatitudeLongitude {Latitude = geoInfo.Latitude, Longitude = geoInfo.Longitude};
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return new LatitudeLongitude {Latitude = decimal.MaxValue, Longitude = decimal.MaxValue};
            }
        }

        public GeoTimeZone ZipToTimeZone(string zipCode)
        {
            //get lat/lng from zip
            var latlng = ZipToGeo(zipCode);

            //get time zone info from lat/lng
            return GeoToTimeZone(latlng);
        }

        public string Description(string zipCode)
        {
            try
            {
                //get geo from local
                var geoInfo = GetGeoFromLocal(zipCode);

                if (geoInfo == null)
                {
                    if (TelephonyGeoLatLngUseGoogle == false)
                    {
                        //Google check is shut off in app.config
                    }
                    else
                    {
                        geoInfo = GetGeoFromGoogle(zipCode);

                        if (geoInfo != null)
                        {
                            geoInfo = AddResultToGeoZipCode(geoInfo);
                        }
                    }
                }

                if (geoInfo == null)
                {
                    return "Unknown Time Zone";
                }

                var now = DateTime.Now;

                //get time zone from geo
                var geoTimeZone = GeoToTimeZone(new LatitudeLongitude {Latitude = geoInfo.Latitude, Longitude = geoInfo.Longitude});

                if (geoTimeZone == null)
                {
                    return "Unknown time zone";
                }

                if (now.IsDaylightSavingTime())
                {
                    return geoTimeZone.TimeZoneNameDaylight;
                }
                    
                return geoTimeZone.TimeZoneNameStandard;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return "unknown time zone";
            }
        }
        #endregion

        #region Private Methods
        private string GetZoneAbbreviation(string timeZoneName)
        {
            string abbreviation = string.Empty;

            var nameSplit = timeZoneName.Split(' ');

            foreach (var name in nameSplit)
            {
                if (name == "Alaska")
                {
                    abbreviation = "AK";
                }
                else if (name == "Hawaii-Aleutian")
                {
                    abbreviation = "HA";
                }
                else
                {
                    abbreviation += name.Substring(0, 1);
                }
            }

            return abbreviation;
        }

        private GeoTimeZone GeoToTimeZone(LatitudeLongitude latlng)
        {
            //get time zone from local
            var geoTimeZone = GetTimeZoneInfoFromLocal(latlng);

            if (geoTimeZone == null)
            {
                if (TelephonyTimeZoneUseGoogle == false)
                {
                    //Google check is shut off in app.config
                    return null;
                }

                //get time zone from google
                var resultTimeZone = GetTimeZoneInfoFromGoogle(latlng);

                if (resultTimeZone.Status == "OK")
                {
                    //add to GeoTimeZone table
                    return AddResultToGeoTimeZone(latlng, resultTimeZone);
                }

                Logger.Error($"Failed to get time zone info from Google, status: {resultTimeZone.Status} geo: {latlng.GetLocation()}");
                return null;
            }

            return geoTimeZone;
        }

        private LatitudeLongitude SplitLatitudeLongitude(string latitudeCommaLongitude)
        {
            var split = latitudeCommaLongitude.Split(',');
            LatitudeLongitude latlng;

            if (split.Length == 2)
            {
                decimal lat;

                if (decimal.TryParse(split[0], out lat))
                {
                    decimal lng;

                    if (decimal.TryParse(split[1], out lng))
                    {
                        latlng = new LatitudeLongitude {Latitude = lat, Longitude = lng};
                    }
                    else
                    {
                        throw new Exception($"Unable to parse, invalid longitude: {split[1]}");
                    }
                }
                else
                {
                    throw new Exception($"Unable to parse, invalid latitude: {split[0]}");
                }
            }
            else
            {
                throw new Exception($"Unable to parse, invalid latitude/longitude string: {latitudeCommaLongitude}");
            }

            return latlng;
        }

        private GeoTimeZone GetTimeZoneInfoFromLocal(LatitudeLongitude latlng)
        {
            return _timeZoneDal.Read(latlng.Latitude, latlng.Longitude);
        }

        private GeoTimeZone AddResultToGeoTimeZone(LatitudeLongitude latlng, TimeZoneResult result)
        {
            var now = DateTime.Now;

            var geoTimeZone = new GeoTimeZone
                                  {
                                      Latitude = latlng.Latitude,
                                      Longitude = latlng.Longitude,
                                      OffsetSecondsUtcDaylight = result.DaylightOffset,
                                      OffsetSecondsUtcRaw = result.StandardOffset,
                                      TimeZoneAbbrStandard = GetZoneAbbreviation(result.TimeZoneNameStandard),
                                      TimeZoneAbbrDaylight = GetZoneAbbreviation(result.TimeZoneNameDaylight),
                                      TimeZoneId = result.TimeZoneId,
                                      TimeZoneNameStandard = result.TimeZoneNameStandard,
                                      TimeZoneNameDaylight = result.TimeZoneNameDaylight,
                                      Created = now,
                                      Updated = now
                                  };

            geoTimeZone.Id = _timeZoneDal.Create(geoTimeZone);
            
            return geoTimeZone;
        }

        private GeoZipCode AddResultToGeoZipCode(GeoZipCode geoZipCode)
        {
            //trim lat/lng to 8 decimal positions
            var latLng = new LatitudeLongitude {Latitude = geoZipCode.Latitude, Longitude = geoZipCode.Longitude};

            var now = DateTime.Now;
            var geoZipTrimmed = new GeoZipCode
                                    {
                                        ZipCode = geoZipCode.ZipCode,
                                        Latitude = latLng.Latitude,
                                        Longitude = latLng.Longitude,
                                        Created = now,
                                        Updated = now
                                    };

            _timeZoneDal.Create(geoZipTrimmed);

            return geoZipTrimmed;
        }

        private TimeZoneResult GetTimeZoneInfoFromGoogle(LatitudeLongitude latlng)
        {
            var standardDateTime = new DateTime(2013, 1, 3, 0, 0, 0, DateTimeKind.Utc);       //Jan 1, 2013 00:00:00 UTC
            var daylightSavingDateTime = new DateTime(2013, 6, 3, 0, 0, 0, DateTimeKind.Utc); //Jun 3, 2013 00:00:00 UTC

            //get standard time zone info
            var resultStandard = GetTimeZoneInfoFromGoogle(latlng, standardDateTime);

            if (resultStandard.status.ToUpper() == "OK")
            {
                //get daylight time zone info
                var resultDaylight = GetTimeZoneInfoFromGoogle(latlng, daylightSavingDateTime);

                if (resultDaylight.status.ToUpper() == "OK")
                {
                    return new TimeZoneResult
                               {
                                   DaylightOffset = resultDaylight.dstOffset,
                                   StandardOffset = resultStandard.rawOffset,
                                   Status = "OK",
                                   TimeZoneId = resultStandard.timeZoneId,
                                   TimeZoneNameStandard = resultStandard.timeZoneName,
                                   TimeZoneNameDaylight = resultDaylight.timeZoneName
                               };
                }
                
                Logger.Error(
                    $"Failed to get daylight time zone information from Google, status: {resultDaylight.status}");
            }
            else
            {
                Logger.Error(
                    $"Failed to get standard time zone information from Google, status: {resultStandard.status}");
            }

            return new TimeZoneResult {Status = "FAILED"};
        }

        private TimeZoneGoogleResult GetTimeZoneInfoFromGoogle(LatitudeLongitude latlng, DateTime now)
        {
            try
            {
                //build resource string based on region
                //  standard non-production resource example:
                //    https://maps.googleapis.com/maps/api/timezone/json?location=41.1572,-96.030&timestamp=1331161200&sensor=false
                //
                //  production resource example with client and signature:
                //    https://maps.googleapis.com/maps/api/timezone/json?location=41.1572,-96.030&timestamp=1331161200&sensor=false&client=gme-infogroupinc&signature=yrmZ985D4sHdbmSA3qzep5dpgA8=

                //get number of seconds since Unix Epoch (00:00:00 UTC on 1 January 1970)
                var span = now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

                var resource =
                    $"/maps/api/timezone/json?location={latlng.GetLocation()}&timestamp={span.TotalSeconds}&sensor=false";

                if (UseTelephonyClient)
                {
                    //add client and sign request
                    resource = Sign(resource + "&client=" + TelephonyClient, TelephonySign + TelephonyDI);
                }

                Logger.Info("GetTimeZoneInfoFromGoogle.Resource: " + resource);

                var restRequest = new RestRequest
                {
                    Resource = resource,
                    Method = Method.POST,
                    RequestFormat = DataFormat.Json
                };

                var restClient = new RestClient { BaseUrl = new Uri("https://maps.googleapis.com"), Timeout = 30000 };

                var response = restClient.Execute(restRequest);

                Logger.Info($"Response status code: {response.StatusCode} Content: {response.Content}");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<TimeZoneGoogleResult>(response.Content);
                }

                //log response error
                Logger.Error($"Bad response, statusCode: {response.StatusCode}, message: {response.ErrorMessage}, content: {response.Content}, resource: {resource}");

                return new TimeZoneGoogleResult { status = "ERROR" };
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error with GetTimeZoneInfoFromGoogle");

                return new TimeZoneGoogleResult { status = "ERROR" };
            }
        }

        public static string Sign(string resource, string keyString)
        {
            var encoding = new ASCIIEncoding();

            // converting key to bytes will throw an exception, need to replace '-' and '_' characters first.
            string usablePrivateKey = keyString.Replace("-", "+").Replace("_", "/");
            byte[] privateKeyBytes = Convert.FromBase64String(usablePrivateKey);

            byte[] encodedPathAndQueryBytes = encoding.GetBytes(resource);

            // compute the hash
            var algorithm = new HMACSHA1(privateKeyBytes);
            byte[] hash = algorithm.ComputeHash(encodedPathAndQueryBytes);

            // convert the bytes to string and make resource-safe by replacing '+' and '/' characters
            string signature = Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_");

            // add the signature to the existing resource.
            return $"{resource}&signature={signature}";
        }

        private GeoZipCode GetGeoFromLocal(string zipCode)
        {
            return _timeZoneDal.Read(zipCode);
        }

        private GeoZipCode GetGeoFromGoogle(string zipCode)
        {
            try
            {
                //build resource string based on region
                //  standard non-production resource example:
                //    https://maps.googleapis.com/maps/api/geocode/json?address=68046&sensor=false
                //
                //  production resource example with client and signature:
                //    https://maps.googleapis.com/maps/api/geocode/json?address=68046&sensor=false&client=gme-infogroupinc&signature=stWtHSKA4XuUqDOujmySAmnRQUw=

                var resource = $"/maps/api/geocode/json?address={zipCode}&sensor=false";

                if (UseTelephonyClient)
                {
                    //add client and sign request
                    resource = Sign(resource + "&client=" + TelephonyClient, TelephonySign + TelephonyDI);
                }

                Logger.Info("GetGeoFromGoogle.Resource: " + resource);

                var restRequest = new RestRequest
                {
                    Resource = resource,
                    Method = Method.POST,
                    RequestFormat = DataFormat.Json
                };

                var restClient = new RestClient { BaseUrl = new Uri("https://maps.googleapis.com"), Timeout = 30000 };

                var response = restClient.Execute(restRequest);

                Logger.Info($"Response status code: {response.StatusCode} Content: {response.Content}");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dynamic jsonContent = JsonConvert.DeserializeObject(response.Content);

                    try
                    {
                        if (((string)jsonContent.status).ToUpper() == "OK")
                        {
                            string lat = jsonContent.results[0].geometry.location.lat;
                            string lng = jsonContent.results[0].geometry.location.lng;

                            return new GeoZipCode { ZipCode = zipCode, Latitude = decimal.Parse(lat), Longitude = decimal.Parse(lng) };
                        }

                        Logger.Error($"Status returned from Google was not OK, content: {response.Content}");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(
                            $"Error finding lat/lng in returned content from Google, content: {response.Content}, error: {ex.Message}");
                        return null;
                    }
                }

                Logger.Error(
                    $"Bad response, statusCode: {response.StatusCode}, message: {response.ErrorMessage}, content: {response.Content}, resource: {resource}");
                return null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error with GetGeoFromGoogle");
                return null;
            }
        }

        private bool UseTelephonyClient => ConfigurationManager.AppSettings["UseTelephonyClient"].ToUpper() == "TRUE";

        private string TelephonyClient => ConfigurationManager.AppSettings["TelephonyClient"];

        private string TelephonySign => ConfigurationManager.AppSettings["TelephonySign"];

        private string TelephonyDI => ConfigurationManager.AppSettings["TelephonyDI"];

        private bool TelephonyTimeZoneUseGoogle => ConfigurationManager.AppSettings["TelephonyTimeZoneUseGoogle"].ToUpper() == "TRUE";

        private bool TelephonyGeoLatLngUseGoogle => ConfigurationManager.AppSettings["TelephonyGeoLatLngUseGoogle"].ToUpper() == "TRUE";

        #endregion
    }
}
