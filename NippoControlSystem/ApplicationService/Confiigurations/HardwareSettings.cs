namespace NippoControlSystem.ApplicationService.Configurations
{
    public class HardwareSettings
    {
        public required string[] DioNames { get; set; }
        public required int[] DioNums { get; set; }
        public required string AoName { get; set; }
        public int AoNum { get; set; }
        public required string AoSwitchName { get; set; }
        public required string[] AoSwitchStatus { get; set; }
        public required string[] AoSwitchGuide { get; set; }
        public int AoSwitchNum { get; set; }
        public int AoSwitchStartBit { get; set; }
        public required string AiName { get; set; }
        public int AiNum { get; set; }
        public int AiCurrentNum { get; set; }
        public required string[] GndNames { get; set; }
        public required int[] GndNums { get; set; }

        // Cyc.IO.DIO
        public required string[] DeviceNameDO { get; set; }
        public int DO_Pmax { get; set; }
        public int DIO_LogLevel { get; set; }

        // Cyc.IO.AIO
        public required string DeviceNameAIO { get; set; }
        public int AImax { get; set; }
        public int AOmax { get; set; }
        public int AIO_LogLevel { get; set; }
        public int AiAveTimes { get; set; }
        public float AoVoltMin { get; set; }
        public float AoVoltMax { get; set; }
        public float AiVoltMin { get; set; }
        public float AiVoltMax { get; set; }
        public required float[] AoCalib_A { get; set; }
        public required float[] AoCalib_B { get; set; }
        public required float[] AoCalib_C { get; set; }
        public required float[] AiCalib_A { get; set; }
        public required float[] AiCalib_B { get; set; }
        public float AoDiv { get; set; }
        public float AiMulti { get; set; }

        // Cyc.IO.AI2DI
        public required string[] DeviceNameAI2DI { get; set; }
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
        public required int[] AI2DI_DeviceChannel { get; set; }
        // OprationPanelの周辺デバイスのBIT指定
        public int DI_BIT_GREEN_SWITCH { get; set; }
        public int DI_BIT_RED_SWITCH { get; set; }
        public int DO_BIT_SWITCHING_POWER { get; set; }
        public int DO_BIT_GREEN_LED { get; set; }
        public int DO_BIT_RED_LED { get; set; }
        public int DO_BIT_INSPECTION_LED { get; set; }
    }
}