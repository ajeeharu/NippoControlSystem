using System.Text;
using System.Text.Json;

namespace NippoControlSystem.Infrastructure.Persistence
{
    public class JsonFileRepository
    {
        private readonly FileIoHelper _ioHelper;
        private readonly Encoding _utf8 = Encoding.UTF8; // JSONはUTF-8

        public JsonFileRepository(FileIoHelper ioHelper)
        {
            _ioHelper = ioHelper;
        }

        public void Save<T>(T data, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(data, options);
            _ioHelper.WriteAllText(filePath, jsonString, _utf8); // UTF-8を指定
        }

        public T Load<T>(string filePath) where T : new()
        {
            string jsonString = _ioHelper.ReadAllText(filePath, _utf8) ?? string.Empty; ; // UTF-8を指定
            if (string.IsNullOrEmpty(jsonString)) return new T();
            return JsonSerializer.Deserialize<T>(jsonString) ?? new T();
        }
    }
}
