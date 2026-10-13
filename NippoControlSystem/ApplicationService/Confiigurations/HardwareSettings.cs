namespace NippoControlSystem.ApplicationService.Configurations
{
    public class HardwareSettings
    {
        public string[] DioNames { get; set; }
        public int[] DioNums { get; set; }
        public string AoName { get; set; }
        public int AoNum { get; set; }
        public string AoSwitchName { get; set; }
        public string[] AoSwitchStatus { get; set; }
        public string[] AoSwitchGuide { get; set; }
        public int AoSwitchNum { get; set; }
        public int AoSwitchStartBit { get; set; }
        public string AiName { get; set; }
        public int AiNum { get; set; }
        public int AiCurrentNum { get; set; }
        public string[] GndNames { get; set; }
        public int[] GndNums { get; set; }

        // Cyc.IO.DIO
        public string[] DeviceNameDO { get; set; }
        public int DO_Pmax { get; set; }
        public int DIO_LogLevel { get; set; }

        // Cyc.IO.AIO
        public string DeviceNameAIO { get; set; }
        public int AImax { get; set; }
        public int AOmax { get; set; }
        public int AIO_LogLevel { get; set; }
        public int AiAveTimes { get; set; }
        public float AoVoltMin { get; set; }
        public float AoVoltMax { get; set; }
        public float AiVoltMin { get; set; }
        public float AiVoltMax { get; set; }
        public float[] AoCalib_A { get; set; }
        public float[] AoCalib_B { get; set; }
        public float[] AoCalib_C { get; set; }
        public float[] AiCalib_A { get; set; }
        public float[] AiCalib_B { get; set; }
        public float AoDiv { get; set; }
        public float AiMulti { get; set; }

        // Cyc.IO.AI2DI
        public string[] DeviceNameAI2DI { get; set; }
        public int AI2DI_BDmax { get; set; }
        public int AI2DI_AImax { get; set; }
        public int AI2DI_DImax { get; set; }
        public int AI2DI_DOmax { get; set; }
        public int AI2DI_LogLevel { get; set; }
        public int AI2DI_AveTimes { get; set; }
        public float AI2DI_VoltMin { get; set; }
        public float AI2DI_VoltMax { get; set; }
        public float AI2V_A { get; set; }
        public float AI2V_B { get; set; }
        public float AI2I_A { get; set; }
        public float AI2I_B { get; set; }
        public int[] AI2DI_DeviceChannel { get; set; }
    }
}