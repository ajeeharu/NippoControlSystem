using NippoControlSystem.Domain.Interfaces;

namespace NippoControlSystem.Domain.Services
{
    public class LedControlService
    {
        private readonly ILedService _ledService;

        //public async Task TurnOnLedAsync()
        //{
        //    // 点灯用の電圧値（例: 5.0V）を設定して制御層に依頼
        //    double targetVoltage = 5.0; 
        //    await Task.Run(() => _ledService.SetLedVoltage(targetVoltage));
        //}
    }
}
