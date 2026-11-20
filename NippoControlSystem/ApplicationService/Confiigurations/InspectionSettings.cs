namespace NippoControlSystem.ApplicationService.Configurations
{
    public class InspectionSettings
    {
        // ガイドおよび各種状態定義（JSONから直接読み込み）
        public required string[] DioStatusGuide { get; set; }
        public required string[] DioStatus { get; set; }
        public required string[] DioStatusOld { get; set; }
        public int InspectionRetryMax { get; set; }
        public required string[] InspectionTypeText { get; set; }
        public int AutoTimeOut { get; set; }
        public required string[] CheckDataLimitFields { get; set; }
        public required float[][] CheckDataLimit { get; set; }
    }
}