using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace O2.Telephony.Api.Configuration
{
	public class TelephonyApiConfigHandler : IConfigurationSectionHandler
	{
		#region Properties
		#endregion

		#region Members

		/// <summary>
		/// Iterate through all the child nodes
		///	of the XMLNode that was passed in (by the section handler plumbin in .NET) 
		/// and extract the information into a list of DataLayer objects
		///	</summary>
		/// <param name="parent"></param>
		/// <param name="configContext"></param>
		/// <param name="section">The XML section we will iterate against</param>
		/// <returns></returns>
		public object Create(object parent, object configContext, XmlNode section)
		{
			if (section == null)
			{
				throw new ArgumentNullException("section", "The TelephonyApi.config file has no section");
			}

			try
			{
				var config = ConvertNode<TelephonyApiConfig>(section);

				return config;
			}
			catch (Exception)
			{			
				// TODO: Log this error
				throw;
			}
		}

		/// <summary>
		/// Converts the node.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		private static T ConvertNode<T>(XmlNode node) where T : class
		{
			T result;

			using(var stream = new MemoryStream())
			{
				using (var streamWriter = new StreamWriter(stream))
				{
					streamWriter.Write(node.OuterXml);
					streamWriter.Flush();

					stream.Position = 0;

					var serializer = new XmlSerializer(typeof(T));
					result = (serializer.Deserialize(stream) as T);				
				}
			}

			return result;
		}
		#endregion
	}

}
