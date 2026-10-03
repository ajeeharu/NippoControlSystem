using NippoControlSystem.Domain.Models.NippoControlSystem.Domain.Models;
using System.Text.Json.Serialization;

namespace NippoControlSystem.Domain.Models
{
    /// <summary>
    /// システム全体の各種ポート設定データを保持するコンテナクラス
    /// </summary>
    public class PortConfigContainer
    {
        [JsonPropertyName("PortConfigurations")]
        public List<PortConfiguration> PortConfigurations { get; set; } = new();
    }
}