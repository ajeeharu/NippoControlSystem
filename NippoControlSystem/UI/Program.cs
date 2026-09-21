using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using NippoControlSystem.ApplicationService.Interfaces;
using NippoControlSystem.ApplicationService.Services;
using NippoControlSystem.UI.ViewModels;
using NippoControlSystem.UI.Views;
using System.Runtime.Versioning;
using System.Text;

namespace NippoControlSystem.UI
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        [SupportedOSPlatform("windows")]
        static void Main()
        {
            // エンコーディングプロバイダーを登録（これを出力・変換処理を行う前に実行する）
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // 以降、Shift_JIS が利用可能になります
            //Encoding shiftJis = Encoding.GetEncoding("Shift_JIS");
            // 自動生成された高 DPI 設定を無効化し、Unaware（非対応）にする
            Application.SetHighDpiMode(HighDpiMode.DpiUnaware);     
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();

            // DI サービスコレクションの設定
            var services = new ServiceCollection();

            // サービスの登録
            services.AddSingleton<INavigationService, NavigationService>();

            // ViewModel と View の登録
            services.AddTransient<OpeningViewModel>();
            services.AddTransient<OpeningView>();

            using var provider = services.BuildServiceProvider();
            // OpeningView を DI 経由で取得して起動
            var mainForm = provider.GetRequiredService<OpeningView>();
            Application.Run(mainForm);
        }
    }
}


