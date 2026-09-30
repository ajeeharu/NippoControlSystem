namespace NippoControlSystem.Infrastructure.Configurations
{
    public class DioSettings
    {
        public string[] DeviceNameDo { get; set; } = Array.Empty<string>();
        public int DoPmax { get; set; }
        public int DioLogLevel { get; set; }

        public string[] DioNames { get; set; } = Array.Empty<string>();
        public int[] DioNums { get; set; } = Array.Empty<int>();
        public string AoName { get; set; } = string.Empty;
        public int AoNum { get; set; }
        public string AoSwitchName { get; set; } = string.Empty;
        public string[] AoSwitchStat { get; set; } = Array.Empty<string>();
        public string[] AoSwitchGuide { get; set; } = Array.Empty<string>();
        public int AoSwitchNum { get; set; }
        public int AoSwitchStartBit { get; set; }
        public string AiName { get; set; } = string.Empty;
        public int AiNum { get; set; }
        public int AiCurrentNum { get; set; }
        public string[] GndNames { get; set; } = Array.Empty<string>();
        public int[] GndNums { get; set; } = Array.Empty<int>();
    }
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
    public class Ai2DiSettings
    {
        public string[] DeviceNameAi2Di { get; set; } = Array.Empty<string>();
        public int Ai2DiBdMax { get; set; }
        public int Ai2DiAiMax { get; set; }
        public int Ai2DiDiMax { get; set; }
        public int Ai2DiDoMax { get; set; }
        public int Ai2DiLogLevel { get; set; }
        public int Ai2DiAveTimes { get; set; }
        public float Ai2DiVoltMin { get; set; }
        public float Ai2DiVoltMax { get; set; }
        public float Ai2vA { get; set; }
        public float Ai2vB { get; set; }
        public float Ai2iA { get; set; }
        public float Ai2iB { get; set; }
        public int[] Ai2DiDeviceChannel { get; set; } = Array.Empty<int>();
    }
}
