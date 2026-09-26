using NippoControlSystem.Infrastructure.Configuration;
using NippoControlSystem.UI.ViewModels;

namespace NippoControlSystem.UI.Views
{
    public partial class OpeningView : Form
    {
        private readonly OpeningViewModel _viewModel;

        // DIコンテナ経由で ViewModel を受け取る
        public OpeningView(OpeningViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _viewModel.CurrentView = this;       // ViewModel への CurrentView 設定
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
        }

        /// <summary>
        /// 「検査」ボタンクリック時の処理を行います。
        /// </summary>
        private void ExecuteInspection_Click(object sender, EventArgs e)
        {
            SyncSelectedValuesToViewModel();

            if (_viewModel.OpenMainViewCommand.CanExecute(null))
            {
                _viewModel.OpenMainViewCommand.Execute(null);
            }
        }
        /// <summary>
        /// 「検査定義の編集」ボタンクリック時の処理を行います。
        /// </summary>
        private void EditSetting_Click(object sender, EventArgs e)
        {
            SyncSelectedValuesToViewModel();

            if (_viewModel.OpenSettingViewCommand.CanExecute(null))
            {
                _viewModel.OpenSettingViewCommand.Execute(null);
            }
        }
        /// <summary>
        /// 「この画面の編集」ボタンクリック時の処理を行います。
        /// </summary>
        private void EditTopView_Click(object sender, EventArgs e)
        {
            SyncSelectedValuesToViewModel();

            if (_viewModel.OpenTopEditViewCommand.CanExecute(null))
            {
                _viewModel.OpenTopEditViewCommand.Execute(null);
            }
        }
        /// <summary>
        /// 「検査履歴」ボタンクリック時の処理を行います。
        /// </summary>
        private void OpenHistory_Click(object sender, EventArgs e)
        {
            SyncSelectedValuesToViewModel();

            if (_viewModel.OpenHistoryViewCommand.CanExecute(null))
            {
                _viewModel.OpenHistoryViewCommand.Execute(null);
            }
        }
        /// <summary>
        /// バージョン表示ラベルクリック時の処理を行います。
        /// </summary>
        private void OpenVersion_Click(object sender, EventArgs e)
        {
            if (_viewModel.OpenVersionViewCommand.CanExecute(null))
            {
                _viewModel.OpenVersionViewCommand.Execute(null);
            }
        }

        /// <summary>
        /// リストボックスのダブルクリック時に検査処理を実行します。
        /// </summary>
        private void ExecuteInspection_OnSubNoDoubleClick(object sender, EventArgs e)
        {
            ExecuteInspection_Click(sender, e);
        }
        /// <summary>
        /// メイン番号の選択変更時に ViewModel へ選択値を同期します。
        /// </summary>
        private void SyncMainNoSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            SyncSelectedValuesToViewModel();
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
        /// 「終了」ボタンクリック時に画面を閉じます。
        /// </summary>
        private void CloseScreen_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 「マニュアル表示」ボタンクリック時の処理を行います。
        /// </summary>
        private void button_ViewManual_Click(object sender, EventArgs e)
        {
            _viewModel.OpenManual();
        }
        /// <summary>
        /// フィルター用テキスト変更時に ViewModel へ処理を委譲し、メニューの絞り込みと再描画を行います。
        /// </summary>
        private void FilterMenu_TextChanged(object sender, EventArgs e)
        {
            // 1. 入力値を ViewModel へ反映
            _viewModel.FilterText = this.textBox_Filter.Text;

            // 2. ViewModel 側のロジックで BindingSource をフィルタリング
            _viewModel.ApplyMenuFilter(this.menuMainBindingSource);

            // 3. 表示コントロールの再描画
            this.listBox_MainNo.Refresh();
            this.listBox_SubNo.Refresh();
        }
        /// <summary>
        /// 「アナログモニター」ボタンクリック時にアナログ入出力モニターを起動します。
        /// </summary>
        private void LaunchAnalogAioMonitor_Click(object sender, EventArgs e)
        {
            // リストで選択された SubID の値を取得して ViewModel 側の起動ロジックを実行
            object selectedSubId = this.listBox_SubNo.SelectedValue;

            bool isSuccess = _viewModel.LaunchAnalogAioMonitor(selectedSubId);

            if (!isSuccess)
            {
                // Properties.Settings.Default を使用するか、直接文字列を指定
                System.Windows.Forms.MessageBox.Show("選択されたSubIDが見つかりません。", Settings.ApplicationName);
                this.Close();
            }
        }
        /// <summary>
        /// View 上の ListBox 選択値を ViewModel のプロパティへ同期します
        /// </summary>
        private void SyncSelectedValuesToViewModel()
        {
            //_viewModel.SelectedMainTitle = listBox_MainNo.Text?.ToString();
            //_viewModel.SelectedSubTitle = listBox_SubNo.Text?.ToString();
            //_viewModel.SelectedSubId = listBox_SubNo.SelectedValue?.ToString();
            //_viewModel.DataSetTopMenu = mc.DataSetTopMenu;
            //_viewModel.SelectedMainIndex = listBox_MainNo.SelectedIndex;
            //_viewModel.SelectedSubIndex = listBox_SubNo.SelectedIndex;
        }
    }
}