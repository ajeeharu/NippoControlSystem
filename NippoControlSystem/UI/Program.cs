using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NippoControlSystem.ApplicationService.Configurations;
using NippoControlSystem.ApplicationService.Dtos;
using NippoControlSystem.ApplicationService.Interfaces;
using NippoControlSystem.ApplicationService.Services;
using NippoControlSystem.Domain.Interfaces;
using NippoControlSystem.Infrastructure.Devices;
using NippoControlSystem.Infrastructure.NativeLibs;
using NippoControlSystem.Infrastructure.Win32;
using NippoControlSystem.UI.Views;
using System.Runtime.Versioning;
using System.Text;

namespace NippoControlSystem.UI
{
    [SupportedOSPlatform("windows")]
    static class Program
    {
        // Mutexオブジェクトを保持するフィールド
        private static System.Threading.Mutex? _mutex;
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

                // 1. HostBuilder を生成
                var builder = Host.CreateApplicationBuilder(args);

                // 設定ファイルの読み込み（JSONファイルの出力先パスに合わせて調整してください）
                builder.Configuration
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("ApplicationService/Configurations/General.Settings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("ApplicationService/Configurations/Inspection.Settings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("ApplicationService/Configurations/Hardware.Settings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("ApplicationService/Configurations/Peripheral.Settings.json", optional: true, reloadOnChange: true);

                // 2. ログの設定
                builder.Logging.ClearProviders();
                builder.Logging.AddConsole();
                builder.Logging.AddDebug();
                // 【追加】独自のログ設定クラス等へレベルを反映させる場合
                // （※LogクラスやLog.LogLevelが実際のコードの定義に合っているかご確認ください）
                builder.Logging.AddFilter("Microsoft", LogLevel.Warning); // Microsoft系のログはWarning以上のみ
                builder.Logging.AddFilter("NippoControlSystem", LogLevel.Debug); // 自作アプリのログはDebugまで許可

                // 3. 設定オブジェクトを Options パターンで登録
                builder.Services.Configure<GeneralSettings>(builder.Configuration.GetSection("General"));
                builder.Services.Configure<InspectionSettings>(builder.Configuration.GetSection("Inspection"));
                builder.Services.Configure<HardwareSettings>(builder.Configuration.GetSection("Hardware"));
                builder.Services.Configure<PeripheralSettings>(builder.Configuration.GetSection("Peripheral"));

                // 直接インスタンスが必要なクラス向けに、IOptions<T>.Value 経由で注入できるよう登録することも可能
                builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<GeneralSettings>>().Value);
                builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<InspectionSettings>>().Value);
                builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<HardwareSettings>>().Value);
                builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<SharedMenuInfoDto>>().Value);
                builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<PeripheralSettings>>().Value);

                // 4. DI サービスの登録
                ConfigureServices(builder.Services);

                using var host = builder.Build();
                var mainForm = host.Services.GetRequiredService<OpeningView>();
                Application.Run(mainForm);
            }
            finally
            {
                if (_mutex != null)
                {
                    if (createdNew) // 自身が所有権を獲得していた場合のみ解放
                    {
                        _mutex.ReleaseMutex();
                    }
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

            // Win32関連サービスの登録
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<IProcessManager, ProcessManagerService>();

            // MeasureCondition関連
            //services.AddTransient<IConditionTextConverter, ConditionTextConverter>();
            //services.AddTransient<IMeasureConditionRepository, CsvMeasureConditionRepository>();
            services.AddTransient<MeasureConditionService>();
            services.AddSingleton<IDialogService, MessageBoxDialogService>();

            // UI および共通機能（AppLogger含む）の一括登録
            services.AddApplicationServices();
        }
        /// <summary>
        ///  ハードウェアの初期化処理
        /// </summary>
        private static void ExecutePresetMode()
        {
            try
            {
                var services = new ServiceCollection();

                // 1. 必要であれば、この場で設定ファイルを読み込む
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("ApplicationService/Configurations/Hardware.Settings.json", optional: true)
                    .Build();

                var hardwareSettings = configuration.GetSection("Hardware").Get<HardwareSettings>();
                services.Configure<HardwareSettings>(configuration.GetSection("Hardware"));

                // 2. LEDの初期化処理に必要なClassのDIコンテナを登録
                services.AddSingleton<IConfiguration>(configuration);
                services.AddSingleton<ICaioDevice, CaioDevice>();
                services.AddSingleton<AnalogIO>();
                services.AddSingleton<ICdioDevice, CdioDevice>();
                services.AddSingleton<Ai2Di>();
                services.AddSingleton<IOperationPanelService, OperationPanelService>();

                // 3. プロバイダをビルド
                using var serviceProvider = services.BuildServiceProvider();
                // 4. LED関連の初期化を実行

                IOperationPanelService _operationPanel = new OperationPanelService();
                _operationPanel.SetInspectionLedControl(LedOnOff.LedOff);
                _operationPanel.SetGreenLedControl(LedOnOff.LedOff);
                _operationPanel.SetRedLedControl(LedOnOff.LedOff);
                _operationPanel.SetPowerSwitch12VoltControl();

                MessageBox.Show("ハードウェアの初期化を実行しました。", "プリセットモード", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ハードウェアの初期化に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

