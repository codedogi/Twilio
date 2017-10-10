using System;
using System.Linq;
using System.Collections.Generic;
using NLog;
using O2.Telephony.Dal.Interfaces;
using O2.Telephony.Logic.Interfaces;
using O2.Telephony.Models;
using O2.Telephony.Models.CallerId;
using O2.Telephony.Models.OutboundCall;
using O2.Telephony.Provider.Interfaces;

namespace O2.Telephony.Logic
{
	public class PhoneLogic : BaseLogic, IPhoneLogic
	{
	    private readonly IPhoneDal _phoneDal;
		private readonly IProviderLogic _providerLogic;
	    private readonly ITimeZoneLogic _timeZoneLogic;
	    private readonly ICreditLogic _creditLogic;

		public PhoneLogic(IPhoneDal phoneDal, IProviderLogic providerLogic, ITimeZoneLogic timeZoneLogic, ICreditLogic creditLogic)
			: base(LogManager.GetCurrentClassLogger())
		{
			Logger.Trace("Instantiated");
		    _phoneDal = phoneDal;
			_providerLogic = providerLogic;
		    _timeZoneLogic = timeZoneLogic;
		    _creditLogic = creditLogic;
		}

		public PhoneNumberAvailableResult<IEnumerable<PhoneNumberAvailable>> GetAvailableLocalPhoneNumbers(
            Guid accountId, PhoneNumberAvailableFilterLocal filter)
		{
			Logger.Debug($"GetAvailableLocalPhoneNumbers({accountId}, {filter})");

			try
			{
				//validate
				if (accountId == Guid.Empty)
				{
					Logger.Trace("PhoneNumberAvailableResultCode.InvalidParameter, accountId");
					return new PhoneNumberAvailableResult<IEnumerable<PhoneNumberAvailable>>(PhoneNumberAvailableResultCode.InvalidParameter, "accountId");
				}

				//get available numbers
				var result = _providerLogic.GetAvailableLocalPhoneNumbers(accountId, ConvertFilter(filter));

				if (result.HasError)
				{
					Logger.Trace($"AccountResultCode.ProviderError, providerMessage: {result.ErrorMessage}");
					return new PhoneNumberAvailableResult<IEnumerable<PhoneNumberAvailable>>(
                        PhoneNumberAvailableResultCode.ProviderError,
					    $"Provider failed to get available local phone numbers, accountId: {accountId}, filter: {filter}");
				}

				return new PhoneNumberAvailableResult<IEnumerable<PhoneNumberAvailable>>(ConvertPhoneNumberAvailableList(result.Value));
			}
			catch (Exception ex)
			{
				Logger.Error(ex, $"Error in GetAvailableLocalPhoneNumbers({accountId}, {filter})");
				return new PhoneNumberAvailableResult<IEnumerable<PhoneNumberAvailable>>(
                    PhoneNumberAvailableResultCode.DatabaseError, ex.Message);
			}
		}

		private IEnumerable<PhoneNumberAvailable> ConvertPhoneNumberAvailableList(IEnumerable<Provider.Models.PhoneNumberAvailable> numbers)
		{
			var list = new List<PhoneNumberAvailable>();

			foreach (var number in numbers)
			{
				list.Add(new PhoneNumberAvailable
							 {
								 FriendlyName = number.FriendlyName,
								 IsoCountry = number.IsoCountry,
								 Lata = number.Lata,
								 Latitude = number.Latitude,
								 Longitude = number.Longitude,
								 PhoneNumber = number.PhoneNumber,
								 PostalCode = number.PostalCode,
								 RateCenter = number.RateCenter,
								 Region = number.Region
							 });
			}

			return list;
		}

		private Provider.Models.PhoneNumberAvailableFilterLocal ConvertFilter(PhoneNumberAvailableFilterLocal filter)
		{
			if (filter == null)
			{
				return new Provider.Models.PhoneNumberAvailableFilterLocal();
			}

			return new Provider.Models.PhoneNumberAvailableFilterLocal
					   {
						   AreaCode = filter.AreaCode,
						   Contains = filter.Contains,
						   InPostalCode = filter.InPostalCode,
						   NearNumber = filter.NearNumber
					   };
		}

		private Provider.Models.PhoneNumberAvailableFilterTollFree ConvertFilter(PhoneNumberAvailableFilterTollFree filter)
		{
			if (filter == null)
			{
				return new Provider.Models.PhoneNumberAvailableFilterTollFree();
			}

			return new Provider.Models.PhoneNumberAvailableFilterTollFree()
			{
				Contains = filter.Contains
			};
		}

		public PhoneNumberAvailableResult<IEnumerable<PhoneNumberAvailable>> GetAvailableTollFreePhoneNumbers(Guid accountId,
																											  PhoneNumberAvailableFilterTollFree
																												  filter)
		{
			Logger.Debug($"GetAvailableTollFreePhoneNumbers({accountId}, {filter})");

			try
			{
				//validate
				if (accountId == Guid.Empty)
				{
					Logger.Trace("PhoneNumberAvailableResultCode.InvalidParameter, accountId");
					return new PhoneNumberAvailableResult<IEnumerable<PhoneNumberAvailable>>(PhoneNumberAvailableResultCode.InvalidParameter, "accountId");
				}

				//get available numbers
				var result = _providerLogic.GetAvailableTollFreePhoneNumbers(accountId, ConvertFilter(filter));

				if (result.HasError)
				{
					Logger.Trace($"AccountResultCode.ProviderError, providerMessage: {result.ErrorMessage}");
					return new PhoneNumberAvailableResult<IEnumerable<PhoneNumberAvailable>>(
                        PhoneNumberAvailableResultCode.ProviderError,
					    $"Provider failed to get available toll free phone numbers, accountId: {accountId}, filter: {filter}");
				}

				return new PhoneNumberAvailableResult<IEnumerable<PhoneNumberAvailable>>(ConvertPhoneNumberAvailableList(result.Value));
			}
			catch (Exception ex)
			{
				Logger.Error(ex, $"Error in GetAvailableTollFreePhoneNumbers({accountId}, {filter})");
				return new PhoneNumberAvailableResult<IEnumerable<PhoneNumberAvailable>>(
                    PhoneNumberAvailableResultCode.DatabaseError, ex.Message);
			}
		}

		public PhoneNumberResult CreateIncomingPhoneNumber(Guid accountId, PhoneNumberOption option)
		{
			Logger.Debug($"CreateIncomingPhoneNumber({accountId}, {option})");

			try
			{
				//validate
				if (accountId == Guid.Empty)
				{
					Logger.Trace($"PhoneNumberResultCode.InvalidParameter, accountId: {accountId}");
					return new PhoneNumberResult(PhoneNumberResultCode.InvalidParameter, "accountId");
				}

				if (option == null)
				{
					Logger.Trace("PhoneNumberResultCode.InvalidParameter, option: null");
					return new PhoneNumberResult(PhoneNumberResultCode.InvalidParameter, "option");
				}

				//create incoming phone number
				var result = _providerLogic.CreateIncomingPhoneNumber(accountId, ConvertOption(option));

				if (result.HasError)
				{
					Logger.Trace($"PhoneNumberResultCode.ProviderError, providerMessage: {result.ErrorMessage}");
					return new PhoneNumberResult(PhoneNumberResultCode.ProviderError,
					    $"Provider failed to create incoming phone number, accountId: {accountId}, option: {option}");
				}

				return new PhoneNumberResult();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, $"Error in CreateIncomingPhoneNumber({accountId}, {option})");
				return new PhoneNumberResult(PhoneNumberResultCode.DatabaseError, ex.Message);
			}
		}

		private Provider.Models.PhoneNumberOption ConvertOption(PhoneNumberOption option)
		{
			if (option == null)
			{
				return new Provider.Models.PhoneNumberOption();
			}

			return new Provider.Models.PhoneNumberOption()
			{
				AccountSid = option.AccountSid,
				AreaCode = option.AreaCode,
				FriendlyName = option.FriendlyName,
				PhoneNumber = option.PhoneNumber,
				SmsApplicationSid = option.SmsApplicationSid,
				SmsFallbackMethod = option.SmsFallbackMethod,
				SmsFallbackUrl = option.SmsFallbackUrl,
				SmsMethod = option.SmsMethod,
				SmsUrl = option.SmsUrl,
				StatusCallback = option.StatusCallback,
				StatusCallbackMethod = option.StatusCallbackMethod,
				VoiceApplicationSid = option.VoiceApplicationSid,
				VoiceCallerIdLookup = option.VoiceCallerIdLookup,
				VoiceFallbackMethod = option.VoiceFallbackMethod,
				VoiceFallbackUrl = option.VoiceFallbackUrl,
				VoiceMethod = option.VoiceMethod,
				VoiceUrl = option.VoiceUrl
			};
		}

