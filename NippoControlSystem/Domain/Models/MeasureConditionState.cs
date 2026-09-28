namespace NippoControlSystem.Domain.Models
{
    /// <summary>
    /// 検査状態・結果データを保持するデータ構造
    /// </summary>
    public class MeasureConditionState
    {
        public DateTime TestEndDT { get; set; }
        public string SerialNo { get; set; }
        public string GoNo { get; set; }
        public string Zuban { get; set; }
        public string Edaban { get; set; }
        public string InspecStartStatString { get; set; }
        public enumInspectStat InspecStat { get; set; }
    }
}