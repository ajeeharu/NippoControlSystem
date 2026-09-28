using Microsoft.Extensions.Configuration;
using NippoControlSystem.ApplicationService.Confiigurations;
using NippoControlSystem.Infrastructure.Confiigurations;
using NippoControlSystem.UI.Confiigurations;
using System.IO;

namespace NippoControlSystem.UI
{
    public class ConfigurationLoader
    {
        public UiSettings uiSettings { get; }
        public AppInfoSettings appInfoSettings { get; }
        public InspectionSettings inspectionSettings { get; }
        public DioSettings dioSettings { get; }
        public AioSettings aioSettings { get; }
        public Ai2DiSettings ai2DiSettings { get; }

        public ConfigurationLoader()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            // 複数レイヤのJSONを順次読み込む
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(Path.Combine("UI", "Configurations", "UI.settings.json"), optional: true, reloadOnChange: true)
                .AddJsonFile(Path.Combine("ApplicationService", "Configurations", "ApplicationService.settings.json"), optional: true, reloadOnChange: true)
                .AddJsonFile(Path.Combine("Infrastructure", "Configurations", "infrastructure.settings.json"), optional: true, reloadOnChange: true)
                .Build();

            // セクションから各POCOクラスへバインド
            uiSettings = config.GetSection("Ui").Get<UiSettings>() ?? new UiSettings();
            appInfoSettings = config.GetSection("AppInfo").Get<AppInfoSettings>() ?? new AppInfoSettings();
            inspectionSettings = config.GetSection("Inspection").Get<InspectionSettings>() ?? new InspectionSettings();
            dioSettings = config.GetSection("Dio").Get<DioSettings>() ?? new DioSettings();
            aioSettings = config.GetSection("Aio").Get<AioSettings>() ?? new AioSettings();
            ai2DiSettings = config.GetSection("Ai2Di").Get<Ai2DiSettings>() ?? new Ai2DiSettings();

            // 実行時パスをアタッチ
            appInfoSettings.ApplicationFolder = basePath;
        }
    }
}