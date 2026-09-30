using NippoControlSystem.Domain.Interfaces.enums;

namespace NippoControlSystem.Domain.Interfaces
{
    internal interface IPoewerSwitchController
    {
        void SetPowerSwitch(PowerSwitchType type, PowerSwitchOnOff onOff);
        PowerSwitchOnOff GetPowerSwitch(PowerSwitchType type);
    }
}
