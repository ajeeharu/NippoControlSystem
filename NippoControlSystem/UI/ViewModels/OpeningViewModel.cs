using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NippoControlSystem.ApplicationService.Interfaces;
using NippoControlSystem.Infrastructure.Devices;
using NippoControlSystem.UI.Views;
using System.Data;
using System.IO;

namespace NippoControlSystem.UI.ViewModels
{
    public partial class OpeningViewModel(INavigationService navigationService, AnalogIO aio) : ObservableObject
    {
        private readonly INavigationService _navigationService = navigationService;
        private readonly AnalogIO _aio = aio;

        /// <summary>
        /// 親画面の参照（Hide/Show制御用）
        /// </summary>
        public Form? CurrentView { get; set; }
        public string? SelectedMainTitle { get; set; }
        public string? SelectedSubTitle { get; set; }
        public string? SelectedSubId { get; set; }
        public DataSet? DataSetTopMenu { get; set; }
        public int SelectedMainIndex { get; set; }
        public int SelectedSubIndex { get; set; }

        // --- 画面遷移のコマンド/メソッド ---

        [RelayCommand]
        public void OpenSettingView()
        {
            if (!ValidateAndGetFolder(out string? folder)) return;

            _navigationService.NavigateAndHideOwner<SettingView>(CurrentView, view =>
            {
                view.MainTitle = SelectedMainTitle;
                view.SubTitle = SelectedSubTitle;
                view.Folder = folder;
            });
        }

        [RelayCommand]
        public void OpenMainView()
        {
            if (!ValidateAndGetFolder(out string? folder)) return;

            _navigationService.NavigateAndHideOwner<MainView>(CurrentView, view =>
            {
                view.MainTitle = SelectedMainTitle;
                view.SubTitle = SelectedSubTitle;
                view.Folder = folder;
            });
        }

        [RelayCommand]
        public void OpenHistoryView()
        {
            if (!ValidateAndGetFolder(out string? folder)) return;

            string? historyFile = _navigationService.ShowOpenFileDialog(folder, "検査結果ファイル(A*.csv)|A*.csv|すべてのファイル(*.*)|*.*");
            if (string.IsNullOrEmpty(historyFile) || !File.Exists(historyFile)) return;

            _navigationService.NavigateAndHideOwner<TestHistoryView>(CurrentView, view =>
            {
                view.MainTitle = SelectedMainTitle;
                view.SubTitle = SelectedSubTitle;
                view.Folder = folder;
                view.historyFile = historyFile;
            });
        }

        [RelayCommand]
        public void OpenVersionView()
        {
            _navigationService.NavigateTo<VersionView>(isModal: true);
        }

        [RelayCommand]
        public void OpenTopEditView()
        {
            if (CurrentView == null) return;

            _navigationService.NavigateAndHideOwner<TopEditView>(CurrentView, view =>
            {
                view.myDataSetTopMenu = DataSetTopMenu as DataSetTopMenu;
                view.SelectedMainNo = SelectedMainIndex;
                view.SelectedSubNo = SelectedSubIndex;
            });
        }
        // ① 画面読み込み時に実行したい処理を [RelayCommand] 化する
        [RelayCommand]
        private async Task InitializeAsync()
        {
            // 画面ロード時に行いたい非同期処理（データ取得など）
            await LoadInitialDataFromDbAsync();
        }
        /// <summary>
        /// アプリケーション終了時のリソース解放および I/O デバイスの終了処理を行います。
        /// </summary>
        public void CleanupResources()
        {
            try
            {
                // ランプの消灯処理
                _aio.setInspctLamp(0);   // LED_OFF
                _aio.setGreenLamp(0);
                _aio.setRedLamp(0);
                _aio.SetPower12V();
            }
            catch (Exception ex)
            {
                // 必要に応じてログ出力
                System.Diagnostics.Debug.WriteLine($"ランプ消灯エラー: {ex.Message}");
            }
            finally
            {
                // I/O デバイスの終了・リソース解放
                _aio?.Exit();
                nio?.Close();
                dio?.Exit();
            }
        }

