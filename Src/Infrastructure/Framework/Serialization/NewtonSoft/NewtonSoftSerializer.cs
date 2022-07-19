using System.Text.Json;
using Newtonsoft.Json;

namespace Framework.Serialization.NewtonSoft;

public class NewtonSoftSerializer : ISerializer
{
    public string Serialize(object obj)
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

    public T? Deserialize<T>(string stream)
    {
            return JsonConvert.DeserializeObject<T>(stream);
    }
}