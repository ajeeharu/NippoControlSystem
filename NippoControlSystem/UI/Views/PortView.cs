using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Versioning;
using NippoControlSystem.UI.Views;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.UI.Views
{
    public partial class PortView : Form
    {
        Cyc.IO.Settings Default = Cyc.IO.Settings.GetInstance();
        Cyc.IO.cDio dio = Cyc.IO.cDio.GetInstance();
        Cyc.IO.NippoDIO nio = Cyc.IO.NippoDIO.GetInstance();
        MeasureCondition mc = MeasureCondition.GetInstance();
        Views mForms = Views.GetInstance();

        System.Windows.Forms.DataGridView[] dataGridView_DIO = null;
        System.Windows.Forms.DataGridView[] dataGridView_AIO = null;
        System.Windows.Forms.DataGridView[] dataGridView_GND = null;
        //Local 変数
        DataGridView dataGridView_DA_CellContextMenuStriped = null;
        DataGridView dataGridView_Entered = null;

        public PortView()
        {
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

        [SupportedOSPlatform("windows")]
        private void frmPort_Load(object sender, EventArgs e)
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmPort"]);
            Views.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmPort"]);

            dataGridView_DIO = new DataGridView[8];
            dataGridView_AIO = new DataGridView[2];
            dataGridView_GND = new DataGridView[10];
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
            dataGridView_GND[8] = this.dataGridView_GndAO;
            dataGridView_GND[9] = this.dataGridView_GndAI;

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
            for (int i = 0; i < Default.GndNames.Length; i++)
            {
                dataGridView_GND[i].DataSource = myDataSetItems;
                dataGridView_GND[i].DataMember = string.Format("View_{0}", Default.GndNames[i]);
            }
            //Visualスタイルを使用しない
            dataGridView_DA.EnableHeadersVisualStyles = false;
            //DataGridViewにショートカットメニューを表示する
            contextMenuStripDA.Items.Clear();
            for (int i = 0; i < mnuEdit.DropDownItems.Count; i++)   //コピペ用
            {
                contextMenuStripDA.Items.Add(mnuEdit.DropDownItems[i].Text, null, toolStripMenuItemTitle_Click);
            }

            dataGridView_DA_Display();
        }

        [SupportedOSPlatform("windows")]
        private void toolStripMenuItemTitle_Click(object sender, EventArgs e)
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
                    ////i番目のメニュが選択された
                    //選択されているセルを表示
                    switch (i)
                    {
                        case 0:     //Input
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
                                    dataGridView_AO_CellDoubleClick(dataGridView1, ee);
                                    break;

                                case "G":
                                    dataGridView_GndDA_CellDoubleClick(dataGridView1, ee);
                                    break;
                            }
                            break;
                        case 1:     //Cut
                            //選択されたセルをクリップボードにコピーする
                            //Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
                            libDataGridView.dataGridView_Copy(dataGridView1, StartEnableColumn, getiDpLMT);
                            Console.WriteLine("選択されているセル");
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
                            libDataGridView.dataGridView_Paste(dataGridView1, StartEnableColumn);
                            break;
                    }

                    break;
                }
            }
        }

        private string getiDpLMT(DataGridView dataGridView1, int insertColumnIndex, int insertRowIndex)
        {
            return "";  //Portは、常に""
        }

        private void dataGridView_DA_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void allClearSelection()
        {
            dataGridView_AI.ClearSelection();
            dataGridView_AO.ClearSelection();

            for (int i = 0; i < Default.DioNames.Length; i++)     // { "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH" }
            {
                dataGridView_DIO[i].CurrentCell = null;
                dataGridView_DIO[i].ClearSelection();
            }
            for (int i = 0; i < Default.GndNames.Length; i++)     // { "GndDA", "GndDB", "GndDC", "GndDD", "GndDE", "GndDF", "GndDG", "GndDH", "GndAO", "GndAI" }
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
                for (int j = 0; j < (i==7 ? 28 :Default.DioNums[i]); j++)    //32loop,Hは28に変更
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
                for (int j = 0; j < Default.GndNums[i]; j++)    //5loop
                {
                    int jj = j + 33;
                    string iFeildName = string.Format("{0}-{1:0}", Default.GndNames[i], j + 1);
                    dtView_GndDIO.Rows[j]["Name"] = dtPortDatRow[iFeildName];
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
            //for (int i = 0; i < Default.GndNames.Length; i++)     // { "GndDA", "GndDB", "GndDC", "GndDD", "GndDE", "GndDF", "GndDG", "GndDH", "GndAO", "GndAI" }
            //{
            //    for (int j = 0; j < Default.GndNums[i]; j++)    //5loop
            //    {
            //        int jj = j + 33;
            //        this.dataGridView_GND[i].Rows[j].HeaderCell.Value = jj.ToString();
            //    }
            //    this.dataGridView_GND[i].TopLeftHeaderCell.Value = "Pin";
            //}
        }

        private void dataGridView_DA_Restore()
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
                    //dtView_DIO.Rows[j]["Name"] = dtPortDatRow[iFeildName];
                    dtPortDatRow[iFeildName] = dtView_DIO.Rows[j]["Name"];
                }
            }
            //dtView_AI
            {
                DataTable dtView_AI = myDataSetItems.Tables[string.Format("View_{0}", Default.AiName)];
                for (int j = 0; j < Default.AiNum; j++)    //32loop
                {
                    int ii = j + 1;
                    string iFeildName = string.Format("{0}-{1:0}", Default.AiName, ii);
                    //dtView_AI.Rows[j]["Name"] = dtPortDatRow[iFeildName];
                    dtPortDatRow[iFeildName] = dtView_AI.Rows[j]["Name"];
                }
            }
            //dtView_AO
            {
                DataTable dtView_AO = myDataSetItems.Tables[string.Format("View_{0}", Default.AoName)];
                for (int j = 0; j < Math.Max(Default.AoNum, Default.AoSwichNum); j++)    //32loop
                {
                    int ii = j + 1;
                    string iFeildName = string.Format("{0}-{1:0}", Default.AoName, ii);
                    //dtView_AO.Rows[j]["Name"] = dtPortDatRow[iFeildName];
                    dtPortDatRow[iFeildName] = dtView_AO.Rows[j]["Name"];
                }
            }
            //dtView_GndDIO
            for (int i = 0; i < Default.GndNames.Length; i++)     // { "GndDA", "GndDB", "GndDC", "GndDD", "GndDE", "GndDF", "GndDG", "GndDH", "GndAO", "GndAI" }
            {
                DataTable dtView_GndDIO = myDataSetItems.Tables[string.Format("View_{0}", Default.GndNames[i])];
                for (int j = 0; j < Default.GndNums[i]; j++)    //5loop
                {
                    int jj = j + 33;
                    string iFeildName = string.Format("{0}-{1:0}", Default.GndNames[i], j + 1);
                    //dtView_GndDIO.Rows[j]["Name"] = dtPortDatRow[iFeildName];
                    dtPortDatRow[iFeildName] = dtView_GndDIO.Rows[j]["Name"];
                }
            }
            dtPortDatRow.AcceptChanges();

        }

        private void frmPort_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmPort_FormClosing");
        }
        private void frmPort_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button_SaveClose_Click(object sender, EventArgs e)
        {
            dataGridView_DA_Restore();
            //mForms.Hide(this);
            this.Close();
        }

        private void frmPort_Shown(object sender, EventArgs e)
        {
            allClearSelection();
        }

        private void dataGridView_DA_Leave(object sender, EventArgs e)
        {
            System.Windows.Forms.DataGridView dv = (System.Windows.Forms.DataGridView)sender;
            dv.ClearSelection();
        }

        private void dataGridView_DA_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_DA_CellDoubleClick:{0},{1}", e.ColumnIndex, e.RowIndex));
            for (int i = 0; i < dataGridView_DIO.Length; i++)
            {
                if (sender.Equals(dataGridView_DIO[i]))
                {
                    if (e.RowIndex >= 0)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_GndDA_CellDoubleClick:find={0}", i));
                        mForms.fmPinName.SelectPin = e.RowIndex;
                        mForms.fmPinName.PinName = dataGridView1[1, e.RowIndex].Value.ToString();
                        mForms.fmPinName.ShowDialog();
                        if (mForms.fmPinName.DialogResult == System.Windows.Forms.DialogResult.OK)
                        {
                            dataGridView1[1, e.RowIndex].Value = mForms.fmPinName.PinName;
                        }
                    }
                    break;
                }
            }
        }

        private void dataGridView_GndDA_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_GndDA_CellDoubleClick:{0},{1}", e.ColumnIndex, e.RowIndex));
            for (int i = 0; i < dataGridView_GND.Length; i++)
            {
                if (sender.Equals(dataGridView_GND[i]))
                {
                    if (e.RowIndex >= 0)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_GndDA_CellDoubleClick:find={0}", i));
                        mForms.fmPinName.SelectPin = e.RowIndex;
                        mForms.fmPinName.PinName = dataGridView1[1, e.RowIndex].Value.ToString();
                        mForms.fmPinName.ShowDialog();
                        if (mForms.fmPinName.DialogResult == System.Windows.Forms.DialogResult.OK)
                        {
                            dataGridView1[1, e.RowIndex].Value = mForms.fmPinName.PinName;
                        }
                    }
                    break;
                }
            }
        }

        private void dataGridView_AO_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_AO_CellDoubleClick:{0},{1}", e.ColumnIndex, e.RowIndex));
            for (int i = 0; i < dataGridView_AIO.Length; i++)
            {
                if (sender.Equals(dataGridView_AIO[i]))
                {
                    if (e.RowIndex >= 0)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_GndDA_CellDoubleClick:find={0}", i));
                        mForms.fmPinName.SelectPin = e.RowIndex;
                        mForms.fmPinName.PinName = dataGridView1[1, e.RowIndex].Value.ToString();
                        mForms.fmPinName.ShowDialog();
                        if (mForms.fmPinName.DialogResult == System.Windows.Forms.DialogResult.OK)
                        {
                            dataGridView1[1, e.RowIndex].Value = mForms.fmPinName.PinName;
                        }
                    }
                    break;
                }
            }
        }

        private void dataGridView_DA_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            //dataGridView_DA_CellContextMenuStriped = dataGridView1;
            //右clickだと、Leaveがはいらないので、ここでLeave相当をする
            //if (dataGridView_Entered != null)
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
                //dataGridView1.SelectAll();      //20161017
                switch (e.ColumnIndex)
                {
                    case -1:
                    case 0:
                    case 1:
                        dataGridView1.CurrentCell = null;
                        dataGridView1.ClearSelection();
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            dataGridView1[1, i].Selected = true;
                        }
                        break;
                }
            }
            else if (e.ColumnIndex == 0)
            {
                //行ヘッダーに表示するContextMenuStripを設定する
                e.ContextMenuStrip = this.contextMenuStripDA;
            }
            else if (e.ColumnIndex == 1 && e.RowIndex >= 0)
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

        private void contextMenuStripAO_Opening(object sender, CancelEventArgs e)
        {

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
                libDataGridView.dataGridView_Paste(dataGridView1, StartEnableColumn);
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
                        dataGridView_AO_CellDoubleClick(sender, ee);
                        break;

                    case "G":
                        dataGridView_GndDA_CellDoubleClick(sender, ee);
                        break;
                }
            }
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

            for (int i = 0; i < dataGridView_GND.Length; i++)
            {
                if (dataGridView1.Equals(dataGridView_GND[i]))
                {
                    return "G";
                }
            }
            return "";
        }

        private int GetStartEnableColumn(DataGridView dataGridView1)
        {
            return 1;
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
                    //dataGridView_DA_CellDoubleClick(dataGridView1, ee);
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
                            dataGridView_AO_CellDoubleClick(sender, ee);
                            break;

                        case "G":
                            dataGridView_GndDA_CellDoubleClick(sender, ee);
                            break;
                    }
                }
            }

        }

        private void mnuCut_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView1 = dataGridView_Entered;
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

            if (dataGridView1 != null)
            {
                if (dataGridView1.SelectedCells != null)
                {
                    libDataGridView.dataGridView_Paste(dataGridView1,1);
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

        private void mnuPrint_Click(object sender, EventArgs e)
        {
            Cyc.Windows.Forms.screenShot.GetInstance().PrintForm(this, true);
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            //button_SaveClose_Click(sender, e);
            //dataGridView_DA_Restore();    たぶん、RejectChanges()をしなくても、DA_Restore()をしなければ、反映されない
            myDataSetItems.RejectChanges();
            this.Close();
        }

        private void dataGridView_DA_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            // ヘッダ以外のセルか？
            //if (e.ColumnIndex < 0 && e.RowIndex < 0)
            if (e.RowIndex < 0)
            {
                //dataGridView1.SelectAll();      //20161017
                switch (e.ColumnIndex)
                {
                    case -1:
                    case 0:
                    case 1:
                        dataGridView1.CurrentCell = null;
                        dataGridView1.ClearSelection();
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            dataGridView1[1, i].Selected = true;
                        }
                        break;
                }
            }

        }

        private void dataGridView_DA_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //DataGridView dataGridView1 = (DataGridView)sender;

            //if (e.ColumnIndex == 0)
            //{
            //    //20161013
            //    dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
            //}
        }

        private void dataGridView_GndDA_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //DataGridView dataGridView1 = (DataGridView)sender;

            ////20161013
            //if (e.ColumnIndex == 0)
            //{
            //    dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 33).ToString();
            //    System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_GndDA_CellFormatting:{0},{1}", e.ColumnIndex, e.RowIndex));
            //}
        }

    }
}