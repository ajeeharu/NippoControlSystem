using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace NippoControlSystem.UI.ViewModels
{
    /// <summary>
    /// 設定複製画面（TopEditCopyView）用 ViewModel
    /// </summary>
    public class TopEditCopyViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly Cyc.IO.Settings _defaultSettings = Cyc.IO.Settings.GetInstance();
        private readonly MeasureCondition _mc = MeasureCondition.GetInstance();

        private string _lblMainNo;
        private string _lblSubNo;

        // 複製元データ
        private string _mainTitle;
        private string _subTitle;
        private string _folder;

        // 複製先データ
        private string _mainTitleCopyed;
        private string _subTitleCopyed;
        private string _folderCopyed;
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
        /// 複製元 仕様書番号（図番）
        /// </summary>
        public string MainTitle
        {
            get => _mainTitle;
            set { if (_mainTitle != value) { _mainTitle = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 複製元 追番（枝番）
        /// </summary>
        public string SubTitle
        {
            get => _subTitle;
            set { if (_subTitle != value) { _subTitle = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 複製元 フォルダパス
        /// </summary>
        public string Folder
        {
            get => _folder;
            set { if (_folder != value) { _folder = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 複製先 仕様書番号（図番）
        /// </summary>
        public string MainTitleCopyed
        {
            get => _mainTitleCopyed;
            set { if (_mainTitleCopyed != value) { _mainTitleCopyed = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 複製先 追番（枝番）
        /// </summary>
        public string SubTitleCopyed
        {
            get => _subTitleCopyed;
            set { if (_subTitleCopyed != value) { _subTitleCopyed = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 複製先 フォルダパス
        /// </summary>
        public string FolderCopyed
        {
            get => _folderCopyed;
            set { if (_folderCopyed != value) { _folderCopyed = value; OnPropertyChanged(); } }
        }
        #endregion

        #region Events / Delegates
        /// <summary>
        /// メッセージボックス表示要求
        /// </summary>
        public event Action<string, string, MessageBoxButtons, Action<DialogResult>> RequestShowDialog;

        /// <summary>
        /// フォルダ参照ダイアログ表示要求 (初期パス, コールバック)
        /// </summary>
        public event Action<string, Action<string>> RequestSelectFolder;

        /// <summary>
        /// 画面クローズ要求（DialogResultを伴う）
        /// </summary>
        public event Action<DialogResult> RequestCloseView;
        #endregion

        #region Constructors
        public TopEditCopyViewModel()
        {
        }

        public TopEditCopyViewModel(string mainTitle, string subTitle, string folder)
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

            // 初期値をセット（複製先図番は複製元と同じ値、追番とフォルダは空文字）
            MainTitleCopyed = MainTitle;
            SubTitleCopyed = string.Empty;
            FolderCopyed = string.Empty;
        }
        #endregion

        #region Operations & Logic Commands

        #region Folder Browser Command
        /// <summary>
        /// フォルダ選択ダイアログの起動リクエスト
        /// </summary>
        public void BrowseFolder()
        {
            string initialPath = Directory.Exists(_defaultSettings.DataFolder) ? _defaultSettings.DataFolder : string.Empty;

            RequestSelectFolder?.Invoke(initialPath, selectedPath =>
            {
                if (string.IsNullOrEmpty(selectedPath)) return;

                if (string.Equals(Folder, selectedPath, StringComparison.OrdinalIgnoreCase))
                {
                    ShowMessage("このフォルダは指定できません。\n\n別のフォルダを指定して下さい。");
                    return;
                }

                FolderCopyed = selectedPath;
            });
        }
        #endregion

        #region Execute Copy Operation
        /// <summary>
        /// コピー実行処理
        /// </summary>
        public void ExecuteCopy()
        {
            // 1. 各項目の入力チェック（バリデーション）
            if (string.IsNullOrEmpty(MainTitleCopyed))
            {
                ShowMessage("仕様書番号が指定されていません。\n\n仕様書番号を指定して下さい。");
                return;
            }

            if (string.IsNullOrEmpty(SubTitleCopyed))
            {
                ShowMessage("追番を入力して下さい。");
                return;
            }

            if (string.Equals(SubTitleCopyed, SubTitle, StringComparison.OrdinalIgnoreCase))
            {
                ShowMessage("複製元と違う追番を入力してください。");
                return;
            }

            if (string.IsNullOrEmpty(FolderCopyed))
            {
                ShowMessage("フォルダが指定されていません。\n\nフォルダを指定して下さい。");
                return;
            }

            // 2. フォルダパス検証と作成確認
            if (!Directory.Exists(FolderCopyed))
            {
                RequestShowDialog?.Invoke(
                    "フォルダが見つかりません。\n\n作成しますか？",
                    _defaultSettings.ApplicationName,
                    MessageBoxButtons.YesNo,
                    result =>
                    {
                        if (result != DialogResult.Yes) return;

                        try
                        {
                            Directory.CreateDirectory(FolderCopyed);
                            ProcessCopyFiles();
                        }
                        catch
                        {
                            ShowMessage("フォルダが作成できません。\n\n別のフォルダを指定して下さい。");
                        }
                    }
                );
                return;
            }

            if (string.Equals(Folder, FolderCopyed, StringComparison.OrdinalIgnoreCase))
            {
                ShowMessage("このフォルダは指定できません。\n\n別のフォルダを指定して下さい。");
                return;
            }

            // 既存フォルダへのコピー処理開始
            ProcessCopyFiles();
        }

        /// <summary>
        /// データファイル (list.dat, Check.dat, port.dat) のコピー処理
        /// </summary>
        private void ProcessCopyFiles()
        {
            // --- 検査ファイル (list.dat) のコピー ---
            string listSrc = Path.Combine(Folder, _defaultSettings.Listdat);
            string listDest = Path.Combine(FolderCopyed, _defaultSettings.Listdat);

            if (File.Exists(listSrc))
            {
                CopyFileWithOverwritePrompt(listSrc, listDest, "検査ファイルが既にあります。\n\n上書きしますか？", () =>
                {
                    // --- 設定ファイル (Checkdat.dat) のコピー/生成 ---
                    ProcessCheckdatCopy(() =>
                    {
                        // --- ポート名ファイル (port.dat) のコピー ---
                        ProcessPortdatCopy(() =>
                        {
                            // 完了時に複製後の値をセットして画面を閉じる
                            MainTitle = MainTitleCopyed;
                            SubTitle = SubTitleCopyed;
                            Folder = FolderCopyed;
                            RequestCloseView?.Invoke(DialogResult.OK);
                        });
                    });
                });
            }
            else
            {
                ShowMessage("複製元の検査ファイルが見つかりません。\n\nコピーできませんでした。");
            }
        }

        private void ProcessCheckdatCopy(Action onCompleted)
        {
            string checkSrc = Path.Combine(Folder, _defaultSettings.Checkdat);
            string checkDest = Path.Combine(FolderCopyed, _defaultSettings.Checkdat);

            if (File.Exists(checkSrc))
            {
                CopyFileWithOverwritePrompt(checkSrc, checkDest, "設定ファイルが既にあります。\n\n上書きしますか？", onCompleted);
            }
            else
            {
                if (File.Exists(checkDest))
                {
                    File.Delete(checkDest);
                }

                _mc.newCheckdatFile(checkDest, "未設定", MainTitleCopyed, SubTitleCopyed, "");
                ShowMessage("複製元の設定ファイルが見つかりません。\n\n新規に作成しました。");
                onCompleted?.Invoke();
            }
        }

        private void ProcessPortdatCopy(Action onCompleted)
        {
            string portSrc = Path.Combine(Folder, _defaultSettings.Portdat);
            string portDest = Path.Combine(FolderCopyed, _defaultSettings.Portdat);

            if (File.Exists(portSrc))
            {
                CopyFileWithOverwritePrompt(portSrc, portDest, "ポート名ファイルが既にあります。\n\n上書きしますか？", onCompleted);
            }
            else
            {
                ShowMessage("複製元のポート名ファイルが見つかりません。\n\nコピーできませんでした。");
                onCompleted?.Invoke();
            }
        }

        /// <summary>
        /// 上書き確認を考慮した汎用ファイルコピーヘルパー
        /// </summary>
        private void CopyFileWithOverwritePrompt(string srcPath, string destPath, string promptMessage, Action onSuccess)
        {
            if (File.Exists(destPath))
            {
                RequestShowDialog?.Invoke(
                    promptMessage,
                    _defaultSettings.ApplicationName,
                    MessageBoxButtons.YesNo,
                    result =>
                    {
                        if (result == DialogResult.Yes)
                        {
                            File.Delete(destPath);
                            File.Copy(srcPath, destPath);
                            onSuccess?.Invoke();
                        }
                    }
                );
            }
            else
            {
                File.Copy(srcPath, destPath);
                onSuccess?.Invoke();
            }
        }

        private void ShowMessage(string message)
        {
            RequestShowDialog?.Invoke(message, _defaultSettings.ApplicationName, MessageBoxButtons.OK, null);
        }
        #endregion

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