using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NippoControlSystem.ApplicationService.Interfaces;
using NippoControlSystem.ApplicationService.Services;
using NippoControlSystem.Domain.Repositories;
using NippoControlSystem.Domain.Services;
using NippoControlSystem.Infrastructure.Repositories;
using NippoControlSystem.Infrastructure.Win32;
using NippoControlSystem.UI.Views;
using System.Runtime.Versioning;
using System.Text;

namespace NippoControlSystem.UI
{
    static class Program
    {
        // Mutexオブジェクトを保持するフィールド
        private static System.Threading.Mutex _mutex;
        private static readonly string MutexName = "Global\\MyUniqueApp_GUID_Here";

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        [SupportedOSPlatform("windows")]
        static void Main(string[] args)
        {
            bool createdNew;
            _mutex = new System.Threading.Mutex(true, MutexName, out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("すでに起動しています！", "二重起動エラー",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (args.Length == 1 && (args[0] == "PRESET_ADO" || args[0] == "PRESET"))
                {
                    ExecutePresetMode();
                    return;
                }

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Application.SetHighDpiMode(HighDpiMode.DpiUnaware);
                Application.SetCompatibleTextRenderingDefault(false);
                Application.EnableVisualStyles();

                // 1. HostBuilder を生成して IHost をビルド
                var builder = Host.CreateApplicationBuilder(args);

                // 2. ログの設定
                builder.Logging.ClearProviders();
                builder.Logging.AddConsole();
                builder.Logging.AddDebug();

                // 3. DI サービスの登録
                ConfigureServices(builder.Services);

                // 4. Host の初期化
                using var host = builder.Build();

                // 5. Host から OpeningView を取得してアプリを起動
                var mainForm = host.Services.GetRequiredService<OpeningView>();
                Application.Run(mainForm);
            }
            finally
            {
                if (_mutex != null)
                {
                    _mutex.ReleaseMutex();
                    _mutex.Dispose();
                }
            }
        }
        /// <summary>
        /// DIサービスの登録
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureServices(IServiceCollection services)
        {
            var config = new ConfigurationLoader();

            services.AddSingleton(config.ai2DiSettings);
            services.AddSingleton(config.dioSettings);
            services.AddSingleton(config.aioSettings);
            services.AddSingleton(config.appInfoSettings);
            services.AddSingleton(config.inspectionSettings);
            services.AddSingleton(config.uiSettings);

            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<IProcessManager, ProcessManagerService>();

            // MeasureCondition関連
            services.AddSingleton<IConditionTextConverter, ConditionTextConverter>();
            services.AddTransient<IMeasureConditionRepository, CsvMeasureConditionRepository>();
            services.AddTransient<MeasureConditionService>();

            // UI および共通機能（AppLogger含む）の一括登録
            services.AddApplicationServices();
        }
        /// <summary>
        ///  ハードウェアの初期化処理
        /// </summary>
        private static void ExecutePresetMode()
        {
            MessageBox.Show("ハードウェアの初期化を実行しました。", "プリセットモード",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

