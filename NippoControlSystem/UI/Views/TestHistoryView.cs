using NippoControlSystem.Infrastructure.Configuration;
using NippoControlSystem.Infrastructure.Devices;
using NippoControlSystem.UI.Controls;
using System.ComponentModel;
using System.Data;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.UI.Views
{
    public partial class TestHistoryView : Form

    {
        //　------　MVVMパターン用にリファクタリングしたコード　------

        //　------　MVVM化のためにリファクタリングする前のコード　------

        Settings Default = Settings.GetInstance();
        DigitalIO dio = DigitalIO.GetInstance();
        private readonly AnalogIO _aio;
        NippoDIO nio = NippoDIO.GetInstance();
        MeasureCondition mc = MeasureCondition.GetInstance();
        screenShot screen = screenShot.GetInstance();
        Views mForms = Views.GetInstance();
        System.Windows.Forms.PictureBox[] pictureBoxMeter;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        DataSetItems myDataSetItems;
        string PowerVolt = "";

        //Const
        const int AveTimes = 10;

        public TestHistoryView(AnalogIO aio)
        {
            InitializeComponent();
            _aio = aio;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ SelectedNo
        string m_HistoryFile = "";
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string historyFile
        {
            get { return m_HistoryFile; }
            set { m_HistoryFile = value; }
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

        ////--------1---------2---------3---------4---------5---------6---------7---------8
        ////プロパティ CurrentTNo
        //int m_CurrentTNo = 0;
        //public int CurrentTNo
        //{
        //    get { return m_CurrentTNo; }
        //    set
        //    {
        //        m_CurrentTNo = value;
        //        System.Windows.Forms.DataGridView dv = dataGridView_Inspection;
        //        dv.CurrentCell = dv[0, m_CurrentTNo];
        //    }
        //}


        private void frmHistView_Load(object sender, EventArgs e)
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmHistView"]);
            Views.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmHistView"]);

            //int iRet = 0;
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

            //updateDataSetItems
            if ((myDataSetItems = mc.createDataSetItems(Folder, MainTitle, SubTitle)) == null)
            {
                this.Close();
                return;
            }
            //HistryFileを読み込む
            updateDataSetItems2();

            //richTextBoxを普通のTextBoxに合わせる魔法の設定 20181004
            textBox_Guide.LanguageOption = RichTextBoxLanguageOptions.UIFonts;

            //VOLT
            PowerVolt = myDataSetItems.CheckDat.Rows[0]["Volt"].ToString();
            textBox_Volt.Text = string.Format("{0}V", PowerVolt == "1" ? 24 : 12);

            //ここで検査内容のDataGredViewとDataSetを作成して、データをセットする
            //myDataSetItems = new DataSetItems();
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


            //dv.DataSource = myDataSetItems;
            //dv.DataMember = "ListDat";
            this.dataSetItemsBindingSource.DataSource = myDataSetItems;
            this.bindingSourceCheckDat.DataSource = myDataSetItems;

            //this.label_ResultNG.Visible = false;
            //this.label_ResultOK.Visible = false;
            //this.textBox_Serial.Text = "";
            //this.textBox_GoNo.Text = "";
            //Stat
            //mc.InspecStat = MeasureCondition.enumInspectStat.Stat_STOP;
            //this.textBox_Status.Text = mc.InspecStatString;
            //timerDrawing
            this.timerDrawing.Enabled = true;
            this.timerReadSw.Enabled = true;
            this.timerInspect.Enabled = true;
        }

        private void updateDataSetItems2()
        {
            //結果ファイルから、各ファイルを生成する。
            //string alltext = System.IO.File.ReadAllText(historyFile, mc.enc);
            string alltext = mc.ReadAllText(historyFile, mc.enc);
            string[] SplitedText = alltext.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if (SplitedText.Length != 4)
            {
            }
            //string Folder = Default.DataFolder + Default.HistViewFolder + System.IO.Path.DirectorySeparatorChar;    //3
            string Folder = Default.ApplicationFloder + Default.SettingsHolder + Default.HistViewFolder + System.IO.Path.DirectorySeparatorChar;    //3
            if (!System.IO.Directory.Exists(Folder))
                System.IO.Directory.CreateDirectory(Folder);

            string CheckdatFile = Folder + Default.Checkdat;
            string ListdatFile = Folder + Default.Listdat;
            string PortdatFile = Folder + Default.Portdat;
            string HistdatFile = Folder + Default.Histdat;

            System.IO.File.WriteAllText(HistdatFile, SplitedText[0], mc.enc);    //HistdatFile
            System.IO.File.WriteAllText(ListdatFile, SplitedText[1], mc.enc);    //ListdatFile
            System.IO.File.WriteAllText(PortdatFile, SplitedText[2], mc.enc);    //PortdatFile
            System.IO.File.WriteAllText(CheckdatFile, SplitedText[3], mc.enc);    //CheckdatFile

            if (!System.IO.File.Exists(CheckdatFile))
            {
                System.Windows.Forms.MessageBox.Show(string.Format("Check.datファイルが見つかりません。({0})", CheckdatFile), Default.ApplicationName);
                this.Close();
            }
            mc.loadCheckdat(myDataSetItems.CheckDat, CheckdatFile);
            mc.loadListdatFile(myDataSetItems.ListDat, ListdatFile);
            mc.loadPortdatFile(myDataSetItems.PortDat, PortdatFile);
            //Checkdat
            mc.loadListdatFile(myDataSetItems.ListDatResult, HistdatFile, true);    //20180731 HostView 
            for (int i = 0; i < myDataSetItems.ListDat.Rows.Count; i++)
            {
                myDataSetItems.ListDat.Rows[i]["Result"] = myDataSetItems.ListDatResult[i]["Result"];
            }
            //////mc.debugPrintDT(myDataSetItems.ListDat);
            //////mc.debugPrintDT(myDataSetItems.ListDatResult);
            //結果を取り出し
            string origFolder = System.IO.Path.GetDirectoryName(historyFile);
            string origFilename = System.IO.Path.GetFileName(historyFile);
            string resultCsvPass = string.Format("{0}{1:yyyy}-log.csv", origFolder + System.IO.Path.DirectorySeparatorChar, origFilename.Substring(1, 4));
            //string rusultCsvText = System.IO.File.ReadAllText(resultCsvPass, mc.enc);
            string rusultCsvText = mc.ReadAllText(resultCsvPass, mc.enc);

            //A20160617_143132.dat -> "2016.05.10 16:34:33"
            string resultDate = string.Format("{0}.{1}.{2} {3}:{4}:{5}",
                origFilename.Substring(1, 4),
                origFilename.Substring(5, 2),
                origFilename.Substring(7, 2),
                origFilename.Substring(10, 2),
                origFilename.Substring(12, 2),
                origFilename.Substring(14, 2)
                );
            List<List<string>> rusultCsv = mc.CsvToArrayList2(rusultCsvText);
            foreach (List<string> rusultColumns in rusultCsv)
            {
                if (resultDate == rusultColumns[0])
                {
                    //0                      1            2   3           4   5          6
                    //"2016.06.17 14:09:40","20160617-2","1","329-00620","T","通常検査","Pass"
                    this.textBox_Serial.Text = rusultColumns[1];    //製造番号
                    this.textBox_GoNo.Text = rusultColumns[2];    //号機
                    this.textBox_Status.Text = rusultColumns[5];    //通常検査
                    if (rusultColumns[6] == "Pass")
                    {
                        this.label_ResultOK.Visible = true;
                        this.label_ResultNG.Visible = false;
                    }
                    else
                    {
                        this.label_ResultOK.Visible = false;
                        this.label_ResultNG.Visible = true;
                    }
                }
            }
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
            this.Close();
        }

        private void button_Next_Click(object sender, EventArgs e)
        {
        }

        private void button_Stop_Click(object sender, EventArgs e)
        {
        }

        private void button_DataInput_Click(object sender, EventArgs e)
        {
            //mForms.ShowDialog(this, mForms.fmDataInput);
            mForms.fmDataInput.ShowDialog();
        }

        private void button_ResultView_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DataGridView dv = this.dataGridView_Inspection;

            //frmResultView fmResultView = mForms.fmResultView;
            ResultView fmResultView = new ResultView(_aio);  //20170126
            fmResultView.myDataSetItems = myDataSetItems;
            fmResultView.TNo = dv.CurrentCell.RowIndex;
            fmResultView.ShowDialog();
            fmResultView.Dispose(); //20170126
        }

        private void button_Debug_Click(object sender, EventArgs e)
        {
        }

        private void button_TNoPlus_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DataGridView dv = this.dataGridView_Inspection;
            if (dv.CurrentCell.RowIndex + 1 < dv.Rows.Count)
            {
                dv.CurrentCell = dv[dv.CurrentCell.ColumnIndex, dv.CurrentCell.RowIndex + 1];
                this.textBox_TestNo.Text = (dv.CurrentCell.RowIndex + 1).ToString();
            }
        }

        private void button_TNoMinus_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DataGridView dv = this.dataGridView_Inspection;
            if (dv.CurrentCell.RowIndex > 0)
            {
                dv.CurrentCell = dv[dv.CurrentCell.ColumnIndex, dv.CurrentCell.RowIndex - 1];
                this.textBox_TestNo.Text = (dv.CurrentCell.RowIndex + 1).ToString();
            }
        }

        private void dataGridView_Inspection_CurrentCellChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.DataGridView dv = (System.Windows.Forms.DataGridView)sender;

            //if (dv.CurrentCell != null)
            //{
            //    this.m_CurrentTNo = dv.CurrentCell.RowIndex;
            //}
        }

        private void timerInspect_Tick(object sender, EventArgs e)
        {
        }

        private void timerDrawing_Tick(object sender, EventArgs e)
        {
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmMain_FormClosing");
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //stat clear
            mc.InspecStat = MeasureCondition.enumInspectStat.Stat_STOP;
            //timerDrawing
            this.timerDrawing.Enabled = false;
            this.timerReadSw.Enabled = false;
            this.timerInspect.Enabled = false;
            //Dio close
            dio.Exit();
            _aio.Exit();
        }

        private void timerReadSw_Tick(object sender, EventArgs e)
        {
        }

        private void dataGridView_Inspection_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_Inspection.CurrentCell != null)
            {
                int TNo = dataGridView_Inspection.CurrentCell.RowIndex;
                this.textBox_TestNo.Text = (TNo + 1).ToString();
                DataRow dtListDatROW = myDataSetItems.ListDat.Rows[TNo];
                this.textBox_InspectType.Text = getInspectType(dtListDatROW["Type"].ToString());
                this.textBox_CurrentResult.Text = dtListDatROW["Result"].ToString();
                richTextConvert(this.textBox_Guide, dtListDatROW["Guide"].ToString(), TNo);     //20181004
            }

        }
        private string getInspectType(string InspectType)
        {
            int inst_Type;
            if (int.TryParse(InspectType, out inst_Type))
            {
                if (inst_Type >= 0 && inst_Type < Default.inspection_TypeText.Length)
                {
                    return Default.inspection_TypeText[inst_Type];
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        private void mnuPrint_Click(object sender, EventArgs e)
        {
            screenShot.GetInstance().PrintForm(this, true);
        }

        private void mnuEnd_Click(object sender, EventArgs e)
        {
            button_Close_Click(sender, e);
        }

        private void dataGridView_Inspection_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            if (e.RowIndex < myDataSetItems.ListDatResult.Rows.Count)
            {
                DataRow dtListDatResultRow = myDataSetItems.ListDatResult.Rows[e.RowIndex];

                string Value = e.Value.ToString();
                if (dtListDatResultRow["Result"].ToString() == "Fail")
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Cyan;
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Cyan;
                    //dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;
                }
                else
                {
                    //dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.White;
                    //dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.White;
                    ////dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Black;
                }
            }
            else
            {
                //dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.White;
                //dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.White;
            }
        }

        private void dataGridView_Inspection_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button_ResultView_Click(sender, e);
        }

        private void mnuView_Click(object sender, EventArgs e)
        {
            button_ResultView_Click(sender, e);
        }

        string lastrichText = null;  //20181003
        int lastrichTextTno = -1;  //20181003
        string PlaySoundWav = null; //20181003   Guideに*.wavがあったら、wavファイルが入る。通常のwavの代わりに鳴らす
        private void richTextConvert(System.Windows.Forms.RichTextBox RichTextBox1, string baseText, int TNo)
        {
            //System.Windows.Forms.RichTextBox RichTextBox1 = this.richTextBox1;
            //string baseText = textBox1.Text;
            if (lastrichText == baseText && lastrichTextTno == TNo) return;
            lastrichText = baseText;
            lastrichTextTno = TNo;
            PlaySoundWav = null;

            //Regexオブジェクトを作成
            System.Text.RegularExpressions.Regex r =
                new System.Text.RegularExpressions.Regex(
                    @"\{.*?\}",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //BoidをFontStyleに追加したFontを作成する
            Font baseFont = RichTextBox1.SelectionFont;
            Font fnt = new Font(baseFont.FontFamily,
                baseFont.Size,
                baseFont.Style | FontStyle.Bold);

            RichTextBox1.Text = null;
            while (baseText.Length >= 0)
            {
                //TextBox1.Text内で正規表現と一致する対象を1つ検索
                System.Text.RegularExpressions.Match m = r.Match(baseText);

                int Pos = -1;
                if ((m.Success))
                {
                    Pos = baseText.IndexOf(m.Value);
                    if (Pos == -1) break;   // 基本的に、ここにはこないはず
                    string pretext = baseText.Substring(0, Pos);
                    baseText = baseText.Substring(Pos + m.Value.Length);

                    string keyWord = m.Value.Substring(1);  //先頭の｛を取る
                    keyWord = keyWord.Substring(0, keyWord.Length - 1); //最後の｝を取る

                    if (keyWord.Length >= 5 && keyWord.Substring(keyWord.Length - 4).ToUpper() == ".WAV")    //a.wav?
                    {
                        //文字列を挿入する
                        RichTextBox1.SelectedText = pretext;    //20180913 {*.wav}の前を表示する
                        //ここで音を鳴らす
                        PlaySoundWav = keyWord;
                    }
                    else
                    {
                        Color color1 = Color.Empty;
                        try
                        {
                            //色コードとして処理
                            color1 = ColorTranslator.FromHtml(keyWord);
                            if (color1 == Color.Black)    //背景が白なので、黄色に置き換えない
                            {
                                color1 = Color.FromArgb(255, 255, 128);
                            }
                        }
                        catch
                        {
                        }
                        //文字列を挿入する
                        RichTextBox1.SelectedText = pretext;

                        //赤にする
                        //RichTextBox1.SelectionColor = Color.Red;
                        if (color1 != Color.Empty)
                        {
                            RichTextBox1.SelectionColor = color1;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            //最後の文字列を挿入する
            if (baseText.Length > 0)
            {
                RichTextBox1.SelectedText = baseText;
            }
        }
    }
}