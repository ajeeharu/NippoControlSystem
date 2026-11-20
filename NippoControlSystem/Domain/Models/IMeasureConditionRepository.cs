namespace NippoControlSystem.Domain.Models
{
    // ログ記録用のデータ転送オブジェクト
    public class MeasureLogRecord
    {
        public DateTime TestEndDateTime { get; set; }
        public string SerialNo { get; set; }
        public string GoNo { get; set; }
        public string Zuban { get; set; }
        public string Edaban { get; set; }
        public string InspectionStartStatusString { get; set; }
        public bool IsPass { get; set; }
    }
}
