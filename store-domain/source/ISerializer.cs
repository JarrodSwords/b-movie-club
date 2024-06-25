using System.Text.Json;

namespace Store.Domain;

public interface ISerializer
{
    Result<T> Deserialize<T>(string jsonData);
    Result<T> Deserialize<T>(Message message);
    Result<string> Serialize(object data);
}

public class DefaultSerializer : ISerializer
{
    public Result<T> Deserialize<T>(string jsonData)
    {
        try
        {
            var data = JsonSerializer.Deserialize<T>(jsonData);

            return data is null
                ? DeserializationFailed()
                : data;
        }
        catch (Exception e)
        {
            return DeserializationFailed(e.Message);
        }
    }

    public Result<T> Deserialize<T>(Message message) => Deserialize<T>(message.JsonData);

    public Result<string> Serialize(object data) => JsonSerializer.Serialize(data);

    private static Error DeserializationFailed(string? description = null) =>
        description is null
            ? new(nameof(DeserializationFailed))
            : new(nameof(DeserializationFailed), description);
}
