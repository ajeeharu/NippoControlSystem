namespace NippoControlSystem.Infrastructure.Devices
{
    public class AioDriver
    {
        public void WriteAnalogOutput(int channel, double voltage)
        {
            // 実際のメーカーSDK/DLLの関数を呼び出す
            // 例: CnetAio.AioSingleAoutVoltage(deviceId, channel, voltage);
        }
    }
}
