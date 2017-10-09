using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace O2.Telephony.Api.Configuration
{
	/// <summary>
	/// Telephony Api configuration.
	/// </summary>
	[DataContract(Name = "TelephonyApiConfig", Namespace = "")]
	public sealed class TelephonyApiConfig
	{
		#region Constants
		#endregion Constants

		#region Fields
		#endregion Fields

		#region Properties
		/// <summary>
		/// Collection of endpoints in the config file.
		/// </summary>
		[DataMember(EmitDefaultValue = false)]
		public List<TelephonyServiceEndpoint> TelephonyServiceEndpoints
		{
			get { return _endpoints; }
			set
			{
				if (ReferenceEquals(value, null))
					throw new ArgumentNullException("value");

				_endpoints = value;
			}
		}

		private List<TelephonyServiceEndpoint> _endpoints = new List<TelephonyServiceEndpoint>();
		#endregion

		#region Methods
		#endregion Methods
	}

}
