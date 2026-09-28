using NippoControlSystem.Domain.Interfaces.enums;

namespace NippoControlSystem.Domain.Interfaces
{
    public interface ILedController
    {
        void SetButtonLed(LedType type, bool turnOn);
        void SetPowerSwitch(bool enable);
    }
}
