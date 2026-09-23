using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NippoControlSystem.UI.ViewModels
{
    /// <summary>
    /// バージョン情報画面（VersionView）用 ViewModel
    /// </summary>
    public class VersionViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _windowTitle;
        private string _companyName;
        private string _productName;
        private string _versionText;
        private string _copyright;
        private string _description;
        #endregion

        #region Properties (View Binding Targets)
        /// <summary>
        /// 画面タイトル (例: "〇〇 のバージョン情報")
        /// </summary>
        public string WindowTitle
        {
            get => _windowTitle;
            private set { if (_windowTitle != value) { _windowTitle = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 会社名
        /// </summary>
        public string CompanyName
        {
            get => _companyName;
            private set { if (_companyName != value) { _companyName = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 製品名
        /// </summary>
        public string ProductName
        {
            get => _productName;
            private set { if (_productName != value) { _productName = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// バージョン表示文字列 (例: "Version 1.0.8320.12345 2022/10/15 14:30:00")
        /// </summary>
        public string VersionText
        {
            get => _versionText;
            private set { if (_versionText != value) { _versionText = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 著作権情報
        /// </summary>
        public string Copyright
        {
            get => _copyright;
            private set { if (_copyright != value) { _copyright = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 詳細説明
        /// </summary>
        public string Description
        {
            get => _description;
            private set { if (_description != value) { _description = value; OnPropertyChanged(); } }
        }
        #endregion

        #region Events / Delegates
        /// <summary>
        /// 画面クローズ要求
        /// </summary>
        public event Action RequestCloseView;
        #endregion

        #region Constructors
        public VersionViewModel()
        {
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Form_Load 時のアセンブリ情報取得・データ初期化処理
        /// </summary>
        public void InitializeData()
        {
            Assembly mainAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

            // 製品名
            string appProductName = GetAssemblyAttribute<AssemblyProductAttribute>(mainAssembly)?.Product
                                    ?? System.Windows.Forms.Application.ProductName;

            // 会社名
            string appCompanyName = GetAssemblyAttribute<AssemblyCompanyAttribute>(mainAssembly)?.Company
                                    ?? System.Windows.Forms.Application.CompanyName;

            // バージョン番号
            string appVersion = System.Windows.Forms.Application.ProductVersion;

            // コピーライト
            string appCopyright = GetAssemblyAttribute<AssemblyCopyrightAttribute>(mainAssembly)?.Copyright ?? "-";

            // 詳細情報
            string appDescription = GetAssemblyAttribute<AssemblyDescriptionAttribute>(mainAssembly)?.Description ?? "-";

            // ビルド日時の計算 (2000年1月1日からの経過日数と経過秒数)
            string buildDateTimeText = FormatBuildDateTime(appVersion);

            // Property更新
            WindowTitle = $"{appProductName} のバージョン情報";
            CompanyName = appCompanyName;
            ProductName = appProductName;
            VersionText = $"Version  {appVersion}   {buildDateTimeText}";
            Copyright = appCopyright;
            Description = appDescription;
        }
        #endregion

        #region Operations & Logic Commands
        /// <summary>
        /// OKボタン押下時（画面を閉じる）
        /// </summary>
        public void Close()
        {
            RequestCloseView?.Invoke();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// アセンブリのカスタム属性を取得するヘルパーメソッド
        /// </summary>
        private static T GetAssemblyAttribute<T>(Assembly assembly) where T : Attribute
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(T), false);
            if (attributes != null && attributes.Length > 0)
            {
                return (T)attributes[0];
            }
            return null;
        }

        /// <summary>
        /// バージョン番号 (Major.Minor.Build.Revision) からビルド日時を計算してフォーマット化
        /// </summary>
        private static string FormatBuildDateTime(string versionString)
        {
            try
            {
                string[] splittedVersion = versionString.Split('.');
                if (splittedVersion.Length >= 4)
                {
                    double days = double.Parse(splittedVersion[2]);
                    double seconds = double.Parse(splittedVersion[3]) * 2d;

                    DateTime buildDate = new DateTime(2000, 1, 1)
                                        + TimeSpan.FromDays(days)
                                        + TimeSpan.FromSeconds(seconds);

                    return buildDate.ToString("yyyy/MM/dd  HH:mm:ss");
                }
            }
            catch
            {
                // 解析エラー時は空文字を返す
            }

            return string.Empty;
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}