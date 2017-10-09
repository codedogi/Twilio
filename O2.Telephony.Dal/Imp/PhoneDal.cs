using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using O2.Telephony.Dal.Interfaces;
using O2.Telephony.Dal.Models;
using O2.Telephony.Dal.PetaPoco;
using O2.Telephony.Models;
using O2.Telephony.Models.CallerId;
using O2.Telephony.Models.OutboundCall;

namespace O2.Telephony.Dal.Imp
{
	/// <summary>
	/// Data access layer for phone
	/// </summary>
	public class PhoneDal : BaseDal, IPhoneDal
	{
		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		public PhoneDal()
			: base(LogManager.GetCurrentClassLogger())
		{
			Logger.Trace("Instantiated");
		}
		#endregion

		#region Caller ID

		/// <summary>
		/// Creates the outgoing caller id.
		/// </summary>
		/// <param name="accountId">The account id.</param>
		/// <param name="callerIdId">The caller id id.</param>
		/// <returns></returns>
		/// <exception cref="System.Exception">Error inserting outgoing caller ID.</exception>
		public Guid CreateOutgoingCallerId(Guid accountId, Guid callerIdId)
		{
			Logger.Debug($"Create({accountId})");

			using (var db = new Database(TelephonyConnection))
			{
				try
				{
					var poco = new OutgoingCallerIdPoco
					{
						Id = callerIdId,
						AccountId = accountId,
						Created = DateTime.Now,
						Status = (byte)CallerIdStatus.Unknown,
					};

					var result = db.Insert(poco);

					if (result == null)
						throw new Exception("Error inserting outgoing caller ID.");

					var id = Guid.Parse(result.ToString());
					return id;
				}
				catch (Exception ex)
				{
					Logger.Error(ex, $"Error db.Insert: {Format(db)}");
					throw;
				}
			}		
		}

		/// <summary>
		/// Deletes the outgoing caller id.
		/// </summary>
		/// <param name="callerIdId">The caller id id.</param>
		/// <returns></returns>
		public bool DeleteOutgoingCallerId(Guid callerIdId)
		{
			Logger.Debug($"Delete({callerIdId})");

			using (var db = new Database(TelephonyConnection))
			{
				try
				{
					var sql = new Sql("UPDATE OutgoingCallerId SET Deleted = @0 WHERE Id = @1", DateTime.Now, callerIdId);

					Logger.Trace($"db.Execute: {Format(sql)}");

					var count = db.Execute(sql);

					return count == 1;
				}
				catch (Exception ex)
				{
					Logger.Error(ex, $"Error db.Delete: {Format(db)}");
					throw;
				}
			}
		}

		/// <summary>
		/// Reads the outgoing caller id.
		/// </summary>
		/// <param name="callerIdId">The caller id id.</param>
		/// <returns></returns>
		public CallerId ReadOutgoingCallerId(Guid callerIdId)
		{
			Logger.Debug($"Read({callerIdId})");

			var sql = new Sql("WHERE Id = @0", callerIdId);

			using (var db = new Database(TelephonyConnection))
			{
				try
				{
					Logger.Trace($"db.SingleOrDefault: {Format(sql)}");

					var poco = db.SingleOrDefault<OutgoingCallerIdPoco>(sql);

					return poco?.ToModel();
				}
				catch (Exception ex)
				{
					Logger.Error(ex, $"Error db.SingleOrDefault: {Format(db)}");
					throw;
				}
			}
		}

		#endregion

		#region Outgoing Calls

		/// <summary>
		/// Creates the outgoing call.
		/// </summary>
		/// <param name="outboundCall">The outbound call.</param>
		/// <returns></returns>
		/// <exception cref="System.Exception">Error inserting outgoing call.</exception>
		public Guid CreateOutgoingCall(OutboundCall outboundCall)
		{
			Logger.Debug($"Create({outboundCall.AccountId}, {outboundCall.Id})");

			using (var db = new Database(TelephonyConnection))
			{
				try
				{
					var poco = new OutgoingCallPoco(outboundCall);

					var result = db.Insert(poco);

					if (result == null)
						throw new Exception("Error inserting outgoing call.");

					var id = Guid.Parse(result.ToString());
					return id;
				}
				catch (Exception ex)
				{
					Logger.Error(ex, $"Error db.Insert: {Format(db)}");
					throw;
				}
			}
		}

