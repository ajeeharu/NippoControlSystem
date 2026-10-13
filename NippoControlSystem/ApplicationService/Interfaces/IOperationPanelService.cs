using NippoControlSystem.Domain.Interfaces;

namespace NippoControlSystem.ApplicationService.Interfaces
{
    public interface IOperationPanelService
    {
        void SetGreenLedControl(LedOnOff OnOff);
        void SetRedLedControl(LedOnOff OnOff);
        void SetInspectionLedControl(LedOnOff OnOff);
        SwitchOnOff GetGreenSwitchControl();
        SwitchOnOff GetRedSwitchControl();
        void SetPowerSwitch12VoltControl();
        void SetPowerSwitch24VoltControl();
    }
}
