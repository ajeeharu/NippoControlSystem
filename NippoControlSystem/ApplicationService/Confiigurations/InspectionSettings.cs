namespace NippoControlSystem.ApplicationService.Configurations
{
    public class InspectionSettings
    {
        // ガイドおよび各種状態定義（JSONから直接読み込み）
        public string[] DioStatusGuide { get; set; }
        public string[] DioStatus { get; set; }
        public string[] DioStatusOld { get; set; }

        public int InspectRetryMax { get; set; }
        public string[] inspection_TypeText { get; set; }
        public int AutoTimeOut { get; set; }
        public string[] CheckDatLMTFields { get; set; }
        public float[][] CheckDatLMT { get; set; }
    }
}