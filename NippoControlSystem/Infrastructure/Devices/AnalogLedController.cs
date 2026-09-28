using NippoControlSystem.Domain.Interfaces;
using NippoControlSystem.Domain.Interfaces.enums;

namespace NippoControlSystem.Infrastructure.Devices
{
    /// <summary>
    /// 本処理：具体的にアナログ出力（電圧）を使って制御するクラス
    /// </summary>
    public class AnalogLedController : ILedController
    {
        public void SetButtonLed(LedType type, bool turnOn)
        {
            // アナログ出力処理を実装
        }

        public void SetPowerSwitch(bool enable)
        {
            // アナログ出力処理を実装
        }
    }
}