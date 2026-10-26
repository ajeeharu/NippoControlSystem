using System.IO;

namespace NippoControlSystem.ApplicationService.Common
{
    public abstract class CsvApplicationServiceBase
    {
        /// <summary>
        /// CSVのテキストデータをパースして指定した型のリストに変換する共通処理
        /// </summary>
        /// <typeparam name="T">変換後のDTO/モデル型</typeparam>
        /// <param name="csvContent">CSVの生テキスト</param>
        /// <param name="mapFunc">1行の文字列配列（Values）を受け取り、オブジェクトに変換するラムダ式</param>
        /// <param name="skipHeader">1行目をヘッダーとしてスキップするかどうか</param>
        protected List<T> ParseCsvContent<T>(string? csvContent, Func<string[], T> mapFunc, bool skipHeader = true)
        {
            var results = new List<T>();

            if (string.IsNullOrWhiteSpace(csvContent))
            {
                return results;
            }

            using (var reader = new StringReader(csvContent))
            {
                string? line;
                bool isHeader = skipHeader;

                while ((line = reader.ReadLine()) != null)
                {
                    if (isHeader)
                    {
                        isHeader = false;
                        continue; // ヘッダー行をスキップ
                    }

                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // カンマ区切りで分割（必要に応じてエスケープ文字等の処理をここに拡張可能）
                    var values = line.Split(',');

                    // 呼び出し元から渡されたマッピングルールを適用
                    var item = mapFunc(values);
                    if (item != null)
                    {
                        results.Add(item);
                    }
                }
            }

            return results;
        }
        /// <summary>
        /// CSVの生テキストを指定されたデリミタや条件でレコード（文字列配列のリスト）に分解する共通処理
        /// </summary>
        protected List<string[]> ParseCsvContentToRecords(string? csvContent, bool skipHeader = true, char delimiter = ',')
        {
            var records = new List<string[]>();

            if (string.IsNullOrWhiteSpace(csvContent))
            {
                return records;
            }

            using (var reader = new StringReader(csvContent))
            {
                string? line;
                bool isHeader = skipHeader;

                while ((line = reader.ReadLine()) != null)
                {
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var values = line.Split(delimiter);
                    records.Add(values);
                }
            }

            return records;
        }
    }
}
