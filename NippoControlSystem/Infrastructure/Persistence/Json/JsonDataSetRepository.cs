using NippoControlSystem.Domain.Models.NippoControlSystem.Domain.Models;
using System.IO;
using System.Text.Json;

namespace NippoControlSystem.Infrastructure.Persistence.Json
{
    public class JsonDataSetRepository
    {
        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public async Task<SystemDataContainer> LoadAsync(string filePath)
        {
            if (!File.Exists(filePath)) return new SystemDataContainer();

            using var stream = File.OpenRead(filePath);
            var data = await JsonSerializer.DeserializeAsync<SystemDataContainer>(stream, _options);
            return data ?? new SystemDataContainer();
        }

        public async Task SaveAsync(string filePath, SystemDataContainer data)
        {
            using var stream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(stream, data, _options);
        }
    }
}