		#region Caller ID

		/// <summary>
		/// Gets the outgoing caller id.
		/// </summary>
		/// <param name="accountId">The account id.</param>
		/// <param name="callerIdId">The caller id id.</param>
		/// <returns></returns>
		public TelephonyResult<CallerId> GetOutgoingCallerId(Guid accountId, Guid callerIdId)
		{
			Logger.Debug($"GetOutgoingCallerId({accountId}, {callerIdId})");

			try
			{
				//validate
				if (accountId == Guid.Empty)
				{
					Logger.Trace($"GetOutgoingCallerId.InvalidParameter, accountId: {accountId}");
					return new TelephonyResult<CallerId>(TelephonyResultCode.InvalidParameter, "Missing accountId parameter");
				}

				if (callerIdId == Guid.Empty)
				{
					Logger.Trace($"GetOutgoingCallerId.InvalidParameter, callerIdId: {callerIdId}");
					return new TelephonyResult<CallerId>(TelephonyResultCode.InvalidParameter, "Missing callerIdId parameter");
				}

				// Get outgoing caller ID
				var result = _providerLogic.GetOutgoingCallerId(accountId, callerIdId);

				if (result.HasError)
				{
					Logger.Trace($"GetOutgoingCallerId.ProviderError, providerMessage: {result.ErrorMessage}");
					return new TelephonyResult<CallerId>(
                        TelephonyResultCode.ProviderError,
					    $"Provider failed to get outgoing caller ID, accountId: {accountId}, callerIdId: {callerIdId}");
				}

				// TODO:  Get and compare provider's caller ID to the one in the database...sync if necessary.

				var callerId = result.Value;

			    return new TelephonyResult<CallerId>(new CallerId
			                                             {
			                                                 Id = callerId.Id,
			                                                 AccountId = callerId.AccountId,
			                                                 PhoneNumber = callerId.PhoneNumber,
			                                                 FriendlyName = callerId.FriendlyName,
			                                                 Created = callerId.DateCreated,
			                                                 Updated = callerId.DateUpdated
			                                             });

			}
			catch (Exception ex)
			{
				Logger.Error(ex, $"Exception in GetOutgoingCallerId({accountId}, {callerIdId})");
				return new TelephonyResult<CallerId>(TelephonyResultCode.Error, ex.Message);
			}
		}

        /// <summary>
        /// Gets the current outgoing caller id.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns>Caller ID info</returns>
        public TelephonyResult<CallerId> GetCurrentOutgoingCallerId(Guid accountId)
        {
            Logger.Debug($"GetCurrentOutgoingCallerId({accountId})");

            try
            {
                //validate
                if (accountId == Guid.Empty)
                {
                    Logger.Trace($"GetCurrentOutgoingCallerId.InvalidParameter, accountId: {accountId}");
                    return new TelephonyResult<CallerId>(TelephonyResultCode.InvalidParameter, "Missing accountId parameter");
                }

                // Get current outgoing caller ID
                var result = _providerLogic.GetCurrentOutgoingCallerId(accountId);

                if (result.HasError)
                {
                    Logger.Trace($"GetCurrentOutgoingCallerId.ProviderError, providerMessage: {result.ErrorMessage}");
                    return new TelephonyResult<CallerId>(
                        TelephonyResultCode.ProviderError,
                        $"Provider failed to get current outgoing caller ID, accountId: {accountId}");
                }

                var callerId = result.Value;

                return new TelephonyResult<CallerId>(new CallerId
                {
                    Id = callerId.Id,
                    AccountId = callerId.AccountId,
                    PhoneNumber = callerId.PhoneNumber,
                    FriendlyName = callerId.FriendlyName,
                    Created = callerId.DateCreated,
                    Updated = callerId.DateUpdated
                });

            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Exception in GetCurrentOutgoingCallerId({accountId})");
                return new TelephonyResult<CallerId>(TelephonyResultCode.Error, ex.Message);
            }
        }

