using NippoControlSystem.ApplicationService.Configurations;
using NippoControlSystem.ApplicationService.Dtos;
using NippoControlSystem.ApplicationService.Interfaces;
using NippoControlSystem.ApplicationService.Services;
using NippoControlSystem.UI.ViewModels;
using System.IO;
using System.Runtime.Versioning;

namespace NippoControlSystem.UI.Views
{
    [SupportedOSPlatform("windows")]
    public partial class OpeningView : Form
    {
        private readonly OpeningViewModel _viewModel;
        private readonly INavigationService _navigationService;
        private readonly MessageBoxDialogService _messageBoxDialogService;
        public OpeningView(
            OpeningViewModel viewModel,
            INavigationService navigationService,
            GeneralSettings appInfoSettings,
            MessageBoxDialogService messageBoxDialogService)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _navigationService = navigationService;
            _messageBoxDialogService = messageBoxDialogService;

            // --- ここは初期化時に1回だけ行えばOK ---
            listBox_MainNo.DisplayMember = "MainTitle";
            listBox_MainNo.ValueMember = "MainId";

            // リストの初期データを流し込む
            listBox_MainNo.DataSource = _viewModel.MenuDataBinding.MainMenus;
        }
        /// <summary>
        /// 画面ロード時に ViewModel の初期化処理を非同期で実行します。
        /// </summary>
        private async void Initialize_Load(object sender, EventArgs e)
        {
            // ViewModel の初期化コマンドを実行
            if (_viewModel.InitializeCommand.CanExecute(null))
            {
                await _viewModel.InitializeCommand.ExecuteAsync(null);
            }
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmOpenning"]);
            Views.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmOpenning"]);

            //CurrentDir,フォルダの指定で、絶対パスでないときエラーにするため、SystemRootに移動、\sssは、C:\sssに作られる20161027
            System.IO.Directory.SetCurrentDirectory(Environment.GetEnvironmentVariable("SystemRoot") ?? string.Empty);      //"C:\\WINDOWS"
            lblVersion.Text = string.Format("Ver {0}", Application.ProductVersion);

            //ここからはFormの更新
            lblMainNo.Text = _viewModel.GeneralSettings.MainNo;   //"仕様書番号";
            lblSubNo.Text = _viewModel.GeneralSettings.SubNo;     //"追番";

            //データを表示
            menuMainBindingSource.DataSource = _viewModel.MenuDataBinding;
            menuMainBindingSource.Filter = null;

            //時間のかかる初期化処理を起動
            timerInitalUpdate.Enabled = true;
        }
        /// <summary>
        /// 「終了」ボタンクリック時に画面を閉じます。
        /// </summary>
        private void CloseScreen_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// バージョン表示ラベルクリック時の処理を行います。
        /// </summary>
        private void OpenVersion_Click(object sender, EventArgs e)
        {
            // 1. 画面遷移（パラメータ渡しは不要）
            _navigationService.NavigateTo<VersionView>(isModal: true);
        }
        /// <summary>
        /// 「マニュアル表示」ボタンクリック時の処理を行います。
        /// </summary>
        private void OpenManual_Click(object sender, EventArgs e)
        {
            //string filePath = @"D:\ogura\工番\P16010063_配線チェッカー\制御\Project\wChecker\Manual.pdf";
            string filePath = _viewModel.GeneralSettings.ApplicationFolder + _viewModel.GeneralSettings.ManualPath;
            string option = "";

            try
            {
                // コマンドライン引数有りで実行
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(filePath, option);
            }
            catch (Exception ee)
            {
                _messageBoxDialogService.ShowMessage(string.Format("操作マニュアルを表示できません。\n\n表示ファイル：{0}\n理由：{1}", filePath, ee.Message), _viewModel.GeneralSettings.ApplicationName);
            }
        }
        /// <summary>
        /// 「検査定義の編集」ボタンクリック時の処理を行います。
        /// </summary>
        private void EditSetting_Click(object sender, EventArgs e)
        {
            // 1. フォルダの存在チェックと AppState への保存を実行
            if (!_viewModel.ValidateAndPrepareForEditSettingsView()) return;

            // 2. 検証OKなら画面遷移（パラメータ渡しは不要）
            _navigationService.NavigateAndHideOwner<SettingView>(this);
        }
        /// <summary>
        /// 「この画面の編集」ボタンクリック時の処理を行います。
        /// </summary>
        private void EditTopView_Click(object sender, EventArgs e)
        {
            // 1. フォルダの存在チェックと AppState への保存を実行
            if (!_viewModel.ValidateAndPrepareFolders()) return;

            // 2. 検証OKなら画面遷移（パラメータ渡しは不要）
            _navigationService.NavigateAndHideOwner<TopEditView>(this);

        }
        /// <summary>
        /// 「検査」ボタンクリック時の処理を行います。
        /// </summary>
        private void ExecuteInspection_Click(object sender, EventArgs e)
        {
            // 1. フォルダの存在チェックと AppState への保存を実行
            if (!_viewModel.ValidateAndPrepareFolders()) return;

            // 2. 検証OKなら画面遷移（パラメータ渡しは不要）
            _navigationService.NavigateAndHideOwner<InspectionMainView>(this);
        }
        // メインメニューの選択が変更されたときのイベント
        private void listBox_MainNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_MainNo.SelectedItem is BindMainMenuDto selectedMain)
            {
                // 1. サブメニューのリストをオブジェクトのままバインド
                listBox_SubNo.DataSource = selectedMain.SubMenus;
                listBox_SubNo.DisplayMember = "SubTitle";
                listBox_SubNo.ValueMember = "SubId";

                // 2. ViewModelへ「選択インデックス」と「メインメニューのID」を正しく渡す
                _viewModel.UpdateMainMenuSelection(listBox_MainNo.SelectedIndex, selectedMain.MainId);
            }
            else
            {
                // 選択解除時の処理
                listBox_SubNo.DataSource = null;
                _viewModel.UpdateMainMenuSelection(BindMainMenuDto.Unselected, BindMainMenuDto.Unselected);
            }
        }

        // サブメニューの選択が変更されたときのイベント
        private void listBox_SubNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ★ ListBox から「BindSubMenuDto オブジェクト」を直接取得する
            if (listBox_SubNo.SelectedItem is BindSubMenuDto selectedSub)
            {
                // ① 親のIDを正しいコントロールから取得する（例: listBox_MainNo から選択中のメインIDを取るなど）
                // ※もし親が別コントロールなら、それに合わせたプロパティ（SelectedValueなど）を指定します
                if (listBox_MainNo.SelectedValue is int mainId)
                {
                    // ② サブ側の選択インデックスを正しく取得する
                    int selectedSubIndex = listBox_SubNo.SelectedIndex;

                    // ViewModel の状態をまとめて更新
                    _viewModel.UpdateSubMenuSelection(mainId, selectedSubIndex, selectedSub);
                }
            }
            else
            {
                // 選択解除（未選択）状態になったときの処理
                if (listBox_MainNo.SelectedValue is int mainId)
                {
                    _viewModel.UpdateSubMenuSelection(mainId, BindSubMenuDto.Unselected, null);
                }
            }
        }
        /// <summary>
        /// フィルター用テキスト変更時に ViewModel へ処理を委譲し、メニューの絞り込みと再描画を行います。
        /// </summary>
        private void FilterMenu_TextChanged(object sender, EventArgs e)
        {
            // 1. 入力値を ViewModel へ反映
            _viewModel.FilterText = textBox_Filter.Text;

            // 2. ViewModel 側のロジックで BindingSource をフィルタリング
            _viewModel.ApplyMenuFilter(menuMainBindingSource);

            // 3. 表示コントロールの再描画
            listBox_MainNo.Refresh();
            listBox_SubNo.Refresh();
        }
        /// <summary>
        /// 「アナログモニター」ボタンクリック時にアナログ入出力モニターを起動します。
        /// </summary>
        private void LaunchAnalogAioMonitor_Click(object sender, EventArgs e)
        {
            // リストで選択された SubID の値を取得して ViewModel 側の起動ロジックを実行
            object? selectedSubId = listBox_SubNo.SelectedValue;

            bool isSuccess = _viewModel.LaunchAnalogAioMonitor(selectedSubId);

            if (!isSuccess)
            {
                // Properties.Settings.Default を使用するか、直接文字列を指定
                _messageBoxDialogService.ShowMessage(string.Format("選択されたSubIDが見つかりません。{0}", _viewModel.GeneralSettings.ApplicationName));
                Close();
            }
        }
        /// <summary>
        /// 画面終了（フォームクローズ）時の処理を行います。
        /// </summary>
        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            // 処理を ViewModel へ委譲
            _viewModel.CleanupResources();
        }
        /// <summary>
        /// 画面が閉じられる直前の処理およびログ出力を行います。
        /// </summary>
        public void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OpenViewClosing");
        }
        /// <summary>
        /// 「検査履歴」ボタンクリック時の処理を行います。
        /// </summary>
        private void OpenHistory_Click(object sender, EventArgs e)
        {
            // 1. フォルダの存在チェックと AppState への保存を実行
            if (!_viewModel.ValidateAndPrepareFolders()) return;
            string? historyFile = _navigationService.ShowOpenFileDialog(_viewModel.GeneralSettings.HistViewFolder
                , "検査結果ファイル(A*.csv)|A*.csv|すべてのファイル(*.*)|*.*");
            if (string.IsNullOrEmpty(historyFile) || !File.Exists(historyFile)) return;

            // 2. 検証OKなら画面遷移（パラメータ渡しは不要）
            _navigationService.NavigateAndHideOwner<TestHistoryView>(this);
        }
        /// <summary>
        /// リストボックスのダブルクリック時に検査処理を実行します。
        /// </summary>
        private void ExecuteInspection_OnSubNoDoubleClick(object sender, EventArgs e)
        {
            ExecuteInspection_Click(sender, e);
        }
    }
}
