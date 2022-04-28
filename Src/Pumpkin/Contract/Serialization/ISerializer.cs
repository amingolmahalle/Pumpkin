namespace Pumpkin.Contract.Serialization;

public interface ISerializer
{
    string Serialize(object message);

    T Deserialize<T>(string stream);

    object Deserialize(string value, Type type);
}