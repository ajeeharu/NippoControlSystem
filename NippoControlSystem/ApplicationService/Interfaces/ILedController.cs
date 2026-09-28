namespace NippoControlSystem.ApplicationService.Interfaces
{
    public interface ILedController
    {
        void SetInspectionBrightness(double percentage);
        void SetButtonLed(LedType type, bool turnOn);
        void SetPowerSwitch(bool enable);
    }
}
