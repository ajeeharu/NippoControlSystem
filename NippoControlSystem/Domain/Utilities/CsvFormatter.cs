using System.Data;
using System.Text;
using static NippoControlSystem.ApplicationService.Services.MeasureConditionService;

namespace NippoControlSystem.Domain.Utilities
{
    public static class CsvFormatter
    {
        /// <summary>
        /// PortDataの1行をCSV文字列に変換するロジック
        /// </summary>
        public static string FormatPortDataRow(DataRow dtItemRow, Cyc.IO.Settings settings)
        {
            var sb = new StringBuilder();
            // DIO, AO, AI, GND の組み立てロジック...
            return sb.ToString();
        }

        /// <summary>
        /// CheckDataの1行をCSV文字列に変換するロジック
        /// </summary>
        public static string FormatCheckDataRow(DataRow dtItemRow, int columnCount)
        {
            var sb = new StringBuilder();
            // 列ごとのエスケープ・クォート処理ロジック...
            return sb.ToString();
        }

        /// <summary>
        /// 必要に応じてダブルクォーテーションで囲む拡張メソッド
        /// </summary>
        public static string EncloseDoubleQuotesIfNeed(this string? field)
        {
            if (string.IsNullOrEmpty(field)) return string.Empty;
            // 拡張メソッドとして正しくインスタンス経由（またはthis経由）で呼び出す
            return field.NeedEncloseDoubleQuotes() ? field.EncloseDoubleQuotes() : field;
        }

        /// <summary>
        /// ダブルクォーテーションで囲む必要があるか判定する拡張メソッド
        /// </summary>
        private static bool NeedEncloseDoubleQuotes(this string? field)
        {
            field ??= string.Empty;
            return field.IndexOf('"') > -1 ||
                   field.IndexOf(',') > -1 ||
                   field.IndexOf('\r') > -1 ||
                   field.IndexOf('\n') > -1 ||
                   field.StartsWith(" ") || field.StartsWith("\t") ||
                   field.EndsWith(" ") || field.EndsWith("\t");
        }

        /// <summary>
        /// CSV用の文字列をダブルクォーテーションで囲む拡張メソッド
        /// </summary>
        public static string EncloseDoubleQuotes(this string? field)
        {
            field ??= string.Empty;
            if (field.IndexOf('"') > -1)
            {
                field = field.Replace("\"", "\"\"");
            }
            return "\"" + field + "\"";
        }

        // CSVファイルを読み込んでリストに展開する
        public List<List<string>> LoadCsvAsList(string filePath)
        {
            string csvText = _ioHelper.ReadAllText(filePath, _shiftJis) ?? string.Empty;
            if (string.IsNullOrEmpty(csvText)) return new List<List<string>>();

            return CsvToArrayList2(csvText);
        }
        /// <summary>
        /// CSVをArrayList（List<List<string>>）に変換
        /// </summary>
        private List<List<string>> CsvToArrayList2(string csvText)
        {
            // 前後の改行を削除しておく
            csvText = csvText.Trim(new char[] { '\r', '\n' });

            List<List<string>> csvRecords = new List<List<string>>();
            List<string> csvFields = new List<string>();

            int csvTextLength = csvText.Length;
            int startPos = 0, endPos = 0;
            string field = "";

            while (true)
            {
                // 空白を飛ばす
                while (startPos < csvTextLength &&
                    (csvText[startPos] == ' ' || csvText[startPos] == '\t'))
                {
                    startPos++;
                }

                // データの最後の位置を取得
                if (startPos < csvTextLength && csvText[startPos] == '"')
                {
                    // "で囲まれているとき
                    // 最後の"を探す
                    endPos = startPos;
                    while (true)
                    {
                        endPos = csvText.IndexOf('"', endPos + 1);
                        if (endPos < 0)
                        {
                            throw new ApplicationException("\"が不正");
                        }
                        // "が2つ続かない時は終了
                        if (endPos + 1 == csvTextLength || csvText[endPos + 1] != '"')
                        {
                            break;
                        }
                        // "が2つ続く
                        endPos++;
                    }

                    // 一つのフィールドを取り出す
                    field = csvText.Substring(startPos, endPos - startPos + 1);
                    // ""を"にする
                    field = field.Substring(1, field.Length - 2).Replace("\"\"", "\"");

                    endPos++;
                    // 空白を飛ばす
                    while (endPos < csvTextLength &&
                        csvText[endPos] != ',' && csvText[endPos] != '\n')
                    {
                        endPos++;
                    }
                }
                else
                {
                    // "で囲まれていない
                    // カンマか改行の位置
                    endPos = startPos;
                    while (endPos < csvTextLength &&
                        csvText[endPos] != ',' && csvText[endPos] != '\n')
                    {
                        endPos++;
                    }

                    // 一つのフィールドを取り出す
                    field = csvText.Substring(startPos, endPos - startPos);
                    // 後ろの空白を削除
                    field = field.TrimEnd();
                }

                // フィールドの追加
                csvFields.Add(field);

                // 行の終了か調べる
                if (endPos >= csvTextLength || csvText[endPos] == '\n')
                {
                    // 行の終了 -> レコードの追加
                    csvRecords.Add(csvFields);
                    csvFields = new List<string>();

                    if (endPos >= csvTextLength)
                    {
                        // 終了
                        break;
                    }
                }

                // 次のデータの開始位置
                startPos = endPos + 1;
            }

            return csvRecords;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startStatus"></param>
        /// <param name="serialNo"></param>
        /// <param name="goNo"></param>
        /// <param name="zuban"></param>
        /// <param name="edaban"></param>
        public static void StartInspection(InspectionStatus startStatus, string serialNo, string goNo, string zuban, string edaban)
        {
            InspectionStartStatus = startStatus;
            CurrentInspectionStatus = InspectionStatus.NormalStart; // 検査中など
            TestStartDT = DateTime.Now;

            SerialNo = serialNo;
            GoNo = goNo;
            Zuban = zuban;
            Edaban = edaban;
        }

        /// <summary>
        /// 検査を終了する際のドメインルール処理
        /// </summary>
        public static void FinishInspection(InspectionStatus finalStatus)
        {
            CurrentInspectionStatus = finalStatus;
            TestEndDateTime = DateTime.Now;
        }

        /// <summary>
        /// ログCSV用の1行文字列を組み立てる
        /// </summary>
        public string BuildResultLogCsvLine()
        {
            string passFailStr = (CurrentInspectionStatus == InspectionStatus.NormalEnd) ? "Pass" : "Fail";

            var fields = new string[]
            {
                TestEndDateTime.ToString("yyyy.MM.dd HH:mm:ss"),
                SerialNo ?? string.Empty,
                GoNo ?? string.Empty,
                Zuban ?? string.Empty,
                Edaban ?? string.Empty,
                InspectionStartStatusString,
                passFailStr
            };

            // 文字列のエスケープを適用してカンマ結合
            return string.Join(",", fields.Select(CsvStringExtensions.EncloseDoubleQuotes));
        }

    }
}