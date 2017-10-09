using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using O2.Telephony.Api.Rest;
using O2.Telephony.Models;
using O2.Telephony.Models.CallerId;
using O2.Telephony.Models.Credits;
using O2.Telephony.Models.OutboundCall;
using O2.Telephony.Models.TimeZone;
using RestSharp;

namespace O2.Telephony.Api
{
	public class TelephonyClient : ITelephonyClient
	{
		#region ITelephonyClient Interface Methods
		/// <summary>
		/// Gets the account.
		/// </summary>
		/// <param name="accountId">The account id.</param>
		/// <returns></returns>
		public Account GetAccount(Guid accountId)
		{
			var restRequest = new RestRequest
			{
				Resource = string.Format("/api/v1/accounts/{0}", accountId),
				Method = Method.GET,
				RequestFormat = DataFormat.Json
			};

			try
			{
				var restClient = new TelephonyRestClient();
				
				var response = restClient.Execute(restRequest);
				
				//TODO: handle rest errors

				var result = JsonConvert.DeserializeObject<Account>(response.Content);

				return result;
			}
			catch (Exception)
			{

				throw;
			}
		}

        /// <summary>
        /// Create telephony account
        /// </summary>
        /// <param name="parentAccountId">parent account id to base credentials off of</param>
        /// <returns>newly created account id</returns>
	    public Guid CreateAccount(Guid parentAccountId)
	    {
	        var restRequest = new RestRequest
	                              {
	                                  Resource = string.Format("/api/v1/accounts/{0}/create", parentAccountId),
	                                  Method = Method.POST,
	                                  RequestFormat = DataFormat.Json
	                              };

	        var restClient = new TelephonyRestClient();

	        var response = restClient.Execute(restRequest);

	        //TODO: handle rest errors

	        var result = JsonConvert.DeserializeObject<Guid>(response.Content);

	        return result;
	    }

