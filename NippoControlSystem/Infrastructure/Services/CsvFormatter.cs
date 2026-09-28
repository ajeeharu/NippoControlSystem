namespace NippoControlSystem.Infrastructure.Formatters
{
    public static class CsvFormatter
    {
        public static string EncloseDoubleQuotes(string field)
        {
            if (string.IsNullOrEmpty(field)) return "\"\"";
            if (field.IndexOf('"') > -1) field = field.Replace("\"", "\"\"");
            return "\"" + field + "\"";
        }

        public static string EncloseDoubleQuotesIfNeed(string field)
        {
            if (string.IsNullOrEmpty(field)) return "";
            return NeedEncloseDoubleQuotes(field) ? EncloseDoubleQuotes(field) : field;
        }

        private static bool NeedEncloseDoubleQuotes(string field)
        {
            return field.IndexOf('"') > -1 ||
                   field.IndexOf(',') > -1 ||
                   field.IndexOf('\r') > -1 ||
                   field.IndexOf('\n') > -1 ||
                   field.StartsWith(" ") || field.StartsWith("\t") ||
                   field.EndsWith(" ") || field.EndsWith("\t");
        }
    }
}