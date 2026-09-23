using NippoControlSystem.Infrastructure.Configuration;
using NippoControlSystem.Infrastructure.Devices;
using NippoControlSystem.UI.Controls;
using System.ComponentModel;
using System.Data;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.UI.Views
{
    public partial class DataInputLimitView : Form
    {
        //　------　MVVMパターン用にリファクタリングしたコード　------

        //　------　MVVM化のためにリファクタリングする前のコード　------

        Settings Default = Settings.GetInstance();
        cDio dio = cDio.GetInstance();
        NippoDIO nio = NippoDIO.GetInstance();
        private readonly Aio _aio;
        MeasureCondition mc = MeasureCondition.GetInstance();
        Views mForms = Views.GetInstance();

        System.Windows.Forms.DataGridView[] dataGridView_DIO = null;
        //System.Windows.Forms.DataGridView[] dataGridView_AIO = null;
        //System.Windows.Forms.DataGridView[] dataGridView_GND = null;
        //Local 変数
        //int TNo = 0;
        DataGridView dataGridView_Entered = null;

        Font fontRegularlstyle = new Font("ＭＳ Ｐゴシック", 9, FontStyle.Regular);
        Font fontBoldstyle = new Font("ＭＳ Ｐゴシック", 9, FontStyle.Bold);
        //Font fontRegularlstyle = new Font("ＭＳ ゴシック", 9, FontStyle.Regular);
        //Font fontBoldstyle = new Font("ＭＳ ゴシック", 9, FontStyle.Bold);

        //FirstPageView
        int delayedViewNo = 0;
        int delayedViewRowIndex = 0;


        public DataInputLimitView(Aio aio)
        {
            _aio = aio;
            InitializeComponent();
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ myDataSetItems
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public DataSetItems myDataSetItems
        {
            get;
            set;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ TNo
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int TNo
        {
            get;
            set;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ PageNo
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int PageNo
        {
            get;
            set;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ PageNo
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string SelectionCell
        {
            get;
            set;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ frmDataInputLMT_Load
        private void frmDataInputLMT_Load(object sender, EventArgs e)
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            //Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmDataInputLMT"]);
            //Forms.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmDataInputLMT"]);

            dataGridView_DIO = new DataGridView[4];
            //dataGridView_AIO = new DataGridView[2];
            //dataGridView_GND = new DataGridView[8];
            dataGridView_DIO[0] = this.dataGridLMT_DA;
            dataGridView_DIO[1] = this.dataGridLMT_DB;
            dataGridView_DIO[2] = this.dataGridLMT_DC;
            dataGridView_DIO[3] = this.dataGridLMT_DD;
            //dataGridView_DIO[4] = this.dataGridView_DE;
            //dataGridView_DIO[5] = this.dataGridView_DF;
            //dataGridView_DIO[6] = this.dataGridView_DG;
            //dataGridView_DIO[7] = this.dataGridView_DH;
            //dataGridView_AIO[0] = this.dataGridView_AO;
            //dataGridView_AIO[1] = this.dataGridView_AI;
            //dataGridView_GND[0] = this.dataGridView_GndDA;
            //dataGridView_GND[1] = this.dataGridView_GndDB;
            //dataGridView_GND[2] = this.dataGridView_GndDC;
            //dataGridView_GND[3] = this.dataGridView_GndDD;
            //dataGridView_GND[4] = this.dataGridView_GndDE;
            //dataGridView_GND[5] = this.dataGridView_GndDF;
            //dataGridView_GND[6] = this.dataGridView_GndDG;
            //dataGridView_GND[7] = this.dataGridView_GndDH;

            //Checkdat
            this.bindingSourceCheckDat.DataSource = myDataSetItems;

            //DataSourceを定義する
            //for (int i = 0; i < Default.DioNames.Length; i++)
            //{
            //    if (dataGridView_DIO[i] != null)
            //    {
            //        dataGridView_DIO[i].DataSource = myDataSetItems;
            //        dataGridView_DIO[i].DataMember = string.Format("View_{0}LMT", Default.DioNames[i+4]);
            //    }
            //}
            //this.dataGridView_AO.DataSource = myDataSetItems;
            //this.dataGridView_AO.DataMember = myDataSetItems.View_AO.TableName;
            //this.dataGridView_AI.DataSource = myDataSetItems;
            //this.dataGridView_AI.DataMember = myDataSetItems.View_AI.TableName;
            //for (int i = 0; i < dataGridView_AIO.Length; i++)
            //{
            //    foreach (DataGridViewColumn item in dataGridView_AIO[i].Columns)
            //    {
            //        item.SortMode = DataGridViewColumnSortMode.NotSortable;
            //    }
            //}

            //dtView_GndDIO
            //for (int i = 0; i < dataGridView_GND.Length; i++)
            //{
            //    dataGridView_GND[i].DataSource = myDataSetItems;
            //    dataGridView_GND[i].DataMember = string.Format("View_{0}", Default.GndNames[i]);
            //}
            //this.dataGridView_GndAIO.DataSource = myDataSetItems;
            //this.dataGridView_GndAIO.DataMember = "View_GndAIO";

            //20180906  
            //12V,24V
            this.comboBox_Volt.ReadOnly = true;
            string strVolt = myDataSetItems.CheckDat.Rows[0]["Volt"].ToString();
            if (strVolt == "1")
            {
                this.comboBox_Volt.Text = "24V";
            }
            else
            {
                this.comboBox_Volt.Text = "12V";
            }

            //
            MainMenu1.Renderer = new ToolStripSystemRenderer();

            // Visualスタイルを使用しない
            dataGridLMT_DA.EnableHeadersVisualStyles = false;

            dataGridView_DA_Display();
            allClearSelection();
            this.textBox_TNo.Text = string.Format("{0}", TNo + 1);
            //dataGridView_DA_Dat(TNo); //↓のPageSettingで行うのでここでは不要
            if (string.IsNullOrEmpty(this.SelectionCell))
            {
                PageSetting(0);
            }
            else
            {
                FirstPageSetting(this.SelectionCell);
            }

            //Color
            this.label_iDb電流下限値.ForeColor = Default.CellStyles[(int)NippoDIO.IO_STAT.iDb - (int)NippoDIO.IO_STAT.oOP].ForeColor;
            this.label_iDc電流下限値.ForeColor = Default.CellStyles[(int)NippoDIO.IO_STAT.iDc - (int)NippoDIO.IO_STAT.oOP].ForeColor;
            this.label_iDs電流下限値.ForeColor = Default.CellStyles[(int)NippoDIO.IO_STAT.iDs - (int)NippoDIO.IO_STAT.oOP].ForeColor;

            //this.Refresh();
            this.TimerReadSw.Enabled = true;

        }

        private void allClearSelection()
        {
            //dataGridView_AI.ClearSelection();
            //dataGridView_AO.ClearSelection();
            //dataGridView_GndAIO.ClearSelection();

            for (int i = 0; i < dataGridView_DIO.Length; i++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
            {
                dataGridView_DIO[i].CurrentCell = null;
                dataGridView_DIO[i].ClearSelection();
            }
            //for (int i = 0; i < dataGridView_GND.Length; i++)     // { "GndDA", "GndDB", "GndDC", "GndDD", "GndDE", "GndDF", "GndDG", "GndDH", "GndAO", "GndAI" }
            //{
            //    dataGridView_GND[i].CurrentCell = null;
            //    dataGridView_GND[i].ClearSelection();
            //}
        }

        private void dataGridView_DA_Display()
        {
            DataRow dtPortDatRow = myDataSetItems.PortDat.Rows[0];
            for (int i = 0; i < Default.DioNames.Length; i++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
            {
                DataTable dtView_DIOLMT = myDataSetItems.Tables[string.Format("View_{0}LMT", Default.DioNames[i])];
                //for (int j = 0; j < (i == 7 ? 28 : Default.DioNums[i]); j++)    //32loop,Hは28に変更
                for (int j = 0; j < Default.DioNums[i]; j++)    //32loop
                {
                    int jj = j + 1;
                    string iFeildName = string.Format("{0}-{1:00}", Default.DioNames[i], jj);
                    dtView_DIOLMT.Rows[j]["Name"] = dtPortDatRow[iFeildName];
                    //dtView_DIOLMT.Rows[j]["Name"] = !string.IsNullOrEmpty(dtPortDatRow[iFeildName].ToString()) ? dtPortDatRow[iFeildName] : Default.DioNames[i] + "-" + jj.ToString("00");
                }
                dtView_DIOLMT.AcceptChanges();
            }
            ////dtView_AI
            //{
            //    DataTable dtView_AI = myDataSetItems.Tables[string.Format("View_{0}", Default.AiName)];
            //    for (int j = 0; j < Default.AiNum; j++)    //32loop
            //    {
            //        int ii = j + 1;
            //        string iFeildName = string.Format("{0}-{1:0}", Default.AiName, ii);
            //        dtView_AI.Rows[j]["Name"] = dtPortDatRow[iFeildName];
            //    }
            //    dtView_AI.AcceptChanges();
            //}
            ////dtView_AO
            //{
            //    DataTable dtView_AO = myDataSetItems.Tables[string.Format("View_{0}", Default.AoName)];
            //    for (int j = 0; j < Math.Max(Default.AoNum, Default.AoSwichNum); j++)    //32loop
            //    {
            //        int ii = j + 1;
            //        string iFeildName = string.Format("{0}-{1:0}", Default.AoName, ii);
            //        dtView_AO.Rows[j]["Name"] = dtPortDatRow[iFeildName];
            //    }
            //    dtView_AO.AcceptChanges();
            //}
            ////dtView_GndDIO
            //for (int i = 0; i < Default.GndNames.Length; i++)     // { "GndDA", "GndDB", "GndDC", "GndDD", "GndDE", "GndDF", "GndDG", "GndDH", "GndAO", "GndAI" }
            //{
            //    DataTable dtView_GndDIO = myDataSetItems.Tables[string.Format("View_{0}", Default.GndNames[i])];
            //    DataTable dtView_GndAIO = myDataSetItems.Tables["View_GndAIO"];
            //    for (int j = 0; j < Default.GndNums[i]; j++)    //5loop
            //    {
            //        int jj = j + 33;
            //        string iFeildName = string.Format("{0}-{1:0}", Default.GndNames[i], j + 1);
            //        dtView_GndDIO.Rows[j]["Name"] = dtPortDatRow[iFeildName];
            //        if (Default.GndNames[i] == "GndAO")
            //        {
            //            dtView_GndAIO.Rows[j]["AoName"] = dtPortDatRow[iFeildName];
            //        }
            //        else if (Default.GndNames[i] == "GndAI")
            //        {
            //            dtView_GndAIO.Rows[j]["AiName"] = dtPortDatRow[iFeildName];
            //        }
            //    }
            //    dtView_GndDIO.AcceptChanges();
            //}
        }


        private void dataGridView_DA_Dat(int TNo)
        {
            DataRow dtListDatRow = myDataSetItems.ListDat.Rows[TNo];
            //DataRow dtListDatResultRow = myDataSetItems.ListDat.Rows[TNo]; ;
            //if (myDataSetItems.ListDatResult.Rows.Count > 0) dtListDatResultRow = myDataSetItems.ListDatResult.Rows[TNo];
            textBox_Title.Text = dtListDatRow["Title"].ToString();
            textBox_Guide.Text = dtListDatRow["Guide"].ToString();
            //comboBox_Type.SelectedIndex = int.Parse(dtListDatRow["Type"].ToString());
            //textBox_Type.Text = dtListDatRow["Type"].ToString();
            int inst_Type;
            if (int.TryParse(dtListDatRow["Type"].ToString(), out inst_Type))
            {
                if (inst_Type >= 0 && inst_Type < Default.inspection_TypeText.Length)
                {
                    this.textBox_Type.Text = Default.inspection_TypeText[inst_Type];
                }
                else
                {
                    this.textBox_Type.Text = "";
                }
            }
            else
            {
                this.textBox_Type.Text = "";
            }
            //20180830
            if (string.IsNullOrEmpty(dtListDatRow["iDs-LoLMT"].ToString()))
            {
                textBox_iDs電流下限値.Text = myDataSetItems.CheckDat.Rows[0]["iDs-LoLMT"].ToString();
            }
            else
            {
                textBox_iDs電流下限値.Text = dtListDatRow["iDs-LoLMT"].ToString();
            }
            if (string.IsNullOrEmpty(dtListDatRow["iDs-HiLMT"].ToString()))
            {
                textBox_iDs電流上限値.Text = myDataSetItems.CheckDat.Rows[0]["iDs-HiLMT"].ToString();
            }
            else
            {
                textBox_iDs電流上限値.Text = dtListDatRow["iDs-HiLMT"].ToString();
            }

            string[] CheckDatLMTLo = new string[(int)NippoDIO.IO_STAT.iTo - (int)NippoDIO.IO_STAT.iOP + 1];
            string[] CheckDatLMTHi = new string[(int)NippoDIO.IO_STAT.iTo - (int)NippoDIO.IO_STAT.iOP + 1];
            string CheckDatiDsLo = "";
            string CheckDatiDsHi = "";
            //LMT
            int iDo_Length = (int)NippoDIO.IO_STAT.iTo - (int)NippoDIO.IO_STAT.iOP + 1;
            string dtCheckDatFields;
            for (int i = 0; i < iDo_Length; i++)
            {
                dtCheckDatFields = ((NippoDIO.IO_STAT)((int)NippoDIO.IO_STAT.iOP + i)).ToString() + Default.CheckDatLMTFields[0];     //Hi
                CheckDatLMTHi[i] = (myDataSetItems.CheckDat.Rows[0][dtCheckDatFields].ToString());    //iOP-HiLMT～iTo-HiLMT
                dtCheckDatFields = ((NippoDIO.IO_STAT)((int)NippoDIO.IO_STAT.iOP + i)).ToString() + Default.CheckDatLMTFields[1];     //Lo
                CheckDatLMTLo[i] = (myDataSetItems.CheckDat.Rows[0][dtCheckDatFields].ToString());    //iOP-LoLMT～iTo-LoLMT
                if (i == ((int)NippoDIO.IO_STAT.iDs - (int)NippoDIO.IO_STAT.iOP))
                {
                    CheckDatiDsHi = CheckDatLMTHi[i];   //各Step毎のiDs-HiLMT、iDs-LoLMTが空欄に備えて、初期値を保存しておく
                    CheckDatiDsLo = CheckDatLMTLo[i];
                }
            }
            //iDs ステップ毎の上下限値を設定,Nullの時は、Checkdatの初期値を設定
            if (string.IsNullOrEmpty(dtListDatRow["iDs-LoLMT"].ToString()))
            {
                CheckDatLMTLo[(int)NippoDIO.IO_STAT.iDs - (int)NippoDIO.IO_STAT.iOP] = CheckDatiDsLo;
            }
            else
            {
                CheckDatLMTLo[(int)NippoDIO.IO_STAT.iDs - (int)NippoDIO.IO_STAT.iOP] = (dtListDatRow["iDs-LoLMT"].ToString());  //iDs-LoLMT
            }
            if (string.IsNullOrEmpty(dtListDatRow["iDs-HiLMT"].ToString()))
            {
                CheckDatLMTHi[(int)NippoDIO.IO_STAT.iDs - (int)NippoDIO.IO_STAT.iOP] = CheckDatiDsHi;
            }
            else
            {
                CheckDatLMTHi[(int)NippoDIO.IO_STAT.iDs - (int)NippoDIO.IO_STAT.iOP] = (dtListDatRow["iDs-HiLMT"].ToString());  //iDs-HiLMT
            }

            for (int i = 0; i < Default.DioNames.Length; i++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
            {
                if ((this.PageNo == 0 && i < 4) || (this.PageNo != 0 && i >= 4))
                {
                    DataTable dtView_DIOLMT = myDataSetItems.Tables[string.Format("View_{0}LMT", Default.DioNames[i])];
                    for (int j = 0; j < Default.DioNums[i]; j++)    //32loop
                    {
                        int jj = j + 1;
                        string iFeildName = string.Format("{0}-{1:00}", Default.DioNames[i], jj);
                        string DoStat = dtListDatRow[iFeildName].ToString();
                        string LmtH = ""; ;
                        string LmtL = ""; ;

                        dtView_DIOLMT.Rows[j]["MeasValue"] = DoStat;
                        NippoDIO.IO_STAT statCurr = (NippoDIO.IO_STAT)mc.dicDioStat[DoStat]; //現在の設定
                        switch (statCurr)
                        {
                            case NippoDIO.IO_STAT.iOP:
                            case NippoDIO.IO_STAT.iGN:
                            case NippoDIO.IO_STAT.iHi:
                            case NippoDIO.IO_STAT.iDo:
                            case NippoDIO.IO_STAT.iDh:
                            case NippoDIO.IO_STAT.iDb:
                            case NippoDIO.IO_STAT.iDc:
                            case NippoDIO.IO_STAT.iDs:
                            case NippoDIO.IO_STAT.iTo:
                                LmtH = CheckDatLMTHi[(int)statCurr - (int)NippoDIO.IO_STAT.iOP];
                                LmtL = CheckDatLMTLo[(int)statCurr - (int)NippoDIO.IO_STAT.iOP];
                                break;

                            case NippoDIO.IO_STAT.nHi:
                            case NippoDIO.IO_STAT.nOP:
                            case NippoDIO.IO_STAT.nGN:
                            case NippoDIO.IO_STAT.nDo:
                            case NippoDIO.IO_STAT.nDh:
                            case NippoDIO.IO_STAT.nDb:
                            case NippoDIO.IO_STAT.nDc:
                            case NippoDIO.IO_STAT.nDs:
                            case NippoDIO.IO_STAT.nTo:
                                LmtH = CheckDatLMTHi[(int)statCurr - (int)NippoDIO.IO_STAT.nOP];
                                LmtL = CheckDatLMTLo[(int)statCurr - (int)NippoDIO.IO_STAT.nOP];
                                break;

                            case NippoDIO.IO_STAT.iDp:
                            case NippoDIO.IO_STAT.nDp:
                                LmtH = (dtListDatRow[iFeildName + "H"].ToString());  //iOP-HiLMT
                                LmtL = (dtListDatRow[iFeildName + "L"].ToString());  //iOP-LoLMT
                                break;

                            case NippoDIO.IO_STAT.dat:
                                LmtH = "";
                                LmtL = "";
                                break;

                            default:
                                LmtH = "";
                                LmtL = "";
                                break;
                        }
                        dtView_DIOLMT.Rows[j]["LowLMT"] = LmtL;
                        dtView_DIOLMT.Rows[j]["HighLMT"] = LmtH;
                    }
                    dtView_DIOLMT.AcceptChanges();
                }
            }
            allClearSelection();
            this.Refresh();
        }

        //private void dataGridView_DA_Restore(int TNo)
        //{
        //    DataRow dtListDatRow = myDataSetItems.ListDat.Rows[TNo];
        //    //20180830
        //    dtListDatRow["iDs-LoLMT"] = textBoxiDs電流下限値.Text.ToString();
        //    dtListDatRow["iDs-HiLMT"] = textBoxiDs電流上限値.Text.ToString();
        //}

        private void button_Next_Click(object sender, EventArgs e)
        {
            this.label_Busy.Visible = true;
            this.label_Busy.Refresh();
            //if (TNo < myDataSetItems.ListDat.Rows.Count)
            //{
            //    dataGridView_DA_Restore(TNo);
            //}

            if (TNo < (myDataSetItems.ListDat.Rows.Count - 1))
            {
                TNo++;
                this.textBox_TNo.Text = string.Format("{0}", TNo + 1);
                this.textBox_TNo.Refresh();
                //dataGridView_DA_Dat(TNo);
                //this.Refresh();
            }
            this.label_Busy.Visible = false;
            this.label_Busy.Refresh();
        }

        private void button_Priv_Click(object sender, EventArgs e)
        {
            this.label_Busy.Visible = true;
            this.label_Busy.Refresh();
            //if (TNo < myDataSetItems.ListDat.Rows.Count)
            //{
            //    dataGridView_DA_Restore(TNo);
            //}
            if (TNo > 0)
            {
                TNo--;
                this.textBox_TNo.Text = string.Format("{0}", TNo + 1);
                this.textBox_TNo.Refresh();
                //dataGridView_DA_Dat(TNo);
                //this.Refresh();
            }
            this.label_Busy.Visible = false;
            this.label_Busy.Refresh();
        }

        private void dataGridView_DA_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            string DoStat = "";

            if (e.ColumnIndex == 3)
            {
                if (e.Value == null) return;    //20180731
                string Value = e.Value.ToString();
                DoStat = getDioStat(dataGridView1, e.RowIndex);
                string[] DoStatSplit = DoStat.Split(':');
                DoStat = (DoStatSplit.Length >= 1) ? DoStatSplit[0] : "";
                if (Value.Length > 0)
                {
                    //if (Value.Substring(0, 1) == "o")
                    //{
                    //    if (string.IsNullOrEmpty(DoStat))
                    //    {
                    //        if (Value != "oGN")
                    //        {
                    //            dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;
                    //            dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.Red;
                    //            dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.White;
                    //        }
                    //        else
                    //        {
                    //            //oGNの場合は、黒のままなので、Colorの設定を変更しない20140419
                    //        }
                    //    }
                    //    else
                    //    {
                    //        dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.White;
                    //        dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.White;
                    //        dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;
                    //    }
                    //}
                    //else
                    //{
                    //    if (string.IsNullOrEmpty(DoStat))
                    //    {
                    //        //dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Black;
                    //        //dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.Black;
                    //        ////dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Black;
                    //        if (Value.Length > 0 && Value == "iDh")    //20170419
                    //        {
                    //            dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Blue;
                    //            dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.Blue;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.White;
                    //        dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.White;
                    //        dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;
                    //    }
                    //}
                    //if (Value.Length > 0 && (Value == "iDh" || Value == "oHi" || Value == "oGN"))   //20170417
                    //{
                    //    dataGridView1[e.ColumnIndex, e.RowIndex].Style.Font = fontBoldstyle;
                    //}
                    //else
                    //{
                    //    dataGridView1[e.ColumnIndex, e.RowIndex].Style.Font = fontRegularlstyle;
                    //}
                    string DoStatLocal = Value;
                    if (mc.dicDioStat.ContainsKey(DoStatLocal))
                    {
                        NippoDIO.IO_STAT statCurr = (NippoDIO.IO_STAT)mc.dicDioStat[DoStatLocal]; //現在の設定

                        int CellStylesIdx = (int)statCurr - (int)NippoDIO.IO_STAT.oOP;
                        if (CellStylesIdx >= (int)NippoDIO.IO_STAT.nOP)
                        {
                            CellStylesIdx -= ((int)NippoDIO.IO_STAT.nOP - 3);
                        }
                        //if (Default.CellStyles[CellStylesIdx].ForeColor != Color.Black)
                        //{
                        //    dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Default.CellStyles[CellStylesIdx].ForeColor;
                        //}
                        //if (Default.CellStyles[CellStylesIdx].SelectionForeColor != Color.Black)
                        //{
                        //    dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Default.CellStyles[CellStylesIdx].ForeColor;
                        //}
                        //if (Default.CellStyles[CellStylesIdx].fontstyle != System.Drawing.FontStyle.Regular)
                        //{
                        //    dataGridView1[e.ColumnIndex, e.RowIndex].Style.Font = fontBoldstyle;
                        //}
                        if (dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor != Default.CellStyles[CellStylesIdx].ForeColor)
                        {
                            dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Default.CellStyles[CellStylesIdx].ForeColor;
                        }
                        if (dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor != Default.CellStyles[CellStylesIdx].ForeColor)
                        {
                            dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Default.CellStyles[CellStylesIdx].ForeColor;
                        }
                        if ((dataGridView1[e.ColumnIndex, e.RowIndex].Style.Font == null) || (dataGridView1[e.ColumnIndex, e.RowIndex].Style.Font.Style != Default.CellStyles[CellStylesIdx].fontstyle))
                        {
                            if (Default.CellStyles[CellStylesIdx].fontstyle == System.Drawing.FontStyle.Regular)
                            {
                                dataGridView1[e.ColumnIndex, e.RowIndex].Style.Font = fontRegularlstyle;
                            }
                            else
                            {
                                dataGridView1[e.ColumnIndex, e.RowIndex].Style.Font = fontBoldstyle;
                            }
                        }
                    }
                }
            }
        }


        private string getDioStat(DataGridView dataGridView1, int RowIndex)
        {
            string iFeildName;
            string DoStat = "";
            DataTable dtListDatResult = myDataSetItems.ListDatResult;
            for (int i = 0; i < dataGridView_DIO.Length; i++)
            {
                if (dataGridView1.Equals(dataGridView_DIO[i]))
                {
                    //Found dataGridView1
                    iFeildName = string.Format("{0}-{1:00}", Default.DioNames[i + this.PageNo * 4], RowIndex + 1);
                    if (TNo < dtListDatResult.Rows.Count)
                    {
                        DoStat = dtListDatResult.Rows[TNo][iFeildName].ToString();
                        break;
                    }
                }
            }
            return DoStat;
        }

        private void dataGridView_DA_Leave(object sender, EventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            dataGridView1.ClearSelection();
        }

        private void dataGridView_DA_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            string iFeildName;
            string DoStat;

            if (e.RowIndex == -1 || e.ColumnIndex != 3)
                return;

            //Error表示を行う
            DataTable dtListDatResult = myDataSetItems.ListDatResult;
            if (TNo < dtListDatResult.Rows.Count)
            {
                for (int i = 0; i < dataGridView_DIO.Length; i++)
                {
                    if (dataGridView1.Equals(dataGridView_DIO[i]))
                    {
                        iFeildName = string.Format("{0}-{1:00}", Default.DioNames[i + this.PageNo * 4], e.RowIndex + 1);
                        DoStat = dtListDatResult.Rows[TNo][iFeildName].ToString();
                        string[] DoStatSplit = DoStat.Split(':');
                        DoStat = (DoStatSplit.Length >= 1) ? DoStatSplit[0] : "";
                        if (!string.IsNullOrEmpty(DoStat))
                        {
                            e.ToolTipText = DoStat;
                        }
                        else
                        {
                            e.ToolTipText = myDataSetItems.ListDat.Rows[TNo][iFeildName].ToString();    //20180731 エラーなしの時は、Statを表示
                        }
                        break;
                    }
                }
            }
        }

        private void frmDataInputLMT_Shown(object sender, EventArgs e)
        {
            allClearSelection();
        }

        //private void dataGridView_AI_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    DataGridView dataGridView1 = (DataGridView)sender;
        //    //string DoLowerValue = "";
        //    //string DoUpperValue = "";
        //    string AiStat = "";
        //    //float LowerValue = float.MinValue;
        //    //float UpperValue = float.MaxValue;

        //    if (e.ColumnIndex == 3)
        //    {
        //        string Value = e.Value.ToString();
        //        //if (float.TryParse(Value, out thisValue))
        //        //{
        //        //DoLowerValue = dataGridView1[2, e.RowIndex].ToString();
        //        //if (!string.IsNullOrEmpty(DoLowerValue))
        //        //{
        //        //    if (float.TryParse(DoLowerValue, out LowerValue))
        //        //    { }
        //        //}
        //        //DoUpperValue = dataGridView1[4, e.RowIndex].ToString();
        //        //if (!string.IsNullOrEmpty(DoUpperValue))
        //        //{
        //        //    if (float.TryParse(DoUpperValue, out UpperValue))
        //        //    { }
        //        //}
        //        //if (thisValue >= LowerValue && thisValue <= UpperValue)
        //        //{
        //        //}
        //        //else
        //        //{
        //        //    dataGridView1[e.ColumnIndex, e.RowIndex].Value = DoValue;
        //        //}
        //        //dataGridView1[e.ColumnIndex, e.RowIndex].Value = DoValue;

        //        AiStat = getAiStat(e.RowIndex);
        //        if (string.IsNullOrEmpty(AiStat))
        //        {
        //            //範囲内
        //            //dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Black;
        //            //dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.Black;
        //            ////dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Black;
        //        }
        //        else
        //        {
        //            //範囲外
        //            dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.White;
        //            dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.White;
        //            dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;
        //            //}
        //        }
        //    }
        //}

        //private void dataGridView_AO_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    DataGridView dataGridView1 = (DataGridView)sender;

        //    if (e.RowIndex >= Default.AoNum && e.ColumnIndex == 2)
        //    {
        //        //BackColor,AO値が不要な2～8は、非入力にする
        //        dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.FromArgb(255, 192, 192);
        //        dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.FromArgb(255, 192, 192);
        //        dataGridView1[e.ColumnIndex, e.RowIndex].ReadOnly = true;
        //    }
        //}

        //private string getAiStat(int RowIndex)
        //{
        //    string iFeildName;
        //    string AiStat = "";
        //    DataTable dtListDatResult = myDataSetItems.ListDatResult;
        //    //Found dataGridView1
        //    iFeildName = string.Format("{0}-{1:0}L", Default.AiName, RowIndex + 1);
        //    if (TNo < dtListDatResult.Rows.Count)
        //    {
        //        AiStat = dtListDatResult.Rows[TNo][iFeildName].ToString();
        //    }
        //    return AiStat;
        //}


        //private string getAioValue(DataGridView dataGridView1, int RowIndex)
        //{
        //    string iFeildName;
        //    string DoStat = "";
        //    DataTable dtListDatResult = myDataSetItems.ListDatResult;
        //    for (int i = 0; i < dataGridView_DIO.Length; i++)
        //    {
        //        if (dataGridView1.Equals(dataGridView_DIO[i]))
        //        {
        //            //Found dataGridView1
        //            iFeildName = string.Format("{0}-{1:00}", Default.AiName, RowIndex + 1);
        //            if (TNo < dtListDatResult.Rows.Count)
        //            {
        //                DoStat = dtListDatResult.Rows[TNo][iFeildName].ToString();
        //                break;
        //            }
        //        }
        //    }
        //    return DoStat;
        //}

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.TimerReadSw.Enabled = false;

            this.Close();
        }

        private void textBox_TNo_TextChanged(object sender, EventArgs e)
        {
            int Tn = 0;
            if (int.TryParse(this.textBox_TNo.Text, out Tn))
            {
                if (Tn > 0 && Tn <= myDataSetItems.ListDat.Rows.Count)
                {
                    TNo = Tn - 1;
                    dataGridView_DA_Dat(TNo);
                    this.Refresh();
                }
                else
                {
                }
            }
            else
            {
            }
        }

        private void textBox_TNo_Validating(object sender, CancelEventArgs e)
        {
            int Tn = 0;
            if (int.TryParse(this.textBox_TNo.Text, out Tn))
            {
                if (Tn > 0 && Tn <= myDataSetItems.ListDat.Rows.Count)
                {
                }
                else
                {
                    this.errorProvider1.SetError((TextBox)sender, "整数を入力して下さい");
                    // e.Cancel = true　でCancel を true にすると正しく入力しないと次に行けない。
                    e.Cancel = true;
                }
            }
            else
            {
                this.errorProvider1.SetError((TextBox)sender, "整数を入力して下さい");
                // e.Cancel = true　でCancel を true にすると正しく入力しないと次に行けない。
                e.Cancel = true;
            }
        }

        private void textBox_TNo_Validated(object sender, EventArgs e)
        {
            this.errorProvider1.SetError((TextBox)sender, null);
        }

        private void frmDataInputLMT_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmDataInputLMT_FormClosing");
        }

        private void frmDataInputLMT_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void TimerReadSw_Tick(object sender, EventArgs e)
        {
            if (GreenSwitchDown())
            {
                button_Close_Click(this.button_Close, new EventArgs());
            }
        }

        private bool GreenSwitchDown()
        {
            if (mc.GreenSwitchStat != 0)
            {
                //一度ONしたら、OFFするまで、無視する
                if (this.switchLabelGreenSwitch.LampValue == 0 && _aio.getGreenSw() == 0)
                {
                    mc.GreenSwitchStat = 0;
                }
                return false;
            }
            else
            {
                //switchLabelGreenSwitch.LampValueは、ON=1、aio.getGreenSw()はON=1
                if (this.switchLabelGreenSwitch.LampValue == 0 && _aio.getGreenSw() == 0)
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
            //if (stopButtonClick)
            //{
            //    stopButtonClick = false;
            //    return true;
            //}
            if (mc.RedSwitchStat != 0)
            {
                //一度ONしたら、OFFするまで、無視する
                if (this.switchLabelRedSwitch.LampValue == 0 && _aio.getRedSw() == 0)
                {
                    mc.RedSwitchStat = 0;
                }
                return false;
            }
            else
            {
                //switchLabelGreenSwitch.LampValueは、ON=1、aio.getGreenSw()はON=1
                if (this.switchLabelRedSwitch.LampValue == 0 && _aio.getRedSw() == 0)
                {
                    //両方OFFなら、falseで抜ける
                    return false;
                }
            }
            mc.RedSwitchStat = 1;   //次のOFFまでは動作しないフラグ
            this.switchLabelRedSwitch.LampValue = 0;
            return true;
        }

        private void mnuPrint_Click(object sender, EventArgs e)
        {
            screenShot.GetInstance().PrintForm(this, true);
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            button_Close_Click(sender, e);
        }

        //private void dataGridView_AO_MouseMove(object sender, MouseEventArgs e)
        //{
        //}

        //private void dataGridView_AO_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    DataGridView dataGridView1 = (DataGridView)sender;
        //    if (e.RowIndex < 0)
        //    {
        //        switch (e.ColumnIndex)
        //        {
        //            case -1:
        //            case 0:
        //            case 1:
        //                dataGridView1.CurrentCell = null;
        //                //dataGridView1.ClearSelection();
        //                dataGridView1.Columns[0].Selected = false;
        //                dataGridView1.Columns[1].Selected = false;
        //                dataGridView1.Columns[2].Selected = true;
        //                dataGridView1.Columns[3].Selected = true;
        //                break;
        //        }
        //    }
        //}

        //private void dataGridView_AO_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    DataGridView dataGridView1 = (DataGridView)sender;

        //    System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AI_CellMouseDown:{0}:{1}", e.ColumnIndex, e.RowIndex));
        //    if (e.RowIndex < 0)
        //    {
        //        if (dataGridView1.SelectionMode != DataGridViewSelectionMode.ColumnHeaderSelect)
        //        {
        //            System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AI_CellMouseDown X={0} Y{1} SelectionMode={2}", e.Location.X, e.Location.Y, "ColumnHeaderSelect"));
        //            dataGridView1.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
        //        }
        //    }
        //    else
        //    {
        //        switch (e.ColumnIndex)
        //        {
        //            case -1:
        //            case 0:
        //            case 1:
        //                if (dataGridView1.SelectionMode != DataGridViewSelectionMode.FullRowSelect)
        //                {
        //                    System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AI_CellMouseDown X={0} Y{1} SelectionMode={2}", e.Location.X, e.Location.Y, "RowHeaderSelect"));
        //                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //                }
        //                break;
        //            case 2:
        //            case 3:
        //                System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AI_CellMouseDown X={0} Y{1} SelectionMode={2}", e.Location.X, e.Location.Y, "RowHeaderSelect"));
        //                dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
        //                break;
        //        }
        //    }
        //}

        //private void dataGridView_AI_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    DataGridView dataGridView1 = (DataGridView)sender;
        //    if (e.RowIndex < 0)
        //    {
        //        switch (e.ColumnIndex)
        //        {
        //            case -1:
        //            case 0:
        //            case 1:
        //                dataGridView1.CurrentCell = null;
        //                //dataGridView1.ClearSelection();
        //                dataGridView1.Columns[0].Selected = false;
        //                dataGridView1.Columns[1].Selected = false;
        //                dataGridView1.Columns[2].Selected = true;
        //                dataGridView1.Columns[3].Selected = true;
        //                dataGridView1.Columns[4].Selected = true;
        //                break;
        //        }
        //    }
        //}

        //private void dataGridView_AI_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    DataGridView dataGridView1 = (DataGridView)sender;

        //    System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AI_CellMouseDown:{0}:{1}", e.ColumnIndex, e.RowIndex));
        //    if (e.RowIndex < 0)
        //    {
        //        if (dataGridView1.SelectionMode != DataGridViewSelectionMode.ColumnHeaderSelect)
        //        {
        //            System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AI_CellMouseDown X={0} Y{1} SelectionMode={2}", e.Location.X, e.Location.Y, "ColumnHeaderSelect"));
        //            dataGridView1.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
        //        }
        //    }
        //    else
        //    {
        //        switch (e.ColumnIndex)
        //        {
        //            case -1:
        //            case 0:
        //            case 1:
        //                if (dataGridView1.SelectionMode != DataGridViewSelectionMode.FullRowSelect)
        //                {
        //                    System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AI_CellMouseDown X={0} Y{1} SelectionMode={2}", e.Location.X, e.Location.Y, "RowHeaderSelect"));
        //                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //                }
        //                break;
        //            case 2:
        //            case 3:
        //                System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AI_CellMouseDown X={0} Y{1} SelectionMode={2}", e.Location.X, e.Location.Y, "RowHeaderSelect"));
        //                dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
        //                break;
        //        }
        //    }
        //}

        private void dataGridView_DA_Enter(object sender, EventArgs e)
        {
            if (dataGridView_Entered != null)
            {
                dataGridView_Entered.ClearSelection();
            }
            dataGridView_Entered = (DataGridView)sender;
        }

        //private void dataGridView_AO_Enter(object sender, EventArgs e)
        //{
        //    if (dataGridView_Entered != null)
        //    {
        //        dataGridView_Entered.ClearSelection();
        //    }
        //    dataGridView_Entered = (DataGridView)sender;
        //}

        //private void dataGridView_AI_Enter(object sender, EventArgs e)
        //{
        //    if (dataGridView_Entered != null)
        //    {
        //        dataGridView_Entered.ClearSelection();
        //    }
        //    dataGridView_Entered = (DataGridView)sender;
        //}

        private void dataGridView_DA_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            int StartEnableColumn = GetStartEnableColumn(dataGridView1);

            //if (e.KeyData == (Keys.Control | Keys.V))
            //{
            //    // Ctrl + V
            //    System.Diagnostics.Debug.WriteLine("Ctrl + V が押されました。");
            //    //dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
            //    string DataGridViewType = whichDataGridView(dataGridView1);
            //    switch (DataGridViewType)
            //    {
            //        case "D":
            //            libDataGridView.dataGridView_Paste(dataGridView1, StartEnableColumn, new libDataGridView.delegateCanPaste(CanPasteDA));
            //            break;

            //        case "O":
            //            libDataGridView.dataGridView_Paste(dataGridView1, StartEnableColumn, new libDataGridView.delegateCanPaste(CanPasteAO));
            //            break;

            //        case "I":
            //            libDataGridView.dataGridView_Paste(dataGridView1, StartEnableColumn, new libDataGridView.delegateCanPaste(CanPasteAI));
            //            break;
            //    }

            //}
            //else if (e.KeyData == (Keys.Control | Keys.X))
            //{
            //    // Ctrl + X
            //    System.Diagnostics.Debug.WriteLine("Ctrl + X が押されました。");
            //    //選択されたセルをクリップボードにコピーする
            //    //Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
            //    libDataGridView.dataGridView_Copy(dataGridView1, StartEnableColumn);
            //    //dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
            //    libDataGridView.dataGridView_Clear(dataGridView1, StartEnableColumn);
            //}
            //else 
            if (e.KeyData == (Keys.Control | Keys.C))
            {
                // Ctrl + C
                System.Diagnostics.Debug.WriteLine("Ctrl + C が押されました。");
                //選択されたセルをクリップボードにコピーする
                libDataGridView.dataGridView_Copy(dataGridView1, StartEnableColumn, getiDpLMT);
                e.SuppressKeyPress = true;
            }
            //else if (e.KeyData == (Keys.Delete))
            //{
            //    // Delete
            //    System.Diagnostics.Debug.WriteLine("Delete が押されました。");
            //    //dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
            //    libDataGridView.dataGridView_Clear(dataGridView1, StartEnableColumn);
            //}
            //else if (e.KeyData == (Keys.Enter))
            //{
            //    DataGridViewCellEventArgs ee = new DataGridViewCellEventArgs(dataGridView1.SelectedCells[0].ColumnIndex, dataGridView1.SelectedCells[0].RowIndex);
            //    string DataGridViewType = whichDataGridView(dataGridView1);
            //    switch (DataGridViewType)
            //    {
            //        case "D":
            //            dataGridView_DA_CellDoubleClick(sender, ee);
            //            break;

            //        case "O":
            //            dataGridView_AO_CellDoubleClick(sender, ee);
            //            break;

            //        case "I":
            //            dataGridView_AI_CellDoubleClick(sender, ee);
            //            break;
            //    }
            //}
        }

        private string getiDpLMT(DataGridView dataGridView1, int insertColumnIndex, int insertRowIndex)
        {
            return "";  //Portは、常に""
        }

        //private void dataGridView_AO_KeyDown(object sender, KeyEventArgs e)
        //{
        //    dataGridView_DA_KeyDown(sender, e);
        //}

        //private void dataGridView_AI_KeyDown(object sender, KeyEventArgs e)
        //{
        //    dataGridView_DA_KeyDown(sender, e);
        //}

        private void dataGridView_DA_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            if (e.RowIndex < 0)
            {
                dataGridView1.SelectAll();
            }
        }

        private int GetStartEnableColumn(DataGridView dataGridView1)
        {
            //if (dataGridView1.Equals(this.dataGridView_AO) || dataGridView1.Equals(this.dataGridView_AI))
            //{
            //    return 2;
            //}
            //else
            //{
            return 2;
            //}
        }

        const int SlLampOn = 1;
        const int SlLampOff = 0;
        private void switchLabelGreenSwitch_MouseDown(object sender, MouseEventArgs e)
        {
            this.switchLabelGreenSwitch.LampValue = SlLampOn;
        }

        private void switchLabelGreenSwitch_MouseLeave(object sender, EventArgs e)
        {
            this.switchLabelGreenSwitch.LampValue = SlLampOff;
        }

        private void switchLabelGreenSwitch_MouseUp(object sender, MouseEventArgs e)
        {
            this.switchLabelGreenSwitch.LampValue = SlLampOff;
        }

        private void switchLabelRedSwitch_MouseDown(object sender, MouseEventArgs e)
        {
            this.switchLabelRedSwitch.LampValue = SlLampOn;
        }

        private void switchLabelRedSwitch_MouseLeave(object sender, EventArgs e)
        {
            this.switchLabelRedSwitch.LampValue = SlLampOff;
        }

        private void switchLabelRedSwitch_MouseUp(object sender, MouseEventArgs e)
        {
            this.switchLabelRedSwitch.LampValue = SlLampOff;
        }

        private void mnuBefore_Click(object sender, EventArgs e)
        {
            button_Priv_Click(sender, e);
        }

        private void nmuNext_Click(object sender, EventArgs e)
        {
            button_Next_Click(sender, e);
        }

        //private void dataGridView_AI_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        //{
        //    DataGridView dataGridView1 = (DataGridView)sender;
        //    //dataGridView_AO_CellContextMenuStriped = dataGridView1;
        //    //右clickだと、Leaveがはいらないので、ここでLeave相当をする
        //    if (dataGridView_Entered != null)
        //    {
        //        dataGridView_Entered.ClearSelection();
        //        dataGridView_Entered = null;
        //    }
        //    dataGridView_Entered = dataGridView1;

        //}

        //private void dataGridView_AO_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        //{
        //    DataGridView dataGridView1 = (DataGridView)sender;
        //    //dataGridView_AO_CellContextMenuStriped = dataGridView1;
        //    //右clickだと、Leaveがはいらないので、ここでLeave相当をする
        //    if (dataGridView_Entered != null)
        //    {
        //        dataGridView_Entered.ClearSelection();
        //        dataGridView_Entered = null;
        //    }
        //    dataGridView_Entered = dataGridView1;

        //}

        private void dataGridView_DA_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            //dataGridView_DA_CellContextMenuStriped = dataGridView1;
            //右clickだと、Leaveがはいらないので、ここでLeave相当をする
            if (dataGridView_Entered != null)
            {
                dataGridView_Entered.ClearSelection();
                dataGridView_Entered = null;
            }
            dataGridView_Entered = dataGridView1;

            if (e.RowIndex < 0)
            {
                //列ヘッダーに表示するContextMenuStripを設定する
                //e.ContextMenuStrip = this.contextMenuStripDA;
                //dataGridView_DA_CellMouseClick(sender, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));   //右クリックでも選択する
            }
            else if (e.ColumnIndex < 0)
            {
                //行ヘッダーに表示するContextMenuStripを設定する
                //e.ContextMenuStrip = this.contextMenuStripDA;
                //dataGridView_DA_CellMouseClick(sender, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));   //右クリックでも選択する
            }
            else if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                dataGridView_Entered[e.ColumnIndex, e.RowIndex].Selected = true;

                //セルが整数型のときに表示するContextMenuStripを変更する
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                //e.ContextMenuStrip = this.contextMenuStripDA;
            }

        }

        private void pictureBoxPage1_Click(object sender, EventArgs e)
        {
            if (this.PageNo != 0)
            {
                PageSetting(0);
            }
        }

        private void pictureBoxPage2_Click(object sender, EventArgs e)
        {
            if (this.PageNo == 0)
            {
                PageSetting(1);
            }
        }

        private void FirstPageSetting(string SelectionCellName)
        {
            if (SelectionCellName.Length != 3) return;
            char charSelectView = SelectionCellName[0];
            delayedViewRowIndex = 0;
            delayedViewNo = 0;
            if (charSelectView >= 'A' && charSelectView <= 'H')
            {
                delayedViewNo = charSelectView - 'A';
            }
            if (int.TryParse(SelectionCellName.Substring(1), out delayedViewRowIndex))
            {
                int pageNo = (delayedViewNo < 4 ? 0 : 1);
                PageSetting(pageNo);
                this.timer_selectionView.Enabled = true;  //Click行を選択するとき
            }
        }

        private void PageSetting(int Page)
        {
            this.PageNo = Page == 0 ? 0 : 1;   //PageNo 0 or 1
            this.textBoxPageNo.Text = (this.PageNo + 1).ToString();

            for (int i = 0; i < dataGridView_DIO.Length; i++)
            {
                dataGridView_DIO[i].DataSource = myDataSetItems;
                dataGridView_DIO[i].DataMember = string.Format("View_{0}LMT", Default.DioNames[i + this.PageNo * 4]);
            }
            if (this.PageNo == 0)
            {
                this._Label1_A.Text = "デジタル A";
                this._Label1_B.Text = "デジタル B";
                this._Label1_C.Text = "デジタル C";
                this._Label1_D.Text = "デジタル D";
                this._Label1_A.BackColor = Color.FromArgb(255, 224, 192);
                this._Label1_B.BackColor = Color.FromArgb(255, 224, 192);
                this._Label1_C.BackColor = Color.FromArgb(255, 224, 192);
                this._Label1_D.BackColor = Color.FromArgb(255, 224, 192);

                for (int i = 0; i < dataGridView_DIO.Length; i++)
                {
                    dataGridView_DIO[i].ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 224, 192);
                    dataGridView_DIO[i].Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 192);
                    dataGridView_DIO[i].Columns[0].DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 224, 192);
                    dataGridView_DIO[i].Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 192);
                    dataGridView_DIO[i].Columns[1].DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 224, 192);
                }
            }
            else
            {
                this._Label1_A.Text = "デジタル E";
                this._Label1_B.Text = "デジタル F";
                this._Label1_C.Text = "デジタル G";
                this._Label1_D.Text = "デジタル H";
                this._Label1_A.BackColor = Color.FromArgb(255, 255, 192);
                this._Label1_B.BackColor = Color.FromArgb(255, 255, 192);
                this._Label1_C.BackColor = Color.FromArgb(255, 255, 192);
                this._Label1_D.BackColor = Color.FromArgb(255, 255, 192);
                for (int i = 0; i < dataGridView_DIO.Length; i++)
                {
                    dataGridView_DIO[i].ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192);
                    dataGridView_DIO[i].Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192);
                    dataGridView_DIO[i].Columns[0].DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 192);
                    dataGridView_DIO[i].Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192);
                    dataGridView_DIO[i].Columns[1].DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 192);
                }
            }
            //dataGridView_DA_Display();
            dataGridView_DA_Dat(this.TNo);
        }

        private void timer_selectionView_Tick(object sender, EventArgs e)
        {
            int selViewNo = delayedViewNo % dataGridView_DIO.Length;
            dataGridView_DIO[selViewNo].Focus();
            dataGridView_DIO[selViewNo].CurrentCell = dataGridView_DIO[selViewNo][2, delayedViewRowIndex];
            this.timer_selectionView.Enabled = false;
        }
    }
}