using NippoControlSystem.Domain.Interfaces.enums;

namespace NippoControlSystem.Domain.Interfaces
{
    public interface ILedController
    {
        void SetLedControl(LedType type, LedOnOff OnOff);
        LedOnOff GetLedControl(LedType type);
    }
}
