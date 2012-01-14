using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Canucks.NewsReader.Common.Helpers
{
    public class JsonHelpers
    {
        public static string SerializeJson<T>(T input)
        {
            var serializer = new DataContractJsonSerializer(input.GetType());
            var ms = new MemoryStream();
            serializer.WriteObject(ms, input);
            byte[] bytes = ms.ToArray();
            string retVal = Encoding.Unicode.GetString(bytes, 0, bytes.Length);
            ms.Dispose();
            return retVal;
        }

        public static T DeserializeJson<T>(string input)
        {
            var obj = Activator.CreateInstance<T>();
            var ms = new MemoryStream(Encoding.Unicode.GetBytes(input));
            var serializer = new DataContractJsonSerializer(obj.GetType());
            try
            {
                obj = (T) serializer.ReadObject(ms);
                ms.Close();
                ms.Dispose();
                return obj;
            }
            catch (Exception)
            {
                obj = (T) serializer.ReadObject(ms);
                ms.Close();
                ms.Dispose();
                return obj;
            }
        }
    }
}