        /// <summary>
        /// 操作マニュアル（PDF 等）を外部プロセスで開きます。
        /// </summary>
        public void OpenManual()
        {
            string filePath = Default.ApplicationFloder + Default.ManualPath;
            string option = "";

            try
            {
                // 外部プロセスとしてマニュアルを起動
                System.Diagnostics.Process.Start(filePath, option);
            }
            catch (Exception ex)
            {
                // ユーザーへの通知（※メッセージ表示サービス等を経由するとより理想的です）
                System.Windows.Forms.MessageBox.Show(
                    $"操作マニュアルを表示できません。\n\n表示ファイル：{filePath}\n理由：{ex.Message}",
                    Default.ApplicationName);
            }
        }
        private string _filterText = string.Empty;

        /// <summary>
        /// メニューフィルター用文字列
        /// </summary>
        public string FilterText
        {
            get => _filterText;
            set
            {
                if (_filterText != value)
                {
                    _filterText = value;
                    OnPropertyChanged(nameof(FilterText));
                }
            }
        }

        /// <summary>
        /// 現在のフィルター文字列に基づいて BindingSource にフィルターを適用します。
        /// </summary>
        /// <param name="bindingSource">対象の BindingSource</param>
        public void ApplyMenuFilter(BindingSource bindingSource)
        {
            if (bindingSource == null) return;

            if (string.IsNullOrEmpty(FilterText))
            {
                bindingSource.Filter = null;
            }
            else
            {
                // シングルクォーテーションのエスケープ（''に置換）を行って安全に前方一致を設定
                string safeText = FilterText.Replace("'", "''");
                bindingSource.Filter = $"Title Like '{safeText}*'";
            }
        }

        /// <summary>
        /// 選択された SubID に紐づくフォルダパスを取得し、アナログ入出力モニターの外部プロセスを起動します。
        /// </summary>
        /// <param name="selectedSubId">選択中の SubID</param>
        /// <returns>SubID が存在し、モニターの起動処理に成功した場合は true。見つからなかった場合は false</returns>
        public bool LaunchAnalogAioMonitor(object selectedSubId)
        {
            if (selectedSubId == null) return false;

            // DataSet から対象の SubID の行を検索
            DataTable dtSubMain = mc.DataSetTopMenu.menuSub;
            DataRow[] dtSubRows = dtSubMain.Select($"SubID='{selectedSubId}'");

            if (dtSubRows == null || dtSubRows.Length == 0)
            {
                return false;
            }

            DataRow founddtSubRow = dtSubRows[0];
            string itemFolder = founddtSubRow["Folder"]?.ToString() ?? string.Empty;

            // アナログ I/O モニターの起動情報を作成
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = Default.AioMonitorExePath,
                Arguments = $"-d \"{itemFolder}\""
            };

            System.Diagnostics.Process.Start(psi);
            return true;
        }
        private async Task LoadInitialDataFromDbAsync()
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmOpenning"]);
            Views.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmOpenning"]);

            //CurrentDir,フォルダの指定で、絶対パスでないときエラーにするため、SystemRootに移動、\sssは、C:\sssに作られる20161027
            //System.IO.Directory.SetCurrentDirectory(Environment.GetEnvironmentVariable("windir"));        //"C:\\WINDOWS"
            System.IO.Directory.SetCurrentDirectory(Environment.GetEnvironmentVariable("SystemRoot"));      //"C:\\WINDOWS"
            //System.IO.Directory.SetCurrentDirectory(Environment.GetEnvironmentVariable("SystemDrive"));   //"C:"

            //this.lblVersion.Text = string.Format("Version {0}", System.Reflection.Assembly.GetEntryAssembly().GetName().VersionCompatibility.ToString());
            this.lblVersion.Text = string.Format("Ver {0}", Application.ProductVersion);

