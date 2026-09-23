using NippoControlSystem.Infrastructure.Configuration;
using NippoControlSystem.Infrastructure.Devices;
using NippoControlSystem.Infrastructure.Services;
using NippoControlSystem.UI.ViewModels;
using System.Data;


#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.UI.Views
{
    public partial class OpeningView : Form
    {
        //　------　MVVMパターン用にリファクタリングしたコード　------

        private readonly OpeningViewModel _viewModel;
        private readonly Aio _aio;
        // DIコンテナ経由で ViewModel を受け取る
        public OpeningView(OpeningViewModel viewModel, Aio aio)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _aio = aio;
            _viewModel.CurrentView = this;
        }


        // 「検査」ボタンクリック
        private void button_MainView_Click(object sender, EventArgs e)
        {
            SyncSelectedValuesToViewModel();
            _viewModel.OpenMainView();
        }

        // 「検査定義の編集」ボタンクリック
        private void button_SettingView_Click(object sender, EventArgs e)
        {
            SyncSelectedValuesToViewModel();
            _viewModel.OpenSettingView();
        }

        // 「この画面の編集」ボタンクリック
        private void button_TopEditView_Click(object sender, EventArgs e)
        {
            SyncSelectedValuesToViewModel();
            _viewModel.OpenTopEditView();
        }

        // 「検査履歴」ボタンクリック
        private void button_HistoryView_Click(object sender, EventArgs e)
        {
            SyncSelectedValuesToViewModel();
            _viewModel.OpenHistoryView();
        }

        /// <summary>
        /// View 上の ListBox 選択値を ViewModel のプロパティへ同期します
        /// </summary>
        private void SyncSelectedValuesToViewModel()
        {
            _viewModel.SelectedMainTitle = listBox_MainNo.Text?.ToString();
            _viewModel.SelectedSubTitle = listBox_SubNo.Text?.ToString();
            _viewModel.SelectedSubId = listBox_SubNo.SelectedValue?.ToString();
            _viewModel.DataSetTopMenu = mc.DataSetTopMenu;
            _viewModel.SelectedMainIndex = listBox_MainNo.SelectedIndex;
            _viewModel.SelectedSubIndex = listBox_SubNo.SelectedIndex;
        }


        //　------　MVVM化のためにリファクタリングする前のコード　------


        Settings Default = Settings.GetInstance();
        cDio dio = cDio.GetInstance();
        NippoDIO nio = NippoDIO.GetInstance();
        MeasureCondition mc = MeasureCondition.GetInstance();
        //Forms mForms = Forms.GetInstance();
        Views mForms = null;            //20170127

        private void frmOpenning_Load(object sender, EventArgs e)
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

        //private void frmMain_Resize(object sender, EventArgs e)
        //{
        //    // サイズ変更が行われた後で、最小化状態でなければ場合はタスクバーにアイコンを表示します。
        //    ShowInTaskbar = (WindowState != FormWindowState.Minimized);
        //}

        ////自ウィンドウをシステムトレイに入れる
        //private void HideMe()
        //{
        //    // フォームの非表示
        //    this.Visible = false;
        //    //ウィンドウを最小化する
        //    this.WindowState = FormWindowState.Minimized;
        //    //タスクバーに表示しない
        //    this.ShowInTaskbar = false;
        //}

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

        //private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        //{
        //    //自ウィンドウを通常表示する
        //    ShowMe();
        //}

        //private void toolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    //自ウィンドウを通常表示する
        //    ShowMe();
        //}

        //private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    //notifyIcon1.Visible = false;    // アイコンをトレイから取り除く
        //    mForms.allFormsClose(this);
        //}

        //private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    //if (e.CloseReason == CloseReason.UserClosing && ApplicationExit == false)
        //    //{
        //    //    // フォームが閉じるのをキャンセル
        //    //    e.Cancel = true;
        //    //    //自ウィンドウをシステムトレイに入れる
        //    //    HideMe();
        //    //}
        //    System.Diagnostics.Debug.WriteLine("frmMain_FormClosing");
        //}
        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblVersion_Click(object sender, EventArgs e)
        {
            VersionView fmVersion = new VersionView();
            fmVersion.ShowDialog();
        }

        private void button_ViewManual_Click(object sender, EventArgs e)
        {
            //string filePath = @"D:\ogura\工番\P16010063_配線チェッカー\制御\Project\NippoControlSystem\Manual.pdf";
            string filePath = Default.ApplicationFloder + Default.ManualPath;
            string option = "";

            try
            {
                // コマンドライン引数有りで実行
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(filePath, option);
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show(string.Format("操作マニュアルを表示できません。\n\n表示ファイル：{0}\n理由：{1}", filePath, ee.Message), Default.ApplicationName);
            }
        }

        //private void button_frmSetting_Click(object sender, EventArgs e)
        //{
        //    string MainTitle;
        //    string SubTitle;
        //    string Folder;

        //    if( preSetting(out MainTitle, out SubTitle, out Folder) )
        //    {
        //        //Debug
        //        Cyc.IO.Log.WriteLine(Cyc.IO.Log.LogLevel.LOG_DEBUG, "button_frmSetting_Click", string.Format("listBox_MainNo.SelectedValue:{0}", listBox_MainNo.SelectedValue.ToString()));
        //        Cyc.IO.Log.WriteLine(Cyc.IO.Log.LogLevel.LOG_DEBUG, "button_frmSetting_Click", string.Format("listBox_SubNo.SelectedValue:{0}", listBox_SubNo.SelectedValue.ToString()));

        //        //Show
        //        SettingView fmSetting = mForms.fmSetting;
        //        fmSetting.MainTitle = MainTitle;
        //        fmSetting.SubTitle = SubTitle;
        //        fmSetting.Folder = Folder;

        //        this.Hide();
        //        fmSetting.ShowDialog();
        //        this.Show();
        //    }
        //}

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

        //private void button_frmTopEdit_Click(object sender, EventArgs e)
        //{
        //    //mc.DataSetTopMenu.AcceptChanges();
        //    //mc.DataSetTopMenuEdit = (DataSetTopMenu)mc.DataSetTopMenu.Copy();
        //    //mc.DataSetTopMenuEdit.AcceptChanges();
        //    //frmTopEdit fmTopEdit = new frmTopEdit();
        //    TopEditView fmTopEdit = mForms.fmTopEdit;
        //    fmTopEdit.myDataSetTopMenu = mc.DataSetTopMenu;
        //    fmTopEdit.SelectedMainNo = listBox_MainNo.SelectedIndex;
        //    fmTopEdit.SelectedSubNo = listBox_SubNo.SelectedIndex;
        //    this.Hide();    //20160915
        //    fmTopEdit.ShowDialog();
        //    this.Show();
        //    //mForms.ShowDialog(this, fmTopEdit);
        //    //if (mc.DataSetTopMenu.GetChanges() != null)     //20161027
        //    {
        //        this.textBox_Filter_TextChanged(this.textBox_Filter, e);
        //    }
        //    //mc.DataSetTopMenuEdit.Clear();
        //}

        //private void button_frmMain_Click(object sender, EventArgs e)
        //{
        //    string MainTitle;
        //    string SubTitle;
        //    string Folder;

        //    if (preSetting(out MainTitle, out SubTitle, out Folder))
        //    {
        //        //Debug
        //        Cyc.IO.Log.WriteLine(Cyc.IO.Log.LogLevel.LOG_DEBUG, "button_frmMain_Click", string.Format("listBox_MainNo.SelectedValue:{0}", listBox_MainNo.SelectedValue.ToString()));
        //        Cyc.IO.Log.WriteLine(Cyc.IO.Log.LogLevel.LOG_DEBUG, "button_frmMain_Click", string.Format("listBox_SubNo.SelectedValue:{0}", listBox_SubNo.SelectedValue.ToString()));

        //        //Show
        //        MainView fmMain = mForms.fmMain;
        //        fmMain.MainTitle = MainTitle;
        //        fmMain.SubTitle = SubTitle;
        //        fmMain.Folder = Folder;

        //        this.Hide();
        //        fmMain.ShowDialog();
        //        this.Show();
        //    }
        //}

        private void listBox_MainNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dtMain = mc.DataSetTopMenu.menuMain;
            //DataTable dtSubMain = mc.DataSetTopMenu.menuSub;

        }

        private void textBox_Filter_TextChanged(object sender, EventArgs e)
        {
#if false
            DataTable dtMain = mc.DataSetTopMenu.menuMain;
            // Copy the entire DataTable.予めDataSetを作らないときは、以下ようにして作成する。
            //DataTable dtMainSelected = dtMain.Copy(); 
            //DataTable dtMainSelected = dtMain.Clone();
            //dtMainSelected.TableName = "menuMainSelected";
            //DataSetTopMenu.Tables.Add(dtMainSelected);
            DataTable dtMainSelected = mc.DataSetTopMenu.menuMainSelected;
            DataTable dtSubMain = mc.DataSetTopMenu.menuSub;

            if (string.IsNullOrEmpty(this.textBox_Filter.Text))
            {
                this.listBox_MainNo.DisplayMember = "menuMain.Title";
                this.listBox_MainNo.ValueMember = "menuMain.MainID";
                this.listBox_MainNo.DataSource = mc.DataSetTopMenu;
                this.listBox_SubNo.DisplayMember = "menuMain.menuMain_menuSub.SubTitle";//menuMain_menuSub
                this.listBox_SubNo.ValueMember = "menuMain.menuMain_menuSub.SubID";//menuMain_menuSub
                this.listBox_SubNo.DataSource = mc.DataSetTopMenu;
            }
            else
            {
                // Copy from the results of a Select method.
                dtMainSelected.Rows.Clear();
                foreach (DataRow MyDataRow in dtMain.Select(string.Format("Title Like '{0}*'", this.textBox_Filter.Text)))   //"Title Like '5C0-00700'"
                {
                    dtMainSelected.ImportRow(MyDataRow);
                }
                this.listBox_MainNo.DisplayMember = "menuMainSelected.Title";
                this.listBox_MainNo.ValueMember = "menuMainSelected.MainID";
                this.listBox_MainNo.DataSource = mc.DataSetTopMenu;
                this.listBox_SubNo.DisplayMember = "menuMainSelected.menuMainSelected_menuSub.SubTitle";//menuMain_menuSub
                this.listBox_SubNo.ValueMember = "menuMainSelected.menuMainSelected_menuSub.SubID";//menuMain_menuSub
                this.listBox_SubNo.DataSource = mc.DataSetTopMenu;
            }
#else
            System.Windows.Forms.TextBox textBox1 = (System.Windows.Forms.TextBox)sender;
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                this.menuMainBindingSource.Filter = null;
            }
            else
            {
                this.menuMainBindingSource.Filter = string.Format("Title Like '{0}*'", this.textBox_Filter.Text);
            }
            this.listBox_MainNo.Refresh();
            this.listBox_SubNo.Refresh();
            //this.Refresh();
