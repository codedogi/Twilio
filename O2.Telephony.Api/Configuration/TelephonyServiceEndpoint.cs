using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace O2.Telephony.Api.Configuration
{
	/// <summary>
	/// A class which contains a definition of a single Telephony Service endpoint
	/// </summary>
	[DataContract(Name = "Endpoint", Namespace = "")]
	public sealed class TelephonyServiceEndpoint
	{
		#region Constants
		#endregion Constants

		#region Fields
		#endregion Fields

		#region Constructors
		/// <summary>
		///   Constructor
		/// </summary>
		public TelephonyServiceEndpoint()
		{
		}

		/// <summary>
		///   Constructor
		/// </summary>
		/// <param name="other"> Object being copied. </param>
		public TelephonyServiceEndpoint(TelephonyServiceEndpoint other)
		{
			if (ReferenceEquals(other, null))
				throw new ArgumentNullException("other");

			Location = other.Location;
			Uri = other.Uri;
			Timeout = other.Timeout;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Location of server
		/// </summary>
		[DataMember]
		public string Location { get; set; }

		/// <summary>
		/// Timeout in seconds.
		/// </summary>
		[DataMember]
		public int Timeout { get; set; }

		/// <summary>
		/// URI to server
		/// </summary>
		[DataMember]
		public string Uri { get; set; }
		#endregion

		#region Methods
		#endregion
	}

}