		/// <summary>
		/// Reads the outgoing call.
		/// </summary>
		/// <param name="callId">The call id.</param>
		/// <returns></returns>
		public OutboundCall ReadOutgoingCall(Guid callId)
		{
			Logger.Debug($"Read({callId})");

			var sql = new Sql("WHERE Id = @0", callId);

			using (var db = new Database(TelephonyConnection))
			{
				try
				{
					Logger.Trace($"db.SingleOrDefault: {Format(sql)}");

					var poco = db.SingleOrDefault<OutgoingCallPoco>(sql);

					return poco?.ToModel();
				}
				catch (Exception ex)
				{
					Logger.Error(ex, $"Error db.SingleOrDefault: {Format(db)}");
					throw;
				}
			}
		}

		#endregion

        #region Call

        /// <summary>
        /// Creates call log.
        /// </summary>
        /// <param name="callLog">Call log</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error inserting outgoing call.</exception>
        public void Create(CallLog callLog)
        {
            Logger.Debug($"Create({callLog})");

            using (var db = new Database(TelephonyConnection))
            {
                try
                {
                    var poco = new CallLogPoco(callLog);

                    db.Insert(poco);

                    Logger.Trace($"db.Insert success: {poco.TelephonyCallId}");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Error db.Insert: {Format(db)}");
                    throw;
                }
            }
        }

        //Update
        public void Update(CallLog callLog, IEnumerable<string> columns)
        {
            Logger.Debug($"Update({callLog}, {columns})");

            using (var db = new Database(TelephonyConnection))
            {
                try
                {
                    db.Update(new CallLogPoco(callLog), columns);
                    Logger.Trace($"db.Update: {Format(db)}");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Error db.Update: {Format(db)}");
                    throw;
                }
            }
        }

        public CallLog ReadCallLog(Guid telephonyCallId)
        {
            Logger.Debug($"Read({telephonyCallId})");

            var sql = new Sql().Append("select * from CallLog where telephonyCallId = @0", telephonyCallId);

            using (var db = new Database(TelephonyConnection))
            {
                try
                {
                    Logger.Trace($"db.SingleOrDefault: {Format(sql)}");
                    return db.SingleOrDefault<CallLogPoco>(sql).ToModel();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Error db.SingleOrDefault: {Format(db)}");
                    throw;
                }
            }
        }

		public PageOfT<CallLog> ReadCallLogPaged(Guid telephonyAccountId, bool childOnly, int page = 1, int itemsPerPage = 25, bool? isFinal = null, DateTime? fromDate = null, DateTime? toDate = null, string status = null)
		{
			Logger.Debug($"ReadCallLogPaged({telephonyAccountId})");

		    var sql = new Sql().Append("SELECT * FROM CallLog WITH(NOLOCK) WHERE TelephonyAccountId = @0", telephonyAccountId);

            if (childOnly)
                sql.Append("and ParentTelephonyCallId is not null");

			if (isFinal.HasValue)
				sql.Append("AND IsFinal = @0", isFinal.Value);

			if (fromDate.HasValue)
				sql.Append("AND StartTimeLocal >= @0", fromDate.Value);

			if (toDate.HasValue)
				sql.Append("AND EndTimeLocal <= @0", toDate.Value);

			if (!string.IsNullOrWhiteSpace(status))
				sql.Append("AND Status = @0", status);

			sql.Append("ORDER BY Created DESC");

			using (var db = new Database(TelephonyConnection))
			{
				try
				{
					Logger.Trace($"db.Page: {Format(sql)}");
					var pageData = db.Page<CallLogPoco>(page, itemsPerPage, sql);
					if (pageData != null)
					{
						return new PageOfT<CallLog>
						{
							CurrentPage = pageData.CurrentPage,
							ItemsPerPage = pageData.ItemsPerPage,
							TotalPages = pageData.TotalPages,
							TotalItems = pageData.TotalItems,
							Items = new List<CallLog>(pageData.Items.Select(p => p.ToModel()))
						};
					}

					return null;
				}
				catch (Exception ex)
				{
					Logger.Error(ex, $"Error db.SingleOrDefault: {Format(db)}");
					throw;
				}
			}
		}

		public CallLog ReadLastOutgoingParentCallForAccount(Guid telephonyAccountId)
		{
			Logger.Debug($"ReadLastOutgoingCallForAccount({telephonyAccountId})");

			var sql = new Sql().Append("SELECT TOP 1 * FROM CallLog WITH(NOLOCK) WHERE TelephonyAccountId = @0 AND ParentTelephonyCallId IS NULL ORDER BY Created DESC", telephonyAccountId);

			using (var db = new Database(TelephonyConnection))
			{
				try
				{
					Logger.Trace($"db.FirstOrDefault: {Format(sql)}");
					var dto = db.FirstOrDefault<CallLogPoco>(sql);
				    return dto?.ToModel();
				}
				catch (Exception ex)
				{
					Logger.Error(ex, $"Error db.FirstOrDefault: {Format(db)}");
					throw;
				}
			}
		}

        #endregion
    }
}
