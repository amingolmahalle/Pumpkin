using System.Text.Json;
using Newtonsoft.Json;

namespace Pumpkin.Domain.Framework.Extensions;

public static partial class Extensions
{
    public static string Serialize(this object obj)
    {
        try
        {
            return System.Text.Json.JsonSerializer.Serialize(obj, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }
        catch
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        }
    }

   public static T Deserialize<T>(this string value)
       {
           return JsonConvert.DeserializeObject<T>(value);
       }
}