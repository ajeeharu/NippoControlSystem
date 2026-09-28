using System.Text.RegularExpressions;

namespace NippoControlSystem.Domain.Services
{
    public interface IConditionTextConverter
    {
        string ConvertiDhiDb(string contents);
        string ConvertiDbiDh(string contents);
    }

    // 純粋な文字列置換ロジック（ファイルI/Oに依存しないため単体テストが非常に容易）
    public class ConditionTextConverter : IConditionTextConverter
    {
        public string ConvertiDhiDb(string contents)
        {
            if (string.IsNullOrEmpty(contents)) return null;

            if (Regex.IsMatch(contents, "(iDh|nDh)"))
            {
                contents = Regex.Replace(contents, "iDh", "iDb");
                contents = Regex.Replace(contents, "nDh", "nDb");
                return contents;
            }
            return null;
        }

        public string ConvertiDbiDh(string contents)
        {
            if (string.IsNullOrEmpty(contents)) return null;

            if (Regex.IsMatch(contents, "(iDb|nDb)"))
            {
                contents = Regex.Replace(contents, "iDb", "iDh");
                contents = Regex.Replace(contents, "nDb", "nDh");
                return contents;
            }
            return null;
        }
    }
}