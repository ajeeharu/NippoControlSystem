using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Windows.Forms;

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
            Application.Run(new Views.OpeningView());
        }
    }
}


