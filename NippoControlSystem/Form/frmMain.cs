using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Versioning;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem
{
    public partial class frmMain : Form
    {
        Cyc.IO.Settings Default = Cyc.IO.Settings.GetInstance();
        Cyc.IO.Log.LogLevel Main_LogLevel = Cyc.IO.Log.LogLevel.LOG_INFO;
        Cyc.IO.cDio dio = Cyc.IO.cDio.GetInstance();
        Cyc.IO.Aio aio = Cyc.IO.Aio.GetInstance();
        Cyc.IO.Ai2Di ai2di = Cyc.IO.Ai2Di.GetInstance();
        Cyc.IO.NippoDIO nio = Cyc.IO.NippoDIO.GetInstance();
        MeasureCondition mc = MeasureCondition.GetInstance();
        Forms mForms = Forms.GetInstance();
        System.Windows.Forms.PictureBox[] pictureBoxMeter;

        //Lamp
        //const bool WorkingLampON = true;    //稼働時点灯
        const bool WorkingLampON = false;    //稼働時点灯
        const int SlLampOn = 1;
        const int SlLampOff = 0;
        const int OpSwLampOn = (WorkingLampON ? 0 : 1);
        const int OpSwLampOff = (WorkingLampON ? 1 : 0);

        //// PictureBoxと同サイズのBitmapオブジェクトを作成
        //Bitmap[] pictureBoxBmp = null;

        //Local 変数
        int TNo = 0;
        int TNoRetryNo = 0;
        float[,] AiDataPop;
        float[] AiDataAve;
        float[] AiDataInput;
        //bool nextButtonClick = false;
        bool stopButtonClick = false;
        string PowerVolt = "";
        DataSetItems myDataSetItems;

        public frmMain()
        {
            InitializeComponent();
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ Serial
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Serial
        {
            get { return this.textBox_Serial.Text; }
            set
            {
                this.textBox_Serial.Text = value;
                this.textBox_Serial.Refresh();
            }
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ GoNo
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string GoNo
        {
            get { return this.textBox_GoNo.Text; }
            set
            {
                this.textBox_GoNo.Text = value;
                this.textBox_GoNo.Refresh();
            }
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ MainTitle
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string MainTitle
        {
            get;
            set;
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ SubTitle
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SubTitle
        {
            get;
            set;
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ Folder
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Folder
        {
            get;
            set;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ CurrentTNo
        int m_CurrentTNo = 0;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [SupportedOSPlatform("windows")]
        public int CurrentTNo
        {
            get { return m_CurrentTNo; }
            set
            {
                m_CurrentTNo = value;
                System.Windows.Forms.DataGridView dv = dataGridView_Inspection;
                // ArrangedElementCollection.Count は Windows でのみサポートされる API のため実行時ガードを追加
                if (OperatingSystem.IsWindows())
                {
                    if (dv.Rows.Count > 0)  dv.CurrentCell = dv[0, m_CurrentTNo];
                }
            }
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmMain"]);
            Forms.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmMain"]);

            int iRet = 0;
            //Form.Loadを一回だけ実行する記述、
            //http://d.hatena.ne.jp/Kazzz/20070913/p4
            //Form.Load イベント:フォームが初めて表示される直前に発生します。 
            //とあるが、ShowDialog();のたびにForm.Loadが発生する怪奇、Show();なら１回だけなのに

            //ここからはFormの更新
            this.lblMainNo.Text = Default.MainNo;   //"仕様書番号";
            this.lblSubNo.Text = Default.SubNo;     //"追番";
            this.lblItem.Text = Default.Item;  //品名
            this.lblSerialTitle.Text = Default.SerialTitle; //製造番号
            this.lblGoTitle.Text = Default.GokiTitle; //号機
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
            iRet = dio.Init();
       #if !DEVICE_DEBUG
            if (iRet != 0)
            {
                this.textBox_Guide.DataBindings.Clear();        //20170112
                this.textBox_Guide.Text = "DIOボードの初期化エラー";
            }
        #endif
            iRet = aio.Init();
        #if !DEVICE_DEBUG
            if (iRet != 0)
            {
                this.textBox_Guide.DataBindings.Clear();
                this.textBox_Guide.Text = "AIOボードの初期化エラー";
            }
        #endif
            aio.setInspctLamp(0);   //LED_OFF
            aio.setGreenLamp(0);
            iRet = ai2di.Init();    //20180801
        #if !DEVICE_DEBUG
            if (iRet != 0)
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
                aio.SetPower24V();
            }
            else
            {
                aio.SetPower12V();
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

        private void dvResults_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            // 行ヘッダのセル領域を、行番号を描画する長方形とする
            // （ただし右端に4ドットのすき間を空ける）
            Rectangle rect = new Rectangle(
              e.RowBounds.Location.X,
              e.RowBounds.Location.Y,
              dataGridView1.RowHeadersWidth + 4,  //-4 -> -2
              e.RowBounds.Height);

            // 上記の長方形内に行番号を縦方向中央＆右詰めで描画する
            // フォントや前景色は行ヘッダの既定値を使用する
            TextRenderer.DrawText(
              e.Graphics,
              (e.RowIndex + 1).ToString(),
              dataGridView1.RowHeadersDefaultCellStyle.Font,
              rect,
              dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
              TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            if (mc.InspecStat == MeasureCondition.enumInspectStat.Stat_NormalStart
               || mc.InspecStat == MeasureCondition.enumInspectStat.Stat_MidStarted
               || mc.InspecStat == MeasureCondition.enumInspectStat.Stat_ForceToEnd)
            {
                //mc.InspecStat = MeasureCondition.enumInspectStat.Stat_FailEND;
                //mc.TestEndDT = DateTime.Now;
                System.Windows.Forms.MessageBox.Show("検査中なので、終了できません。", Default.ApplicationName);
                return;
            }

            if (mc.InspecStat != MeasureCondition.enumInspectStat.Stat_STOP)    //検査が始まっていないときは、保存画面を出さない
            {
                //操作スイッチの読み込みを終了する
                //timerDrawing
                this.timerDrawing.Enabled = false;
                this.timerReadSw.Enabled = false;
                this.timerInspect.Enabled = false;
                aio.setInspctLamp(0);   //LED_OFF
                //fmSerialを表示する。
                frmSerial fmSerial = mForms.fmSerial;
                mc.SerialNo = this.textBox_Serial.Text;
                mc.GoNo = this.textBox_GoNo.Text;
                mc.Zuban = this.textBox_Zuban.Text;
                mc.Edaban = this.textBox_Edaban.Text;
                fmSerial.Folder = this.Folder;
                fmSerial.myDataSetItems = this.myDataSetItems;
                fmSerial.ShowDialog();
                if (fmSerial.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                    return;
            }
            this.Close();
        }

        private void button_Next_Click(object sender, EventArgs e)
        {
            //検査条件で
            if (mc.InspecStat == MeasureCondition.enumInspectStat.Stat_STOP ||
                mc.InspecStat == MeasureCondition.enumInspectStat.Stat_NormalEND ||
                mc.InspecStat == MeasureCondition.enumInspectStat.Stat_FailEND)
            {
                //int iRetDio = dio.Init();
                //int iRetAio = aio.Init();

                //if (iRetDio != 0 || iRetAio != 0)
                //{
                //    System.Windows.Forms.MessageBox.Show("検査できません。\n\nDIOボードまたはAIOボードに異常があります。", Default.ApplicationName);
                //    return;
                //}
                //検査を開始する
                stopButtonClick = false; 
                inspect_Run(0, MeasureCondition.enumInspectStat.Stat_NormalStart);
            }
            else
            {
                this.switchLabelGreenSwitch.LampValue = 1;  //20170126
            }
        }

        private void button_Enable(bool enableStat)
        {
            //this.button_Next.Enabled = enableStat;
            this.button_Stop.Enabled = !enableStat;
            this.button_DataInput.Enabled = enableStat;
            this.button_ResultView.Enabled = enableStat;
            this.button_Debug.Enabled = enableStat;
            this.button_Close.Enabled = enableStat;
            //以下はメニューのボタン
            this.mnuStart.Enabled = enableStat;
            this.mnuStop.Enabled = !enableStat;
            this.mnuEdit.Enabled = enableStat;
            this.mnuView.Enabled = enableStat;
        }

        private void button_Stop_Click(object sender, EventArgs e)
        {
            //button_Enable(true);
            stopButtonClick = true;
        }

        private void button_DataInput_Click(object sender, EventArgs e)
        {
            DataSetItems newDataSetItems = null;

            if (mc.InspecStat == MeasureCondition.enumInspectStat.Stat_NormalStart
               || mc.InspecStat == MeasureCondition.enumInspectStat.Stat_MidStarted
               || mc.InspecStat == MeasureCondition.enumInspectStat.Stat_ForceToEnd)
            {
                //検査中はなにもしない
                return;
            }

            //mForms.ShowDialog(this, mForms.fmDataInput);
            frmSetting fmSetting = mForms.fmSetting;
            fmSetting.MainTitle = this.MainTitle;
            fmSetting.SubTitle = this.SubTitle;
            fmSetting.Folder = this.Folder;
            //Debug
            //Cyc.IO.Log.WriteLine(Cyc.IO.Log.LogLevel.LOG_DEBUG, "button_DataInput_Click", string.Format("listBox_MainNo.SelectedValue:{0}", this.MainTitle));
            //Cyc.IO.Log.WriteLine(Cyc.IO.Log.LogLevel.LOG_DEBUG, "button_DataInput_Click", string.Format("listBox_SubNo.SelectedValue:{0}", this.SubTitle));

            this.Hide();
            fmSetting.ShowDialog();
            this.Show();

            if (fmSetting.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                //20160907
                //updateDataSetItems();
                if ((newDataSetItems = mc.createDataSetItems(Folder, MainTitle, SubTitle)) == null)
                {
                    System.Windows.Forms.MessageBox.Show("検査ファイルの再読み込みを失敗しました。\n\n画面を閉じます。", Default.ApplicationName);
                    this.Close();
                }
                //検査結果を引き継ぐ20161102
                DataTable dtListDatResult = newDataSetItems.ListDatResult;
                dtListDatResult.Rows.Clear();
                for (int i = 0; i < Math.Min(myDataSetItems.ListDat.Count, myDataSetItems.ListDatResult.Rows.Count); i++)
                {
                    DataRow dtListDatResultNewRow = dtListDatResult.NewRow();
                    dtListDatResultNewRow.ItemArray = myDataSetItems.ListDatResult.Rows[i].ItemArray;
                    dtListDatResult.Rows.Add(dtListDatResultNewRow);
                    newDataSetItems.ListDat.Rows[i]["Result"] = myDataSetItems.ListDatResult.Rows[i]["Result"];     //20170114
                }
                dtListDatResult.AcceptChanges();
                myDataSetItems = newDataSetItems;
                this.dataSetItemsBindingSource.DataSource = myDataSetItems;
                this.bindingSourceCheckDat.DataSource = myDataSetItems;
                //VOLT　20170126
                PowerVolt = myDataSetItems.CheckDat.Rows[0]["Volt"].ToString();
                textBox_Volt.Text = string.Format("{0}V", PowerVolt == "1" ? 24 : 12);
                if (PowerVolt == "1")
                {
                    aio.SetPower24V();
                }
                else
                {
                    aio.SetPower12V();
                }
            }
            this.textBox_Guide.Text = null;
        }

        private void button_ResultView_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DataGridView dv = this.dataGridView_Inspection;

            if (dv.CurrentCell == null)
            {
                return;
            }
            this.timerReadSw.Enabled = false;
            frmResultView fmResultView = mForms.fmResultView;
            fmResultView.myDataSetItems = myDataSetItems;
            fmResultView.TNo = dv.CurrentCell.RowIndex;
            fmResultView.ShowDialog();
            this.timerReadSw.Enabled = true;
        }

        private void button_Debug_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DataGridView dv = this.dataGridView_Inspection;
            if (dv.CurrentCell == null)
            {
                return;
            }

            if (mc.InspecStat == MeasureCondition.enumInspectStat.Stat_NormalStart
               || mc.InspecStat == MeasureCondition.enumInspectStat.Stat_MidStarted
               || mc.InspecStat == MeasureCondition.enumInspectStat.Stat_ForceToEnd)
            {
                //検査中はなにもしない
                return;
            }

            this.timerReadSw.Enabled = false;   //操作SWで先に進まないように、timerReadSwを止める
            frmDebug fmDebug = mForms.fmDebug;
            fmDebug.myDataSetItems = myDataSetItems;
            fmDebug.CurrentTNo = dv.CurrentCell.RowIndex;
            fmDebug.ShowDialog();
            this.timerReadSw.Enabled = true;    //timerReadSw再開
            if (fmDebug.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                int iRetDio = dio.Init();
                int iRetAio = aio.Init();

                //if (iRetDio != 0 || iRetAio != 0)
                //{
                //    System.Windows.Forms.MessageBox.Show("検査できません。\n\nDIOボードまたはAIOボードに異常があります。", Default.ApplicationName);
                //    return;
                //}
                //Stat_MidStartedで、Tno==0のときは通常開始とする。
                if (fmDebug.start_stat == MeasureCondition.enumInspectStat.Stat_MidStarted && fmDebug.CurrentTNo == 0)
                {
                    this.inspect_Run(0, MeasureCondition.enumInspectStat.Stat_NormalStart);
                }
                else
                {
                    this.inspect_Run(fmDebug.CurrentTNo, fmDebug.start_stat);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DataGridView dv = this.dataGridView_Inspection;
            if (dv.CurrentCell.RowIndex + 1 < dv.Rows.Count)
            {
                dv.CurrentCell = dv[dv.CurrentCell.ColumnIndex, dv.CurrentCell.RowIndex + 1];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DataGridView dv = this.dataGridView_Inspection;
            if (dv.CurrentCell.RowIndex > 0)
            {
                dv.CurrentCell = dv[dv.CurrentCell.ColumnIndex, dv.CurrentCell.RowIndex - 1];
            }
        }

        private void dataGridView_Inspection_CurrentCellChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.DataGridView dv = (System.Windows.Forms.DataGridView)sender;

            if (dv.CurrentCell != null)
            {
                this.m_CurrentTNo = dv.CurrentCell.RowIndex;
            }
        }

        private void timerInspect_Tick(object sender, EventArgs e)
        {
            timerInspect_Tick_Do(sender, e);
        }

        private void timerDrawing_Tick(object sender, EventArgs e)
        {
            //float[] AiDataInput = new float[8];
            float work;
            int ret = aio.MultiAi(AiDataInput);
            //AiDataAve = new float[10,8];
            for (int i = 0; i < (Default.AiAveTimes - 1); i++)
            {
                for (int j = 0; j < Default.AiNum; j++)
                {
                    AiDataPop[Default.AiAveTimes - i - 1, j] = AiDataPop[Default.AiAveTimes - i - 2, j];  //[0,*]->[1,+]...[8,*]->[9,*]
                }
            }

            for (int j = 0; j < Default.AiNum; j++)
            {
                AiDataPop[0, j] = AiDataInput[j];  //[0,*]<-Now data
            }
            //Ave
            for (int j = 0; j < Default.AiNum; j++)
            {
                work = 0f;
                for (int i = 0; i < Default.AiAveTimes; i++)
                {
                    work += AiDataPop[i, j];
                }
                AiDataAve[j] = work / (float)Default.AiAveTimes;
            }
            //Display
            //if (mc.InspecStat == MeasureCondition.enumInspectStat.Stat_NormalStart || mc.InspecStat == MeasureCondition.enumInspectStat.Stat_ForceToEnd || mc.InspecStat == MeasureCondition.enumInspectStat.Stat_MidStarted)
            {
                for (int i = 0; i < Default.AiNum; i++)
                {
                    DrawMeter(this.pictureBoxMeter[i], string.Format("AI-{0}", i + 1), AiDataAve[i]);    // 表示をを最新の状態にアップデート
                }
            }
            //Stat
            this.textBox_Status.Text = mc.InspecStatString;
            //検査中ランプ
            if (mc.InspecStat == MeasureCondition.enumInspectStat.Stat_NormalStart || mc.InspecStat == MeasureCondition.enumInspectStat.Stat_ForceToEnd || mc.InspecStat == MeasureCondition.enumInspectStat.Stat_MidStarted)
            {
                aio.setInspctLamp(1);
            }
            else
            {
                aio.setInspctLamp(0);   //LED_OFF
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmMain_FormClosing");
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Lamp
            this.switchLabelGreenSwitch.LampValue = SlLampOff;
            this.switchLabelRedSwitch.LampValue = SlLampOff;
            aio.setGreenLamp(0);
            aio.setRedLamp(0);

            //stat clear
            mc.InspecStat = MeasureCondition.enumInspectStat.Stat_STOP;
            //timerDrawing
            this.timerDrawing.Enabled = false;
            this.timerReadSw.Enabled = false;
            this.timerInspect.Enabled = false;
            aio.setInspctLamp(0);   //LED_OFF
            StopSound();
            //Dio close
            allClear();
            dio.Exit();
            aio.Exit();
        }

        private bool GreenSwitchDown()
        {
            if (mc.GreenSwitchStat != 0)
            {
                //一度ONしたら、OFFするまで、無視する
                if (this.switchLabelGreenSwitch.LampValue == 0 && aio.getGreenSw() == 0)
                {
                    mc.GreenSwitchStat = 0;
                }
                return false;
            }
            else
            {
                //switchLabelGreenSwitch.LampValueは、ON=1、aio.getGreenSw()はON=1
                if (this.switchLabelGreenSwitch.LampValue == 0 && aio.getGreenSw() == 0)
                {
                    //両方OFFなら、falseで抜ける
                    return false;
                }
            }
            mc.GreenSwitchStat = 1; //次のOFFまでは動作しないフラグ
            this.switchLabelGreenSwitch.LampValue = 0;
            return true;
        }

        private bool RedSwitchDown()
        {
            //画面の停止ボタンが押された
            if (stopButtonClick)
            {
                stopButtonClick = false;
                return true;
            }
            if (mc.RedSwitchStat != 0)
            {
                //一度ONしたら、OFFするまで、無視する
                if (this.switchLabelRedSwitch.LampValue == 0 && aio.getRedSw() == 0)
                {
                    mc.RedSwitchStat = 0;
                }
                return false;
            }
            else
            {
                //switchLabelGreenSwitch.LampValueは、ON=1、aio.getGreenSw()はON=1
                if (this.switchLabelRedSwitch.LampValue == 0 && aio.getRedSw() == 0)
                {
                    //両方OFFなら、falseで抜ける
                    return false;
                }
            }
            mc.RedSwitchStat = 1;   //次のOFFまでは動作しないフラグ
            return true;
        }

        private void switchLabelGreenSwitch_MouseDown(object sender, MouseEventArgs e)
        {
            this.switchLabelGreenSwitch.LampValue = SlLampOn;
        }

        private void switchLabelGreenSwitch_MouseUp(object sender, MouseEventArgs e)
        {
            this.switchLabelGreenSwitch.LampValue = SlLampOff;
        }

        private void switchLabelGreenSwitch_MouseLeave(object sender, EventArgs e)
        {
            this.switchLabelGreenSwitch.LampValue = SlLampOff;
        }

        private void switchLabelRedSwitch_MouseDown(object sender, MouseEventArgs e)
        {
            this.switchLabelRedSwitch.LampValue = SlLampOn;
        }

        private void switchLabelRedSwitch_MouseUp(object sender, MouseEventArgs e)
        {
            this.switchLabelRedSwitch.LampValue = SlLampOff;
        }

        private void switchLabelRedSwitch_Leave(object sender, EventArgs e)
        {
            this.switchLabelRedSwitch.LampValue = SlLampOff;
        }

        private void timerReadSw_Tick(object sender, EventArgs e)
        {
            ////Aio-Dio
            //newGreenSwitch = aio.getGreenSw();
            //newRedSwitch = aio.getRedSw();

            ////検査終了直後？
            //if (restartTimer.IsRunning && restartTimer.ElapsedMilliseconds < 2000)
            //{
            //    aio.setGreenLamp(0);
            //    aio.setRedLamp(0);
            //    newGreenSwitch = 0;
            //    newRedSwitch = 0;
            //}
            //else
            //{
            //    if (restartTimer.IsRunning)
            //        restartTimer.Stop();

            //    aio.setGreenLamp(newGreenSwitch ^ (WorkingLampON ? 1 : 0));
            //    aio.setRedLamp(newRedSwitch ^ (WorkingLampON ? 1 : 0));

            //    GreenSwitchDown = (newGreenSwitch == 1);
            //    RedSwitchDown = (newRedSwitch == 1);
            //}
            if (mc.GreenSwitchStat != 0)    //OFFしたら、GreenSwitchStat(ON中)をリセットする 20170126
            {
                //一度ONしたら、OFFするまで、無視する
                if (this.switchLabelGreenSwitch.LampValue == 0 && aio.getGreenSw() == 0)　
                {
                    mc.GreenSwitchStat = 0;
                }
            }
            if (mc.RedSwitchStat != 0)    //OFFしたら、RedSwitchStat(ON中)をリセットする 20170126
            {
                //一度ONしたら、OFFするまで、無視する
                if (this.switchLabelRedSwitch.LampValue == 0 && aio.getRedSw() == 0)
                {
                    mc.RedSwitchStat = 0;
                }
            }

            aio.setGreenLamp(aio.getGreenSw());
            aio.setRedLamp(aio.getRedSw());
        }

        private void mnuPrint_Click(object sender, EventArgs e)
        {
            Cyc.Windows.Forms.screenShot.GetInstance().PrintForm(this, true);
        }

        private void mnuEnd_Click(object sender, EventArgs e)
        {
            button_Close_Click(sender, e);
        }

        private void dataGridView_Inspection_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button_ResultView_Click(sender, e);
        }

        private void dataGridView_Inspection_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            if (e.RowIndex < myDataSetItems.ListDatResult.Rows.Count)
            {
                DataRow dtListDatResultRow = myDataSetItems.ListDatResult.Rows[e.RowIndex];

                string Value = e.Value.ToString();
                if (dtListDatResultRow["Result"].ToString() == "Fail" || e.RowIndex == TNo)
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Cyan;
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Cyan;
                    //dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;
                }
                else
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.White;
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.White;
                    //dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Black;
                }
            }
            else
            {
                dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.White;
                dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.White;
            }
        }

        private void mnuView_Click(object sender, EventArgs e)
        {
            button_ResultView_Click(sender, e);
        }

        private void mnuEdit_Click(object sender, EventArgs e)
        {
            button_DataInput_Click(sender, e);
        }

        private void mnuStop_Click(object sender, EventArgs e)
        {
            button_Stop_Click(sender, e);
        }

        private void mnuStart_Click(object sender, EventArgs e)
        {
            button_Next_Click(this.button_Next, new EventArgs());
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            frmVersion fmVersion = new frmVersion();
            fmVersion.ShowDialog();
        }

        private void mnManual_Click(object sender, EventArgs e)
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

        private void タイムアウト時間ToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender; //タイムアウト時間ToolStripMenuItem

            for (int i = 0; i < menu.DropDown.Items.Count; i++)
            {
                ToolStripMenuItem subMenu = (ToolStripMenuItem)menu.DropDown.Items[i];
                if ((i+1) == this.AutoTimeOut)
                {
                    subMenu.Checked = true;
                }
                else
                {
                    subMenu.Checked = false;
                }
            }
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)タイムアウト時間ToolStripMenuItem; //タイムアウト時間ToolStripMenuItem

            for (int i = 0; i < menu.DropDown.Items.Count; i++)
            {
                ToolStripMenuItem subMenu = (ToolStripMenuItem)menu.DropDown.Items[i];
                if (sender.Equals(subMenu))
                {
                    this.AutoTimeOut = (i + 1);
                    menu.Text = string.Format("タイムアウト {0}分", this.AutoTimeOut); 
                }
            }
        }
    }
}