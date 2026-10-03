using System.Text.Json.Serialization;

namespace NippoControlSystem.Domain.Models
{
    namespace NippoControlSystem.Domain.Models
    {
        /// <summary>
        /// ポート入出力設定データモデル
        /// </summary>
        public class PortConfiguration
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; set; } = string.Empty;

            [JsonPropertyName("guide")]
            public string Guide { get; set; } = string.Empty;

            [JsonPropertyName("portType")]
            public int PortType { get; set; }

            [JsonPropertyName("digitalOutputs")]
            public int[] DigitalOutputs { get; set; } = new int[32];

            [JsonPropertyName("digitalInputs")]
            public int[] DigitalInputs { get; set; } = new int[32];

            [JsonPropertyName("analogOutputs")]
            public double[] AnalogOutputs { get; set; } = new double[8];

            [JsonPropertyName("analogInputs")]
            public double[] AnalogInputs { get; set; } = new double[8];

            [JsonPropertyName("groundDigitalOutputs")]
            public int[] GroundDigitalOutputs { get; set; } = new int[5];

            [JsonPropertyName("groundAnalogOutputs")]
            public double[] GroundAnalogOutputs { get; set; } = new double[5];
        }

        /// <summary>
        /// JSONルートコンテナ
        /// </summary>
        public class SystemDataContainer
        {
            [JsonPropertyName("portConfigurations")]
            public List<PortConfiguration> PortConfigurations { get; set; } = new();
        }
    }
}