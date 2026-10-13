using NippoControlSystem.Domain.Interfaces;
using NippoControlSystem.Infrastructure.Devices;

namespace NippoControlSystem.Domain.Services
{
    public class OperationPanel : IOperationPanel
    {
        private AnalogIO _analogIO;
        OperationPanel(AnalogIO analogIO)
        {
            _analogIO = analogIO;
        }
        /// <summary>
        /// Sets the LED control based on the specified type and on/off state.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="OnOff"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SetLedControl(LedType type, LedOnOff OnOff)
        {
            switch (type)
            {
                case LedType.Inspection:
                    _analogIO.setInspctLamp((int)OnOff);
                    break;
                case LedType.RedButton:
                    _analogIO.setRedLamp((int)OnOff);
                    break;
                case LedType.GreenButton:
                    _analogIO.setGreenLamp((int)OnOff);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        /// <summary>
        /// Gets the switch control state based on the specified type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public SwitchOnOff GetSwitchControl(SwitchType type)
        {
            int switchState;

            switch (type)
            {
                case SwitchType.RedSwitch:
                    switchState = _analogIO.getRedSw();
                    break;
                case SwitchType.GreenSwitch:
                    switchState = _analogIO.getGreenSw();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            if (switchState == 1)
            {
                return SwitchOnOff.SwitchOn;
            }
            return SwitchOnOff.SwitchOff;
        }
        /// <summary>
        /// Sets the power switch based on the specified type.
        /// </summary>
        /// <param name="type"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SetPowerSwitch(PowerSwitchType type)
        {
            switch (type)
            {
                case PowerSwitchType.PowerSwitch12Volt:
                    _analogIO.setPower12V();
                    break;
                case PowerSwitchType.PowerSwitch24Volt:
                    _analogIO.setPower24V();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
