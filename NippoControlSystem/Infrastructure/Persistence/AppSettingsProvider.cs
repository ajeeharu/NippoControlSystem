namespace NippoControlSystem.Infrastructure.Persistence
{
    public static class AppSettingsProvider
    {
        public static string GetSettingPath()
        {
            // App.config からパスを取得するインフラ側の処理
            return System.Configuration.ConfigurationManager.AppSettings["Default.config Path"] ?? string.Empty;
        }
    }
}