	    /// <summary>
		/// Finds the local phone numbers given the provided filter.
		/// </summary>
		/// <param name="accountId">The account id.</param>
		/// <param name="filter">The filter.</param>
		/// <returns></returns>
		public IEnumerable<PhoneNumberAvailable> FindLocalPhoneNumbers(Guid accountId, PhoneNumberAvailableFilterLocal filter)
		{
			var restRequest = new RestRequest
			{
				Resource = string.Format("/api/v1/phone/{0}/FindLocalNumbers", accountId),
				Method = Method.POST,
				RequestFormat = DataFormat.Json,				
			};

			restRequest.AddBody(filter);

			try
			{
				var restClient = new TelephonyRestClient();

				var response = restClient.Execute(restRequest);

				//TODO: handle rest errors

				var result = JsonConvert.DeserializeObject<List<PhoneNumberAvailable>>(response.Content);

				return result;
			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// Creates the phone number associated with the account.
		/// </summary>
		/// <param name="accountId">The account id.</param>
		/// <param name="option">The option.</param>
		/// <returns></returns>
		public PhoneNumberResult CreatePhoneNumber(Guid accountId, PhoneNumberOption option)
		{
			var restRequest = new RestRequest
			{
				Resource = string.Format("/api/v1/phone/{0}/CreateNumber", accountId),
				Method = Method.POST,
				RequestFormat = DataFormat.Json
			};

			restRequest.AddBody(option);

			try
			{
				var restClient = new TelephonyRestClient();

				var response = restClient.Execute(restRequest);

				//TODO: handle rest errors

				// TODO:  this is currently returning a 500 when the phone number isn't available...do we need to be more specific?

				var result = JsonConvert.DeserializeObject<PhoneNumberResult>(response.Content);

				return result;
			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// Gets all of the caller ids.
		/// </summary>
		/// <param name="accountId">The account id.</param>
		/// <param name="phoneNumber">The phone number to filter by, if present.</param>
		/// <returns></returns>
		public IEnumerable<CallerId> GetCallerIds(Guid accountId, string phoneNumber = null)
		{
			var restRequest = new RestRequest
			{
				Resource = string.Format("/api/v1/phone/{0}/OutgoingCallerIds", accountId),
				Method = Method.GET,
				RequestFormat = DataFormat.Json
			};

			if (!string.IsNullOrWhiteSpace(phoneNumber))
			{
				var encodedPhoneNumber = HttpUtility.UrlEncode(phoneNumber);
				restRequest.Resource += string.Format("?phoneNumber={0}", encodedPhoneNumber);
			}

			try
			{
				var restClient = new TelephonyRestClient();

				var response = restClient.Execute(restRequest);

				//TODO: handle rest errors

				// Just return an empty list if there are no caller IDs for this account.
				if(string.IsNullOrWhiteSpace(response.Content))
					return new List<CallerId>();

				var result = JsonConvert.DeserializeObject<IEnumerable<CallerId>>(response.Content);

				return result;
			}
			catch (Exception)
			{
				throw;
			}
		}

        /// <summary>
        /// Gets all of the caller ids.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <param name="phoneNumber">The phone number to filter by, if present.</param>
        /// <returns></returns>
        public CallerId GetCurrentCallerId(Guid accountId)
        {
            var restRequest = new RestRequest
            {
                Resource = string.Format("/api/v1/phone/{0}/CurrentOutgoingCallerId", accountId),
                Method = Method.GET,
                RequestFormat = DataFormat.Json
            };

            try
            {
                var restClient = new TelephonyRestClient();

                var response = restClient.Execute(restRequest);

                //TODO: handle rest errors

                // Just return an empty list if there are no caller IDs for this account.
                if (string.IsNullOrWhiteSpace(response.Content))
                    return new CallerId();

                var result = JsonConvert.DeserializeObject<CallerId>(response.Content);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
		/// Searches for caller ids.
		/// </summary>
		/// <param name="accountId">The account id.</param>
		/// <param name="phoneNumber">The phone number.</param>
		/// <param name="friendlyName">Name of the friendly.</param>
		/// <param name="page">The page.</param>
		/// <param name="perPage">The per page.</param>
		/// <returns></returns>
		public PageOfT<CallerIdSearchResult> SearchForCallerIds(Guid accountId, string phoneNumber, string friendlyName, int? page = 1, int? perPage = 25)
		{
			var restRequest = new RestRequest
			{
				Resource = string.Format("/api/v1/phone/{0}/SearchForOutgoingCallerIds", accountId),
				Method = Method.POST,
				RequestFormat = DataFormat.Json
			};

			restRequest.AddBody(new
			{
				phoneNumber,
				friendlyName,
				page,
				perPage
			});

			try
			{
				var restClient = new TelephonyRestClient();

				var response = restClient.Execute(restRequest);

				//TODO: handle rest errors

				var result = JsonConvert.DeserializeObject<PageOfT<CallerIdSearchResult>>(response.Content);

				return result;
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Gets a single caller id.
		/// </summary>
		/// <param name="accountId">The account id.</param>
		/// <param name="callerIdId">The caller id id.</param>
		/// <returns></returns>
		public CallerId GetCallerId(Guid accountId, Guid callerIdId)
		{
			var restRequest = new RestRequest
			{
				Resource = string.Format("/api/v1/phone/{0}/OutgoingCallerId?callerIdId={1}", accountId, callerIdId),
				Method = Method.GET,
				RequestFormat = DataFormat.Json
			};

			var restClient = new TelephonyRestClient();

			var response = restClient.Execute(restRequest);

			//TODO: handle rest errors

			var result = JsonConvert.DeserializeObject<CallerId>(response.Content);

			return result;
		}

		/// <summary>
		/// Adds the caller id.
		/// </summary>
		/// <param name="accountId">The account id.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public AddCallerIdResult AddCallerId(Guid accountId, AddCallerIdOptions options)
		{
			var restRequest = new RestRequest
			{
				Resource = string.Format("/api/v1/phone/{0}/OutgoingCallerId", accountId),
				Method = Method.POST,
				RequestFormat = DataFormat.Json
			};

			restRequest.AddBody(options);

			try
			{
				var restClient = new TelephonyRestClient();

				var response = restClient.Execute(restRequest);

				//TODO: handle rest errors

				var result = JsonConvert.DeserializeObject<AddCallerIdResult>(response.Content);

				return result;
			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// Updates the caller id.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public UpdateCallerIdResult UpdateCallerId(UpdateCallerIdOptions options)
		{
			var restRequest = new RestRequest
			{
				Resource = string.Format("/api/v1/phone/{0}/OutgoingCallerId", options.TelephonyAccountId),
				Method = Method.PUT,
				RequestFormat = DataFormat.Json
			};

			restRequest.AddBody(options);

			try
			{
				var restClient = new TelephonyRestClient();

				var response = restClient.Execute(restRequest);

				//TODO: handle rest errors

				var result = JsonConvert.DeserializeObject<UpdateCallerIdResult>(response.Content);

				return result;
			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// Removes the caller id.
		/// </summary>
		/// <param name="accountId">The account id.</param>
		/// <param name="callerIdId">The caller id id.</param>
		/// <param name="providerId">The provider id.</param>
		/// <returns></returns>
		public RemoveCallerIdResult RemoveCallerId(Guid accountId, Guid callerIdId, string providerId = null)
		{
			var restRequest = new RestRequest
			{
				Resource = string.Format("/api/v1/phone/{0}/OutgoingCallerId?callerIdId={1}", accountId, callerIdId),
				Method = Method.DELETE,
				RequestFormat = DataFormat.Json
			};
			
			if (!string.IsNullOrWhiteSpace(providerId))
				restRequest.Resource += string.Format("&providerId={0}", providerId);

			try
			{
				var restClient = new TelephonyRestClient();

				var response = restClient.Execute(restRequest);

				//TODO: handle rest errors

				var result = JsonConvert.DeserializeObject<RemoveCallerIdResult>(response.Content);

				return result;
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Gets the caller id verification status.
		/// </summary>
		/// <param name="telephonyAccountId">The telephony account id.</param>
		/// <param name="telephonyCallerIdId">The telephony caller id id.</param>
		/// <returns></returns>
		public CallerIdVerificationStatusResult GetCallerIdVerificationStatus(Guid telephonyAccountId, Guid telephonyCallerIdId)
		{
			var restRequest = new RestRequest
			{
				Resource = string.Format("/api/v1/phone/{0}/OutgoingCallerIdVerificationStatus?callerIdId={1}", telephonyAccountId, telephonyCallerIdId),
				Method = Method.GET,
				RequestFormat = DataFormat.Json
			};

			var restClient = new TelephonyRestClient();

			var response = restClient.Execute(restRequest);

			//TODO: handle rest errors

			var result = JsonConvert.DeserializeObject<CallerIdVerificationStatusResult>(response.Content);

			return result;
		}

		/// <summary>
		/// Makes the single outbound call.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public OutboundCallResult MakeSingleOutboundCall(OutboundCallOptions options)
		{
			var restRequest = new RestRequest
			{
				Resource = string.Format("/api/v1/phone/{0}/MakeSingleOutboundCall", options.TelephonyAccountId),
				Method = Method.POST,
				RequestFormat = DataFormat.Json
			};

			restRequest.AddBody(options);

			var restClient = new TelephonyRestClient();

			var response = restClient.Execute(restRequest);

			//TODO: handle rest errors

			var result = JsonConvert.DeserializeObject<OutboundCallResult>(response.Content);

			return result;			
		}

        /// <summary>
        /// Makes the multiple outbound call.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public OutboundCallResult MakeMultipleOutboundCallParent(OutboundCallOptions options)
        {
            var restRequest = new RestRequest
            {
                Resource = string.Format("/api/v1/phone/{0}/MakeMultipleOutboundCallParent", options.TelephonyAccountId),
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };

            restRequest.AddBody(options);

            var restClient = new TelephonyRestClient();

            var response = restClient.Execute(restRequest);

            //TODO: handle rest errors

            var result = JsonConvert.DeserializeObject<OutboundCallResult>(response.Content);

            return result;
        }

        /// <summary>
        /// Makes the multiple outbound call.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public OutboundCallResult MakeMultipleOutboundCallChild(OutboundCallOptions options)
        {
            var restRequest = new RestRequest
            {
                Resource = string.Format("/api/v1/phone/{0}/MakeMultipleOutboundCallChild", options.TelephonyAccountId),
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };

            restRequest.AddBody(options);

            var restClient = new TelephonyRestClient();

            var response = restClient.Execute(restRequest);

            //TODO: handle rest errors

            var result = JsonConvert.DeserializeObject<OutboundCallResult>(response.Content);

            return result;
        }

        /// <summary>
        /// End a single outbound call.
        /// </summary>
        /// <param name="telephonyAccountId">telephony account id</param>
        /// <param name="telephonyCallId">telephony call id</param>
        /// <returns></returns>
        public TelephonyResult EndSingleOutboundCall(Guid telephonyAccountId, Guid telephonyCallId)
        {
            var restRequest = new RestRequest
            {
                Resource = string.Format("/api/v1/phone/{0}/EndSingleOutboundCall", telephonyAccountId),
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };

            restRequest.AddBody(telephonyCallId);

            var restClient = new TelephonyRestClient();

            var response = restClient.Execute(restRequest);

            //TODO: handle rest errors

            var result = JsonConvert.DeserializeObject<TelephonyResult>(response.Content);

            return result;
        }

        /// <summary>
        /// End parent call of multiple outbound call.
        /// </summary>
        /// <param name="telephonyAccountId">telephony account id</param>
        /// <param name="telephonyCallId">telephony call id</param>
        /// <returns></returns>
        public TelephonyResult EndParentMultipleOutboundCall(Guid telephonyAccountId, Guid telephonyCallId)
        {
            var restRequest = new RestRequest
            {
                Resource = string.Format("/api/v1/phone/{0}/EndParentMultipleOutboundCall", telephonyAccountId),
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };

            restRequest.AddBody(telephonyCallId);

            var restClient = new TelephonyRestClient();

            var response = restClient.Execute(restRequest);

            //TODO: handle rest errors

            var result = JsonConvert.DeserializeObject<TelephonyResult>(response.Content);

            return result;
        }

        /// <summary>
        /// End child call of multiple outbound call.
        /// </summary>
        /// <param name="telephonyAccountId">telephony account id</param>
        /// <param name="telephonyCallId">telephony call id</param>
        /// <returns></returns>
        public TelephonyResult EndChildMultipleOutboundCall(Guid telephonyAccountId, Guid telephonyCallId)
        {
            var restRequest = new RestRequest
            {
                Resource = string.Format("/api/v1/phone/{0}/EndChildMultipleOutboundCall", telephonyAccountId),
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };

            restRequest.AddBody(telephonyCallId);

            var restClient = new TelephonyRestClient();

            var response = restClient.Execute(restRequest);

            //TODO: handle rest errors

            var result = JsonConvert.DeserializeObject<TelephonyResult>(response.Content);

            return result;
        }

		/// <summary>
		/// Extend call pause
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns></returns>
        public TelephonyResult ExtendCallPause(PauseCallOptions options)
        {
            var restRequest = new RestRequest
            {
                Resource = string.Format("/api/v1/phone/{0}/ExtendPauseOutboundCall", options.TelephonyAccountId),
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };

            restRequest.AddBody(options);

            var restClient = new TelephonyRestClient();

            var response = restClient.Execute(restRequest);

            //TODO: handle rest errors

            var result = JsonConvert.DeserializeObject<TelephonyResult>(response.Content);

            return result;
        }

        /// <summary>
        /// Disconnect parent call
        /// </summary>
        /// <param name="telephonyAccountId">telephony account id</param>
        /// <param name="telephonyCallId">telephony call id</param>
        /// <returns></returns>
        public TelephonyResult DisconnectParentCall(Guid telephonyAccountId, Guid telephonyCallId)
        {
            var restRequest = new RestRequest
            {
                Resource = string.Format("/api/v1/phone/{0}/DisconnectParentCall", telephonyAccountId),
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };

            restRequest.AddBody(telephonyCallId);

            var restClient = new TelephonyRestClient();

            var response = restClient.Execute(restRequest);

            //TODO: handle rest errors

            var result = JsonConvert.DeserializeObject<TelephonyResult>(response.Content);

            return result;
        }

		/// <summary>
		/// Gets the outbound call status.
		/// </summary>
		/// <param name="telephonyAccountId">The telephony account id.</param>
		/// <param name="telephonyCallId">The telephony call id.</param>
		/// <returns></returns>
		public OutboundCallResult GetOutboundCallStatus(Guid telephonyAccountId, Guid telephonyCallId)
		{
			var restRequest = new RestRequest
			{
				Resource = string.Format("/api/v1/phone/{0}/OutboundCall?telephonyCallId={1}", telephonyAccountId, telephonyCallId),
				Method = Method.GET,
				RequestFormat = DataFormat.Json
			};

			var restClient = new TelephonyRestClient();

			var response = restClient.Execute(restRequest);

			//TODO: handle rest errors

			var result = JsonConvert.DeserializeObject<OutboundCallResult>(response.Content);

			return result;
		}

		/// <summary>
		/// Gets the outbound child call status.
		/// </summary>
		/// <param name="telephonyAccountId">The telephony account id.</param>
		/// <param name="telephonyCallId">The telephony call id.</param>
		/// <param name="toPhoneNumber">To phone number.</param>
		/// <param name="fromPhoneNumber">From phone number.</param>
		/// <param name="callStartTime">The call start time.</param>
		/// <returns></returns>
		public OutboundCallResult GetOutboundChildCallStatus(Guid telephonyAccountId, Guid telephonyCallId, string toPhoneNumber, string fromPhoneNumber, DateTime? callStartTime)
		{
			//var encodedToPhoneNumber = HttpUtility.UrlEncode(toPhoneNumber);
			//var encodedFromPhoneNumber = HttpUtility.UrlEncode(fromPhoneNumber);

			var options = new OutboundCallStatusOptions
			{
				TelephonyAccountId = telephonyAccountId,
				TelephonyCallId = telephonyCallId,
				FromNumber = fromPhoneNumber,
				ToNumber = toPhoneNumber,
				CallStartTime = callStartTime,
			};

			var restRequest = new RestRequest
			{
				Resource = string.Format("/api/v1/phone/{0}/OutboundChildCall", telephonyAccountId),
				Method = Method.POST,
				RequestFormat = DataFormat.Json
			};

			restRequest.AddBody(options);

			var restClient = new TelephonyRestClient();

			var response = restClient.Execute(restRequest);

			//TODO: handle rest errors

			var result = JsonConvert.DeserializeObject<OutboundCallResult>(response.Content);

			return result;
		}

		/// <summary>
		/// Gets the local time.
		/// </summary>
		/// <param name="latitude">The latitude.</param>
		/// <param name="longitude">The longitude.</param>
		/// <returns></returns>
        public string GetLocalTime(string latitude, string longitude)
        {
            if (string.IsNullOrWhiteSpace(latitude))
            {
                //log error
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(longitude))
            {
                //log error
                return string.Empty;
            }

			var restRequest = new RestRequest
			{
				Resource = string.Format("/api/v1/timezone/{0},{1}/localtime", latitude,longitude),
				Method = Method.GET,
				RequestFormat = DataFormat.Json
			};

			var restClient = new TelephonyRestClient();

			var response = restClient.Execute(restRequest);

			//TODO: handle rest errors

			return JsonConvert.DeserializeObject<string>(response.Content);
        }

        /// <summary>
        /// Gets the time zone description based on zipcode.
        /// </summary>
        /// <param name="zipCode">zipcode.</param>
        /// <returns></returns>
        public string GetTimeZoneDescription(string zipCode)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
            {
                //log error
                return string.Empty;
            }

            var restRequest = new RestRequest
            {
                Resource = string.Format("/api/v1/timezone/{0}/description", zipCode),
                Method = Method.GET,
                RequestFormat = DataFormat.Json
            };

            var restClient = new TelephonyRestClient();

            var response = restClient.Execute(restRequest);

            //TODO: handle rest errors

            return JsonConvert.DeserializeObject<string>(response.Content);
        }

		/// <summary>
		/// Gets the geo lat LNG.
		/// </summary>
		/// <param name="zipCode">The zip code.</param>
		/// <returns></returns>
        public LatitudeLongitude GetGeoLatLng(string zipCode)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
            {
                //log error
                return new LatitudeLongitude {Latitude = decimal.MaxValue, Longitude = decimal.MaxValue};
            }

            var restRequest = new RestRequest
            {
                Resource = string.Format("/api/v1/timezone/{0}/geolatlng", zipCode),
                Method = Method.GET,
                RequestFormat = DataFormat.Json
            };

            var restClient = new TelephonyRestClient();

            var response = restClient.Execute(restRequest);

            //TODO: handle rest errors

            return JsonConvert.DeserializeObject<LatitudeLongitude>(response.Content);
        }

		/// <summary>
		/// Gets the outbound call log paged.
		/// </summary>
		/// <param name="telephonyAccountId">The telephony account id.</param>
		/// <param name="page">The page.</param>
		/// <param name="itemsPerPage">The items per page.</param>
		/// <param name="isFinal">if set to <c>true</c> [is final].</param>
		/// <param name="childOnly">if set to <c>true</c> [child only].</param>
		/// <returns></returns>
		public TelephonyResult<PageOfT<CallLog>> GetOutboundCallLogPaged(Guid telephonyAccountId, int page = 1, int itemsPerPage = 25, bool? isFinal = null, bool? childOnly = null)
		{
			var restRequest = new RestRequest
			{
				Resource = string.Format("/api/v1/phone/{0}/OutboundCallLogPaged?page={1}&itemsPerPage={2}", telephonyAccountId, page, itemsPerPage),
				Method = Method.GET,
				RequestFormat = DataFormat.Json
			};

			if (isFinal.HasValue)
				restRequest.Resource += string.Format("&isFinal={0}", isFinal.Value);

            if (childOnly.HasValue)
                restRequest.Resource += string.Format("&childOnly={0}", childOnly.Value);

			var restClient = new TelephonyRestClient();

			var response = restClient.Execute(restRequest);

			//TODO: handle rest errors

			var result = JsonConvert.DeserializeObject<TelephonyResult<PageOfT<CallLog>>>(response.Content);

			return result;
		}

		public TelephonyResult<string> GetLastOutboundParentCallStatus(Guid telephonyAccountId)
		{
			var restRequest = new RestRequest
			{
				Resource = string.Format("/api/v1/phone/{0}/LastOutboundParentCallStatus", telephonyAccountId),
				Method = Method.GET,
				RequestFormat = DataFormat.Json
			};

			var restClient = new TelephonyRestClient();

			var response = restClient.Execute(restRequest);

			//TODO: handle rest errors

			var result = JsonConvert.DeserializeObject<TelephonyResult<string>>(response.Content);

			return result;
		}

        public CreditTransaction AdjustCredits(Guid telephonyAccountId, AdjustmentType type, string username, string processedBy, int minutes, string orderId)
        {
            var restRequest = new RestRequest
            {
                Resource = string.Format("/api/v1/credits/{0}/AdjustCredits", telephonyAccountId),
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };

            restRequest.AddBody(new AdjustmentOptions()
            {
                AdjustmentType = type,
                Username = username,
                Minutes = minutes,
                OrderId = orderId,
                ProcessedBy = processedBy
            });

            var restClient = new TelephonyRestClient();

            var response = restClient.Execute(restRequest);
			if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
            {
                var result = JsonConvert.DeserializeObject<CreditResult<CreditTransaction>>(response.Content);

                return result.Value;
            }
            return null;
        }

        public CreditTransaction DeductCredits(Guid telephonyAccountId, Guid? callId, string username, string processedBy, int seconds, string orderId)
        {
            var restRequest = new RestRequest
            {
                Resource = string.Format("/api/v1/credits/{0}/DeductCredits", telephonyAccountId),
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };

            restRequest.AddBody(new DeductOptions()
            {
                OrderId = orderId,
                ProcessedBy = processedBy,
                Seconds = seconds,
                TelephonyCallId = callId,
                Username = username
            });

            var restClient = new TelephonyRestClient();

            var response = restClient.Execute(restRequest);
			if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
            {
                var result = JsonConvert.DeserializeObject<CreditResult<CreditTransaction>>(response.Content);

                return result.Value;
            }
            return null;
        }

        public CreditTransaction Read(int id)
        {
            var restRequest = new RestRequest
            {
                Resource = string.Format("/api/v1/credits/{0}/ReadById", id),
                Method = Method.GET,
                RequestFormat = DataFormat.Json
            };

            var restClient = new TelephonyRestClient();

            var response = restClient.Execute(restRequest);
			if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
	        {
                var result = JsonConvert.DeserializeObject<CreditResult<CreditTransaction>>(response.Content);

                return result.Value;
            }
            return null;
        }

        public CreditSummary ReadSummary(Guid telephonyAccountId)
        {
            var restRequest = new RestRequest
            {
                Resource = string.Format("/api/v1/credits/{0}/ReadSummary", telephonyAccountId),
                Method = Method.GET,
                RequestFormat = DataFormat.Json
            };

            var restClient = new TelephonyRestClient();

            var response = restClient.Execute(restRequest);            
	        if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
	        {
                var result = JsonConvert.DeserializeObject<CreditResult<CreditSummary>>(response.Content);

                return result.Value;
            }
            return null;
        }

        public int ReadAvailable(Guid telephonyAccountId)
        {
            var restRequest = new RestRequest
            {
                Resource = string.Format("/api/v1/credits/{0}/ReadAvailable", telephonyAccountId),
                Method = Method.GET,
                RequestFormat = DataFormat.Json
            };

            var restClient = new TelephonyRestClient();

            var response = restClient.Execute(restRequest);

            var result = JsonConvert.DeserializeObject<CreditResult<int>>(response.Content);

            return result.Value;
        }

	    public IEnumerable<CreditTransaction> ReadTransactions(Guid telephonyAccountId)
        {
            var restRequest = new RestRequest
            {
                Resource = string.Format("/api/v1/credits/{0}/ReadTransactions", telephonyAccountId),
                Method = Method.GET,
                RequestFormat = DataFormat.Json
            };

            var restClient = new TelephonyRestClient();

            var response = restClient.Execute(restRequest);
			if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
	        {
	            var result = JsonConvert.DeserializeObject<CreditResult<IEnumerable<CreditTransaction>>>(response.Content);

	            return result.Value;
	        }
            return null;
        }
		#endregion
    }
}
