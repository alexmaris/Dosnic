namespace Dosnic.Utils
{
    public interface IJsonSerializer
    {
        T Deserialize<T>(string json);
        string Serialize(object value);
    }

    public class JsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string json)
        {
            return NetJSON.NetJSON.Deserialize<T>(json);
        }

        public string Serialize(object value)
        {
            return NetJSON.NetJSON.Serialize(value);
        }
    }
}
