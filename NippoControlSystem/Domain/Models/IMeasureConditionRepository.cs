using NippoControlSystem.Domain.Models;
using System.Data;

namespace NippoControlSystem.Domain.Models
{
    // ログ記録用のデータ転送オブジェクト
    public class MeasureLogRecord
    {
        public DateTime TestEndDT { get; set; }
        public string SerialNo { get; set; }
        public string GoNo { get; set; }
        public string Zuban { get; set; }
        public string Edaban { get; set; }
        public string InspecStartStatString { get; set; }
        public bool IsPass { get; set; }
    }
}

namespace NippoControlSystem.Domain.Repositories
{
    // リポジトリインターフェース（具体実装は Infrastructure 層）
    public interface IMeasureConditionRepository
    {
        void SaveResultLog(MeasureLogRecord record, string folderPath);
        bool SaveInspectItem(DataTable listDat, string filePath, bool withResult, bool checkChanged);
        bool SavePortData(DataTable portDat, string filePath, bool checkChanged);
        bool SaveCheckData(DataTable checkDat, string filePath, bool checkChanged);
        string ReadText(string filePath);
        void AppendText(string targetPath, string content);
    }
}