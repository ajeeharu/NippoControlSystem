using NippoControlSystem.ApplicationService.Interfaces;
using NippoControlSystem.Domain.Interfaces;

namespace NippoControlSystem.ApplicationService.Services
{
    public class OperationPanelService : IOperationPanelService
    {
        readonly IOperationPanel _operationPanel;

        /// <summary>
        /// Sets the control for the green LED.
        /// </summary>
        /// <param name="OnOff"></param>
        public void SetGreenLedControl(LedOnOff OnOff)
        {
            _operationPanel.SetLedControl(LedType.GreenButton, OnOff);
            // Implementation for setting green button LED control
        }

        /// <summary>
        /// Sets the control for the red LED.
        /// </summary>
        /// <param name="OnOff"></param>
        public void SetRedLedControl(LedOnOff OnOff)
        {
            _operationPanel.SetLedControl(LedType.RedButton, OnOff);
            // Implementation for setting red button LED control
        }

        /// <summary>
        /// Sets the control for the inspection LED.
        /// </summary>
        /// <param name="OnOff"></param>
        public void SetInspectionLedControl(LedOnOff OnOff)
        {
            _operationPanel.SetLedControl(LedType.Inspection, OnOff);
            // Implementation for setting inspection LED control
        }

        /// <summary>
        /// Gets the control state of the green switch.
        /// </summary>
        /// <returns></returns>
        public SwitchOnOff GetGreenSwitchControl()
        {
            // Implementation for getting green button LED control
            return _operationPanel.GetSwitchControl(SwitchType.GreenSwitch);
        }

        /// <summary>
        /// Gets the control state of the red switch.
        /// </summary>
        /// <returns></returns>
        public SwitchOnOff GetRedSwitchControl()
        {
            // Implementation for getting red button LED control
            return _operationPanel.GetSwitchControl(SwitchType.RedSwitch);
        }

        /// <summary>
        /// Sets the control for the 12V power switch.
        /// </summary>
        public void SetPowerSwitch12VoltControl()
        {
            _operationPanel.SetPowerSwitch(PowerSwitchType.PowerSwitch12Volt);
            // Implementation for setting 12V power switch
        }

        /// <summary>
        /// Sets the control for the 24V power switch.
        /// </summary>
        public void SetPowerSwitch24VoltControl()
        {
            _operationPanel.SetPowerSwitch(PowerSwitchType.PowerSwitch24Volt);
            // Implementation for setting 24V power switch
        }
    }
}
