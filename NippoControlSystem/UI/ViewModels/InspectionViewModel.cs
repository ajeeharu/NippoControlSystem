using CommunityToolkit.Mvvm.ComponentModel;
using NippoControlSystem.ApplicationService.Configurations;
using NippoControlSystem.ApplicationService.Dtos;
using NippoControlSystem.ApplicationService.Interfaces;
using NippoControlSystem.ApplicationService.Services;
using NippoControlSystem.ApplicationServices.Services;
using NippoControlSystem.Domain.Interfaces.enums;
using NippoControlSystem.Infrastructure.Persistence;
using System.ComponentModel;
using System.Runtime.Versioning;

namespace NippoControlSystem.UI.ViewModels
{
    [SupportedOSPlatform("windows")]
    public partial class InspectionViewModel : ObservableObject
    {
        private readonly MainMenuItem _sharedMenuInfo;
        private readonly IOperationPanelService _operationPanel;
        private readonly MenuApplicationService _menuApplicationService;
        private GeneralSettings _generalSettings;    // View から直接参照するために public に変更
        private readonly MessageBoxDialogService _messageBoxDialogService;

        private readonly IDialogService _dialogService;
        // リソース解放フラグ
        private bool _disposed = false;
        // View へのデータバインド用

        // 画面のコントロールと同期させたいプロパティ
        [ObservableProperty]
        private MenuBindDto _menuDataBinding = new()
        {
            // 追加・削除をUIに通知できる BindingList を使う
            MainMenus = new BindingList<BindMainMenuDto>()
        };
        public GeneralSettings GeneralSettings => _generalSettings;
        public InspectionViewModel(
            MainMenuItem sharedMenuInfo,
            GeneralSettings generalSettings,
            IOperationPanelService operationPanelService,
            MenuApplicationService menuApplicationService,
            IDialogService dialogService,
            MessageBoxDialogService messageBoxDialogService)
        {
            _sharedMenuInfo = sharedMenuInfo;
            _generalSettings = generalSettings;
            _operationPanel = operationPanelService;
            _menuApplicationService = menuApplicationService;
            _dialogService = dialogService;
            _messageBoxDialogService = messageBoxDialogService;

        }
        private async Task InitializeAsync()
        {
            // 画面ロード時に行いたい非同期処理（データ取得など）
            await LoadInitialDataFromDbAsync();
        }
        /// <summary>
        /// 初期データをCSVファイルから非同期で読み込み、画面の初期化を行います。
        /// </summary>
        private async Task LoadInitialDataFromDbAsync()
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmMain"]);
            Views.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmMain"]);

            CioDeviceErrorCode idRet = 0;
            CioDeviceErrorCode iaRet = 0;
            //Form.Loadを一回だけ実行する記述、
            //http://d.hatena.ne.jp/Kazzz/20070913/p4
            //Form.Load イベント:フォームが初めて表示される直前に発生します。 
            //とあるが、ShowDialog();のたびにForm.Loadが発生する怪奇、Show();なら１回だけなのに

            //ここからはFormの更新
            this.lblMainNo.Text = Default.MainNo;   //"仕様書番号";
            this.lblSubNo.Text = Default.SubNo;     //"追番";
            this.lblItem.Text = Default.Item;  //品名
            this.lblSerialTitle.Text = Default.SerialTitle; //製造番号
            this.lblGoTitle.Text = Default.MachineNumber; //号機
            //pictureBoxMeter
            pictureBoxMeter = new PictureBox[8];
            pictureBoxMeter[0] = this.pictureBox1;
            pictureBoxMeter[1] = this.pictureBox2;
            pictureBoxMeter[2] = this.pictureBox3;
            pictureBoxMeter[3] = this.pictureBox4;
            pictureBoxMeter[4] = this.pictureBox5;
            pictureBoxMeter[5] = this.pictureBox6;
            pictureBoxMeter[6] = this.pictureBox7;
            pictureBoxMeter[7] = this.pictureBox8;
            pictureBox1.Image = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            pictureBox2.Image = new Bitmap(pictureBox2.Size.Width, pictureBox2.Size.Height);
            pictureBox3.Image = new Bitmap(pictureBox3.Size.Width, pictureBox3.Size.Height);
            pictureBox4.Image = new Bitmap(pictureBox4.Size.Width, pictureBox4.Size.Height);
            pictureBox5.Image = new Bitmap(pictureBox5.Size.Width, pictureBox5.Size.Height);
            pictureBox6.Image = new Bitmap(pictureBox6.Size.Width, pictureBox6.Size.Height);
            pictureBox7.Image = new Bitmap(pictureBox7.Size.Width, pictureBox7.Size.Height);
            pictureBox8.Image = new Bitmap(pictureBox8.Size.Width, pictureBox8.Size.Height);

