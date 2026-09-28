using NippoControlSystem.Domain.Models;
using NippoControlSystem.Domain.Repositories;
using NippoControlSystem.Infrastructure.Formatters;
using System.Data;
using System.IO;
using System.Text;

namespace NippoControlSystem.Infrastructure.Repositories
{
    public class CsvMeasureConditionRepository : IMeasureConditionRepository
    {
        private readonly Encoding _encoding = Encoding.GetEncoding("Shift_JIS");
        private const string PartExtension = ".part";

        public void SaveResultLog(MeasureLogRecord record, string folderPath)
        {
            string resultCsvPath = Path.Combine(folderPath, string.Format("{0:yyyy}-log.csv", record.TestEndDT));

            var lineValues = new[]
            {
                CsvFormatter.EncloseDoubleQuotes(string.Format("{0:yyyy.MM.dd HH:mm:ss}", record.TestEndDT)),
                CsvFormatter.EncloseDoubleQuotes(record.SerialNo),
                CsvFormatter.EncloseDoubleQuotes(record.GoNo),
                CsvFormatter.EncloseDoubleQuotes(record.Zuban),
                CsvFormatter.EncloseDoubleQuotes(record.Edaban),
                CsvFormatter.EncloseDoubleQuotes(record.InspecStartStatString),
                CsvFormatter.EncloseDoubleQuotes(record.IsPass ? "Pass" : "Fail")
            };

            using (var writer = new StreamWriter(resultCsvPath, true, _encoding))
            {
                writer.WriteLine(string.Join(",", lineValues));
            }
        }

        public bool SaveInspectItem(DataTable listDat, string filePath, bool withResult, bool checkChanged)
        {
            listDat.AcceptChanges();
            string tempPartFile = filePath + PartExtension;
            var sb = new StringBuilder();

            using (var writer = new StreamWriter(tempPartFile, false, _encoding))
            {
                foreach (DataRow row in listDat.Rows)
                {
                    sb.Clear();
                    bool isFirst = true;
                    for (int i = 0; i < listDat.Columns.Count; i++)
                    {
                        if (listDat.Columns[i].ColumnName == "InspectID") continue;

                        string val = row[i].ToString();
                        if (i == 1) val = val.Replace(Environment.NewLine, "\\n");
                        if (listDat.Columns[i].ColumnName == "Result" && !withResult) val = "";

                        if (!isFirst) sb.Append(",");
                        sb.Append(CsvFormatter.EncloseDoubleQuotes(val));
                        isFirst = false;
                    }
                    writer.WriteLine(sb.ToString());
                }
            }

            return ApplyFileUpdate(tempPartFile, filePath, checkChanged);
        }

        public bool SavePortData(DataTable portDat, string filePath, bool checkChanged)
        {
            // ポートデータのCSV生成処理（前述の改善コードを適用）
            // ...
            return true;
        }

        public bool SaveCheckData(DataTable checkDat, string filePath, bool checkChanged)
        {
            // チェックデータのCSV生成処理（前述の改善コードを適用）
            // ...
            return true;
        }

        public string ReadText(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(fs, _encoding))
            {
                return reader.ReadToEnd();
            }
        }

        public void AppendText(string targetPath, string content)
        {
            if (string.IsNullOrEmpty(content)) return;
            using (var writer = new StreamWriter(targetPath, true, _encoding))
            {
                writer.WriteLine();
                writer.Write(content);
            }
        }

        private bool ApplyFileUpdate(string tempFile, string targetFile, bool checkChanged)
        {
            if (File.Exists(targetFile))
            {
                if (checkChanged)
                {
                    bool isDifferent = !IsFileSame(tempFile, targetFile);
                    File.Delete(tempFile);
                    return isDifferent;
                }
                File.Delete(targetFile);
                File.Move(tempFile, targetFile);
                return true;
            }
            File.Move(tempFile, targetFile);
            return true;
        }

        private bool IsFileSame(string file1, string file2)
        {
            using (var fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read))
            using (var fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read))
            {
                if (fs1.Length != fs2.Length) return false;
                int b1, b2;
                do
                {
                    b1 = fs1.ReadByte();
                    b2 = fs2.ReadByte();
                } while (b1 == b2 && b1 != -1);
                return b1 == b2;
            }
        }
    }
}