using System.Text.Json;
using Newtonsoft.Json;
using Pumpkin.Domain.Framework.Serialization;

namespace Pumpkin.Infrastructure.Framework.Serialization;

public class JsonSerializer : ISerializer
{
    public string Serialize(object obj)
    {
        try
        {
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            return System.Text.Json.JsonSerializer.Serialize(
                obj,
                serializeOptions);
        }
        catch
        {
            return JsonConvert.SerializeObject(
                obj,
                Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }
    }

    public T Deserialize<T>(string stream)
    {
        return JsonConvert.DeserializeObject<T>(stream);
    }
}