#endif
        }

        private void timerInitalUpdate_Tick(object sender, EventArgs e)
        {
            //timerInitalUpdate
            //事前にフォームを作成
            //mForms.fmSetting = new frmSetting();
            //mForms.fmPort = new frmPort();
            //mForms.fmDataInput = new frmDataInput();
            mForms.allFormsCreate();

            this.timerInitalUpdate.Enabled = false;

        }

        private void button_AnalogMonitor_Click(object sender, EventArgs e)
        {
            //frmAIOMonitor fmAIOMonitor = new frmAIOMonitor();
            //fmAIOMonitor.SelectMainNo = listBox_MainNo.SelectedValue.ToString();
            //fmAIOMonitor.SelectSubNo = listBox_SubNo.SelectedValue.ToString();
            ////Debug
            //Cyc.IO.Log.WriteLine(Cyc.IO.Log.LogLevel.LOG_DEBUG, "button_AnalogMonitor_Click", string.Format("listBox_MainNo.SelectedValue:{0}", listBox_MainNo.SelectedValue.ToString()));
            //Cyc.IO.Log.WriteLine(Cyc.IO.Log.LogLevel.LOG_DEBUG, "button_AnalogMonitor_Click", string.Format("listBox_SubNo.SelectedValue:{0}", listBox_SubNo.SelectedValue.ToString()));
            ////mForms.ShowDialog(this, fmSetting);

            //fmAIOMonitor.Show();

            //Folder
            DataTable dtSubMain = mc.DataSetTopMenu.menuSub;
            DataRow founddtSubRow = null;
            DataRow[] dtSubRows = dtSubMain.Select(string.Format("SubID='{0}'", listBox_SubNo.SelectedValue.ToString()));
            if (dtSubRows == null)
            {
                System.Windows.Forms.MessageBox.Show("選択されたSubIDが見つかりません。", Default.ApplicationName);
                this.Close();
            }
            founddtSubRow = dtSubRows[0];
            string ItemFolder = founddtSubRow["Folder"].ToString();
            //exe実行
            //ProcessStartInfoオブジェクトを作成する
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            //起動するファイルのパスを指定する
            psi.FileName = Default.AioMonitorExePath;
            //コマンドライン引数を指定する
            psi.Arguments = string.Format("-d \"{0}\"", ItemFolder);

            //アプリケーションを起動する
            System.Diagnostics.Process.Start(psi);

        }

        private void frmOpenning_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Lamp Off 2016/10/27
            _aio.setInspctLamp(0);   //LED_OFF
            _aio.setGreenLamp(0);
            _aio.setRedLamp(0);
            _aio.SetPower12V();
            //Close
            _aio.Exit();
            nio.Close();
            dio.Exit();
        }

        private void frmOpenning_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmOpenning_FormClosing");

        }

        //private void buttonHistory_Click(object sender, EventArgs e)
        //{
        //    string MainTitle;
        //    string SubTitle;
        //    string Folder;

        //    if (preSetting(out MainTitle, out SubTitle, out Folder))
        //    {
        //        string historyFile = showOpenFileDialog(Folder);
        //        if (!System.IO.File.Exists(historyFile))
        //        {
        //            return;
        //        }

        //        //Debug
        //        Cyc.IO.Log.WriteLine(Cyc.IO.Log.LogLevel.LOG_DEBUG, "buttonHistory_Click", string.Format("listBox_MainNo.SelectedValue:{0}", listBox_MainNo.SelectedValue.ToString()));
        //        Cyc.IO.Log.WriteLine(Cyc.IO.Log.LogLevel.LOG_DEBUG, "buttonHistory_Click", string.Format("listBox_SubNo.SelectedValue:{0}", listBox_SubNo.SelectedValue.ToString()));

        //        //Show
        //        TestHistoryView fmHist = mForms.fmHistView;
        //        fmHist.MainTitle = MainTitle;
        //        fmHist.SubTitle = SubTitle;
        //        fmHist.Folder = Folder;
        //        fmHist.historyFile = historyFile;

        //        this.Hide();
        //        fmHist.ShowDialog();
        //        this.Show();
        //    }
        //}

        private string showOpenFileDialog(string InitialDirectory)
        {
            string HistoryFile = "";
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            //ofd.FileName = "default.dat";
            ofd.FileName = "default.csv";       //20180827
            //はじめに表示されるフォルダを指定する    
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            //ofd.InitialDirectory = @"C:\";
            ofd.InitialDirectory = InitialDirectory;
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            //ofd.Filter = "HTMLファイル(*.html;*.htm)|*.html;*.htm|すべてのファイル(*.*)|*.*";
            //ofd.Filter = "検査結果ファイル(A*.dat)|A*.dat|すべてのファイル(*.*)|*.*";
            ofd.Filter = "検査結果ファイル(A*.csv)|A*.csv|すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに選択されるものを指定する
            //2番目の「すべてのファイル」が選択されているようにする
            //ofd.FilterIndex = 2;
            ofd.FilterIndex = 1;
            //タイトルを設定する
            ofd.Title = "開くファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;
            //存在しないファイルの名前が指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckFileExists = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                Console.WriteLine(ofd.FileName);
                HistoryFile = ofd.FileName;
            }


            return HistoryFile;
        }

        private void listBox_SubNo_DoubleClick(object sender, EventArgs e)
        {
            button_MainView_Click(sender, e);    //20161203

        }

    }
}