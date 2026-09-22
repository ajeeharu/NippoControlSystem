using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.UI.Views
{
    public partial class DataInputView : Form
    {
        //　------　MVVMパターン用にリファクタリングしたコード　------

        //　------　MVVM化のためにリファクタリングする前のコード　------

        Cyc.IO.Settings Default = Cyc.IO.Settings.GetInstance();
        //Cyc.IO.cDio dio = Cyc.IO.cDio.GetInstance();
        //Cyc.IO.NippoDIO nio = Cyc.IO.NippoDIO.GetInstance();
        MeasureCondition mc = MeasureCondition.GetInstance();
        Views mForms = Views.GetInstance();

        System.Windows.Forms.DataGridView[] dataGridView_DIO = null;
        System.Windows.Forms.DataGridView[] dataGridView_AIO = null;
        System.Windows.Forms.DataGridView[] dataGridView_GND = null;
        //Local 変数
        DataGridView dataGridView_DA_CellContextMenuStriped = null;
        DataGridView dataGridView_Entered = null;
        int TNo = 0;

        const int ColumnIndexIOdataField = 2;
        const int ColumnIndexAOdataField = 2;
        const int ColumnIndexAOswField = 3;

        Font fontRegularlstyle = new Font("ＭＳ Ｐゴシック", 9, FontStyle.Regular);
        Font fontBoldstyle = new Font("ＭＳ Ｐゴシック", 9, FontStyle.Bold);
        //Font fontRegularlstyle = new Font("ＭＳ ゴシック", 9, FontStyle.Regular);
        //Font fontBoldstyle = new Font("ＭＳ ゴシック", 9, FontStyle.Bold);

        public DataInputView()
        {
            InitializeComponent();
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ myDataSetItems
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataSetItems myDataSetItems
        {
            get;
            set;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ TNo
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int StartTNo
        {
            get;
            set;
        }

        [SupportedOSPlatform("windows")]
        private void frmDataInput_Load(object sender, EventArgs e)
        {
            // ランタイムでプラットフォームをチェックし、Windows以外ではWindows専用APIの呼び出しをスキップする
            if (!OperatingSystem.IsWindows())
            {
                // 非Windows環境では、Windows固有の初期化を行わない
                return;
            }

            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmDataInput"]);
            Views.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmDataInput"]);

            dataGridView_DIO = new DataGridView[8];
            dataGridView_AIO = new DataGridView[2];
            dataGridView_GND = new DataGridView[9];
            dataGridView_DIO[0] = this.dataGridView_DA;
            dataGridView_DIO[1] = this.dataGridView_DB;
            dataGridView_DIO[2] = this.dataGridView_DC;
            dataGridView_DIO[3] = this.dataGridView_DD;
            dataGridView_DIO[4] = this.dataGridView_DE;
            dataGridView_DIO[5] = this.dataGridView_DF;
            dataGridView_DIO[6] = this.dataGridView_DG;
            dataGridView_DIO[7] = this.dataGridView_DH;
            dataGridView_AIO[0] = this.dataGridView_AO;
            dataGridView_AIO[1] = this.dataGridView_AI;
            dataGridView_GND[0] = this.dataGridView_GndDA;
            dataGridView_GND[1] = this.dataGridView_GndDB;
            dataGridView_GND[2] = this.dataGridView_GndDC;
            dataGridView_GND[3] = this.dataGridView_GndDD;
            dataGridView_GND[4] = this.dataGridView_GndDE;
            dataGridView_GND[5] = this.dataGridView_GndDF;
            dataGridView_GND[6] = this.dataGridView_GndDG;
            dataGridView_GND[7] = this.dataGridView_GndDH;
            dataGridView_GND[8] = this.dataGridView_GndAIO;

            //DataSourceを定義する
            for (int i = 0; i < Default.DioNames.Length; i++)
            {
                dataGridView_DIO[i].DataSource = myDataSetItems;
                dataGridView_DIO[i].DataMember = string.Format("View_{0}", Default.DioNames[i]);
            }
            this.dataGridView_AO.DataSource = myDataSetItems;
            this.dataGridView_AO.DataMember = myDataSetItems.View_AO.TableName;
            this.dataGridView_AI.DataSource = myDataSetItems;
            this.dataGridView_AI.DataMember = myDataSetItems.View_AI.TableName;
            //for (int i = 0; i < dataGridView_AIO.Length; i++)
            //{
            //    foreach (DataGridViewColumn item in dataGridView_AIO[i].Columns)
            //    {
            //        item.SortMode = DataGridViewColumnSortMode.NotSortable;
            //    }
            //}

            //dtView_GndDIO
            for (int i = 0; i < dataGridView_GND.Length; i++)
            {
                dataGridView_GND[i].DataSource = myDataSetItems;
                dataGridView_GND[i].DataMember = string.Format("View_{0}", Default.GndNames[i]);
            }
            this.dataGridView_GndAIO.DataSource = myDataSetItems;
            this.dataGridView_GndAIO.DataMember = "View_GndAIO";
            libDataGridView.DebugPrintDataTable(myDataSetItems, "View_GndAIO");

            //
            MainMenu1.Renderer = new ToolStripSystemRenderer();

            //DataGridViewにショートカットメニューを表示する
            contextMenuStripDA.Items.Clear();
            for (int i = 0; i < mnuEdit.DropDownItems.Count; i++)   //コピペ用
            {
                contextMenuStripDA.Items.Add(mnuEdit.DropDownItems[i].Text, null, toolStripMenuItemDA_Click);
            }
            contextMenuStripAO.Items.Clear();    //AOのEnable用
            for (int i = 0; i < mnuEdit.DropDownItems.Count; i++)   //コピペ用
            {
                contextMenuStripAO.Items.Add(mnuEdit.DropDownItems[i].Text, null, toolStripMenuItemAO_Click);
            }
            contextMenuStripAI.Items.Clear();    //AIのEnable用
            for (int i = 0; i < mnuEdit.DropDownItems.Count; i++)   //コピペ用
            {
                contextMenuStripAI.Items.Add(mnuEdit.DropDownItems[i].Text, null, toolStripMenuItemAI_Click);
            }

            // Visualスタイルを使用しない
            dataGridView_DA.EnableHeadersVisualStyles = false;

            TNo = StartTNo;
            dataGridView_DA_Display();
            //dataGridView_DA_Dat(TNo);   //TNoは、0から、表示のときに＋１する
            textBox_TNo.Text = (TNo + 1).ToString();
            textBox_TNo_TextChanged(this.textBox_TNo, new EventArgs()); //20170125
        }

        private void toolStripMenuItemDA_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem iTem = (ToolStripMenuItem)sender;
            DataGridView dataGridView1 = dataGridView_DA_CellContextMenuStriped;
            int StartEnableColumn = GetStartEnableColumn(dataGridView1);

            System.Diagnostics.Debug.WriteLine(string.Format("toolStripMenuItem_Click:{0}", iTem.Text));
            //iTem.Textには、入力Open、入力GNDが入るので、文字列で確認する。
            for (int i = 0; i < mnuEdit.DropDownItems.Count; i++)
            {
                if (iTem.Equals(contextMenuStripDA.Items[i]))
                {
                    if (dataGridView1.SelectedCells != null)
                    {
                        ////i番目のメニュが選択された
                        //選択されているセルを表示
                        switch (i)
                        {
                            case 0:     //Input
                                int minSelectedColumn = libDataGridView.minSelectedColumn(dataGridView1);
                                int minSelectedRows = libDataGridView.minSelectedRows(dataGridView1);
                                DataGridViewCellEventArgs ee = new DataGridViewCellEventArgs(minSelectedColumn, minSelectedRows);
                                dataGridView_DA_CellDoubleClick(dataGridView1, ee);
                                break;
                            case 1:     //Cut
                                //選択されたセルをクリップボードにコピーする
                                //Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
                                libDataGridView.dataGridView_Copy(dataGridView1, StartEnableColumn, getiDpLMT);
                                //dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                                libDataGridView.dataGridView_Clear(dataGridView1, StartEnableColumn);
                                break;
                            case 2:     //Copy
                                //選択されたセルをクリップボードにコピーする
                                //Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
                                libDataGridView.dataGridView_Copy(dataGridView1, StartEnableColumn, getiDpLMT);
                                break;
                            case 3:     //Paste
                                //dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                                //libDataGridView.dataGridView_Paste(dataGridView1, StartEnableColumn);   //
                                libDataGridView.dataGridView_Paste(dataGridView1, StartEnableColumn, new libDataGridView.delegateCanPaste(CanPasteDA), new libDataGridView.delegateSetiDpLMT(SetiDpLMT));
                                break;
                        }

                        break;
                    }
                }
            }
        }

        private string getiDpLMT(DataGridView dataGridView1, int insertColumnIndex, int insertRowIndex)
        {
            //DataGridView dataGridView1 = (DataGridView)sender;
            //該当のCellがiDp/nDpなら、Limit値を返す
            string returnString = "";

            System.Diagnostics.Debug.WriteLine(string.Format("getiDpLMT:{0},{1}", insertColumnIndex, insertRowIndex));
            string CellValue = dataGridView1[insertColumnIndex, insertRowIndex].Value.ToString();
            if (CellValue == "iDp" || CellValue == "nDp")   //20180816
            {
                for (int i = 0; i < dataGridView_DIO.Length; i++)
                {
                    if (dataGridView1.Equals(dataGridView_DIO[i]))
                    {
                        if (insertRowIndex >= 0)
                        {
                            DataRow dtListDatRow = myDataSetItems.ListDat.Rows[TNo];
                            int jj = insertRowIndex + 1;
                            string iFeildName = string.Format("{0}-{1:00}", Default.DioNames[i], jj);
                            //dtView_DIO.Rows[j]["IO"] = dtListDatRow[iFeildName];
                            returnString = string.Format(":{0}:{1}", dtListDatRow[iFeildName + "L"], dtListDatRow[iFeildName + "H"]);
                        }
                    }
                }
            }
            return returnString;
        }

        bool CanPasteDA(string val, int ColumnIndex, int RowIndex)
        {
            if (val == "") return true;

            if (ColumnIndex == 0) return false;
            if (ColumnIndex == 1) return false;
            //if (ColumnIndex == 2 && mc.dicDioStat.ContainsKey(val)) return true;
            if (ColumnIndex == 2 && mc.dicDioStat.ContainsKey(val.IndexOf(":") == -1 ? val : (val.Split(':'))[0])) return true; //20180806 iDpｺﾋﾟﾍﾟ対応
            return false;
        }

        bool SetiDpLMT(DataGridView dataGridView1, int ColumnIndex, int RowIndex, string setValue)  //20180817
        {
            //DataGridView dataGridView1 = (DataGridView)sender;
            //該当のCellがiDp/nDpなら、Limit値をSetする

            System.Diagnostics.Debug.WriteLine(string.Format("SetiDpLMT:{0},{1}", ColumnIndex, RowIndex));

            for (int i = 0; i < dataGridView_DIO.Length; i++)
            {
                if (dataGridView1.Equals(dataGridView_DIO[i]))
                {
                    if (RowIndex >= 0)
                    {
                        DataRow dtListDatRow = myDataSetItems.ListDat.Rows[TNo];
                        int jj = RowIndex + 1;
                        string iFeildName = string.Format("{0}-{1:00}", Default.DioNames[i], jj);

                        string CellValue = dataGridView1[ColumnIndex, RowIndex].Value.ToString();
                        if (CellValue == "iDp" || CellValue == "nDp")   //20180816
                        {
                            //dtView_DIO.Rows[j]["IO"] = dtListDatRow[iFeildName];
                            //returnString = string.Format(":{0}:{1}", dtListDatRow[iFeildName + "L"], dtListDatRow[iFeildName + "H"]);
                            string[] setValueSplit = setValue.Split(':');
                            if (setValueSplit.Length == 3)
                            {
                                if (setValueSplit[0] == "iDp" || setValueSplit[0] == "nDp")
                                {
                                    dtListDatRow[iFeildName + "L"] = setValueSplit[1];
                                    dtListDatRow[iFeildName + "H"] = setValueSplit[2];
                                return true;
                                }
                                else
                                {
                                    //setValueが"iDp"/"nDp"でないときは、LMTをクリア 基本的にないはず
                                    dtListDatRow[iFeildName + "L"] = null;
                                    dtListDatRow[iFeildName + "H"] = null;
                                }
                            }
                            else
                            {
                                //LMTがないときは、LMTをクリア 基本的にないはず
                                dtListDatRow[iFeildName + "L"] = null;
                                dtListDatRow[iFeildName + "H"] = null;
                            }
                        }
                        else
                        {
                            //iDp/nDpでないときは、LMTをClear
                            dtListDatRow[iFeildName + "L"] = null;
                            dtListDatRow[iFeildName + "H"] = null;

                            return false;
                        }
                    }   //!if (RowIndex >= 0) //Cellでなければ、LMTのコピペは不要
                }   //!if (dataGridView1.Equals(dataGridView_DIO[i])) DIOでなければ、LMTのコピペは不要
            }

            return false;
        }

        private void toolStripMenuItemAO_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem iTem = (ToolStripMenuItem)sender;
            DataGridView dataGridView1 = dataGridView_DA_CellContextMenuStriped;
            int StartEnableColumn = GetStartEnableColumn(dataGridView1);

            System.Diagnostics.Debug.WriteLine(string.Format("toolStripMenuItem_Click:{0}", iTem.Text));
            //iTem.Textには、入力Open、入力GNDが入るので、文字列で確認する。
            for (int i = 0; i < mnuEdit.DropDownItems.Count; i++)
            {
                if (iTem.Text == mnuEdit.DropDownItems[i].Text)
                {
                    if (dataGridView1.SelectedCells != null)
                    {
                        ////i番目のメニュが選択された
                        //選択されているセルを表示
                        switch (i)
                        {
                            case 0:     //Input
                                int minSelectedColumn = libDataGridView.minSelectedColumn(dataGridView1);
                                int minSelectedRows = libDataGridView.minSelectedRows(dataGridView1);
                                DataGridViewCellEventArgs ee = new DataGridViewCellEventArgs(minSelectedColumn, minSelectedRows);
                                dataGridView_AO_CellDoubleClick(dataGridView1, ee);
                                break;
                            case 1:     //Cut
                                //選択されたセルをクリップボードにコピーする
                                //Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
                                libDataGridView.dataGridView_Copy(dataGridView1, StartEnableColumn, getiDpLMT);
                                //dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                                libDataGridView.dataGridView_Clear(dataGridView1, StartEnableColumn);
                                break;
                            case 2:     //Copy
                                //選択されたセルをクリップボードにコピーする
                                //Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
                                libDataGridView.dataGridView_Copy(dataGridView1, StartEnableColumn, getiDpLMT);
                                //dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                                break;
                            case 3:     //Paste
                                //dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                                libDataGridView.dataGridView_Paste(dataGridView1, StartEnableColumn, new libDataGridView.delegateCanPaste(CanPasteAO));
                                break;
                        }

                        break;
                    }
                }
            }
        }

        bool CanPasteAO(string val, int ColumnIndex, int RowIndex)
        {
            if (val == "") return true;

            if (ColumnIndex == 0) return false;
            if (ColumnIndex == 1) return false;
            if (ColumnIndex == 2 && RowIndex < 2 && AOinRange(val)) return true;
            if (ColumnIndex == 3 && (val == Default.AoSwichStat[0] || val == Default.AoSwichStat[1])) return true;
            return false;
        }

        bool AOinRange(string val)
        {
            double d;
            if (double.TryParse(val, out d))
            {
                return (Default.AoVoltMin <= d && d <= Default.AoVoltMax);
            }
            else
            {
                return false;
            }

        }

        private void toolStripMenuItemAI_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem iTem = (ToolStripMenuItem)sender;
            DataGridView dataGridView1 = dataGridView_DA_CellContextMenuStriped;
            int StartEnableColumn = GetStartEnableColumn(dataGridView1);

            System.Diagnostics.Debug.WriteLine(string.Format("toolStripMenuItem_Click:{0}", iTem.Text));
            //iTem.Textには、入力Open、入力GNDが入るので、文字列で確認する。
            for (int i = 0; i < mnuEdit.DropDownItems.Count; i++)
            {
                if (iTem.Text == mnuEdit.DropDownItems[i].Text)
                {
                    if (dataGridView1.SelectedCells != null)
                    {
                        ////i番目のメニュが選択された
                        //選択されているセルを表示
                        switch (i)
                        {
                            case 0:     //Input
                                int minSelectedColumn = libDataGridView.minSelectedColumn(dataGridView1);
                                int minSelectedRows = libDataGridView.minSelectedRows(dataGridView1);
                                DataGridViewCellEventArgs ee = new DataGridViewCellEventArgs(minSelectedColumn, minSelectedRows);
                                dataGridView_AI_CellDoubleClick(dataGridView1, ee);
                                break;
                            case 1:     //Cut
                                //選択されたセルをクリップボードにコピーする
                                //Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
                                libDataGridView.dataGridView_Copy(dataGridView1, StartEnableColumn, getiDpLMT);
                                //dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                                libDataGridView.dataGridView_Clear(dataGridView1, StartEnableColumn);
                                break;
                            case 2:     //Copy
                                //選択されたセルをクリップボードにコピーする
                                //Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
                                libDataGridView.dataGridView_Copy(dataGridView1, StartEnableColumn, getiDpLMT);
                                //dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                                break;
                            case 3:     //Paste
                                //dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                                libDataGridView.dataGridView_Paste(dataGridView1, StartEnableColumn, new libDataGridView.delegateCanPaste(CanPasteAI));
                                break;
                        }

                        break;
                    }
                }
            }
        }

        bool CanPasteAI(string val, int ColumnIndex, int RowIndex)
        {
            if (val == "") return true;

            if (ColumnIndex == 0) return false;
            if (ColumnIndex == 1) return false;
            if (ColumnIndex == 2 && AIinRange(val)) return true;
            if (ColumnIndex == 3 && AIinRange(val)) return true;
            return false;
        }

        bool AIinRange(string val)
        {
            double d;
            if (double.TryParse(val, out d))
            {
                return (Default.AiVoltMin <= d && d <= Default.AiVoltMax);
            }
            else
            {
                return false;
            }
        }

        private int GetStartEnableColumn(DataGridView dataGridView1)
        {
            if (dataGridView1.Equals(this.dataGridView_AO) || dataGridView1.Equals(this.dataGridView_AI))
            {
                return 2;
            }
            else
            {
                return 2;
            }
        }

        private void allClearSelection()
        {
            dataGridView_AI.ClearSelection();
            dataGridView_AO.ClearSelection();
            dataGridView_GndAIO.ClearSelection();

            for (int i = 0; i < Default.DioNames.Length; i++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
            {
                dataGridView_DIO[i].CurrentCell = null;
                dataGridView_DIO[i].ClearSelection();
            }
            for (int i = 0; i < dataGridView_GND.Length; i++)     // { "GndDA", "GndDB", "GndDC", "GndDD", "GndDE", "GndDF", "GndDG", "GndDH", "GndAO", "GndAI" }
            {
                dataGridView_GND[i].CurrentCell = null;
                dataGridView_GND[i].ClearSelection();
            }
        }

        private void dataGridView_DA_Display()
        {
            DataRow dtPortDatRow = myDataSetItems.PortDat.Rows[0];
            for (int i = 0; i < Default.DioNames.Length; i++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
            {
                DataTable dtView_DIO = myDataSetItems.Tables[string.Format("View_{0}", Default.DioNames[i])];
                //for (int j = 0; j < (i == 7 ? 28 : Default.DioNums[i]); j++)    //32loop,Hは28に変更
                for (int j = 0; j < Default.DioNums[i]; j++)    //32loop
                {
                    int jj = j + 1;
                    string iFeildName = string.Format("{0}-{1:00}", Default.DioNames[i], jj);
                    dtView_DIO.Rows[j]["Name"] = dtPortDatRow[iFeildName];
                }
                dtView_DIO.AcceptChanges();
            }
            //dtView_AI
            {
                DataTable dtView_AI = myDataSetItems.Tables[string.Format("View_{0}", Default.AiName)];
                for (int j = 0; j < Default.AiNum; j++)    //32loop
                {
                    int ii = j + 1;
                    string iFeildName = string.Format("{0}-{1:0}", Default.AiName, ii);
                    dtView_AI.Rows[j]["Name"] = dtPortDatRow[iFeildName];
                }
                dtView_AI.AcceptChanges();
            }
            //dtView_AO
            {
                DataTable dtView_AO = myDataSetItems.Tables[string.Format("View_{0}", Default.AoName)];
                for (int j = 0; j < Math.Max(Default.AoNum, Default.AoSwichNum); j++)    //32loop
                {
                    int ii = j + 1;
                    string iFeildName = string.Format("{0}-{1:0}", Default.AoName, ii);
                    dtView_AO.Rows[j]["Name"] = dtPortDatRow[iFeildName];
                }
                dtView_AO.AcceptChanges();
            }
            //dtView_GndDIO
            for (int i = 0; i < Default.GndNames.Length; i++)     // { "GndDA", "GndDB", "GndDC", "GndDD", "GndDE", "GndDF", "GndDG", "GndDH", "GndAO", "GndAI" }
            {
                DataTable dtView_GndDIO = myDataSetItems.Tables[string.Format("View_{0}", Default.GndNames[i])];
                DataTable dtView_GndAIO = myDataSetItems.Tables["View_GndAIO"];
                for (int j = 0; j < Default.GndNums[i]; j++)    //5loop
                {
                    int jj = j + 33;
                    string iFeildName = string.Format("{0}-{1:0}", Default.GndNames[i], j + 1);
                    dtView_GndDIO.Rows[j]["Name"] = dtPortDatRow[iFeildName];
                    if (Default.GndNames[i] == "GndAO")
                    {
                        dtView_GndAIO.Rows[j]["AoName"] = dtPortDatRow[iFeildName];
                    }
                    else if (Default.GndNames[i] == "GndAI")
                    {
                        dtView_GndAIO.Rows[j]["AiName"] = dtPortDatRow[iFeildName];
                    }
                }
                dtView_GndDIO.AcceptChanges();
            }
            ////PinをRowsHeader化 20161013
            //for (int i = 0; i < Default.DioNames.Length; i++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
            //{
            //    for (int j = 0; j < (i == 7 ? 28 : Default.DioNums[i]); j++)    //32loop,Hは28に変更
            //    {
            //        int jj = j + 1;
            //        //20161013
            //        this.dataGridView_DIO[i].Rows[j].HeaderCell.Value = jj.ToString();
            //    }
            //    this.dataGridView_DIO[i].TopLeftHeaderCell.Value = "Pin";
            //}
            ////dtView_AI
            //{
            //    for (int j = 0; j < Default.AiNum; j++)    //32loop
            //    {
            //        int ii = j + 1;
            //        this.dataGridView_AI.Rows[j].HeaderCell.Value = ii.ToString();
            //    }
            //    this.dataGridView_AI.TopLeftHeaderCell.Value = "Pin";
            //}
            ////dtView_AO
            //{
            //    for (int j = 0; j < Math.Max(Default.AoNum, Default.AoSwichNum); j++)    //32loop
            //    {
            //        int ii = j + 1;
            //        this.dataGridView_AO.Rows[j].HeaderCell.Value = ii.ToString();
            //    }
            //    this.dataGridView_AO.TopLeftHeaderCell.Value = "Pin";
            //}
            ////dtView_GndDIO
            //for (int i = 0; i < this.dataGridView_GND.Length; i++)     // { "GndDA", "GndDB", "GndDC", "GndDD", "GndDE", "GndDF", "GndDG", "GndDH", "GndAO", "GndAI" }
            //{
            //    for (int j = 0; j < Default.GndNums[i]; j++)    //5loop
            //    {
            //        int jj = j + 33;
            //        this.dataGridView_GND[i].Rows[j].HeaderCell.Value = jj.ToString();
            //    }
            //    this.dataGridView_GND[i].TopLeftHeaderCell.Value = "Pin";
            //}
        }


        private void dataGridView_DA_Dat(int TNo)
        {
            DataRow dtListDatRow = myDataSetItems.ListDat.Rows[TNo];
            textBox_Title.Text = dtListDatRow["Title"].ToString();
            textBox_Guide.Text = dtListDatRow["Guide"].ToString();
            if (string.IsNullOrEmpty(dtListDatRow["Type"].ToString()))
            {
                comboBox_Type.SelectedIndex = 0;
            }
            else
            {
                comboBox_Type.SelectedIndex = int.Parse(dtListDatRow["Type"].ToString());
            }
            //if (string.IsNullOrEmpty(dtListDatRow["iDs-LoLMT"].ToString()))   20180827
            //{
            //    textBoxiDs電流下限値.Text = myDataSetItems.CheckDat.Rows[0]["iDs-LoLMT"].ToString();
            //}
            //else
            //{
            //    textBoxiDs電流下限値.Text = dtListDatRow["iDs-LoLMT"].ToString();
            //}
            //if (string.IsNullOrEmpty(dtListDatRow["iDs-HiLMT"].ToString()))
            //{
            //    textBoxiDs電流上限値.Text = myDataSetItems.CheckDat.Rows[0]["iDs-HiLMT"].ToString();
            //}
            //else
            //{
            //    textBoxiDs電流上限値.Text = dtListDatRow["iDs-HiLMT"].ToString();
            //}

            for (int i = 0; i < Default.DioNames.Length; i++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
            {
                DataTable dtView_DIO = myDataSetItems.Tables[string.Format("View_{0}", Default.DioNames[i])];
                //for (int j = 0; j < (i == 7 ? 28 : Default.DioNums[i]); j++)    //32loop,Hは28に変更
                for (int j = 0; j < Default.DioNums[i]; j++)    //32loop
                {
                    int jj = j + 1;
                    string iFeildName = string.Format("{0}-{1:00}", Default.DioNames[i], jj);
                    //dtView_DIO.Rows[j]["IO"] = dtListDatRow[iFeildName];
                    DataGridView dataGridView1 = dataGridView_DIO[i];
                    dataGridView1[ColumnIndexIOdataField, j].Value = dtListDatRow[iFeildName];
                }
                dtView_DIO.AcceptChanges();
            }
            {
                DataTable dtView_AO = myDataSetItems.Tables[string.Format("View_{0}", Default.AoName)];
                for (int i = 0; i < Default.AoSwichNum; i++)     // AO
                {
                    int ii = i + 1;
                    string iFeildName = string.Format("{0}-{1:0}", Default.AoName, ii);
                    //this.dataGridView_AO.Rows.Add(ii.ToString(), dtListDatRow[iFeildName], null);
                    DataGridView dataGridView1 = dataGridView_AO;
                    if (i < Default.AoNum)
                    {
                        //dtView_AO.Rows[i]["Value"] = dtListDatRow[iFeildName];
                        dataGridView1[ColumnIndexIOdataField, i].Value = dtListDatRow[iFeildName];
                    }
                    iFeildName = string.Format("{0}-{1:0}", Default.AoSwichName, ii);
                    //dtView_AO.Rows[i]["Enable"] = dtListDatRow[iFeildName];
                    dataGridView1[ColumnIndexAOswField, i].Value = dtListDatRow[iFeildName];
                }
                dtView_AO.AcceptChanges();
            }
            {
                DataTable dtView_AI = myDataSetItems.Tables[string.Format("View_{0}", Default.AiName)];
                for (int i = 0; i < Default.AiNum; i++)     // AI
                {
                    int ii = i + 1;
                    string iFeildName = string.Format("{0}-{1:0}L", Default.AiName, ii);
                    DataGridView dataGridView1 = dataGridView_AI;

                    //dtView_AI.Rows[i]["Lower"] = dtListDatRow[iFeildName];
                    dataGridView1[ColumnIndexIOdataField, i].Value = dtListDatRow[iFeildName];
                    iFeildName = string.Format("{0}-{1:0}H", Default.AiName, ii);
                    //dtView_AI.Rows[i]["Upper"] = dtListDatRow[iFeildName];
                    dataGridView1[ColumnIndexAOswField, i].Value = dtListDatRow[iFeildName];
                }
                dtView_AI.AcceptChanges();
            }
            allClearSelection();
        }

        private void dataGridView_DA_Restore(int TNo)
        {
            DataRow dtListDatRow = myDataSetItems.ListDat.Rows[TNo];
            //textBox_Title.Text = dtListDatRow["Title"].ToString();
            //textBox_Guide.Text = dtListDatRow["Guide"].ToString();
            //comboBox_Type.SelectedIndex = int.Parse(dtListDatRow["Type"].ToString());
            dtListDatRow["Title"] = textBox_Title.Text.ToString();
            dtListDatRow["Guide"] = textBox_Guide.Text.ToString();
            dtListDatRow["Type"] = comboBox_Type.SelectedIndex;
            ////20180726,20180827
            //dtListDatRow["iDs-LoLMT"] = textBoxiDs電流下限値.Text.ToString();
            //dtListDatRow["iDs-HiLMT"] = textBoxiDs電流上限値.Text.ToString();

            for (int i = 0; i < Default.DioNames.Length; i++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
            {
                DataTable dtView_DIO = myDataSetItems.Tables[string.Format("View_{0}", Default.DioNames[i])];
                //for (int j = 0; j < (i == 7 ? 28 : Default.DioNums[i]); j++)    //32loop,Hは28に変更
                for (int j = 0; j < Default.DioNums[i]; j++)    //32loop
                {
                    int jj = j + 1;
                    string iFeildName = string.Format("{0}-{1:00}", Default.DioNames[i], jj);
                    //dtView_DIO.Rows[j]["IO"] = dtListDatRow[iFeildName];
                    dtListDatRow[iFeildName] = dtView_DIO.Rows[j]["IO"];
                }
                //dtView_DIO.AcceptChanges();
            }
            {
                DataTable dtView_AO = myDataSetItems.Tables[string.Format("View_{0}", Default.AoName)];
                for (int i = 0; i < Default.AoSwichNum; i++)     // AO
                {
                    int ii = i + 1;
                    string iFeildName = string.Format("{0}-{1:0}", Default.AoName, ii);
                    //this.dataGridView_AO.Rows.Add(ii.ToString(), dtListDatRow[iFeildName], null);
                    if (i < Default.AoNum)
                    {
                        //dtView_AO.Rows[i]["Value"] = dtListDatRow[iFeildName];
                        dtListDatRow[iFeildName] = dtView_AO.Rows[i]["Value"];
                    }
                    iFeildName = string.Format("{0}-{1:0}", Default.AoSwichName, ii);
                    //dtView_AO.Rows[i]["Enable"] = dtListDatRow[iFeildName];
                    dtListDatRow[iFeildName] = dtView_AO.Rows[i]["Enable"];
                }
                //dtView_AO.AcceptChanges();
            }
            {
                DataTable dtView_AI = myDataSetItems.Tables[string.Format("View_{0}", Default.AiName)];
                for (int i = 0; i < Default.AiNum; i++)     // AI
                {
                    int ii = i + 1;
                    string iFeildName = string.Format("{0}-{1:0}L", Default.AiName, ii);
                    //dtView_AI.Rows[i]["Lower"] = dtListDatRow[iFeildName];
                    dtListDatRow[iFeildName] = dtView_AI.Rows[i]["Lower"];
                    iFeildName = string.Format("{0}-{1:0}H", Default.AiName, ii);
                    //dtView_AI.Rows[i]["Upper"] = dtListDatRow[iFeildName];
                    dtListDatRow[iFeildName] = dtView_AI.Rows[i]["Upper"];
                }
                //dtView_AI.AcceptChanges();
            }
            dtListDatRow.AcceptChanges();
        }

        private void button_Priv_Click(object sender, EventArgs e)
        {
            if (this.button_Priv.Enabled == false) return;
            this.button_Priv.Enabled = false;
            this.label_Busy.Visible = true;
            this.label_Busy.Refresh();
            if (TNo < myDataSetItems.ListDat.Rows.Count)
            {
                dataGridView_DA_Restore(TNo);
            }
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
            this.button_Priv.Enabled = true;
        }

        private void button_Next_Click(object sender, EventArgs e)
        {
            if (this.button_Next.Enabled == false) return;
            this.button_Next.Enabled = false;
            this.label_Busy.Visible = true;
            this.label_Busy.Refresh();
            if (TNo < myDataSetItems.ListDat.Rows.Count)
            {
                dataGridView_DA_Restore(TNo);
            }

            if (TNo < (myDataSetItems.ListDat.Rows.Count-1))
            {
                TNo++;
                this.textBox_TNo.Text = string.Format("{0}", TNo + 1);
                this.textBox_TNo.Refresh();
                //dataGridView_DA_Dat(TNo);
                //this.Refresh();
            }
            else
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show("最後の項目です。\n\n追加しますか？", Default.ApplicationName,MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    DataTable dtInspectItem = myDataSetItems.ListDat;
                    DataRow InspectItemRowNew = dtInspectItem.NewRow();
                    dtInspectItem.Rows.InsertAt(InspectItemRowNew, dtInspectItem.Rows.Count);

                    TNo++;
                    this.textBox_TNo.Text = string.Format("{0}", TNo + 1);
                    this.textBox_TNo.Refresh();

                }

            }
            this.label_Busy.Visible = false;
            this.label_Busy.Refresh();
            this.button_Next.Enabled = true;

        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            int TNo = int.Parse(textBox_TNo.Text);
            if (TNo <= myDataSetItems.ListDat.Rows.Count)
            {
                this.label_Busy.Visible = true;
                this.label_Busy.Refresh();
                dataGridView_DA_Restore(TNo - 1);
                this.label_Busy.Visible = false;
                this.label_Busy.Refresh();
            }
            //mForms.Hide(this);
            this.Close();
        }

        private void frmDataInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            //mForms.Hide(this, e);
            System.Diagnostics.Debug.WriteLine("frmDataInput_FormClosing");
        }
        private void frmDataInput_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void frmDataInput_Shown(object sender, EventArgs e)
        {
            allClearSelection();
            TNo = StartTNo;
            textBox_TNo.Text = (TNo + 1).ToString();
        }

        private void dataGridView_DA_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_DA_CellDoubleClick:{0},{1}", e.ColumnIndex, e.RowIndex));
            for (int i = 0; i < dataGridView_DIO.Length; i++)
            {
                if (dataGridView1.Equals(dataGridView_DIO[i]))
                {
                    if (e.RowIndex >= 0)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_DA_CellDoubleClick:find={0}", i));
                        mForms.fmPinIO.SelectPin = e.RowIndex;
                        mForms.fmPinIO.PinIO = dataGridView1[2, e.RowIndex].Value.ToString();
                        //20180726
                        DataRow dtListDatRow = myDataSetItems.ListDat.Rows[TNo];
                        int RowIndex = e.RowIndex+1;
                        string iFeildName;
                        iFeildName = string.Format("{0}-{1:00}L", Default.DioNames[i], RowIndex);
                        string iDpL = dtListDatRow[iFeildName].ToString();
                        mForms.fmPinIO.iDpL = (string.IsNullOrEmpty(iDpL) ? myDataSetItems.CheckDat.Rows[0]["iDp-LoLMT"].ToString() : iDpL);
                        iFeildName = string.Format("{0}-{1:00}H", Default.DioNames[i], RowIndex);
                        string iDpH = dtListDatRow[iFeildName].ToString();
                        mForms.fmPinIO.iDpH = (string.IsNullOrEmpty(iDpH) ? myDataSetItems.CheckDat.Rows[0]["iDp-HiLMT"].ToString() : iDpH);
                        //20180827 iDsもセット
                        string iDsL = dtListDatRow["iDs-LoLMT"].ToString();
                        mForms.fmPinIO.iDsL = (string.IsNullOrEmpty(iDsL) ? myDataSetItems.CheckDat.Rows[0]["iDs-LoLMT"].ToString() : iDsL);
                        string iDsH = dtListDatRow["iDs-HiLMT"].ToString();
                        mForms.fmPinIO.iDsH = (string.IsNullOrEmpty(iDsH) ? myDataSetItems.CheckDat.Rows[0]["iDs-HiLMT"].ToString() : iDsH);

                        ////フォームを画面の真ん中に表示する
                        //mForms.fmPinIO.StartPosition = FormStartPosition.CenterScreen;
                        //親フォームの中央に表示する
                        mForms.fmPinIO.Left = this.Left + this.Width / 2 - (mForms.fmPinIO.Width / 2);
                        mForms.fmPinIO.Top = this.Top + this.Height / 2 - (mForms.fmPinIO.Height/2);
                        mForms.fmPinIO.StartPosition = FormStartPosition.Manual;//
                        mForms.fmPinIO.ShowDialog();
                        if (mForms.fmPinIO.DialogResult == System.Windows.Forms.DialogResult.OK)
                        {
                            dataGridView1[2, e.RowIndex].Value = mForms.fmPinIO.PinIO;
                            switch (mForms.fmPinIO.PinIO)
                            {
                                case "iDp":
                                case "nDp":
                                    iFeildName = string.Format("{0}-{1:00}L", Default.DioNames[i], RowIndex);
                                    dtListDatRow[iFeildName] = mForms.fmPinIO.iDpL;
                                    iFeildName = string.Format("{0}-{1:00}H", Default.DioNames[i], RowIndex);
                                    dtListDatRow[iFeildName] = mForms.fmPinIO.iDpH;
                                    break;

                                case "iDs":
                                case "nDs":
                                    dtListDatRow["iDs-HiLMT"] = mForms.fmPinIO.iDsH;
                                    dtListDatRow["iDs-LoLMT"] = mForms.fmPinIO.iDsL;
                                    //dtListDatRow.AcceptChanges();
                                    break;

                                default:
                                    break;
                            }
                        }
                        break;
                    }
                }
            }
        }

        private void dataGridView_AI_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AI_CellDoubleClick:{0},{1}", e.ColumnIndex, e.RowIndex));
            if (e.RowIndex >= 0)
            {
                mForms.fmPinAi.SelectPin = e.RowIndex;
                mForms.fmPinAi.LowerValue = dataGridView1[2, e.RowIndex].Value.ToString();
                mForms.fmPinAi.UpperValue = dataGridView1[3, e.RowIndex].Value.ToString();
                mForms.fmPinAi.ShowDialog();
                if (mForms.fmPinAi.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    dataGridView1[2, e.RowIndex].Value = mForms.fmPinAi.LowerValue;
                    dataGridView1[3, e.RowIndex].Value = mForms.fmPinAi.UpperValue;
                }
            }
        }

        private void dataGridView_AO_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AO_CellDoubleClick:{0},{1}", e.ColumnIndex, e.RowIndex));
            if (e.RowIndex >= 0)
            {
                mForms.fmPinAo.SelectPin = e.RowIndex;
                mForms.fmPinAo.AoValue = dataGridView1[2, e.RowIndex].Value.ToString();
                mForms.fmPinAo.AoSw = dataGridView1[3, e.RowIndex].Value.ToString();
                mForms.fmPinAo.ShowDialog();
                if (mForms.fmPinAo.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    if (e.RowIndex < 2)
                    {
                        this.dataGridView_AO[2, e.RowIndex].Value = mForms.fmPinAo.AoValue;
                    }
                    this.dataGridView_AO[3, e.RowIndex].Value = mForms.fmPinAo.AoSw;
                }
            }
        }

        private void dataGridView_DA_Leave(object sender, EventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            dataGridView1.ClearSelection();
        }

        private void dataGridView_DA_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            if (e.ColumnIndex == ColumnIndexIOdataField)    //20161017
            {
                string Value = e.Value.ToString();
                if (Value.Length > 0)
                {
                    //if (Value.Length > 0 && (Value == "oHi" || Value == "oOP"))
                    //{
                    //    dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;
                    //    dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.Red;
                    //    //dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;
                    //}
                    //else if (Value.Length > 0 && Value == "iDh")    //20170417
                    //{
                    //    dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Blue;
                    //    dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.Blue;
                    //}
                    //else
                    //{
                    //    dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Black;
                    //    dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.Black;
                    //    //dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Black;
                    //}
                    //if (Value.Length > 0 && (Value == "iDh" || Value == "oHi" || Value == "oGN") )   //20170417
                    //{
                    //    dataGridView1[e.ColumnIndex, e.RowIndex].Style.Font = fontBoldstyle;
                    //}
                    //else
                    //{
                    //    dataGridView1[e.ColumnIndex, e.RowIndex].Style.Font = fontRegularlstyle;
                    //}
                    string DoStat = Value;
                    if (mc.dicDioStat.ContainsKey(DoStat))
                    {
                        Cyc.IO.NippoDIO.IO_STAT statCurr = (Cyc.IO.NippoDIO.IO_STAT)mc.dicDioStat[DoStat]; //現在の設定

                        int CellStylesIdx = (int)statCurr - (int)Cyc.IO.NippoDIO.IO_STAT.oOP;
                        if (CellStylesIdx >= (int)Cyc.IO.NippoDIO.IO_STAT.nOP)
                        {
                            CellStylesIdx -= ((int)Cyc.IO.NippoDIO.IO_STAT.nOP - 3);
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
                else
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.Font = fontRegularlstyle;
                }
            }
        }

        private void dataGridView_DA_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            //dataGridView_DA_CellContextMenuStriped = dataGridView1;
            //右clickだと、Leaveがはいらないので、ここでLeave相当をする
            if (dataGridView_Entered != null && !sender.Equals(dataGridView_Entered))   //20170126
            {
                dataGridView_Entered.ClearSelection();
                dataGridView_Entered = null;
            }
            dataGridView_Entered = dataGridView1;

            if (e.RowIndex < 0)
            {
                //列ヘッダーに表示するContextMenuStripを設定する
                e.ContextMenuStrip = this.contextMenuStripDA;
                dataGridView_DA_CellMouseClick(sender, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));   //右クリックでも選択する
            }
            else if (e.ColumnIndex < 0)
            {
                //行ヘッダーに表示するContextMenuStripを設定する
                e.ContextMenuStrip = this.contextMenuStripDA;
                dataGridView_DA_CellMouseClick(sender, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));   //右クリックでも選択する
            }
            else if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                dataGridView_Entered[e.ColumnIndex, e.RowIndex].Selected = true;

                //セルが整数型のときに表示するContextMenuStripを変更する
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                e.ContextMenuStrip = this.contextMenuStripDA;
            }
        }

        private void contextMenuStripDA_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip menu = (ContextMenuStrip)sender;

            dataGridView_DA_CellContextMenuStriped = (DataGridView)menu.SourceControl;
        }

        private void dataGridView_AO_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            if (e.RowIndex >= Default.AoNum && e.ColumnIndex == ColumnIndexAOdataField) //20161017
            {
                //BackColor,AO値が不要な2～8は、非入力にする
                dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.FromArgb(255, 192, 192);
                dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.FromArgb(255, 192, 192);
                dataGridView1[e.ColumnIndex, e.RowIndex].ReadOnly = true;
            }
        }

        private void dataGridView_AO_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            //dataGridView_AO_CellContextMenuStriped = dataGridView1;
            //右clickだと、Leaveがはいらないので、ここでLeave相当をする
            //if (dataGridView_Entered != null)
            if (dataGridView_Entered != null && !sender.Equals(dataGridView_Entered))   //20170127
            {
                dataGridView_Entered.ClearSelection();
                dataGridView_Entered = null;
            }
            dataGridView_Entered = dataGridView1;

            if (e.RowIndex < 0)
            {
                //列ヘッダーに表示するContextMenuStripを設定する
                e.ContextMenuStrip = this.contextMenuStripAO;
                dataGridView_AO_CellMouseClick(dataGridView1, new DataGridViewCellEventArgs(e.ColumnIndex,e.RowIndex)); //右クリックでも選択する
            }
            else if (e.ColumnIndex < 0)
            {
                //行ヘッダーに表示するContextMenuStripを設定する
                e.ContextMenuStrip = this.contextMenuStripAO;
                dataGridView_AO_CellMouseClick(dataGridView1, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex)); //右クリックでも選択する
            }
            else if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                dataGridView_Entered[e.ColumnIndex, e.RowIndex].Selected = true;
                //セルが整数型のときに表示するContextMenuStripを変更する
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                e.ContextMenuStrip = this.contextMenuStripAO;
            }
            else if (e.ColumnIndex == 2 && e.RowIndex >= 0 && e.RowIndex < 2)
            {
                dataGridView_Entered[e.ColumnIndex, e.RowIndex].Selected = true;
                //セルが整数型のときに表示するContextMenuStripを変更する
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                e.ContextMenuStrip = this.contextMenuStripAO;
            }
        }

        private void dataGridView_AI_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            //dataGridView_AO_CellContextMenuStriped = dataGridView1;
            //右clickだと、Leaveがはいらないので、ここでLeave相当をする
            //if (dataGridView_Entered != null)
            if (dataGridView_Entered != null && !sender.Equals(dataGridView_Entered))   //20170127
            {
                dataGridView_Entered.ClearSelection();
                dataGridView_Entered = null;
            }
            dataGridView_Entered = dataGridView1;

            if (e.RowIndex < 0)
            {
                //列ヘッダーに表示するContextMenuStripを設定する
                e.ContextMenuStrip = this.contextMenuStripAI;
                dataGridView_AI_CellMouseClick(sender, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));   //右クリックでも選択する
            }
            else if (e.ColumnIndex < 0)
            {
                //行ヘッダーに表示するContextMenuStripを設定する
                e.ContextMenuStrip = this.contextMenuStripAI;
                dataGridView_AI_CellMouseClick(sender, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));   //右クリックでも選択する
            }
            else if ((e.ColumnIndex == 2 || e.ColumnIndex == 3) && e.RowIndex >= 0)
            {
                dataGridView_Entered[e.ColumnIndex, e.RowIndex].Selected = true;
                //セルが整数型のときに表示するContextMenuStripを変更する
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                e.ContextMenuStrip = this.contextMenuStripAI;
            }
        }

        private void contextMenuStripAO_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip menu = (ContextMenuStrip)sender;

            dataGridView_DA_CellContextMenuStriped = (DataGridView)menu.SourceControl;
        }

        private void contextMenuStripAI_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip menu = (ContextMenuStrip)sender;

            dataGridView_DA_CellContextMenuStriped = (DataGridView)menu.SourceControl;
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

        private void dataGridView_DA_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            int StartEnableColumn = GetStartEnableColumn(dataGridView1);

            if (e.KeyData == (Keys.Control | Keys.V))
            {
                // Ctrl + V
                System.Diagnostics.Debug.WriteLine("Ctrl + V が押されました。");
                //dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                string DataGridViewType = whichDataGridView(dataGridView1);
                switch (DataGridViewType)
                {
                    case "D":
                        //libDataGridView.dataGridView_Paste(dataGridView1, StartEnableColumn, new libDataGridView.delegateCanPaste(CanPasteDA));
                        libDataGridView.dataGridView_Paste(dataGridView1, StartEnableColumn, new libDataGridView.delegateCanPaste(CanPasteDA), new libDataGridView.delegateSetiDpLMT(SetiDpLMT));
                        break;

                    case "O":
                        libDataGridView.dataGridView_Paste(dataGridView1, StartEnableColumn, new libDataGridView.delegateCanPaste(CanPasteAO));
                        break;

                    case "I":
                        libDataGridView.dataGridView_Paste(dataGridView1, StartEnableColumn, new libDataGridView.delegateCanPaste(CanPasteAI));
                        break;
                }

            }
            else if (e.KeyData == (Keys.Control | Keys.X))
            {
                // Ctrl + X
                System.Diagnostics.Debug.WriteLine("Ctrl + X が押されました。");
                //選択されたセルをクリップボードにコピーする
                //Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
                libDataGridView.dataGridView_Copy(dataGridView1, StartEnableColumn, getiDpLMT);
                //dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                libDataGridView.dataGridView_Clear(dataGridView1, StartEnableColumn);
            }
            else if (e.KeyData == (Keys.Control | Keys.C))
            {
                // Ctrl + C
                System.Diagnostics.Debug.WriteLine("Ctrl + C が押されました。");
                //選択されたセルをクリップボードにコピーする
                libDataGridView.dataGridView_Copy(dataGridView1, StartEnableColumn, getiDpLMT);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == (Keys.Delete))
            {
                // Delete
                System.Diagnostics.Debug.WriteLine("Delete が押されました。");
                //dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                libDataGridView.dataGridView_Clear(dataGridView1, StartEnableColumn);
            }
            else if (e.KeyData == (Keys.Enter))
            {
                int minSelectedColumn = libDataGridView.minSelectedColumn(dataGridView1);
                int minSelectedRows = libDataGridView.minSelectedRows(dataGridView1);
                DataGridViewCellEventArgs ee = new DataGridViewCellEventArgs(minSelectedColumn, minSelectedRows);
                string DataGridViewType = whichDataGridView(dataGridView1);
                switch (DataGridViewType)
                {
                    case "D":
                        dataGridView_DA_CellDoubleClick(sender, ee);
                        break;

                    case "O":
                        dataGridView_AO_CellDoubleClick(sender, ee);
                        break;

                    case "I":
                        dataGridView_AI_CellDoubleClick(sender, ee);
                        break;
                }
            }
        }

        private void dataGridView_AO_KeyDown(object sender, KeyEventArgs e)
        {
            dataGridView_DA_KeyDown(sender, e);
        }

        private void dataGridView_AI_KeyDown(object sender, KeyEventArgs e)
        {
            dataGridView_DA_KeyDown(sender, e);
        }

        private void mnuInput_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView1 = dataGridView_Entered;
            if (dataGridView1 != null)
            {
                if (dataGridView1.SelectedCells != null)
                {
                    int minSelectedColumn = libDataGridView.minSelectedColumn(dataGridView1);
                    int minSelectedRows = libDataGridView.minSelectedRows(dataGridView1);
                    DataGridViewCellEventArgs ee = new DataGridViewCellEventArgs(minSelectedColumn, minSelectedRows);
                    string DataGridViewType = whichDataGridView(dataGridView1);
                    switch (DataGridViewType)
                    {
                        case "D":
                            dataGridView_DA_CellDoubleClick(dataGridView1, ee);
                            break;

                        case "O":
                            dataGridView_AO_CellDoubleClick(dataGridView1, ee);
                            break;

                        case "I":
                            dataGridView_AI_CellDoubleClick(dataGridView1, ee);
                            break;
                    }
                }
            }
        }

        private void mnuCut_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView1 = dataGridView_Entered;
            if (dataGridView1 == null) return;
            int StartEnableColumn = GetStartEnableColumn(dataGridView1);

            if (dataGridView1 != null)
            {
                if (dataGridView1.SelectedCells != null)
                {
                    //Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
                    libDataGridView.dataGridView_Copy(dataGridView1, StartEnableColumn, getiDpLMT);
                    //dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                    libDataGridView.dataGridView_Clear(dataGridView1, StartEnableColumn);
                }
            }
        }

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView1 = dataGridView_Entered;
            if (dataGridView1 == null) return;
            int StartEnableColumn = GetStartEnableColumn(dataGridView1);

            if (dataGridView1 != null)
            {
                if (dataGridView1.SelectedCells != null)
                {
                    //Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
                    libDataGridView.dataGridView_Copy(dataGridView1, StartEnableColumn, getiDpLMT);
                    //dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                }
            }
        }

        private void mnuPaste_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView1 = dataGridView_Entered;
            if (dataGridView1 == null) return;
            int StartEnableColumn = GetStartEnableColumn(dataGridView1);

            if (dataGridView1 != null)
            {
                //dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                string DataGridViewType = whichDataGridView(dataGridView1);
                switch (DataGridViewType)
                {
                    case "D":
                        //libDataGridView.dataGridView_Paste(dataGridView1, StartEnableColumn, new libDataGridView.delegateCanPaste(CanPasteDA));
                        libDataGridView.dataGridView_Paste(dataGridView1, StartEnableColumn, new libDataGridView.delegateCanPaste(CanPasteDA), new libDataGridView.delegateSetiDpLMT(SetiDpLMT));
                        break;

                    case "O":
                        libDataGridView.dataGridView_Paste(dataGridView1, StartEnableColumn, new libDataGridView.delegateCanPaste(CanPasteAO));
                        break;

                    case "I":
                        libDataGridView.dataGridView_Paste(dataGridView1, StartEnableColumn, new libDataGridView.delegateCanPaste(CanPasteAI));
                        break;
                }
            }
        }

        private void dataGridView_DA_Enter(object sender, EventArgs e)
        {
            if (dataGridView_Entered != null)
            {
                dataGridView_Entered.ClearSelection();
            }
            dataGridView_Entered = (DataGridView)sender;
        }

        private void dataGridView_AO_Enter(object sender, EventArgs e)
        {
            if (dataGridView_Entered != null)
            {
                dataGridView_Entered.ClearSelection();
            }
            dataGridView_Entered = (DataGridView)sender;
        }

        private void dataGridView_AI_Enter(object sender, EventArgs e)
        {
            if (dataGridView_Entered != null)
            {
                dataGridView_Entered.ClearSelection();
            }
            dataGridView_Entered = (DataGridView)sender;
        }

        private string whichDataGridView(DataGridView dataGridView1)
        {
            for (int i = 0; i < dataGridView_DIO.Length; i++)
            {
                if (dataGridView1.Equals(dataGridView_DIO[i]))
                {
                    return "D";
                }
            }

            if (dataGridView1.Equals(dataGridView_AIO[0]))
            {
                return "O";
            }
            else if (dataGridView1.Equals(dataGridView_AIO[1]))
            {
                return "I";
            }
            return "";
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            button_Close_Click(sender, e);
        }

        private void dataGridView_DA_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView_DA_CellMouseClick(sender, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));
        }

        private void dataGridView_DA_CellMouseClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            if (e.RowIndex < 0)
            {
                dataGridView1.SelectAll();
            }
        }

        private void dataGridView_AO_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView_AO_CellMouseClick(sender, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));
        }

        private void dataGridView_AO_CellMouseClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            if (e.RowIndex < 0)
            {
                switch (e.ColumnIndex)
                {
                    case -1:
                    case 0:
                    case 1:
                        dataGridView1.CurrentCell = null;
                        //dataGridView1.ClearSelection();
                        dataGridView1.Columns[0].Selected = false;
                        dataGridView1.Columns[1].Selected = false;
                        dataGridView1.Columns[2].Selected = true;
                        dataGridView1.Columns[3].Selected = true;
                        break;
                }
            }

        }

        private void dataGridView_AO_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AO_CellMouseDown:{0}:{1}", e.ColumnIndex, e.RowIndex));
            if (e.RowIndex < 0)
            {
                if (dataGridView1.SelectionMode != DataGridViewSelectionMode.ColumnHeaderSelect)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AO_CellMouseDown X={0} Y{1} SelectionMode={2}", e.Location.X, e.Location.Y, "ColumnHeaderSelect"));
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
                }
            }
            else
            {
                switch (e.ColumnIndex)
                {
                    case -1:
                    case 0:
                    case 1:
                        if (dataGridView1.SelectionMode != DataGridViewSelectionMode.FullRowSelect)
                        {
                            System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AO_CellMouseDown X={0} Y{1} SelectionMode={2}", e.Location.X, e.Location.Y, "RowHeaderSelect"));
                            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        }
                        break;
                    case 2:
                    case 3:
                        System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AO_CellMouseDown X={0} Y{1} SelectionMode={2}", e.Location.X, e.Location.Y, "RowHeaderSelect"));
                        dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        break;
                }
            }
        }

        private void dataGridView_AI_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView_AI_CellMouseClick(sender, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));
        }

        private void dataGridView_AI_CellMouseClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            if (e.RowIndex < 0)
            {
                switch (e.ColumnIndex)
                {
                    case -1:
                    case 0:
                    case 1:
                        dataGridView1.CurrentCell = null;
                        //dataGridView1.ClearSelection();
                        dataGridView1.Columns[0].Selected = false;
                        dataGridView1.Columns[1].Selected = false;
                        dataGridView1.Columns[2].Selected = true;
                        dataGridView1.Columns[3].Selected = true;
                        break;
                }
            }
         }

        private void dataGridView_AI_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AI_CellMouseDown:{0}:{1}", e.ColumnIndex, e.RowIndex));
            if (e.RowIndex < 0)
            {
                if (dataGridView1.SelectionMode != DataGridViewSelectionMode.ColumnHeaderSelect)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AI_CellMouseDown X={0} Y{1} SelectionMode={2}", e.Location.X, e.Location.Y, "ColumnHeaderSelect"));
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
                }
            }
            else
            {
                switch (e.ColumnIndex)
                {
                    case -1:
                    case 0:
                    case 1:
                        if (dataGridView1.SelectionMode != DataGridViewSelectionMode.FullRowSelect)
                        {
                            System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AI_CellMouseDown X={0} Y{1} SelectionMode={2}", e.Location.X, e.Location.Y, "RowHeaderSelect"));
                            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        }
                        break;
                    case 2:
                    case 3:
                        System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AI_CellMouseDown X={0} Y{1} SelectionMode={2}", e.Location.X, e.Location.Y, "RowHeaderSelect"));
                        dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        break;
                }
            }
        }

        private void dataGridView_DA_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_DA_DataError:{0}", e.ColumnIndex));
        }

        private void dataGridView_AO_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void dataGridView_AI_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void mnuBack_Click(object sender, EventArgs e)
        {
            button_Priv_Click(this.button_Priv, new EventArgs());
        }

        private void mnuNext_Click(object sender, EventArgs e)
        {
            button_Next_Click(this.button_Next, new EventArgs());
        }

        private void buttonViewLMT_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView1 = dataGridView_Entered;
            dataGridView_DA_Restore(TNo);

            if (dataGridView_Entered == null)
            {
                showFrmResultViewLMT(string.Format("{0}{1:00}", (char)('A' + 0), 0));
            }
            else
            {
                //DataGridView dataGridView1 = (DataGridView)sender;

                for (int i = 0; i < dataGridView_DIO.Length; i++)
                {
                    if (dataGridView1.Equals(dataGridView_DIO[i]))
                    {
                        int CurrentRowIndex = dataGridView1.CurrentRow == null ? 0 : dataGridView1.CurrentRow.Index;    //20180829-1
                        showFrmResultViewLMT(string.Format("{0}{1:00}", (char)('A' + i), CurrentRowIndex));
                        break;
                    }
                }
            }

        }

        private void showFrmResultViewLMT(string SelectionCell)
        {
            DataInputLimitView fmDataInputLMT = new DataInputLimitView();  //20170126
            fmDataInputLMT.myDataSetItems = this.myDataSetItems;
            fmDataInputLMT.TNo = this.TNo;
            //fmResultViewLMT.PageNo = PageNo;
            fmDataInputLMT.SelectionCell = SelectionCell;
            fmDataInputLMT.Top = this.Top;
            fmDataInputLMT.Left = this.Left;
            fmDataInputLMT.StartPosition = FormStartPosition.Manual;

            this.Hide();
            fmDataInputLMT.ShowDialog();
            this.Top = fmDataInputLMT.Top;
            this.Left = fmDataInputLMT.Left;
            this.Show();
            fmDataInputLMT.Dispose(); //20170126
        }

    }
}