            //コマンドラインを配列で取得する 20170127
            string[] cmds;
            cmds = System.Environment.GetCommandLineArgs();
            string CommandLineArgs = "";
            for (int i = 0; i < cmds.Length; i++)
                CommandLineArgs += (" " + cmds[i]);
            if (cmds.Length == 2 && (cmds[1] == "PRESET_ADO" || cmds[1] == "PRESET"))
            {
                // AIOのDOをプリセット（検査ランプ消灯、操作SW 緑、赤を消灯）して自身は終了する
                //Lamp Off
                _aio.setInspctLamp(0);   //LED_OFF
                _aio.setGreenLamp(0);
                _aio.setRedLamp(0);
                _aio.SetPower12V();
                System.Environment.Exit(0);     //自分自身も終了する
            }

            // 同じ実行ファイル名のプロセスは起動しない
            // 注) パス違いでも「同名」実行ファイルは起動しない！
            if (System.Diagnostics.Process.GetProcessesByName(
                    System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("すでに起動しています！");
                System.Environment.Exit(0);
            }

            //自ウィンドウをシステムトレイに入れる
            //HideMe();
            //事前にフォームを作成
            mForms = Views.GetInstance();       //20170127 PRESET_ADOを速やかに実行するため、そのあとで、フォームを作成
            mForms.fmSetting = new SettingView();
            mForms.fmTopEdit = new TopEditView();

            //ここからはFormの更新
            this.lblMainNo.Text = Default.MainNo;   //"仕様書番号";
            this.lblSubNo.Text = Default.SubNo;     //"追番";

            //Lamp Off
            _aio.setInspctLamp(0);   //LED_OFF
            _aio.setGreenLamp(0);
            _aio.setRedLamp(0);
            _aio.SetPower12V();

            //DataSet
            mc.DataSetTopMenu = new DataSetTopMenu();
            string menuCsvPass = Default.ApplicationFloder + Default.MenuFolder + Default.Menucsv;
            mc.loadMenuCsv(mc.DataSetTopMenu, menuCsvPass);

            //Log Level
            Log.LoggingLevel = Log.LogLevel.LOG_INFO;

            //Debug
            Log.LoggingLevel = Log.LogLevel.LOG_DEBUG;
            //foreach (DataRow dtMainRow in mc.DataSetTopMenu.menuMain.Rows)
            //{
            //    Cyc.IO.Log.WriteLine(Cyc.IO.Log.LogLevel.LOG_DEBUG, "frmOpenning", string.Format("menuMain:{0}:{1}", dtMainRow["MainID"], dtMainRow["Title"]));
            //    foreach (DataRow dtSubRow in mc.DataSetTopMenu.menuSub.Select(string.Format("MainID='{0}'", dtMainRow["MainID"])))
            //    {
            //        Cyc.IO.Log.WriteLine(Cyc.IO.Log.LogLevel.LOG_DEBUG, "frmOpenning", string.Format("menuSub :{0}:{1}:{2}:{3}", dtSubRow["MainID"], dtSubRow["SubID"], dtSubRow["SubTitle"], dtSubRow["Folder"]));
            //    }

            //    //DataSet to ListBox
            //}


            //データを表示
            this.menuMainBindingSource.DataSource = mc.DataSetTopMenu;
            //this.menuMainBindingSource.DataMember = "menuMain";
            //this.listBox_MainNo.DataSource = this.menuMainBindingSource;
            //this.listBox_MainNo.DisplayMember = "menuMain.Title";
            //this.listBox_MainNo.ValueMember = "menuMain.MainID";

            //this.menuSubBindingSource.DataSource = this.menuMainBindingSource;
            //this.menuSubBindingSource.DataMember = "menuMain_menuSub";
            //this.listBox_SubNo.DataSource = this.menuSubBindingSource;
            //this.listBox_SubNo.DisplayMember = "menuMain.menuMain_menuSub.SubTitle";//menuMain_menuSub
            //this.listBox_SubNo.ValueMember = "menuMain.menuMain_menuSub.SubID";//menuMain_menuSub

            //this.menuMainBindingSource.Filter = string.Format("Title Like '{0}*'", "5");
            this.menuMainBindingSource.Filter = null;

            //時間のかかる初期化処理を起動
            this.timerInitalUpdate.Enabled = true;
        }

