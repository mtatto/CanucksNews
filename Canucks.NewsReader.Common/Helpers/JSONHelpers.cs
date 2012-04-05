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
           return ServiceStack.Text.JsonSerializer.SerializeToString(input);
        }

        public static T DeserializeJson<T>(string input)
        {
            try
            {
                 return ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(input);
                
            }
            catch (Exception)
            {
                return ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(input);
            }
        }
    }
}