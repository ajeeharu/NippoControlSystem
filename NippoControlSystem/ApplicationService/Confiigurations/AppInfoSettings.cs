using Microsoft.Extensions.Logging;

namespace NippoControlSystem.ApplicationService.Configurations
{
    public class AppInfoSettings
    {
        // アプリケーション基本情報
        public string ApplicationName { get; set; } = string.Empty;
        public string ApplicationNameEn { get; set; } = string.Empty;

        // 実行時パス（自動取得にして外部からの変更を防ぐ）
        public string ApplicationFolder => AppDomain.CurrentDomain.BaseDirectory;

        public LogLevel LoggingLevel { get; set; } = LogLevel.Information;

        // グループ化（ネスト構造化）
        public PathSettings Paths { get; set; } = new PathSettings();
        public SerialSettings SerialInfo { get; set; } = new SerialSettings();
    }

    public class PathSettings
    {
        public string SettingsHolder { get; set; } = string.Empty;
        public string DefaultConfigFilename { get; set; } = string.Empty;
        public string MenuFolder { get; set; } = string.Empty;
        public string MenuExtension { get; set; } = string.Empty;
        public string DataFolder { get; set; } = string.Empty;
        public string DataExtension { get; set; } = string.Empty;
        public string HistViewFolder { get; set; } = string.Empty;
        public string MeasureConditionPath { get; set; } = string.Empty;
        public string ManualPath { get; set; } = string.Empty;

        // DAT / CSV ファイル名
        public string CheckDat { get; set; } = string.Empty;
        public string ListDat { get; set; } = string.Empty;
        public string HistDat { get; set; } = string.Empty;
        public string PortDat { get; set; } = string.Empty;
        public string MenuCsv { get; set; } = string.Empty;
        public string MenuDirPrior { get; set; } = string.Empty;
    }

    public class SerialSettings
    {
        public string Serial { get; set; } = string.Empty;
        public int SerialSuffixLength { get; set; }
        public string Item { get; set; } = string.Empty;
        public string MainNo { get; set; } = string.Empty;
        public string SubNo { get; set; } = string.Empty;
        public string SerialTitle { get; set; } = string.Empty;
        public string GokiTitle { get; set; } = string.Empty;
    }
}