        private bool ValidateAndGetFolder(out string? folder)
        {
            folder = null;

            if (string.IsNullOrEmpty(SelectedMainTitle) || string.IsNullOrEmpty(SelectedSubTitle))
            {
                _navigationService.ShowMessageBox("仕様書番号または追番が選択されていません。\n\n仕様書番号または追番を選択してください。");
                return false;
            }

            if (DataSetTopMenu?.Tables["menuSub"] == null) return false;

            DataRow[]? dtSubRows = DataSetTopMenu.Tables["menuSub"]?.Select($"SubID='{SelectedSubId}'");
            if (dtSubRows?.Length != 1)
            {
                _navigationService.ShowMessageBox("選択されたSubIDが見つかりません。");
                return false;
            }

            folder = dtSubRows[0]["Folder"].ToString() ?? string.Empty;
            if (!Directory.Exists(folder))
            {
                _navigationService.ShowMessageBox("検査フォルダが見つかりません。");
                return false;
            }

            return true;
        }

        private bool preSetting(out string MainTitle, out string SubTitle, out string Folder)
        {
            MainTitle = null;
            SubTitle = null;
            Folder = null;

            if (listBox_MainNo.SelectedValue == null || listBox_SubNo.SelectedValue == null)
            {
                System.Windows.Forms.MessageBox.Show("仕様書番号または追番が選択されていません。\n\n仕様書番号または追番を選択してください。", Default.ApplicationName);
                return false;
            }

            MainTitle = listBox_MainNo.Text.ToString();
            SubTitle = listBox_SubNo.Text.ToString();
            //Folder,Folderは、DataSetTopMenuにしか入っていないので、読み出す
            DataTable dtSubMain = mc.DataSetTopMenu.menuSub;
            DataRow[] dtSubRows = dtSubMain.Select(string.Format("SubID='{0}'", listBox_SubNo.SelectedValue.ToString()));
            if (dtSubRows.Length != 1)
            {
                System.Windows.Forms.MessageBox.Show("選択されたSubIDが見つかりません。", Default.ApplicationName);
                return false;
            }
            Folder = dtSubRows[0]["Folder"].ToString();
            if (!System.IO.Directory.Exists(Folder))
            {
                System.Windows.Forms.MessageBox.Show("検査フォルダが見つかりません。\n\n【この画面の編集】をクリックして設定して下さい。", Default.ApplicationName);
                return false;
            }
            string CheckdatFile = Folder + System.IO.Path.DirectorySeparatorChar + Default.Checkdat;
            if (System.IO.File.Exists(CheckdatFile))
            {
                DataTable dtCheckDat = new DataSetItems.CheckDatDataTable();

                mc.loadCheckdat(dtCheckDat, CheckdatFile);
                if (dtCheckDat.Rows.Count > 0)
                {
                    if (MainTitle != dtCheckDat.Rows[0]["Title"].ToString() || SubTitle != dtCheckDat.Rows[0]["SubTitle"].ToString())
                    {
                        System.Windows.Forms.MessageBox.Show("【重要】\n\n選択した仕様書番号または追番が保存データと違っています。\n\nフォルダが、別の検査のものと重複している可能性があります。確認してください。", Default.ApplicationName);
                    }
                }
                else
                {
                    //中身がないので、CheckdatFileを新規作成する
                    mc.newCheckdatFile(CheckdatFile, "未設定", MainTitle, SubTitle, "");
                }
            }
            else
            {
                //ファイルがないので、CheckdatFileを新規作成する
                mc.newCheckdatFile(CheckdatFile, "未設定", MainTitle, SubTitle, "");
            }
            return true;
        }

        //自ウィンドウをシステムトレイにから取出し通常表示する
        private void ShowMe()
        {
            // フォームの表示
            this.Visible = true;
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal; // 最小化をやめる
            }
            //タスクバーにアイコンを表示する
            this.ShowInTaskbar = true;
            // フォームをアクティブにする
            this.Activate();
        }

    }
}