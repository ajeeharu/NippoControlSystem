using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace NippoControlSystem.UI.ViewModels
{
    /// <summary>
    /// 新規・編集入力画面（TopEditInputView）用 ViewModel
    /// </summary>
    public class TopEditInputViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly Cyc.IO.Settings _defaultSettings = Cyc.IO.Settings.GetInstance();

        private string _lblMainNo;
        private string _lblSubNo;

        private string _mainTitle;
        private string _subTitle;
        private string _folder;
        #endregion

        #region Properties (View Binding Targets)
        public string LblMainNo
        {
            get => _lblMainNo;
            private set { if (_lblMainNo != value) { _lblMainNo = value; OnPropertyChanged(); } }
        }

        public string LblSubNo
        {
            get => _lblSubNo;
            private set { if (_lblSubNo != value) { _lblSubNo = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 仕様書番号（図番）
        /// </summary>
        public string MainTitle
        {
            get => _mainTitle;
            set { if (_mainTitle != value) { _mainTitle = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 追番（枝番）
        /// </summary>
        public string SubTitle
        {
            get => _subTitle;
            set { if (_subTitle != value) { _subTitle = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// フォルダパス
        /// </summary>
        public string Folder
        {
            get => _folder;
            set { if (_folder != value) { _folder = value; OnPropertyChanged(); } }
        }
        #endregion

        #region Events / Delegates
        /// <summary>
        /// ダイアログ表示要求 (メッセージ, タイトル, ボタンタイプ, 結果コールバック)
        /// </summary>
        public event Action<string, string, MessageBoxButtons, Action<DialogResult>> RequestShowDialog;

        /// <summary>
        /// フォルダ参照ダイアログ表示要求 (初期パス, 結果パスコールバック)
        /// </summary>
        public event Action<string, Action<string>> RequestSelectFolder;

        /// <summary>
        /// 画面を閉じる要求（DialogResultを伴う）
        /// </summary>
        public event Action<DialogResult> RequestCloseView;
        #endregion

        #region Constructors
        public TopEditInputViewModel()
        {
        }

        public TopEditInputViewModel(string mainTitle, string subTitle, string folder)
        {
            MainTitle = mainTitle;
            SubTitle = subTitle;
            Folder = folder;
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Form_Load 時の初期化処理
        /// </summary>
        public void InitializeData()
        {
            LblMainNo = _defaultSettings.MainNo;
            LblSubNo = _defaultSettings.SubNo;
        }
        #endregion

        #region Operations & Logic Commands

        #region Folder Selection
        /// <summary>
        /// フォルダ参照ダイアログを表示してパスを設定する
        /// </summary>
        public void BrowseFolder()
        {
            string initialPath = Directory.Exists(_defaultSettings.DataFolder) ? _defaultSettings.DataFolder : string.Empty;

            RequestSelectFolder?.Invoke(initialPath, selectedPath =>
            {
                if (string.IsNullOrEmpty(selectedPath)) return;

                Folder = selectedPath;

                // フォルダ選択時、仕様書番号が空であれば親フォルダ名を補完設定
                if (string.IsNullOrEmpty(MainTitle))
                {
                    try
                    {
                        string parentDir = Path.GetDirectoryName(selectedPath);
                        if (!string.IsNullOrEmpty(parentDir))
                        {
                            MainTitle = Path.GetFileName(parentDir);
                        }
                    }
                    catch
                    {
                        // 例外発生時は何もしない
                    }
                }

                // フォルダ選択時、追番が空であれば選択されたフォルダ名を補完設定
                if (string.IsNullOrEmpty(SubTitle))
                {
                    try
                    {
                        SubTitle = Path.GetFileName(selectedPath);
                    }
                    catch
                    {
                        // 例外発生時は何もしない
                    }
                }
            });
        }
        #endregion

        #region Save / Confirm Logic
        /// <summary>
        /// 決定（OK）ボタン押下時の処理
        /// </summary>
        public void ConfirmInput()
        {
            if (string.IsNullOrEmpty(Folder))
            {
                // フォルダパス未入力の場合はそのまま確定して終了
                RequestCloseView?.Invoke(DialogResult.OK);
                return;
            }

            if (!Directory.Exists(Folder))
            {
                RequestShowDialog?.Invoke(
                    "フォルダが見つかりません。\n\n作成しますか？",
                    _defaultSettings.ApplicationName,
                    MessageBoxButtons.YesNo,
                    result =>
                    {
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                Directory.CreateDirectory(Folder);
                                RequestCloseView?.Invoke(DialogResult.OK);
                            }
                            catch
                            {
                                RequestShowDialog?.Invoke(
                                    "フォルダが作成できません。\n\n別のフォルダを指定して下さい。",
                                    _defaultSettings.ApplicationName,
                                    MessageBoxButtons.OK,
                                    null
                                );
                            }
                        }
                    }
                );
            }
            else
            {
                RequestCloseView?.Invoke(DialogResult.OK);
            }
        }
        #endregion

        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}