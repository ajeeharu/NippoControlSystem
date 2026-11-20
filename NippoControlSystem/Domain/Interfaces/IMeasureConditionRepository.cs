
using NippoControlSystem.Domain.Models;
using System.Data;

namespace NippoControlSystem.Domain.Interfaces
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