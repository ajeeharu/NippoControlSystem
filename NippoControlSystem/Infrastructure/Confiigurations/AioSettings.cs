namespace NippoControlSystem.Infrastructure.Confiigurations
{
    public class AioSettings
    {
        public string DeviceNameAio { get; set; } = string.Empty;
        public int AiMax { get; set; }
        public int AoMax { get; set; }
        public int AioLogLevel { get; set; }
        public int AiAveTimes { get; set; }
        public float AoVoltMin { get; set; }
        public float AoVoltMax { get; set; }
        public float AiVoltMin { get; set; }
        public float AiVoltMax { get; set; }
        public float[] AoCalibA { get; set; } = Array.Empty<float>();
        public float[] AoCalibB { get; set; } = Array.Empty<float>();
        public float[] AoCalibC { get; set; } = Array.Empty<float>();
        public float[] AiCalibA { get; set; } = Array.Empty<float>();
        public float[] AiCalibB { get; set; } = Array.Empty<float>();

        // 読み取り専用の計算用プロパティ（JSONからは割り当てない）
        public float AoDiv => (AoVoltMax - AoVoltMin) / 10.0f;
        public float AiMulti => (AoVoltMax - AiVoltMin) / 10.0f;
    }
}
