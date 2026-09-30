namespace NippoControlSystem.ApplicationService.Configurations
{
    public class InspectionSettings
    {
        public int InspectRetryMax { get; set; } = 10;
        public List<string> InspectionTypeText { get; set; } = new();
        public string AioMonitorExePath { get; set; } = string.Empty;
        public int AutoTimeOut { get; set; } = 2;
        public List<string> CheckDatLmtFields { get; set; } = new();

        // ジャグ配列 (float[][])
        public float[][] CheckDatLmt { get; set; } = Array.Empty<float[]>();
    }
}