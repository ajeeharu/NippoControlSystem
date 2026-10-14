namespace NippoControlSystem.ApplicationService.Configurations
{
    public class GeneralSettings
    {
        // Main
        public required string ApplicationName { get; set; }
        public required string ApplicationNameEn { get; set; }
        public required string ApplicationFolder { get; set; }
        public required string SettingsFolder { get; set; }
        public required string DefaultConfigFilename { get; set; }
        public required string MenuFolder { get; set; }
        public required string MenuExtension { get; set; }
        public required string DataFolder { get; set; }
        public required string DataExtension { get; set; }
        public required string HistViewFolder { get; set; }
        public required string MeasureConditionPath { get; set; }
        public required string ManualPath { get; set; }
        public int LoggingLevel { get; set; }
        public required string Serial { get; set; }
        public int SerialSuffixLength { get; set; }
        public required string Item { get; set; }
        public required string MainNo { get; set; }
        public required string SubNo { get; set; }
        public required string SerialTitle { get; set; }
        public required string MachineNumber { get; set; }
        public required string CheckFileName { get; set; }
        public required string ListFileName { get; set; }
        public required string HistFileName { get; set; }
        public required string PortFileName { get; set; }
        public required string MenuCsvFileName { get; set; }
        public required string MenuDirPriority { get; set; }
    }
}