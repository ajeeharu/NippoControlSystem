using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace NippoControlSystem.Infrastructure.Persistence
{
    public class JsonFileRepository
    {
        private readonly FileIoHelper _ioHelper;
        private readonly Encoding _utf8 = Encoding.UTF8;
        private readonly JsonSerializerOptions _jsonOptions;

        public JsonFileRepository(FileIoHelper ioHelper)
        {
            _ioHelper = ioHelper ?? throw new ArgumentNullException(nameof(ioHelper));
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
        }

        public void Save<T>(T data, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(data, _jsonOptions);
            _ioHelper.WriteAllText(filePath, jsonString, _utf8);
        }

        public T Load<T>(string filePath) where T : new()
        {
            string jsonString = _ioHelper.ReadAllText(filePath, _utf8) ?? string.Empty;
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return new T();
            }

            return JsonSerializer.Deserialize<T>(jsonString, _jsonOptions) ?? new T();
        }
    }
}