namespace NippoControlSystem.Infrastructure.Confiigurations
{
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
