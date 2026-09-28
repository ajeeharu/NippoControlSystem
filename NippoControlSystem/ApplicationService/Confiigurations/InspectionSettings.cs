namespace NippoControlSystem.ApplicationService.Confiigurations
{
    public class InspectionSettings
    {
        public int InspectRetryMax { get; set; }
        public string[] InspectionTypeText { get; set; } = Array.Empty<string>();
        public string AioMonitorExePath { get; set; } = string.Empty;
        public int AutoTimeOut { get; set; }
        public string[] CheckDatLmtFields { get; set; } = Array.Empty<string>();
        public float[][] CheckDatLmt { get; set; } = Array.Empty<float[]>();
    }
}
