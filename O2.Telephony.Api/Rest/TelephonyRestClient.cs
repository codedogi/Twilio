using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using O2.Telephony.Api.Configuration;
using RestSharp;

namespace O2.Telephony.Api.Rest
{
    internal sealed class TelephonyRestClient : RestClient
	{
		#region Properties
		#endregion

		#region Constructors
		internal TelephonyRestClient()
		{
			var serviceEndpoint = TelephonyApiManager.ServiceEndpoint;

			BaseUrl = new Uri(serviceEndpoint.Uri);
			Timeout = serviceEndpoint.Timeout;
		}
		#endregion

		#region Methods

		internal new IRestResponse Execute(IRestRequest request)
		{
			try
			{
				// TODO:  Log all requests

				var response = base.Execute(request);

				return response;
			}
			catch (Exception)
			{
				// TODO:  log error				
				throw;
			}
		}

		#endregion

		///// <summary>
        ///// Execute a manual REST request
        ///// 
        ///// </summary>
        ///// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam><param name="request">The RestRequest to execute (will use client credentials)</param>
        //public virtual T Execute<T>(RestRequest request) where T : new()
        //{
        //    request.OnBeforeDeserialization = (Action<IRestResponse>)(resp =>
        //    {
        //        if (resp.StatusCode < HttpStatusCode.BadRequest)
        //            return;
        //        string local_2 = string.Format("{{ \"RestException\" : {0} }}", (object)MiscExtensions.AsString(resp.RawBytes));
        //        resp.Content = (string)null;
        //        resp.RawBytes = Encoding.UTF8.GetBytes(((object)local_2).ToString());
        //    });
        //    request.DateFormat = "ddd, dd MMM yyyy HH:mm:ss '+0000'";
        //    return this._client.Execute<T>((IRestRequest)request).Data;
        //}

    }
}
