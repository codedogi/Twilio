using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Hosting;

namespace O2.Telephony.Api.Configuration
{
	internal class TelephonyApiManager
	{
		#region Properties
		/// <summary>
		///   Server URI appropriate for the current location
		/// </summary>
		public static TelephonyServiceEndpoint ServiceEndpoint
		{
			get
			{
				if (_endpoint == null)
					_endpoint = Config.TelephonyServiceEndpoints.FirstOrDefault(e => e.Location == Location);

				if (_endpoint == null)
					throw new InvalidOperationException("No endpoints configured for location " + Location);

				return _endpoint;
			}

			set { _endpoint = value; }
		}

		private static TelephonyServiceEndpoint _endpoint;

		/// <summary>
		///  Static instance of the config file.
		/// </summary>
		internal static TelephonyApiConfig Configuration
		{
			get { return Config; }
		}

		private static readonly TelephonyApiConfig Config;

		/// <summary>
		/// Gets the location.
		/// </summary>
		internal static string Location
		{
			get { return LocationElement; }
		}

		private static readonly string LocationElement;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes the <see cref="TelephonyApiManager"/> class.
		/// </summary>
		static TelephonyApiManager()
		{
			string baseDir = string.Empty;

			System.Configuration.Configuration config = null;

			if (AppDomain.CurrentDomain.GetData(".appDomain") != null)
				config = WebConfigurationManager.OpenWebConfiguration("~");
			else
			{
				// For unit tests...
				try
				{
#if DEBUG
					string applicationName = Environment.GetCommandLineArgs()[0];
#else 
					string applicationName = Environment.GetCommandLineArgs()[0]+ ".exe";
#endif
					string exePath = System.IO.Path.Combine(Environment.CurrentDirectory, applicationName);
					config = ConfigurationManager.OpenExeConfiguration(exePath);
				}
				catch (Exception)
				{
					config = ConfigurationManager.OpenMachineConfiguration();
				}
			}

			KeyValueConfigurationElement element = config.AppSettings.Settings["Location"];
			if (element != null)
				LocationElement = element.Value;

			if (string.IsNullOrWhiteSpace(LocationElement))
				throw new Exception("You must setup the location of this server in the Framework web.config appSettings - Location=Local or Location=Test");

			Config = ConfigurationManager.GetSection("TelephonyApiConfig") as TelephonyApiConfig;
		}
		#endregion

	}
}
