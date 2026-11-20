namespace NippoControlSystem.Domain.Utilities
{
    /// <summary>
    /// CSV用の文字列加工ルールを分離するための拡張メソッド（またはヘルパー）
    /// </summary>
    public static class CsvStringExtensions
    {
        public static string EncloseDoubleQuotes(this string? field)
        {
            field ??= string.Empty;
            if (field.IndexOf('"') > -1)
            {
                field = field.Replace("\"", "\"\"");
            }
            return "\"" + field + "\"";
        }
    }
}
