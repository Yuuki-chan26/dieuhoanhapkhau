using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace idocNet.Core.Data.Serialization
{
	public class JsonSerializer
	{
		public static T Deserialise<T>(string json)
		{

			T obj = Activator.CreateInstance<T>();
			using (MemoryStream ms = new MemoryStream(System.Text.Encoding.Unicode.GetBytes(json)))
			{
				DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
				obj = (T)serializer.ReadObject(ms); // <== Your missing line
				return obj;
			}
		}


		public static string Serialize<T>(T obj)
		{

			DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
			using (MemoryStream ms = new MemoryStream())
			{
				serializer.WriteObject(ms, obj);
				return System.Text.Encoding.UTF8.GetString(ms.ToArray());

			}
		}
	}
}
