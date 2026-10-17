using NippoControlSystem.Infrastructure.Persistence;
using System.Text;

namespace wChecker.Infrastructure
{
    public class CsvFileRepository
    {
        private readonly FileIoHelper _ioHelper;
        private readonly Encoding _shiftJis = Encoding.GetEncoding("Shift_JIS"); // CSVはShift_JIS

        public CsvFileRepository(FileIoHelper ioHelper)
        {
            _ioHelper = ioHelper;
        }

        // CSVファイルを読み込んでリストに展開する
        public List<List<string>> LoadCsvAsList(string filePath)
        {
            string csvText = _ioHelper.ReadAllText(filePath, _shiftJis) ?? string.Empty;
            if (string.IsNullOrEmpty(csvText)) return new List<List<string>>();

            return CsvToArrayList2(csvText);
        }

        public void AppendLine(string filePath, string line)
        {
            _ioHelper.AppendLine(filePath, line, _shiftJis);
        }

        public void AppendText(string filePath, string text)
        {
            _ioHelper.AppendText(filePath, text, _shiftJis);
        }

        public bool Exists(string filePath)
        {
            return _ioHelper.Exists(filePath);
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
    }
}