            //AiDataAve
            AiDataAve = new float[Default.AiNum];
            AiDataPop = new float[Default.AiAveTimes, Default.AiNum];
            AiDataInput = new float[Default.AiNum];

            //updateDataSetItems
            if ((myDataSetItems = mc.createDataSetItems(Folder, MainTitle, SubTitle)) == null)
            {
                this.Close();
                return;
            }

            //タイムアウト時間更新20170809
            ToolStripMenuItem menu = (ToolStripMenuItem)タイムアウト時間ToolStripMenuItem; //タイムアウト時間ToolStripMenuItem
            menu.Text = string.Format("タイムアウト {0}分", this.AutoTimeOut);

            this.textBox_Guide.Text = "";   //メッセージクリア
            //ボタンを元に戻す
            button_Enable(true);

            //richTextBoxを普通のTextBoxに合わせる魔法の設定
            textBox_Guide.LanguageOption = RichTextBoxLanguageOptions.UIFonts;

            //IO
            idRet = _dioNative.Init();
#if !DEVICE_DEBUG
            if (idRet != 0)
            {
                this.textBox_Guide.DataBindings.Clear();        //20170112
                this.textBox_Guide.Text = "DIOボードの初期化エラー";
            }
#endif
            iaRet = _aio.Init();
#if !DEVICE_DEBUG
            if (iaRet != 0)
            {
                this.textBox_Guide.DataBindings.Clear();
                this.textBox_Guide.Text = "AIOボードの初期化エラー";
            }
#endif
            _aio.setInspctLamp(0);   //LED_OFF
            _aio.setGreenLamp(0);
            iaRet = ai2di.Init();    //20180801
#if !DEVICE_DEBUG
            if (iaRet != 0)
            {
                this.textBox_Guide.DataBindings.Clear();
                this.textBox_Guide.Text = "AIボードの初期化エラー";
            }
#endif

            //VOLT
            PowerVolt = myDataSetItems.CheckDat.Rows[0]["Volt"].ToString();
            textBox_Volt.Text = string.Format("{0}V", PowerVolt == "1" ? 24 : 12);
            if (PowerVolt == "1")
            {
                _aio.SetPower24V();
            }
            else
            {
                _aio.SetPower12V();
            }

            //ここで検査内容のDataGredViewとDataSetを作成して、データをセットする
            //myDataSetTopMenu = new DataSetItems();
            //DataTable dt = new DataSetItems.ListDatDataTable();
            //string IoName = "";
            System.Windows.Forms.DataGridView dv = this.dataGridView_Inspection;
            //dataGridView_Inspectionの定義
            //dv.Columns[0].HeaderText = "検査タイトル";
            //dv.Columns[1].HeaderText = "結果";
            dv.TopLeftHeaderCell.Value = "T#";
            //dv.Columns[0].Width = 265;
            //dv.Columns[1].Width = 445;
            //dv.Columns[2].Width = 55;
            //for (int i = 3; i < dv.Columns.Count; i++)
            //{
            //    dv.Columns[i].Width = 50;
            //}

            this.textBox_TestNo.Text = null;
            this.textBox_InspectType.Text = null;
            this.textBox_CurrentResult.Text = null;

            //dv.DataSource = myDataSetTopMenu;
            //dv.DataMember = "ListDat";
            // DataSource は Windows のみサポートされる API のため実行時ガードを追加
            if (OperatingSystem.IsWindows())
            {
                this.dataSetItemsBindingSource.DataSource = myDataSetItems;
                this.bindingSourceCheckDat.DataSource = myDataSetItems;
            }
            CurrentTNo = 0;

            this.label_ResultNG.Visible = false;
            this.label_ResultOK.Visible = false;
            this.textBox_GoNo.Text = "";
            //Stat
            mc.InspecStat = MeasureCondition.enumInspectStat.Stat_STOP;
            this.textBox_Status.Text = mc.InspecStatString;
            //timerDrawing
            this.timerDrawing.Enabled = true;
            this.timerReadSw.Enabled = true;
            this.timerInspect.Enabled = true;
            //this.textBox_Serial.Text = Default.Serial;
            this.textBox_Serial.Text = libSerialNo.GetSerialNo(Default.Serial);

        }
    }
}
