using System.Text.Json;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using Pumpkin.Contract.Serialization;

namespace Pumpkin.Core.Serialization;

public class NewtonSoftSerializer : ISerializer
{
    private static readonly JsonSerializerSettings Setting;

    static NewtonSoftSerializer()
    {
        Setting = JsonConvert.DefaultSettings == null
            ? new JsonSerializerSettings()
            : JsonConvert.DefaultSettings();
            
        Setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            
        foreach (var converter in GeoJsonSerializer.Create(new GeometryFactory(new PrecisionModel(), 4326))
                     .Converters)
        {
            Setting.Converters.Add(converter);
        }
    }

    public T Deserialize<T>(string stream)
    {
        return (T) Deserialize(stream, typeof(T));
    }

    public object Deserialize(string value, Type type)
    {
        return JsonConvert.DeserializeObject(value, type, Setting);
    }

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
}