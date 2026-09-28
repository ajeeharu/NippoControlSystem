namespace NippoControlSystem.Infrastructure.Confiigurations
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
}
