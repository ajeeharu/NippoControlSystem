namespace NippoControlSystem.Domain.Interfaces
{
    public enum LedType
    {
        Inspection,     // 検査用
        RedButton,      // 赤ボタン用
        GreenButton     // 緑ボタン用
    }
    public enum SwitchType
    {
        RedSwitch,      // 赤スイッチ用
        GreenSwitch     // 緑スイッチ用
    }

    public enum LedOnOff
    {
        LedOff = 0,     // LedOff
        LedOn = 1       // LedOn
    }
    public enum SwitchOnOff
    {
        SwitchOff = 0,  // SwitchOff
        SwitchOn = 1    // SwitchOn
    }
    public enum PowerSwitchType
    {
        PowerSwitch12Volt = 0,     // 12Volt用
        PowerSwitch24Volt = 1      // 24Volt用
    }
    public interface IOperationPanel
    {
        void SetLedControl(LedType type, LedOnOff OnOff);
        SwitchOnOff GetSwitchControl(SwitchType type);
        void SetPowerSwitch(PowerSwitchType type);
    }

}
