namespace NippoControlSystem.ApplicationService.Confiigurations
{
    public class AppInfoSettings
    {
        public string ApplicationName { get; set; } = string.Empty;
        public string ApplicationNameEn { get; set; } = string.Empty;
        public string ApplicationFolder { get; set; } = string.Empty; // 実行時にシステムで自動設定
        public string SettingsHolder { get; set; } = string.Empty;
        public string DefaultConfigFilename { get; set; } = string.Empty;
        public string MenuFolder { get; set; } = string.Empty;
        public string MenuExtension { get; set; } = string.Empty;
        public string DataFolder { get; set; } = string.Empty;
        public string DataExtension { get; set; } = string.Empty;
        public string HistViewFolder { get; set; } = string.Empty;
        public string MeasureConditionPath { get; set; } = string.Empty;
        public string ManualPath { get; set; } = string.Empty;
        public int LoggingLevel { get; set; }

        public string Serial { get; set; } = string.Empty;
        public int SerialSuffixLength { get; set; }
        public string Item { get; set; } = string.Empty;
        public string MainNo { get; set; } = string.Empty;
        public string SubNo { get; set; } = string.Empty;
        public string SerialTitle { get; set; } = string.Empty;
        public string GokiTitle { get; set; } = string.Empty;

        public string CheckDat { get; set; } = string.Empty;
        public string ListDat { get; set; } = string.Empty;
        public string HistDat { get; set; } = string.Empty;
        public string PortDat { get; set; } = string.Empty;
        public string MenuCsv { get; set; } = string.Empty;
        public string MenuDirPrior { get; set; } = string.Empty;
    }
}
