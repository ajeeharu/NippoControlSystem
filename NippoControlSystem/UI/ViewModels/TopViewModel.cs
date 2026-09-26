using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace NippoControlSystem.UI.ViewModels
{
    public class TopViewModel
    {
        // 画面に表示するためのバージョン文字列プロパティ
        public string AppVersion { get; }

        public TopViewModel()
        {
            // アセンブリ（.exe）からバージョン情報を取得して整形
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            // メジャー.マイナー.ビルド番号 の形式に整形 (例: "Ver. 1.0.0")
            AppVersion = $"Ver. {version?.Major}.{version?.Minor}.{version?.Build}";
        }
    }
}