namespace Pumpkin.Domain.Framework.Serialization;

public interface ISerializer
{
    string Serialize(object message);

    T? Deserialize<T>(string stream);
}