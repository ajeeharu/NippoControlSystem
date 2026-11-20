using System.Text.RegularExpressions;

namespace NippoControlSystem.Domain.Utiities
{
    // 純粋な文字列置換ロジック（ファイルI/Oに依存しないため単体テストが非常に容易）
    public static class ConditionTextConverter
    {
        public static string? ConvertiDhiDb(this string? contents)
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

        public static string? ConvertiDbiDh(this string? contents)
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