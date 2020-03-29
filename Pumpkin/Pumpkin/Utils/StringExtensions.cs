using System;
using Newtonsoft.Json;

namespace Pumpkin.Core.Utils
{
    public static class StringExtensions
    {
        public static T ToEnum<T>(this string value)
        {
            Enum.TryParse(typeof(T), value, true, out var res);

            return (T) res;
        }

        public static bool IsValidJson(this string text)
        {
            text = text.Trim();
            if ((text.StartsWith("{") && text.EndsWith("}")) || // For object
                (text.StartsWith("[") && text.EndsWith("]"))) // For array
            {
                try
                {
                   // var obj = JToken.Parse(text);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }
    }
}