		/// <summary>
		/// Gets all of the outgoing caller ids.
		/// </summary>
		/// <param name="accountId">The account id.</param>
		/// <param name="phoneNumber">The phone number to filter by.</param>
		/// <returns></returns>
		public TelephonyResult<IEnumerable<CallerId>> GetOutgoingCallerIds(Guid accountId, string phoneNumber = null)
		{
			Logger.Debug($"GetOutgoingCallerIds({accountId})");

			try
			{
				//validate
				if (accountId == Guid.Empty)
				{
					Logger.Trace($"GetOutgoingCallerIds.InvalidParameter, accountId: {accountId}");
					return new TelephonyResult<IEnumerable<CallerId>>(TelephonyResultCode.InvalidParameter, "Missing accountId parameter");
				}

				// Get outgoing caller IDs
				var result = _providerLogic.GetOutgoingCallerIds(accountId, phoneNumber);

				if (result.HasError)
				{
					Logger.Trace($"GetOutgoingCallerIds.ProviderError, providerMessage: {result.ErrorMessage}");
					return new TelephonyResult<IEnumerable<CallerId>>(TelephonyResultCode.ProviderError,
					    $"Provider failed to get list of outgoing caller IDs, accountId: {accountId}");
				}

				// TODO:  Get and compare provider's caller ID to the one in the database...sync if necessary.

				var callerIds = new List<CallerId>();
				callerIds.AddRange(result.Value.Select(c => new CallerId
				{
					Id = c.Id,
					AccountId = c.AccountId,
					PhoneNumber = c.PhoneNumber,
					FriendlyName = c.FriendlyName,
					Created = c.DateCreated,
					Updated = c.DateUpdated,
				}));

			    return new TelephonyResult<IEnumerable<CallerId>>(callerIds);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, $"Exception in GetOutgoingCallerIds({accountId})");
				return new TelephonyResult<IEnumerable<CallerId>>(TelephonyResultCode.Error, ex.Message);
			}
		}

		/// <summary>
		/// Searches for outgoing caller ids.
		/// </summary>
		/// <param name="accountId">The account id.</param>
		/// <param name="phoneNumber">The phone number.</param>
		/// <param name="friendlyName">Name of the friendly.</param>
		/// <param name="page">The page.</param>
		/// <param name="perPage">The per page.</param>
		/// <returns></returns>
		public TelephonyResult<PageOfT<CallerIdSearchResult>> SearchForOutgoingCallerIds(Guid accountId, string phoneNumber, string friendlyName, int? page, int? perPage)
		{
			Logger.Debug($"SearchForOutgoingCallerIds({accountId})");

			try
			{
				//validate
				if (accountId == Guid.Empty)
				{
					Logger.Trace($"SearchForOutgoingCallerIds.InvalidParameter, accountId: {accountId}");
					return new TelephonyResult<PageOfT<CallerIdSearchResult>>(
                        TelephonyResultCode.InvalidParameter, "Missing accountId parameter");
				}

				// Get outgoing caller IDs
				var result = _providerLogic.SearchForOutgoingCallerIds(accountId, phoneNumber, friendlyName, page, perPage);

				if (result.HasError)
				{
					Logger.Trace($"SearchForOutgoingCallerIds.ProviderError, providerMessage: {result.ErrorMessage}");
					return new TelephonyResult<PageOfT<CallerIdSearchResult>>(TelephonyResultCode.ProviderError,
					    $"Provider failed to search for outgoing caller IDs, accountId: {accountId}");
				}

				var returnResult = new PageOfT<CallerIdSearchResult>
				{
					CurrentPage = result.Value.CurrentPage,
					TotalItems = result.Value.TotalItems,
					TotalPages = result.Value.TotalPages,
					ItemsPerPage = result.Value.ItemsPerPage,
					Items = new List<CallerIdSearchResult>()
				};

				returnResult.Items.AddRange(result.Value.Items.Select(c => new CallerIdSearchResult
				{
					Id = c.Id,
					AccountId = c.AccountId,
					PhoneNumber = c.PhoneNumber,
					FriendlyName = c.FriendlyName,
					ProviderId = c.ProviderId,
					DateCreated = c.DateCreated,
					DateUpdated = c.DateUpdated,
				}));

				return new TelephonyResult<PageOfT<CallerIdSearchResult>>(returnResult);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, $"Exception in SearchForOutgoingCallerIds({accountId})");
				return new TelephonyResult<PageOfT<CallerIdSearchResult>>(TelephonyResultCode.Error, ex.Message);
			}
		}

	    /// <summary>
	    /// Adds the outgoing caller id.
	    /// </summary>
	    /// <param name="options">The options.</param>
	    /// <returns></returns>
	    public AddCallerIdResult AddOutgoingCallerId(AddCallerIdOptions options)
		{
			try
			{
				//validate
				if (options == null)
				{
					Logger.Trace("AddOutgoingCallerId.InvalidParameter, options: null");
					return new AddCallerIdResult(CallerIdResultCode.InvalidParameter, "Missing options parameter");
				}

				if (options.TelephonyAccountId == Guid.Empty)
				{
					Logger.Trace($"AddOutgoingCallerId.InvalidParameter, accountId: {options.TelephonyAccountId}");
					return new AddCallerIdResult(CallerIdResultCode.InvalidParameter, "Missing accountId parameter");
				}

				if (options.TelephonyCallerIdId == Guid.Empty)
				{
					Logger.Trace($"AddOutgoingCallerId.InvalidParameter, telephonyCallerIdId: {options.TelephonyCallerIdId}");
					return new AddCallerIdResult(CallerIdResultCode.InvalidParameter, "Missing callerIdId parameter");
				}

				// Create outgoing caller ID
				var result = _providerLogic.AddOutgoingCallerId(ConvertAddCallerId(options));

				if (result.HasError)
				{
					Logger.Trace($"AddOutgoingCallerId.ProviderError, providerMessage: {result.ErrorMessage}");
					return new AddCallerIdResult(CallerIdResultCode.ProviderError,
					    $"Provider failed to add outgoing caller ID, options: {options}");
				}

				try
				{
					_phoneDal.CreateOutgoingCallerId(options.TelephonyAccountId, options.TelephonyCallerIdId);
				}
				catch (Exception ex)
				{
					Logger.Error(ex, $"AddOutgoingCallerId.DatabaseError, options: {options}");
					return new AddCallerIdResult(CallerIdResultCode.DatabaseError,
					    $"Database failed to add outgoing caller ID, options: {options}");					
				}

				return new AddCallerIdResult
				{
					TelephonyCallerIdId = options.TelephonyCallerIdId,
					ValidationCode = result.ValidationCode,
					ResultCode = CallerIdResultCode.Success,
				};
			}
			catch (Exception ex)
			{
				Logger.Error(ex, $"Error in AddOutgoingCallerId({options})");
				return new AddCallerIdResult(CallerIdResultCode.Error, ex.Message);
			}
		}

		/// <summary>
		/// Removes the outgoing caller id.
		/// </summary>
		/// <param name="accountId">The account id.</param>
		/// <param name="callerIdId">The caller id id.</param>
		/// <param name="providerId">The provider id.</param>
		/// <returns></returns>
		/// <exception cref="System.Exception">Error deleting caller ID</exception>
		public RemoveCallerIdResult RemoveOutgoingCallerId(Guid accountId, Guid callerIdId, string providerId = null)
		{
			Logger.Debug($"RemoveOutgoingCallerId({accountId}, {callerIdId})");

			try
			{
				//validate
				if (accountId == Guid.Empty)
				{
					Logger.Trace($"RemoveOutgoingCallerId.InvalidParameter, accountId: {accountId}");
					return new RemoveCallerIdResult(CallerIdResultCode.InvalidParameter, "Missing accountId parameter");
				}

				if (callerIdId == Guid.Empty && string.IsNullOrWhiteSpace(providerId))
				{
					Logger.Trace("RemoveOutgoingCallerId.InvalidParameter, callerIdId: null");
					return new RemoveCallerIdResult(CallerIdResultCode.InvalidParameter, "Missing callerIdId parameter");
				}

				// Can't use both the CallerIdId AND the ProviderId together...use one or the other!
				if (callerIdId != Guid.Empty && !string.IsNullOrWhiteSpace(providerId))
				{
					Logger.Trace("RemoveOutgoingCallerId.InvalidParameter, callerIdId and providerId cannot be used together");
					return new RemoveCallerIdResult(CallerIdResultCode.InvalidParameter, "Missing callerIdId and providerId parameters");
				}

				// Create outgoing caller ID
				var result = _providerLogic.RemoveOutgoingCallerId(accountId, callerIdId, providerId);

				if (result.HasError)
				{
					Logger.Trace($"RemoveOutgoingCallerId.ProviderError, providerMessage: {result.ErrorMessage}");
					return new RemoveCallerIdResult(CallerIdResultCode.ProviderError,
					    $"Provider failed to delete outgoing caller ID, accountId: {accountId}, callerIdId: {callerIdId}");
				}

				if (string.IsNullOrWhiteSpace(providerId))
				{
					try
					{
						if (!_phoneDal.DeleteOutgoingCallerId(callerIdId))
							throw new Exception("Error deleting caller ID");
					}
					catch (Exception ex)
					{
						Logger.Error(ex, $"AddOutgoingCallerId.DatabaseError, accountId: {accountId}, callerIdId: {callerIdId}");
						return new RemoveCallerIdResult(CallerIdResultCode.DatabaseError,
						    $"Database failed to add outgoing caller ID, accountId: {accountId}, options: {callerIdId}");
					}					
				}

				return new RemoveCallerIdResult
				{
					ResultCode = CallerIdResultCode.Success,
				};
			}
			catch (Exception ex)
			{
				Logger.Error(ex, $"Exception in RemoveOutgoingCallerId({accountId}, {callerIdId})");
				return new RemoveCallerIdResult(CallerIdResultCode.Error, ex.Message);
			}
		}

		/// <summary>
		/// Updates the outgoing caller id.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public UpdateCallerIdResult UpdateOutgoingCallerId(UpdateCallerIdOptions options)
		{
			Logger.Debug($"AddOutgoingCallerId({options})");

			try
			{
				//validate
				if (options == null)
				{
					Logger.Trace("AddOutgoingCallerId.InvalidParameter, options: null");
					return new UpdateCallerIdResult(CallerIdResultCode.InvalidParameter, "Missing options parameter");
				}

				if (options.TelephonyAccountId == Guid.Empty)
				{
					Logger.Trace($"AddOutgoingCallerId.InvalidParameter, accountId: {options.TelephonyAccountId}");
					return new UpdateCallerIdResult(CallerIdResultCode.InvalidParameter, "Missing accountId parameter");
				}

				// Create outgoing caller ID
				var result = _providerLogic.UpdateOutgoingCallerId(ConvertUpdateCallerId(options));

				if (result.HasError)
				{
					Logger.Trace($"AddOutgoingCallerId.ProviderError, providerMessage: {result.ErrorMessage}");
					return new UpdateCallerIdResult(CallerIdResultCode.ProviderError,
					    $"Provider failed to add outgoing caller ID, options: {options}");
				}

				return new UpdateCallerIdResult
				{
					ResultCode = CallerIdResultCode.Success,
				};
			}
			catch (Exception ex)
			{
				Logger.Error(ex, $"Error in AddOutgoingCallerId({options})");
				return new UpdateCallerIdResult(CallerIdResultCode.Error, ex.Message);
			}		
		}

		/// <summary>
		/// Gets the outgoing caller id verification status.
		/// </summary>
		/// <param name="telephonyCallerIdId">The telephony caller id id.</param>
		/// <returns></returns>
		public CallerIdVerificationStatusResult GetOutgoingCallerIdVerificationStatus(Guid telephonyCallerIdId)
		{
			Logger.Debug($"GetOutgoingCallerIdVerificationStatus({telephonyCallerIdId})");

			try
			{
				//validate
				if (telephonyCallerIdId == Guid.Empty)
				{
					Logger.Trace($"GetOutgoingCallerIdVerificationStatus.InvalidParameter, telephonyCallerIdId: {telephonyCallerIdId}");
					return new CallerIdVerificationStatusResult(CallerIdResultCode.InvalidParameter, "Missing telephonyCallerIdId parameter");
				}

				// Get outgoing caller ID
				var result = _providerLogic.GetOutgoingCallerIdVerificationStatus(telephonyCallerIdId);

				if (result.HasError)
				{
					Logger.Trace($"GetOutgoingCallerIdVerificationStatus.ProviderError, providerMessage: {result.ErrorMessage}");
					return new CallerIdVerificationStatusResult(CallerIdResultCode.ProviderError,
					    $"Provider failed to get outgoing caller ID verification status, callerIdId: {telephonyCallerIdId}");
				}

				return new CallerIdVerificationStatusResult
				{
					ResultCode = CallerIdResultCode.Success,
					TelephonyAccountId = result.TelephonyAccountId,
					TelephonyCallerIdId = result.TelephonyCallerIdId,
					ReceivedVerification = result.ReceivedVerification,
					VerificationStatus = result.VerificationStatus,
				};
			}
			catch (Exception ex)
			{
				Logger.Error(ex, $"Exception in GetOutgoingCallerIdVerificationStatus({telephonyCallerIdId})");
				return new CallerIdVerificationStatusResult(CallerIdResultCode.Error, ex.Message);
			}
		}

		/// <summary>
		/// Converts the add caller id options to the provider model's representation.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		private Provider.Models.CallerId.AddCallerIdOptions ConvertAddCallerId(AddCallerIdOptions options)
		{
			if (options == null)
				return new Provider.Models.CallerId.AddCallerIdOptions();

			return new Provider.Models.CallerId.AddCallerIdOptions()
			{
				TelephonyCallerIdId = options.TelephonyCallerIdId,
				TelephonyAccountId = options.TelephonyAccountId,
				FriendlyName = options.FriendlyName,
				PhoneNumberToVerify = options.PhoneNumberToVerify,
				VerifyDelay = options.VerifyDelay,
			};
		}

		/// <summary>
		/// Converts the update caller id options to the provider model's representation.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		private Provider.Models.CallerId.UpdateCallerIdOptions ConvertUpdateCallerId(UpdateCallerIdOptions options)
		{
			if (options == null)
				return new Provider.Models.CallerId.UpdateCallerIdOptions();

			return new Provider.Models.CallerId.UpdateCallerIdOptions()
			{
				TelephonyAccountId = options.TelephonyAccountId,
				TelephonyCallerIdId = options.TelephonyCallerIdId,
				FriendlyName = options.FriendlyName,
			};
		}

		#endregion Caller ID

		#region Outbound Calling

		/// <summary>
		/// Makes the single outbound call.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public TelephonyResult<OutboundCallResult> MakeSingleOutboundCall(OutboundCallOptions options)
		{
			try
			{
                #region Validate

			    if (options == null)
			    {
			        Logger.Trace("MakeSingleOutboundCall.InvalidParameter, options is NULL");
			        return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.InvalidParameter, "Missing options parameter");
			    }

			    Logger.Debug($"MakeSingleOutboundCall({options})");

				if (options.TelephonyAccountId == Guid.Empty)
				{
					Logger.Trace($"MakeSingleOutboundCall.InvalidParameter, accountId: {options.TelephonyAccountId}");
					return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.InvalidParameter, "Missing accountId parameter");
				}

			    if (options.TelephonyCallId == Guid.Empty)
			    {
			        Logger.Trace($"MakeSingleOutboundCall.InvalidParameter, TelephonyCallId: {options.TelephonyCallId}");
			        return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.InvalidParameter, "Missing TelephonyCallId parameter");
			    }

			    var availableResults = _creditLogic.ReadAvailable(options.TelephonyAccountId);

			    if (availableResults.HasError)
			    {
                    Logger.Warn(
                        $"MakeSingleOutboundCall.Error, Unable to get available minutes, TelephonyCallId: {options.TelephonyCallId}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.Error, "Unable to get available minutes.");			        
			    }

                if (availableResults.Value < 1)
                {
                    Logger.Warn(
                        $"MakeSingleOutboundCall.Error, No telephony minutes left, Minutes: {availableResults.Value} TelephonyCallId: {options.TelephonyCallId}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.Error, "No minutes available.");
                }
                
			    #endregion

                //get local time zone info
                var now = DateTime.Now;
			    var nowUtc = now.ToUniversalTime();
			    
                var geoTimeZone = _timeZoneLogic.ZipToTimeZone(options.TimeZoneZip) ?? _timeZoneLogic.ZipToTimeZone("68046");

			    var totalOffset = geoTimeZone.OffsetSecondsUtcRaw;
			    var timeZoneAbbr = geoTimeZone.TimeZoneAbbrStandard;

                if (now.IsDaylightSavingTime())
                {
                    totalOffset += geoTimeZone.OffsetSecondsUtcDaylight;
                    timeZoneAbbr = geoTimeZone.TimeZoneAbbrDaylight;
                }
                
                // Make outgoing call
				var outboundCallOptions = ConvertOutboundCallOptions(options, CallSessionType.SingleOutbound);

                //create call log for parent
                try
                {
                    var callLog = new CallLog
                                      {
                                          TelephonyCallId = outboundCallOptions.TelephonyCallId,
                                          TelephonyAccountId = outboundCallOptions.TelephonyAccountId,
                                          Username = options.Username,
                                          SalesRepName = options.SalesRepName,
                                          DivisionNumber = options.DivisionNumber,
                                          IsEnterprise = options.IsEnterprise,
                                          ProductType = options.ProductType,
                                          SessionType = CallSessionType.SingleOutbound,
                                          Status = CallStatusType.BeforeQueued,
                                          DurationTotalSeconds = 0,
                                          DurationRoundedMinutes = 0,
                                          DateUtc = nowUtc.Date,
                                          DateLocal = nowUtc.AddSeconds(totalOffset).Date,
                                          LocalTimeZoneAbbr = timeZoneAbbr,
                                          LocalTimeTotalOffset = totalOffset,
                                          RecordType = CallRecordType.Parent,
                                          RecordName = "Parent",
                                          FromPhone = "Parent",
                                          ToPhone = outboundCallOptions.From,
                                          Created = now,
                                          Updated = now,
                                          IsFinal = false
                                      };

                    _phoneDal.Create(callLog);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Parent CreateCallLog.DatabaseError, options: {options}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.DatabaseError,
                        $"Database failed to add parent call log, options: {options}");
                }

                //create call log for child
                try
                {
                    var callLog = new CallLog
                    {
                        TelephonyCallId = outboundCallOptions.TelephonyCallIdChild,
                        ParentTelephonyCallId = outboundCallOptions.TelephonyCallId,
                        TelephonyAccountId = outboundCallOptions.TelephonyAccountId,
                        Username = options.Username,
                        SalesRepName = options.SalesRepName,
                        DivisionNumber = options.DivisionNumber,
                        IsEnterprise = options.IsEnterprise,
                        ProductType = options.ProductType,
                        SessionType = CallSessionType.SingleOutbound,
                        Status = CallStatusType.BeforeQueued,
                        DurationTotalSeconds = 0,
                        DurationRoundedMinutes = 0,
                        DateUtc = nowUtc.Date,
                        DateLocal = nowUtc.AddSeconds(totalOffset).Date,
                        LocalTimeZoneAbbr = timeZoneAbbr,
                        LocalTimeTotalOffset = totalOffset,
                        RecordType = options.RecordType,
                        RecordId = options.RecordId,
                        RecordName = options.RecordName,
                        FromPhone = outboundCallOptions.From,
                        ToPhone = outboundCallOptions.To,
                        Created = now,
                        Updated = now,
                        IsFinal = false
                    };

                    _phoneDal.Create(callLog);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Child CreateCallLog.DatabaseError, options: {options}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.DatabaseError,
                        $"Database failed to add child call log, options: {options}");
                }

				var result = _providerLogic.MakeSingleOutboundCall(options.TelephonyAccountId, outboundCallOptions);

                //update call log
                try
                {
                    CallStatusType status;
                    if (Enum.TryParse(result.Value.Status.ToString(), true, out status) == false)
                    {
                        Logger.Error(
                            $"Failed to parse TwilioCall.Status to CallStatusType for value: {result.Value.Status}");
                    }

                    var callLog = new CallLog
                    {
                        TelephonyCallId = result.Value.TelephonyCallId,
                        Status = status,
                        DurationTotalSeconds = result.Value.DurationTotalSeconds,
                        DurationRoundedMinutes = result.Value.DurationRoundedMinutes,
                        FromPhone = result.Value.FromPhone,
                        Updated = DateTime.Now,
                    };

                    if (result.Value.DateUtc == DateTime.MinValue)
                    {
                        callLog.DateUtc = nowUtc;
                        callLog.DateLocal = now;
                    }
                    else
                    {
                        callLog.DateUtc = result.Value.DateUtc.Date;
                        callLog.DateLocal = result.Value.DateUtc.AddSeconds(totalOffset).Date;
                    }

                    _phoneDal.Update(callLog,
                                     new List<string>
                                         {
                                             "TelephonyCallId",
                                             "Status",
                                             "DurationTotalSeconds",
                                             "DurationRoundedMinutes",
                                             "DateUtc",
                                             "DateLocal",
                                             "FromPhone",
                                             "Updated"
                                         });
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "UpdateCallLog.DatabaseError");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.DatabaseError,
                        $"Database failed to add call log, options: {options}");
                }

				if (result.HasError)
				{
					Logger.Trace($"MakeSingleOutboundCall.ProviderError, providerMessage: {result.ErrorMessage}");
					return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.ProviderError,
					    $"Provider failed to make single outgoing call, options: {options}");
				}

                try
                {
                    var outboundCall = new OutboundCall
                    {
                        Id = options.TelephonyCallId,
                        AccountId = options.TelephonyAccountId,
                        Created = DateTime.Now,
                    };

                    _phoneDal.CreateOutgoingCall(outboundCall);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"AddOutgoingCallerId.DatabaseError, options: {options}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.DatabaseError,
                        $"Database failed to add outgoing, options: {options}");
                }

			    return new TelephonyResult<OutboundCallResult>(new OutboundCallResult
			                                                       {
			                                                           TelephonyCallId = result.Value.TelephonyCallId,
			                                                           TelephonyAccountId = result.Value.TelephonyAccountId,
			                                                           CallStatus = result.Value.Status.ToString(),
																	   StartTime = result.Value.StartTimeUtc,
																	   Duration = result.Value.DurationTotalSeconds,
			                                                       });
			}
			catch (Exception ex)
			{
				Logger.Error(ex, $"Exception in MakeSingleOutboundCall({options})");
				return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.Error, ex.Message);
			}			
		}

        public CallLog UpdateCallLog(Guid telephonyCallId, string callSid)
        {
            try
            {
                //get call log from db
                var dbCallLog = _phoneDal.ReadCallLog(telephonyCallId);

                if (dbCallLog == null)
                {
                    Logger.Error($"Failed to read db CallLog for telephonyCallId: {telephonyCallId}");
                    return null;
                }

                //get call log from provider
                var providerCallLog = _providerLogic.GetCallLog(dbCallLog.TelephonyAccountId, callSid);

                if (providerCallLog == null)
                {
                    Logger.Error(
                        $"Failed to read provider CallLog for telephonyAcountid: {dbCallLog.TelephonyAccountId} callSid: {callSid}");
                    return null;
                }

                //convert status from provider to telephony
                CallStatusType status;
                if (Enum.TryParse(providerCallLog.Status.ToString(), true, out status) == false)
                {
                    Logger.Error(
                        $"Failed to parse Provider.CallLog.Status to CallStatusType for value: {providerCallLog.Status}");
                }

                //set updated fields
                dbCallLog.Status = status;
                dbCallLog.DurationTotalSeconds = providerCallLog.DurationTotalSeconds;
                dbCallLog.DurationRoundedMinutes = providerCallLog.DurationRoundedMinutes;
                dbCallLog.StartTimeUtc = providerCallLog.StartTimeUtc;
                dbCallLog.EndTimeUtc = providerCallLog.EndTimeUtc;

                //determine local start time
                if (providerCallLog.StartTimeUtc.HasValue)
                {
                    dbCallLog.StartTimeLocal = providerCallLog.StartTimeUtc.Value.AddSeconds(dbCallLog.LocalTimeTotalOffset);
                }

                //determine local end time
                if (providerCallLog.EndTimeUtc.HasValue)
                {
                    dbCallLog.EndTimeLocal = providerCallLog.EndTimeUtc.Value.AddSeconds(dbCallLog.LocalTimeTotalOffset);
                }
                
                dbCallLog.Updated = DateTime.Now;
                dbCallLog.IsFinal = true;

                //update record
                _phoneDal.Update(dbCallLog, new List<string>
                                         {
                                             "TelephonyCallId",
                                             "Status",
                                             "DurationTotalSeconds",
                                             "DurationRoundedMinutes",
                                             "StartTimeUtc",
                                             "EndTimeUtc",
                                             "StartTimeLocal",
                                             "EndTimeLocal",
                                             "Updated",
                                             "IsFinal"
                                         });

                return dbCallLog;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "UpdateCallLog.DatabaseError");
            }
            return null;
        }

        /// <summary>
        /// Makes the multiple outbound call.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public TelephonyResult<OutboundCallResult> MakeMultipleOutboundCallParent(OutboundCallOptions options)
        {
            try
            {
                #region Validate
                if (options == null)
                {
                    Logger.Trace("MakeMultipleOutboundCall.InvalidParameter, options is NULL");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.InvalidParameter, "Missing options parameter");
                }

                Logger.Debug($"MakeMultipleOutboundCall({options})");

                //validate
                if (options.TelephonyAccountId == Guid.Empty)
                {
                    Logger.Trace($"MakeMultipleOutboundCall.InvalidParameter, accountId: {options.TelephonyAccountId}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.InvalidParameter, "Missing accountId parameter");
                }

                if (options.TelephonyCallId == Guid.Empty)
                {
                    Logger.Trace(
                        $"MakeMultipleOutboundCall.InvalidParameter, TelephonyCallId: {options.TelephonyCallId}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.InvalidParameter, "Missing TelephonyCallId parameter");
                }

                var availableResults = _creditLogic.ReadAvailable(options.TelephonyAccountId);

                if (availableResults.HasError)
                {
                    Logger.Warn(
                        $"MakeMultipleOutboundCall.Error, Unable to get available minutes, TelephonyCallId: {options.TelephonyCallId}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.Error, "Unable to get available minutes.");
                }

                if (availableResults.Value < 1)
                {
                    Logger.Warn(
                        $"MakeMultipleOutboundCall.Error, No telephony minutes left, Minutes: {availableResults.Value} TelephonyCallId: {options.TelephonyCallId}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.Error, "No minutes available.");
                }

                #endregion

                //get local time zone info
                var now = DateTime.Now;
                var nowUtc = now.ToUniversalTime();

                var geoTimeZone = _timeZoneLogic.ZipToTimeZone(options.TimeZoneZip) ?? _timeZoneLogic.ZipToTimeZone("68046");

                var totalOffset = geoTimeZone.OffsetSecondsUtcRaw;
                var timeZoneAbbr = geoTimeZone.TimeZoneAbbrStandard;

                if (now.IsDaylightSavingTime())
                {
                    totalOffset += geoTimeZone.OffsetSecondsUtcDaylight;
                    timeZoneAbbr = geoTimeZone.TimeZoneAbbrDaylight;
                }

                var outboundCallOptions = ConvertOutboundCallOptions(options, CallSessionType.MultipleOutbound);

                //create call log for parent
                try
                {
                    var callLog = new CallLog
                    {
                        TelephonyCallId = outboundCallOptions.TelephonyCallId,
                        TelephonyAccountId = outboundCallOptions.TelephonyAccountId,
                        Username = options.Username,
                        SalesRepName = options.SalesRepName,
                        DivisionNumber = options.DivisionNumber,
                        IsEnterprise = options.IsEnterprise,
                        ProductType = options.ProductType,
                        SessionType = CallSessionType.MultipleOutbound,
                        CampaignId = options.CampaignId,
                        CampaignName = options.CampaignName,
                        Status = CallStatusType.BeforeQueued,
                        DurationTotalSeconds = 0,
                        DurationRoundedMinutes = 0,
                        DateUtc = nowUtc.Date,
                        DateLocal = nowUtc.AddSeconds(totalOffset).Date,
                        LocalTimeZoneAbbr = timeZoneAbbr,
                        LocalTimeTotalOffset = totalOffset,
                        RecordType = CallRecordType.Parent,
                        RecordName = "Parent",
                        FromPhone = "Parent",
                        ToPhone = outboundCallOptions.From,
                        Created = now,
                        Updated = now,
                        IsFinal = false
                    };

                    _phoneDal.Create(callLog);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Parent CreateCallLog.DatabaseError, options: {options}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.DatabaseError,
                        $"Database failed to add parent call log, options: {options}");
                }

                //create call log for child
                try
                {
                    var callLog = new CallLog
                    {
                        TelephonyCallId = outboundCallOptions.TelephonyCallIdChild,
                        ParentTelephonyCallId = outboundCallOptions.TelephonyCallId,
                        TelephonyAccountId = outboundCallOptions.TelephonyAccountId,
                        Username = options.Username,
                        SalesRepName = options.SalesRepName,
                        DivisionNumber = options.DivisionNumber,
                        IsEnterprise = options.IsEnterprise,
                        ProductType = options.ProductType,
                        SessionType = CallSessionType.MultipleOutbound,
                        CampaignId = options.CampaignId,
                        CampaignName = options.CampaignName,
                        Status = CallStatusType.BeforeQueued,
                        DurationTotalSeconds = 0,
                        DurationRoundedMinutes = 0,
                        DateUtc = nowUtc.Date,
                        DateLocal = nowUtc.AddSeconds(totalOffset).Date,
                        LocalTimeZoneAbbr = timeZoneAbbr,
                        LocalTimeTotalOffset = totalOffset,
                        RecordType = options.RecordType,
                        RecordId = options.RecordId,
                        RecordName = options.RecordName,
                        FromPhone = outboundCallOptions.From,
                        ToPhone = outboundCallOptions.To,
                        Created = now,
                        Updated = now,
                        IsFinal = false
                    };

                    _phoneDal.Create(callLog);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Child CreateCallLog.DatabaseError, options: {options}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.DatabaseError,
                        $"Database failed to add child call log, options: {options}");
                }

                // Make outgoing call
                var result = _providerLogic.MakeMultipleOutboundCallParent(options.TelephonyAccountId, outboundCallOptions);

                //update call log
                try
                {
                    CallStatusType status;
                    if (Enum.TryParse(result.Value.Status.ToString(), true, out status) == false)
                    {
                        Logger.Error(
                            $"Failed to parse TwilioCall.Status to CallStatusType for value: {result.Value.Status}");
                    }

                    var callLog = new CallLog
                    {
                        TelephonyCallId = result.Value.TelephonyCallId,
                        Status = status,
                        DurationTotalSeconds = result.Value.DurationTotalSeconds,
                        DurationRoundedMinutes = result.Value.DurationRoundedMinutes,
                        FromPhone = result.Value.FromPhone,
                        Updated = DateTime.Now,
                    };

                    if (result.Value.DateUtc == DateTime.MinValue)
                    {
                        callLog.DateUtc = nowUtc;
                        callLog.DateLocal = now;
                    }
                    else
                    {
                        callLog.DateUtc = result.Value.DateUtc.Date;
                        callLog.DateLocal = result.Value.DateUtc.AddSeconds(totalOffset).Date;
                    }

                    _phoneDal.Update(callLog,
                                     new List<string>
                                         {
                                             "TelephonyCallId",
                                             "Status",
                                             "DurationTotalSeconds",
                                             "DurationRoundedMinutes",
                                             "DateUtc",
                                             "DateLocal",
                                             "FromPhone",
                                             "Updated"
                                         });
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "UpdateCallLog.DatabaseError");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.DatabaseError,
                        $"Database failed to add call log, options: {options}");
                }

                if (result.HasError)
                {
                    Logger.Trace($"MakeMultipleOutboundCall.ProviderError, providerMessage: {result.ErrorMessage}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.ProviderError,
                        $"Provider failed to make single outgoing call, options: {options}");
                }

                try
                {
                    var outboundCall = new OutboundCall
                    {
                        Id = options.TelephonyCallId,
                        AccountId = options.TelephonyAccountId,
                        Created = DateTime.Now,
                    };

                    _phoneDal.CreateOutgoingCall(outboundCall);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"AddOutgoingCallerId.DatabaseError, options: {options}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.DatabaseError,
                        $"Database failed to add outgoing, options: {options}");
                }

                return new TelephonyResult<OutboundCallResult>(new OutboundCallResult
                                                                   {
                                                                       TelephonyCallId = result.Value.TelephonyCallId,
                                                                       TelephonyAccountId = result.Value.TelephonyAccountId,
                                                                       CallStatus = result.Value.Status.ToString(),
                                                                       StartTime = result.Value.StartTimeUtc,
                                                                       Duration = result.Value.DurationTotalSeconds,
                                                                   });
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Exception in MakeSingleOutboundCall({options})");
                return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.Error, ex.Message);
            }
        }

        public TelephonyResult<OutboundCallResult> MakeMultipleOutboundCallChild(OutboundCallOptions options)
        {
            try
            {
                #region Validate
                if (options == null)
                {
                    Logger.Trace("MakeMultipleOutboundCallChild.InvalidParameter, options is NULL");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.InvalidParameter, "Missing options parameter");
                }

                Logger.Debug($"MakeMultipleOutboundCallChild({options})");

                //validate
                if (options.TelephonyAccountId == Guid.Empty)
                {
                    Logger.Trace(
                        $"MakeMultipleOutboundCallChild.InvalidParameter, accountId: {options.TelephonyAccountId}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.InvalidParameter, "Missing accountId parameter");
                }

                if (options.TelephonyCallId == Guid.Empty)
                {
                    Logger.Trace(
                        $"MakeMultipleOutboundCallChild.InvalidParameter, TelephonyCallId: {options.TelephonyCallId}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.InvalidParameter, "Missing TelephonyCallId parameter");
                }

                var availableResults = _creditLogic.ReadAvailable(options.TelephonyAccountId);

                if (availableResults.HasError)
                {
                    Logger.Warn(
                        $"MakeMultipleOutboundCallChild.Error, Unable to get available minutes, TelephonyCallId: {options.TelephonyCallId}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.Error, "Unable to get available minutes.");
                }

                if (availableResults.Value < 1)
                {
                    Logger.Warn(
                        $"MakeMultipleOutboundCallChild.Error, No telephony minutes left, Minutes: {availableResults.Value} TelephonyCallId: {options.TelephonyCallId}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.Error, "No minutes available.");
                }
                #endregion

                //get local time zone info
                var now = DateTime.Now;
                var nowUtc = now.ToUniversalTime();

                var geoTimeZone = _timeZoneLogic.ZipToTimeZone(options.TimeZoneZip) ?? _timeZoneLogic.ZipToTimeZone("68046");

                var totalOffset = geoTimeZone.OffsetSecondsUtcRaw;
                var timeZoneAbbr = geoTimeZone.TimeZoneAbbrStandard;

                if (now.IsDaylightSavingTime())
                {
                    totalOffset += geoTimeZone.OffsetSecondsUtcDaylight;
                    timeZoneAbbr = geoTimeZone.TimeZoneAbbrDaylight;
                }

                var outboundCallOptions = ConvertOutboundCallOptions(options, CallSessionType.MultipleOutbound);

                //create call log for child
                try
                {
                    var callLog = new CallLog
                    {
                        TelephonyCallId = outboundCallOptions.TelephonyCallIdChild,
                        ParentTelephonyCallId = outboundCallOptions.TelephonyCallId,
                        TelephonyAccountId = outboundCallOptions.TelephonyAccountId,
                        Username = options.Username,
                        SalesRepName = options.SalesRepName,
                        DivisionNumber = options.DivisionNumber,
                        IsEnterprise = options.IsEnterprise,
                        ProductType = options.ProductType,
                        SessionType = CallSessionType.MultipleOutbound,
                        CampaignId = options.CampaignId,
                        CampaignName = options.CampaignName,
                        Status = CallStatusType.BeforeQueued,
                        DurationTotalSeconds = 0,
                        DurationRoundedMinutes = 0,
                        DateUtc = nowUtc.Date,
                        DateLocal = nowUtc.AddSeconds(totalOffset).Date,
                        LocalTimeZoneAbbr = timeZoneAbbr,
                        LocalTimeTotalOffset = totalOffset,
                        RecordType = options.RecordType,
                        RecordId = options.RecordId,
                        RecordName = options.RecordName,
                        FromPhone = outboundCallOptions.From,
                        ToPhone = outboundCallOptions.To,
                        Created = now,
                        Updated = now,
                        IsFinal = false
                    };

                    _phoneDal.Create(callLog);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Child CreateCallLog.DatabaseError, options: {options}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.DatabaseError,
                        $"Database failed to add child call log, options: {options}");
                }

                // Make outgoing call
                var result = _providerLogic.MakeMultipleOutboundCallChild(options.TelephonyAccountId, outboundCallOptions);

                if (result.HasError)
                {
                    Logger.Trace($"MakeMultipleOutboundCallChild.ProviderError, providerMessage: {result.ErrorMessage}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.ProviderError,
                        $"Provider failed to make multiple outgoing child call, options: {options}");
                }

                try
                {
                    var outboundCall = new OutboundCall
                    {
                    //    Id = options.TelephonyCallId,
						Id = result.Value.Id,
                        AccountId = options.TelephonyAccountId,
                        Created = DateTime.Now,
                    };

                    _phoneDal.CreateOutgoingCall(outboundCall);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"MakeMultipleOutboundCallChild.DatabaseError, options: {options}");
                    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.DatabaseError,
                        $"Database failed to add outgoing, options: {options}");
                }

                return new TelephonyResult<OutboundCallResult>(new OutboundCallResult
                {
                    TelephonyCallId = result.Value.Id,
                    TelephonyAccountId = result.Value.AccountId,
                    CallStatus = result.Value.CallStatus,
                    StartTime = result.Value.StartTime,
                    Duration = result.Value.Duration,
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Exception in MakeMultipleOutboundCallChild({options})");
                return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.Error, ex.Message);
            }
        }

		/// <summary>
		/// Gets the outbound call status.
		/// </summary>
		/// <param name="telephonyAccountId">The telephony account id.</param>
		/// <param name="telephonyCallId">The telephony call id.</param>
		/// <returns></returns>
		public TelephonyResult<OutboundCallResult> GetOutboundCallStatus(Guid telephonyAccountId, Guid telephonyCallId)
		{
			Logger.Debug($"GetOutboundCallStatus({telephonyAccountId}, {telephonyCallId})");

			try
			{
				//validate
				if (telephonyAccountId == Guid.Empty)
				{
					Logger.Trace($"GetOutboundCallStatus.InvalidParameter, accountId: {telephonyAccountId}");
					return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.InvalidParameter, "Missing accountId parameter");
				}

				if (telephonyCallId == Guid.Empty)
				{
					Logger.Trace($"GetOutboundCallStatus.InvalidParameter, telephonyCallId: {telephonyCallId}");
					return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.InvalidParameter, "Missing telephonyCallId parameter");
				}

				// Get call status
				var result = _providerLogic.GetOutboundCallStatus(telephonyAccountId, telephonyCallId);

				if (result.HasError)
				{
					Logger.Trace($"GetOutboundCallStatus.ProviderError, providerMessage: {result.ErrorMessage}");
					return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.ProviderError,
					    $"Provider failed to get outgoing call status, account: {telephonyAccountId}, callId: {telephonyCallId}");
				}

			    return new TelephonyResult<OutboundCallResult>(new OutboundCallResult
			                                                       {
			                                                           TelephonyCallId = result.Value.Id,
			                                                           TelephonyAccountId = result.Value.AccountId,
			                                                           CallStatus = result.Value.CallStatus,
																	   StartTime = result.Value.StartTime,
																	   Duration = result.Value.Duration,
																   });
			}
			catch (Exception ex)
			{
				Logger.Error(ex, $"Exception in GetOutboundCallStatus({telephonyAccountId}, {telephonyCallId})");
				return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.Error, ex.Message);
			}
		}

		/// <summary>
		/// Gets the outbound call child status.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public TelephonyResult<OutboundCallResult> GetOutboundCallChildStatus(OutboundCallStatusOptions options)
		{
			try
			{
				#region Validation

				//validate
				if (options == null)
				{
					Logger.Trace("GetOutboundCallChildStatus.InvalidParameter, options is NULL");
					return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.InvalidParameter, "Missing options parameter");
				}

				Logger.Debug(
				    $"GetOutboundCallChildStatus({options.TelephonyAccountId}, {options.TelephonyCallId}, {options.ToNumber})");

				if (options.TelephonyAccountId == Guid.Empty)
				{
					Logger.Trace($"GetOutboundCallChildStatus.InvalidParameter, accountId: {options.TelephonyAccountId}");
					return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.InvalidParameter, "Missing accountId parameter");
				}

				if (options.TelephonyCallId == Guid.Empty)
				{
					Logger.Trace($"GetOutboundCallChildStatus.InvalidParameter, telephonyCallId: {options.TelephonyCallId}");
					return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.InvalidParameter, "Missing telephonyCallId parameter");
				}

				if (string.IsNullOrWhiteSpace(options.ToNumber))
				{
					Logger.Trace("GetOutboundCallChildStatus.InvalidParameter, toPhoneNumber is missing");
					return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.InvalidParameter, "Missing toPhoneNumber parameter");
				}

				if (string.IsNullOrWhiteSpace(options.FromNumber))
				{
					Logger.Trace("GetOutboundCallChildStatus.InvalidParameter, fromPhoneNumber is missing");
					return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.InvalidParameter, "Missing fromPhoneNumber parameter");
				}

				#endregion

				// Get call status
				var result = _providerLogic.GetOutboundChildCallStatus(options.TelephonyAccountId, options.TelephonyCallId, options.ToNumber, options.FromNumber, options.CallStartTime);

				if (result.HasError)
				{
					Logger.Trace($"GetOutboundCallChildStatus.ProviderError, providerMessage: {result.ErrorMessage}");
					return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.ProviderError,
					    $"Provider failed to get outgoing child call status, account: {options.TelephonyAccountId}, callId: {options.TelephonyCallId}, toPhone: {options.ToNumber}");
				}

			    return new TelephonyResult<OutboundCallResult>(new OutboundCallResult
			                                                       {
			                                                           TelephonyCallId = result.Value.Id,
			                                                           TelephonyAccountId = result.Value.AccountId,
			                                                           CallStatus = result.Value.CallStatus,
																	   StartTime = result.Value.StartTime,
																	   Duration = result.Value.Duration,
			                                                       });
			}
			catch (Exception ex)
			{
			    if (options != null)
			        Logger.Error(ex,
			            $"Exception in GetOutboundCallChildStatus({options.TelephonyAccountId}, {options.TelephonyCallId}, {options.ToNumber})");
			    return new TelephonyResult<OutboundCallResult>(TelephonyResultCode.Error, ex.Message);
			}
		}

        public TelephonyResult<PageOfT<CallLog>> GetOutboundCallLogPaged(Guid telephonyAccountId, bool childOnly, int page = 1, int itemsPerPage = 25, bool? isFinal = null)
		{
			try
			{
				#region Validation

				//validate
				Logger.Debug($"GetOutboundCallLogPaged({telephonyAccountId})");

				if (telephonyAccountId == Guid.Empty)
				{
					Logger.Trace($"GetOutboundCallLogPaged.InvalidParameter, accountId: {telephonyAccountId}");
					return new TelephonyResult<PageOfT<CallLog>>(TelephonyResultCode.InvalidParameter, "Missing accountId parameter");
				}

				#endregion

			    var pagedData = _phoneDal.ReadCallLogPaged(telephonyAccountId, childOnly, page, itemsPerPage, isFinal);

				return new TelephonyResult<PageOfT<CallLog>>(pagedData);

			}
			catch (Exception ex)
			{
				Logger.Error(ex, $"Exception in GetOutboundCallLogPaged({telephonyAccountId})");
				return new TelephonyResult<PageOfT<CallLog>>(TelephonyResultCode.Error, ex.Message);
			}
		}

        public CallLog GetCallLog(Guid telephonyCallId)
	    {
            try
            {
                //get call log from db
                return _phoneDal.ReadCallLog(telephonyCallId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "UpdateCallLog.GetCallLog");
            }
            return null;
	    }

		/// <summary>
		/// Converts the outbound call options to the provider's representation.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <param name="callSessionType">Type of the call session.</param>
		/// <returns></returns>
		private Provider.Models.OutboundCallOption ConvertOutboundCallOptions(OutboundCallOptions options, CallSessionType callSessionType)
		{
			if (options == null)
				return new Provider.Models.OutboundCallOption();

			Provider.Models.CallSessionType providerCallSessionType;
			Enum.TryParse(callSessionType.ToString(), out providerCallSessionType);

			return new Provider.Models.OutboundCallOption()
			{
				TelephonyAccountId = options.TelephonyAccountId,
				TelephonyCallId = options.TelephonyCallId,
                TelephonyCallIdChild = Guid.NewGuid(),
				From = options.FromNumber,
				To = options.ToNumber,
				Timeout = options.NoAnswerTimeout,
				RecordId = options.RecordId,
				Say = options.Say != null ? new List<string>(options.Say) : null,
				CallSessionType = providerCallSessionType,
				HangupIfMachine = options.HangupIfMachine,			
			};
		}

        public TelephonyResult EndSingleOutboundCall(Guid telephonyAccountId, Guid telephonyCallId)
        {
            try
            {
                Logger.Debug($"EndSingleOutboundCall({telephonyAccountId}, {telephonyCallId})");

                //validate
                if (telephonyAccountId == Guid.Empty)
                {
                    Logger.Trace($"TelephonyResultCode.InvalidParameter, telephonyAccountId: {telephonyAccountId}");
                    return new TelephonyResult(TelephonyResultCode.InvalidParameter, "telephonyAccountId");
                }

                if (telephonyCallId == Guid.Empty)
                {
                    Logger.Trace($"TelephonyResultCode.InvalidParameter, telephonyCallId: {telephonyCallId}");
                    return new TelephonyResult(TelephonyResultCode.InvalidParameter, "telephonyCallId");
                }

                //end outgoing call
                var result = _providerLogic.EndSingleOutboundCall(telephonyAccountId, telephonyCallId);

                if (result.HasError)
                {
                    Logger.Trace($"TelephonyResultCode.ProviderError, providerMessage: {result.ErrorMessage}");
                    return new TelephonyResult(TelephonyResultCode.ProviderError,
                        $"Provider failed to end single outgoing call, telephonyAccountId: {telephonyAccountId}, telephonyCallId: {telephonyCallId}");
                }

                return new TelephonyResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Exception in EndSingleOutboundCall({telephonyAccountId}, {telephonyCallId})");
                return new TelephonyResult(TelephonyResultCode.Error, ex.Message);
            }			            
        }

        public TelephonyResult EndParentMultipleOutboundCall(Guid telephonyAccountId, Guid telephonyCallId)
        {
            try
            {
                Logger.Debug($"EndParentMultipleOutboundCall({telephonyAccountId}, {telephonyCallId})");

                //validate
                if (telephonyAccountId == Guid.Empty)
                {
                    Logger.Trace($"TelephonyResultCode.InvalidParameter, telephonyAccountId: {telephonyAccountId}");
                    return new TelephonyResult(TelephonyResultCode.InvalidParameter, "telephonyAccountId");
                }

                if (telephonyCallId == Guid.Empty)
                {
                    Logger.Trace($"TelephonyResultCode.InvalidParameter, telephonyCallId: {telephonyCallId}");
                    return new TelephonyResult(TelephonyResultCode.InvalidParameter, "telephonyCallId");
                }

                //end outgoing call
                var result = _providerLogic.EndParentMultipleOutboundCall(telephonyAccountId, telephonyCallId);

                if (result.HasError)
                {
                    Logger.Trace($"TelephonyResultCode.ProviderError, providerMessage: {result.ErrorMessage}");
                    return new TelephonyResult(TelephonyResultCode.ProviderError,
                        $"Provider failed to end single outgoing call, telephonyAccountId: {telephonyAccountId}, telephonyCallId: {telephonyCallId}");
                }

                return new TelephonyResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Exception in EndParentMultipleOutboundCall({telephonyAccountId}, {telephonyCallId})");
                return new TelephonyResult(TelephonyResultCode.Error, ex.Message);
            }
        }

        public TelephonyResult EndChildMultipleOutboundCall(Guid telephonyAccountId, Guid telephonyCallId)
        {
            try
            {
                Logger.Debug($"EndChildMultipleOutboundCall({telephonyAccountId}, {telephonyCallId})");

                //validate
                if (telephonyAccountId == Guid.Empty)
                {
                    Logger.Trace($"TelephonyResultCode.InvalidParameter, telephonyAccountId: {telephonyAccountId}");
                    return new TelephonyResult(TelephonyResultCode.InvalidParameter, "telephonyAccountId");
                }

                if (telephonyCallId == Guid.Empty)
                {
                    Logger.Trace($"TelephonyResultCode.InvalidParameter, telephonyCallId: {telephonyCallId}");
                    return new TelephonyResult(TelephonyResultCode.InvalidParameter, "telephonyCallId");
                }

                //end outgoing call
                var result = _providerLogic.EndChildMultipleOutboundCall(telephonyAccountId, telephonyCallId);

                if (result.HasError)
                {
                    Logger.Trace($"TelephonyResultCode.ProviderError, providerMessage: {result.ErrorMessage}");
                    return new TelephonyResult(TelephonyResultCode.ProviderError,
                        $"Provider failed to end child on multiple outgoing call, telephonyAccountId: {telephonyAccountId}, telephonyCallId: {telephonyCallId}");
                }

                return new TelephonyResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Exception in EndChildMultipleOutboundCall({telephonyAccountId}, {telephonyCallId})");
                return new TelephonyResult(TelephonyResultCode.Error, ex.Message);
            }
        }

        public TelephonyResult ExtendPauseOutboundCall(PauseCallOptions options)
        {
            try
            {
                //validate
				if (options == null)
				{
					Logger.Trace("ExtendPauseOutboundCall.InvalidParameter, options");
					return new TelephonyResult(TelephonyResultCode.InvalidParameter, "options");
				}

				Logger.Debug(
				    $"ExtendPauseOutboundCall({options.TelephonyAccountId}, {options.TelephonyCallId}, {options.SecondsToPause})");

                if (options.TelephonyAccountId == Guid.Empty)
                {
					Logger.Trace($"ExtendPauseOutboundCall.InvalidParameter, telephonyAccountId: {options.TelephonyAccountId}");
                    return new TelephonyResult(TelephonyResultCode.InvalidParameter, "telephonyAccountId");
                }

                if (options.TelephonyCallId == Guid.Empty)
                {
					Logger.Trace($"ExtendPauseOutboundCall.InvalidParameter, telephonyCallId: {options.TelephonyCallId}");
                    return new TelephonyResult(TelephonyResultCode.InvalidParameter, "telephonyCallId");
                }

                //pause outgoing call
	            var providerOptions = new Provider.Models.PauseCallOptions
		        {
					TelephonyAccountId = options.TelephonyAccountId,
					TelephonyCallId = options.TelephonyCallId,
					SecondsToPause = options.SecondsToPause,
					SayBeforePause = options.SayBeforePause,
					SayAfterPause = options.SayAfterPause,
					Hangup = options.Hangup,
		        };

                var result = _providerLogic.ExtendPauseOutboundCall(providerOptions);

                if (result.HasError)
                {
                    Logger.Trace($"TelephonyResultCode.ProviderError, providerMessage: {result.ErrorMessage}");
                    return new TelephonyResult(TelephonyResultCode.ProviderError,
                        $"Provider failed to extend pause outgoing call, telephonyAccountId: {options.TelephonyAccountId}, telephonyCallId: {options.TelephonyCallId}");
                }

                return new TelephonyResult();
            }
            catch (Exception ex)
            {
                if (options != null)
                    Logger.Error(ex,
                        $"Exception in ExtendPauseOutboundCall({options.TelephonyAccountId}, {options.TelephonyCallId})");
                return new TelephonyResult(TelephonyResultCode.Error, ex.Message);
            }
        }

        public TelephonyResult DisconnectParentCall(Guid telephonyAccountId, Guid telephonyCallId)
        {
            try
            {
                Logger.Debug($"DisconnectParentCall({telephonyAccountId}, {telephonyCallId})");

                //validate
                if (telephonyAccountId == Guid.Empty)
                {
                    Logger.Trace($"TelephonyResultCode.InvalidParameter, telephonyAccountId: {telephonyAccountId}");
                    return new TelephonyResult(TelephonyResultCode.InvalidParameter, "telephonyAccountId");
                }

                if (telephonyCallId == Guid.Empty)
                {
                    Logger.Trace($"TelephonyResultCode.InvalidParameter, telephonyCallId: {telephonyCallId}");
                    return new TelephonyResult(TelephonyResultCode.InvalidParameter, "telephonyCallId");
                }

                //end outgoing call
                var result = _providerLogic.DisconnectParentCall(telephonyAccountId, telephonyCallId);

                if (result.HasError)
                {
                    Logger.Trace($"TelephonyResultCode.ProviderError, providerMessage: {result.ErrorMessage}");
                    return new TelephonyResult(TelephonyResultCode.ProviderError,
                        $"Provider failed to disconnect parent call, telephonyAccountId: {telephonyAccountId}, telephonyCallId: {telephonyCallId}");
                }

                return new TelephonyResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Exception in DisconnectParentCall({telephonyAccountId}, {telephonyCallId})");
                return new TelephonyResult(TelephonyResultCode.Error, ex.Message);
            }
        }

		public TelephonyResult<string> GetLastOutboundCallStatusForAccount(Guid telephonyAccountId)
		{
			try
			{
				Logger.Debug($"GetLastOutboundCallStatusForAccount({telephonyAccountId})");

				//validate
				if (telephonyAccountId == Guid.Empty)
				{
					Logger.Trace($"GetLastOutboundCallStatusForAccount.InvalidParameter, telephonyAccountId: {telephonyAccountId}");
					return new TelephonyResult<string>(TelephonyResultCode.InvalidParameter, "telephonyAccountId");
				}

				// get the last call made...if there is one
				var callLog = _phoneDal.ReadLastOutgoingParentCallForAccount(telephonyAccountId);
				
				// No calls present
				if(callLog == null)
					return new TelephonyResult<string>(string.Empty);

				//read outgoing call status
				var result = _providerLogic.GetOutboundCallStatus(telephonyAccountId, callLog.TelephonyCallId);

				if (result.HasError)
				{
					Logger.Trace($"GetLastOutboundCallStatusForAccount.ProviderError, providerMessage: {result.ErrorMessage}");
					return new TelephonyResult<string>(TelephonyResultCode.ProviderError,
					    $"Provider error getting last call, telephonyAccountId: {telephonyAccountId}, telephonyCallId: {callLog.TelephonyCallId}");
				}

				var call = result.Value;
				if (call == null)
					return new TelephonyResult<string>(callLog.Status.ToString());
				else
					return new TelephonyResult<string>(call.CallStatus);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, $"Exception in GetLastOutboundCallStatusForAccount({telephonyAccountId})");
				return new TelephonyResult<string>(TelephonyResultCode.Error, ex.Message);
			}
		}

		#endregion

	}
}
