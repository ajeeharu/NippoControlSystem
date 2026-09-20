using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using NippoControlSystem.UI.Views; // setforegraoundwindow

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.UI.Views
{
    public partial class SettingView : Form
    {
        Cyc.IO.Settings Default = Cyc.IO.Settings.GetInstance();
        //Cyc.IO.cDio dio = Cyc.IO.cDio.GetInstance();
        //Cyc.IO.NippoDIO nio = Cyc.IO.NippoDIO.GetInstance();
        MeasureCondition mc = MeasureCondition.GetInstance();
        Views mForms = Views.GetInstance();
        DataSetItems myDataSetItems = null;
        DataTable savedListDat = null;
        int itemFound = 0;

        //Local 変数
        DataGridView dataGridView_DA_CellContextMenuStriped = null;
        //DataGridView dataGridView_AO_CellContextMenuStriped = null;
        Font fontRegularlstyle = new Font("ＭＳ Ｐゴシック", 9, FontStyle.Regular);
        Font fontBoldstyle = new Font("ＭＳ Ｐゴシック", 9, FontStyle.Bold);

        public SettingView()
        {
            InitializeComponent();
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

        private void frmSetting_Load(object sender, EventArgs e)
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmSetting"]);
            Views.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmSetting"]);

            //ここからはFormの更新
            this.lblMainNo.Text = Default.MainNo;   //"仕様書番号";
            this.lblSubNo.Text = Default.SubNo;     //"追番";
            this.lblItem.Text = Default.Item;  //品名
            //
            if ((myDataSetItems = mc.createDataSetItems(Folder, MainTitle, SubTitle)) == null)
            {
                this.Close();
                return;
            }

            //DataSet
            DataTable dtCheckDat = myDataSetItems.CheckDat;

            //Checkdat
            this.bindingSourceCheckDat.DataSource = myDataSetItems;
            //this.bindingSource1.DataMember = "CheckDat";
            itemFound = this.bindingSourceCheckDat.Find("Title", myDataSetItems.CheckDat.Rows[0]["Title"]);
            //int itemFound = this.bindingSource1.Find("Title", "5C0-00700");
            if (itemFound >= 0)
            {
                bindingSourceCheckDat.Position = itemFound;
            }

            //this.comboBox_Volt.DataBindings.Add("SelectedIndex", this.bindingSourceCheckDat, "Volt");
            //string strVolt = this.textBox_Volt.Text.ToString();
            string strVolt = myDataSetItems.CheckDat.Rows[itemFound]["Volt"].ToString();
            this.textBox_Volt.Text = getVoltString(strVolt);

            this.lblFolderDir.Text = string.Format("フォルダ = {0}", this.Folder);
            if (!string.IsNullOrEmpty(this.textBox_Item.Text))
            {
                //文字が選択されるのを防ぐ
                this.textBox_Item.SelectionStart = 0;
                this.textBox_Item.SelectionLength = 0;
            }

            //ここで検査内容のDataGredViewとDataSetを作成して、データをセットする
            //myDataSetItems = new DataSetItems();
            //DataTable dt = new DataSetItems.ListDatDataTable();
            //string IoName = "";
            System.Windows.Forms.DataGridView dv = this.dataGridView_Inspection;
            //dataGridView_Inspectionの定義
            dv.TopLeftHeaderCell.Value = "T#";  //20161108-2
            dv.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;  //20161108-2
            dv.Columns[0].HeaderText = "検査タイトル";
            dv.Columns[1].HeaderText = "操作ガイド";
            dv.Columns[2].HeaderText = "タイプ";
            dv.Columns[0].Width = 265;
            dv.Columns[1].Width = 445;
            dv.Columns[2].Width = 55;
            //for (int i = 3; i < dv.Columns.Count; i++)
            //{
            //    dv.Columns[i].Width = 50;
            //}


            //dv.DataSource = myDataSetItems;
            //dv.DataMember = "ListDat";
            this.dataSetItemsBindingSource.DataSource = myDataSetItems;

            //DataGridViewにショートカットメニューを表示する
            contextMenuStripDA.Items.Clear();
            for (int i = 0; i < mnuEdit.DropDownItems.Count; i++)   //コピペ用
            {
                contextMenuStripDA.Items.Add(mnuEdit.DropDownItems[i].Text, null, toolStripMenuItemTitle_Click);
            }
            contextMenuStripDA.Items[0].Enabled = false;        //Undoは初めは不可
            //contextMenuStripAO.Items.Clear();    //AOのEnable用
            //for (int i = 0; i < mnuEdit.DropDownItems.Count; i++)   //コピペ用
            //{
            //    contextMenuStripAO.Items.Add(mnuEdit.DropDownItems[i].Text, null, toolStripMenuItemTitle_Click);
            //}
            //contextMenuStripType.Items.Clear();    //TypeのEnable用
            //for (int i = 0; i < mnuEdit.DropDownItems.Count; i++)   //コピペ用
            //{
            //    contextMenuStripType.Items.Add(mnuEdit.DropDownItems[i].Text, null, toolStripMenuItemTitle_Click);
            //}
            //contextMenuStripTitle.Items.Clear();    //TitleのEnable用
            //for (int i = 0; i < mnuEdit.DropDownItems.Count; i++)
            //{
            //    contextMenuStripTitle.Items.Add(mnuEdit.DropDownItems[i].Text, null, toolStripMenuItemTitle_Click);
            //}
            //contextMenuStripGuide.Items.Clear();    //GuideのEnable用
            //for (int i = 0; i < mnuEdit.DropDownItems.Count; i++)
            //{
            //    contextMenuStripGuide.Items.Add(mnuEdit.DropDownItems[i].Text, null, toolStripMenuItemGuide_Click);
            //}

            // Visualスタイルを使用しない
            dataGridView_Inspection.EnableHeadersVisualStyles = false;
            //dataGridView_Inspection.CurrentCell = null;
        }

        private string getVoltString(string valueString)
        {
            string[] voltstring = { "12V", "24V" };
            return (valueString == "1" ? voltstring[1] : voltstring[0]);
        }

        private void ChangeVolt()
        {
            Console.WriteLine(System.DateTime.Now.ToLongTimeString() + " ChangeVolt:" );

            string strVolt = myDataSetItems.CheckDat.Rows[itemFound]["Volt"].ToString();
            string newVolt = (strVolt == "1" ? "0" : "1");

            DialogResult dr = System.Windows.Forms.MessageBox.Show(string.Format("電圧を{0}に変更しますか？", getVoltString(newVolt)), Default.ApplicationName, MessageBoxButtons.YesNo);
            if (dr != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            this.textBox_Volt.Text = getVoltString(newVolt);
            myDataSetItems.CheckDat.Rows[0]["Volt"] = newVolt;
            DataRow dtCheckDatRow = myDataSetItems.CheckDat.Rows[itemFound];

            int iDo_Length = (int)Cyc.IO.NippoDIO.IO_STAT.iTo - (int)Cyc.IO.NippoDIO.IO_STAT.iOP + 1;
            //12V 24V
            int mVoltIdx = (newVolt == "1" ? 2 : 0); //0:12V 2:24V

            string dtCheckDatFields = null;
            for (int i = 0; i < iDo_Length; i++)
            {
                dtCheckDatFields = ((Cyc.IO.NippoDIO.IO_STAT)((int)Cyc.IO.NippoDIO.IO_STAT.iOP + i)).ToString() + Default.CheckDatLMTFields[0];     //Hi
                dtCheckDatRow[dtCheckDatFields] = Default.CheckDatLMT[mVoltIdx][i].ToString("F1");    //iOP-HiLMT～iTo-HiLMT
                dtCheckDatFields = ((Cyc.IO.NippoDIO.IO_STAT)((int)Cyc.IO.NippoDIO.IO_STAT.iOP + i)).ToString() + Default.CheckDatLMTFields[1];     //Lo
                dtCheckDatRow[dtCheckDatFields] = Default.CheckDatLMT[mVoltIdx + 1][i].ToString("F1");    //iOP-LoLMT～iTo-LoLMT
            }
        }

        private void buttonChangeVolt_Click(object sender, EventArgs e)
        {
            ChangeVolt();
        }

        private void toolStripMenuItemTitle_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem iTem = (ToolStripMenuItem)sender;
            DataGridView dataGridView1 = dataGridView_Inspection;

            System.Diagnostics.Debug.WriteLine(string.Format("toolStripMenuItem_Click:{0}", iTem.Text));
            //iTem.Textには、入力Open、入力GNDが入るので、文字列で確認する。
            for (int i = 0; i < mnuEdit.DropDownItems.Count; i++)
            {
                if (iTem.Equals(contextMenuStripDA.Items[i]))
                {
                    ////i番目のメニュが選択された
                    //選択されているセルを表示
                    switch (i)
                    {
                        case 0:     //Undo
                            mnuUndo_Click(sender, e);
                            break;
                        case 1:     //Cut
                            //選択されたセルをクリップボードにコピーする
                            //Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
                            libDataGridView.dataGridView_Copy(dataGridView1, 0, getiDpLMT);
                            Console.WriteLine("選択されているセル");
                            dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                            libDataGridView.dataGridView_Clear(dataGridView1, 0);
                            break;
                        case 2:     //Copy
                            //選択されたセルをクリップボードにコピーする
                            //Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
                            //選択されたセルをクリップボードにコピーする
                            libDataGridView.dataGridView_Copy(dataGridView1, 0, getiDpLMT);
                            break;
                        case 3:     //Paste
                            dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                            //libDataGridView.dataGridView_Paste(dataGridView1, 0);
                            libDataGridView.dataGridView_Paste(dataGridView1, 0, new libDataGridView.delegateCanPaste(CanPaste), new libDataGridView.delegateSetiDpLMT(SetiDpLMT));
                            break;
                    }
                    break;
                }
            }
        }

        private string getiDpLMT(DataGridView dataGridView1, int insertColumnIndex, int insertRowIndex)
        {
            //DataGridView dataGridView1 = (DataGridView)sender;
            //該当のCellがiDp/nDpなら、Limit値を返す
            string returnString = "";
            if ((insertColumnIndex >= 3 && insertColumnIndex < (32 * 8 + 3)))
            {
                System.Diagnostics.Debug.WriteLine(string.Format("getiDpLMT:{0},{1}", insertColumnIndex, insertRowIndex));
                string CellValue = dataGridView1[insertColumnIndex, insertRowIndex].Value.ToString();
                if (CellValue == "iDp" || CellValue == "nDp")   //20180816
                {
                    int iDIO = (insertColumnIndex - 3) / 32;

                    if (insertRowIndex >= 0)
                    {
                        DataRow dtListDatRow = myDataSetItems.ListDat.Rows[insertRowIndex];
                        int jj = 32 - (insertColumnIndex - 3) % 32;
                        string iFeildName = string.Format("{0}-{1:00}", Default.DioNames[iDIO], jj);
                        //dtView_DIO.Rows[j]["IO"] = dtListDatRow[iFeildName];
                        returnString = string.Format(":{0}:{1}", dtListDatRow[iFeildName + "L"], dtListDatRow[iFeildName + "H"]);
                    }
                }
            }
            return returnString;
        }

        bool CanPaste(string val, int ColumnIndex, int RowIndex)
        {
            if (val == "") return true;

            if (ColumnIndex == 0) return true;
            if (ColumnIndex == 1) return true;
            if (ColumnIndex == 2 && (val == "0" || val == "1" || val == "2")) return true;

            if ((ColumnIndex >= 3 && ColumnIndex < (32 * 8 + 3)) && mc.dicDioStat.ContainsKey(val)) return true;
            //AO 2Col
            if ((ColumnIndex >= (32 * 8 + 3) && ColumnIndex < (32 * 8 + 3 + 2)) && AOinRange(val)) return true;
            //AI-L,AI-H*2=16
            if ((ColumnIndex >= (32 * 8 + 3 + 2) && ColumnIndex < (32 * 8 + 3 + 2 + 16)) && AIinRange(val)) return true;
            //AoSw 8Col
            if ((ColumnIndex >= (32 * 8 + 3 + 2 + 16) && ColumnIndex < (32 * 8 + 3 + 2 + 16 + 8)) && (val == Default.AoSwichStat[0] || val == Default.AoSwichStat[1])) return true;
            return false;
        }

        bool SetiDpLMT(DataGridView dataGridView1, int ColumnIndex, int RowIndex, string setValue)  //20180817
        {
            //DataGridView dataGridView1 = (DataGridView)sender;
            //該当のCellがiDp/nDpなら、Limit値をSetする
            if ((ColumnIndex >= 3 && ColumnIndex < (32 * 8 + 3)))
            {
                if (RowIndex >= 0)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("SetiDpLMT:{0},{1}", ColumnIndex, RowIndex));
                    int iDIO = (ColumnIndex - 3) / 32;
                    DataRow dtListDatRow = myDataSetItems.ListDat.Rows[RowIndex];
                    int jj = 32 - (ColumnIndex - 3) % 32;
                    string iFeildName = string.Format("{0}-{1:00}", Default.DioNames[iDIO], jj);

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
                        //CellValueが"iDp"/"nDp"でないときは、LMTをクリア
                        dtListDatRow[iFeildName + "L"] = null;
                        dtListDatRow[iFeildName + "H"] = null;
                        return false;
                    }
                }   //!if (RowIndex >= 0) //Cellでなければ、LMTのコピペは不要
            }   //DIOでなければ、LMTのコピペは不要
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

        private void dvResults_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            // 行ヘッダのセル領域を、行番号を描画する長方形とする
            // （ただし右端に4ドットのすき間を空ける）
            Rectangle rect = new Rectangle(
              e.RowBounds.Location.X,
              e.RowBounds.Location.Y,
              dataGridView1.RowHeadersWidth - 4,
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

        private void button_AddItem_Click(object sender, EventArgs e)
        {
            DataTable dtInspectItem = myDataSetItems.ListDat;
            int CurrentIndexof = -1;
            DataRow InspectItemRowNew = dtInspectItem.NewRow();
            //InspectItemRowNew["Title"] = "";    // CurrentIndexof;    //for Debug
            //InspectItemRowNew["Type"] = "0";    //20160617

            if (dataGridView_Inspection.Rows.Count > 0 && dataGridView_Inspection.CurrentCell != null)
            {
                //this.dataGridView_Edaban.Rows.Insert(dataGridView_Edaban.CurrentCell.RowIndex,"","","","");   //bindのあるDatagridviewには、Add/Insertできません
                // なのでTableに追加する
                CurrentIndexof = dataGridView_Inspection.CurrentRow.Index;

                dtInspectItem.Rows.InsertAt(InspectItemRowNew, CurrentIndexof + 1); //InsertAtを使うことで、その位置に挿入される（ので）後にSortとかいらない
                CurrentIndexof = dataGridView_Inspection.CurrentRow.Index;
                //this.dataGridView_Inspection.CurrentCell = this.dataGridView_Inspection[0, CurrentIndexof];
                dtInspectItem.AcceptChanges();

                CurrentIndexof = dataGridView_Inspection.CurrentRow.Index;
                libDataGridView.dataGridView_SelectionClear(dataGridView_Inspection);

                this.dataGridView_Inspection.CurrentCell = this.dataGridView_Inspection[0, CurrentIndexof + 1];
                this.dataGridView_Inspection.Rows[CurrentIndexof + 1].Selected = true;

                //for (int i = 0; i < this.dataGridView_Inspection.ColumnCount; i++)
                //{
                //    this.dataGridView_Inspection[i, CurrentIndexof + 1].Selected = true;
                //}
            }
            else
            {
                dtInspectItem.Rows.Add(InspectItemRowNew); //InsertAtを使うことで、その位置に挿入される（ので）後にSortとかいらない
            }
        }


        private void button_DeleteItem_Click(object sender, EventArgs e)
        {
            DataTable dtInspectItem = myDataSetItems.ListDat;
            if (dataGridView_Inspection.Rows.Count > 0 && dataGridView_Inspection.CurrentCell != null)
            {
                int minRows = libDataGridView.minSelectedRows(dataGridView_Inspection);
                int CurrentIndexof = dataGridView_Inspection.CurrentRow.Index;

                int countRows = libDataGridView.countSelectedRows(dataGridView_Inspection);
                if (countRows == this.dataGridView_Inspection.Rows.Count)
                {
                    DialogResult dr = System.Windows.Forms.MessageBox.Show("本当に全部を削除するのですか？", Default.ApplicationName, MessageBoxButtons.YesNo);
                    if (dr != System.Windows.Forms.DialogResult.Yes)
                    {
                        return;
                    }
                }
                if (countRows < dataGridView_Inspection.Rows.Count)
                {
                    //行を削除後だと、カーソル移動がうまくいかないので、削除前に、（残す）カーソルに移動する
                    if (CurrentIndexof + countRows < dataGridView_Inspection.Rows.Count)
                    {
                        //削除後、うしろに行が残るときは、残る後ろのほうの行に移動する
                        int CurrentRowIndex = CurrentIndexof + countRows;
                        libDataGridView.dataGridView_SelectionClear(dataGridView_Inspection);
                        this.dataGridView_Inspection.CurrentCell = this.dataGridView_Inspection[0, CurrentRowIndex];
                        this.dataGridView_Inspection.Rows[CurrentRowIndex].Selected = true;
                    }
                    else
                    {
                        //少なくとも、１行目が残るときは、前方へ移動する
                        int CurrentRowIndex = CurrentIndexof - 1;
                        libDataGridView.dataGridView_SelectionClear(dataGridView_Inspection);
                        this.dataGridView_Inspection.CurrentCell = this.dataGridView_Inspection[0, CurrentRowIndex];
                        this.dataGridView_Inspection.Rows[CurrentRowIndex].Selected = true;
                    }
                }

                libDataGridView.dataGridView_SelectionClear(this.dataGridView_Inspection);
                for (int i = 0; i < countRows; i++)
                {
                    dataGridView_Inspection.Rows.RemoveAt(minRows + countRows - 1 - i);
                    //dtInspectItem.Rows[minRows + countRows - 1 - i].Delete();
                }
                dtInspectItem.AcceptChanges();  //20161013
                this.dataGridView_Inspection.Refresh();
                if (dataGridView_Inspection.Rows.Count == 0)
                {
                    //全部削除した後は、１行追加する
                    button_AddItem_Click(sender, e);
                }
            }
        }

        private void button_MoveUp_Click(object sender, EventArgs e)
        {
            DataTable dtInspectItem = myDataSetItems.ListDat;
            int CurrentRowIndex = 0;
            int CurrentIndexof = -1;

            if (dataGridView_Inspection.Rows.Count >= 2 && dataGridView_Inspection.CurrentCell != null)
            {
                CurrentIndexof = dataGridView_Inspection.CurrentRow.Index;
                CurrentRowIndex = dataGridView_Inspection.CurrentCell.RowIndex;
                if (CurrentIndexof > 0)
                {
                    DataRow dtInspectItemRowCurr = dtInspectItem.NewRow();
                    dtInspectItemRowCurr.ItemArray = dtInspectItem.Rows[CurrentIndexof].ItemArray;
                    dtInspectItem.Rows.InsertAt(dtInspectItemRowCurr, CurrentIndexof - 1);
                    CurrentIndexof = dataGridView_Inspection.CurrentRow.Index;
                    dtInspectItem.Rows.RemoveAt(CurrentIndexof);
                    this.dataGridView_Inspection.CurrentCell = this.dataGridView_Inspection[0, CurrentRowIndex - 1];
                }
            }
        }

        private void button_MoveDown_Click(object sender, EventArgs e)
        {
            DataTable dtInspectItem = myDataSetItems.ListDat;
            int CurrentRowIndex = 0;
            int CurrentIndexof = -1;

            if (dataGridView_Inspection.Rows.Count >= 2 && dataGridView_Inspection.CurrentCell != null)
            {
                CurrentIndexof = dataGridView_Inspection.CurrentRow.Index;
                CurrentRowIndex = dataGridView_Inspection.CurrentCell.RowIndex;
                if (CurrentIndexof < dataGridView_Inspection.Rows.Count - 1)
                {
                    DataRow dtInspectItemRowCurr = dtInspectItem.NewRow();
                    dtInspectItemRowCurr.ItemArray = dtInspectItem.Rows[CurrentIndexof].ItemArray;
                    dtInspectItem.Rows.InsertAt(dtInspectItemRowCurr, CurrentIndexof + 2);
                    CurrentIndexof = dataGridView_Inspection.CurrentRow.Index;
                    dtInspectItem.Rows.RemoveAt(CurrentIndexof);
                    this.dataGridView_Inspection.CurrentCell = this.dataGridView_Inspection[0, CurrentRowIndex + 1];
                }
            }
        }

        private void button_SaveClose_Click(object sender, EventArgs e)
        {
            //if (this.comboBox_Volt.SelectedIndex == -1)
            //{
            //    myDataSetItems.CheckDat.Rows[0]["Volt"] = 0;
            //}
            //else
            //{
            //    myDataSetItems.CheckDat.Rows[0]["Volt"] = this.comboBox_Volt.SelectedIndex;
            //}

            DialogResult dr;
            if (mc.checkChangedPortdatFile(myDataSetItems.PortDat, Folder + System.IO.Path.DirectorySeparatorChar + Default.Portdat)
                || mc.checkChangedInspectItem(myDataSetItems.ListDat, Folder + System.IO.Path.DirectorySeparatorChar + Default.Listdat)
                || mc.checkChangedCheckdat(myDataSetItems.CheckDat, Folder + System.IO.Path.DirectorySeparatorChar + Default.Checkdat))
            {
            }
            else
            {
                dr = System.Windows.Forms.MessageBox.Show("変更はありませんが、保存しますか？", Default.ApplicationName, MessageBoxButtons.YesNo);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
            }
            dr = System.Windows.Forms.MessageBox.Show("変更履歴を作成しますか？", Default.ApplicationName, MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                //ここで変更履歴を作成
                string FilePath = Folder + System.IO.Path.DirectorySeparatorChar + "変更履歴.txt";
                bool closed = CloseNotepad(FilePath);
                if (closed)
                {
                    StartNotepad(FilePath);
                }
                else
                {
                    Console.WriteLine("CloseNotepad: 終了しません。");
                }
            }

            //if (mc.DataSetInspectItem.GetChanges() != null)
            {
                //loadInspectItem
                mc.savePortdatFile(myDataSetItems.PortDat, Folder + System.IO.Path.DirectorySeparatorChar + Default.Portdat);
            }
            //if (mc.DataSetInspectItem.GetChanges() != null)
            {
                //loadInspectItem
                mc.clearListdatLMT(myDataSetItems.ListDat);     //20180818
                mc.saveInspectItem(myDataSetItems.ListDat, Folder + System.IO.Path.DirectorySeparatorChar + Default.Listdat);
            }
            //if (mc.DataSetTopMenu.CheckDat.GetChanges() != null)
            {
                mc.saveCheckdat(myDataSetItems.CheckDat, Folder + System.IO.Path.DirectorySeparatorChar + Default.Checkdat);
            }

            this.DialogResult = DialogResult.OK;
            //mForms.Hide(this);
            this.Close();
        }

        private void button_EditTanshi_Click(object sender, EventArgs e)
        {
            PortView fmPort = mForms.fmPort;
            fmPort.myDataSetItems = myDataSetItems;

            fmPort.ShowDialog();
        }

        private void button_EditItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_Inspection.CurrentCell == null)
            {
                return;     //データがないときは、何もしない
            }
            DataInputView fmDataInput = mForms.fmDataInput;
            fmDataInput.StartTNo = dataGridView_Inspection.CurrentCell.RowIndex;
            fmDataInput.myDataSetItems = myDataSetItems;

            fmDataInput.ShowDialog();
        }

        private void frmSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            //mForms.Hide(this);
            System.Diagnostics.Debug.WriteLine("frmSetting_FormClosing");
        }
        private void frmSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
#if false
            this.button_SaveClose.Focus();  //textBox_Item,textBox_Zuban,textBox_Edabanの変更を確定するために、フォーカスを移動する
            if (this.comboBox_Volt.SelectedIndex == -1)
            {
                myDataSetItems.CheckDat.Rows[0]["Volt"] = 0;
            }
            else
            {
                myDataSetItems.CheckDat.Rows[0]["Volt"] = this.comboBox_Volt.SelectedIndex;
            }

            if (mc.checkChangedPortdatFile(myDataSetItems.PortDat, Folder + System.IO.Path.DirectorySeparatorChar + Default.Portdat)
                || mc.checkChangedInspectItem(myDataSetItems.ListDat, Folder + System.IO.Path.DirectorySeparatorChar + Default.Listdat)
                || mc.checkChangedCheckdat(myDataSetItems.CheckDat, Folder + System.IO.Path.DirectorySeparatorChar + Default.Checkdat))
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show("検査定義が変更されています。保存しますか？", Default.ApplicationName, MessageBoxButtons.YesNo);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
                //if (mc.DataSetInspectItem.GetChanges() != null)
                {
                    //loadInspectItem
                    mc.savePortdatFile(myDataSetItems.PortDat, Folder + System.IO.Path.DirectorySeparatorChar + Default.Portdat);
                }
                //if (mc.DataSetInspectItem.GetChanges() != null)
                {
                    //loadInspectItem
                    mc.clearListdatLMT(myDataSetItems.ListDat);     //20180818
                    mc.saveInspectItem(myDataSetItems.ListDat, Folder + System.IO.Path.DirectorySeparatorChar + Default.Listdat);
                }
                //if (mc.DataSetTopMenu.CheckDat.GetChanges() != null)
                {
                    mc.saveCheckdat(myDataSetItems.CheckDat, Folder + System.IO.Path.DirectorySeparatorChar + Default.Checkdat);
                }
            }
            else
            {
            }
#endif
        }

        private void contextMenuStripDA_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip menu = (ContextMenuStrip)sender;

            dataGridView_DA_CellContextMenuStriped = (DataGridView)menu.SourceControl;

        }

        private void dataGridView_Inspection_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            //dataGridView_DA_CellContextMenuStriped = dataGridView1;

            if (e.RowIndex < 0)
            {
                //列ヘッダーに表示するContextMenuStripを設定する
                //e.ContextMenuStrip = this.ContextMenuStrip1;
                e.ContextMenuStrip = this.contextMenuStripDA;   //20170216
                libDataGridView.dataGridView_SelectionClear(dataGridView1); //20170216
                dataGridView1.Columns[e.ColumnIndex].Selected = true;//20170216
            }
            else if (e.ColumnIndex < 0)
            {
                //行ヘッダーに表示するContextMenuStripを設定する
                //e.ContextMenuStrip = this.ContextMenuStrip2;
                e.ContextMenuStrip = this.contextMenuStripDA;   //20170216
                libDataGridView.dataGridView_SelectionClear(dataGridView1); //20170216
                dataGridView1.Rows[e.RowIndex].Selected = true;//20170216
            }
            else
            {
                //ContextMenuStripを表示する
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                e.ContextMenuStrip = this.contextMenuStripDA;
            }
        }


        private void contextMenuStripDA_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStripAO_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStripAO_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_Inspection_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
#if true
            DataGridView dataGridView1 = (DataGridView)sender;

            if (e.ColumnIndex >= 3 && e.ColumnIndex < (32 * 8 + 3))
            {
                string Value = e.Value.ToString();
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
                    if ( (dataGridView1[e.ColumnIndex, e.RowIndex].Style.Font == null) || ( dataGridView1[e.ColumnIndex, e.RowIndex].Style.Font.Style != Default.CellStyles[CellStylesIdx].fontstyle))
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


#else
            DataGridView dataGridView1 = (DataGridView)sender;

            if (e.ColumnIndex >= 3 && e.ColumnIndex < (32 * 8 + 3))
            {
                string Value = e.Value.ToString();
                if (Value.Length > 0 && Value.Substring(0, 1) == "o")
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.Red;
                    //dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;
                }
                else
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Black;             //20170908-1
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.SelectionForeColor = Color.Black;    //20170908-1
                    ////dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Black;
                }
            }
#endif
        }

        private void dataGridViewAcceptChanges()
        {
            savedListDat = myDataSetItems.Tables["ListDat"].Copy();
            this.mnuUndo.Enabled = true;
            contextMenuStripDA.Items[0].Enabled = true;        //Undoを許可
        }

        private void mnuUndo_Click(object sender, EventArgs e)
        {
            if (savedListDat != null)
            {
                libDataGridView.dataGridView_SelectionClear(this.dataGridView_Inspection); //20170216

                myDataSetItems.Tables["ListDat"].Rows.Clear();
                foreach (DataRow ListDatRow in savedListDat.Rows)
                {
                    myDataSetItems.Tables["ListDat"].ImportRow(ListDatRow);
                }

                //myDataSetItems.Tables["ListDat"].RejectChanges();
                this.mnuUndo.Enabled = false;
                contextMenuStripDA.Items[0].Enabled = false;        //Undoを不許可
                this.savedListDat = null;
            }
        }

        private void dataGridView_Inspection_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;

            if (e.KeyData == (Keys.Control | Keys.V))
            {
                // Ctrl + V
                System.Diagnostics.Debug.WriteLine("Ctrl + V が押されました。");
                dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                //libDataGridView.dataGridView_Paste(dataGridView1,0);
                libDataGridView.dataGridView_Paste(dataGridView1, 0, new libDataGridView.delegateCanPaste(CanPaste), new libDataGridView.delegateSetiDpLMT(SetiDpLMT));
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.Z))
            {
                // Ctrl + X
                System.Diagnostics.Debug.WriteLine("Ctrl + Z が押されました。");
                //Undo
                mnuUndo_Click(sender, e);
                dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.X))
            {
                // Ctrl + X
                System.Diagnostics.Debug.WriteLine("Ctrl + X が押されました。");
                //選択されたセルをクリップボードにコピーする
                //Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
                libDataGridView.dataGridView_Copy(dataGridView1, 0, getiDpLMT);
                dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                libDataGridView.dataGridView_Clear(dataGridView1, 0);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == (Keys.Control | Keys.C))
            {
                // Ctrl + C
                System.Diagnostics.Debug.WriteLine("Ctrl + C が押されました。");
                //選択されたセルをクリップボードにコピーする
                libDataGridView.dataGridView_Copy(dataGridView1, 0, getiDpLMT);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == (Keys.Delete))
            {
                // Delete
                System.Diagnostics.Debug.WriteLine("Delete が押されました。");
                dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
                libDataGridView.dataGridView_Clear(dataGridView1, 0);
            }
        }

        private void mnuCut_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView1 = dataGridView_Inspection;

            //選択されたセルをクリップボードにコピーする
            //Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
            libDataGridView.dataGridView_Copy(dataGridView1, 0, getiDpLMT);
            Console.WriteLine("選択されているセル");
            dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
            libDataGridView.dataGridView_Clear(dataGridView1, 0);
        }

        private void mmuCopy_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView1 = dataGridView_Inspection;

            //選択されたセルをクリップボードにコピーする
            //Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
            libDataGridView.dataGridView_Copy(dataGridView1, 0, getiDpLMT);
        }

        private void mnuPaste_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView1 = dataGridView_Inspection;

            dataGridViewAcceptChanges();    //undoできるように、変更を確定する。
            //libDataGridView.dataGridView_Paste(dataGridView1,0);
            libDataGridView.dataGridView_Paste(dataGridView1, 0, new libDataGridView.delegateCanPaste(CanPaste), new libDataGridView.delegateSetiDpLMT(SetiDpLMT));
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            button_SaveClose_Click(sender, e);
        }

        private void dataGridView_Inspection_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button_EditItem_Click(sender, e);
        }

        private void dataGridView_Inspection_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            DataGridViewSelectedCellCollection BeforeSelectedCells = dataGridView1.SelectedCells;

            if (e.Location.Y < dataGridView1.ColumnHeadersHeight)
            {
                if (dataGridView1.SelectionMode != DataGridViewSelectionMode.ColumnHeaderSelect)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_Inspection_MouseMove X={0} Y{1} SelectionMode={2}", e.Location.X, e.Location.Y, "ColumnHeaderSelect"));
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
                    foreach (DataGridViewCell c in BeforeSelectedCells)
                    {
                        //SelectionModeを変更すると、選択がResetされるので、選択されていたセルをSelectする。
                        c.Selected = true;
                    }

                }
            }
            else if (e.Location.X < dataGridView1.RowHeadersWidth)
            {
                if (dataGridView1.SelectionMode != DataGridViewSelectionMode.RowHeaderSelect)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("dataGridView_Inspection_MouseMove X={0} Y{1} SelectionMode={2}", e.Location.X, e.Location.Y, "RowHeaderSelect"));
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                    foreach (DataGridViewCell c in BeforeSelectedCells)
                    {
                        //SelectionModeを変更すると、選択がResetされるので、選択されていたセルをSelectする。
                        c.Selected = true;
                    }
                }
            }
        }

        private void buttonHLLimit_Click(object sender, EventArgs e)
        {
            SettingLimitView fmSLMT = new SettingLimitView();
            fmSLMT.myDataSetItems = myDataSetItems;
            fmSLMT.Folder = this.Folder;
            fmSLMT.ShowDialog();
            fmSLMT.Dispose();
            string strVolt = myDataSetItems.CheckDat.Rows[0]["Volt"].ToString();
            this.textBox_Volt.Text = getVoltString(strVolt);
            bindingSourceCheckDat.ResetItem(0);
        }

        private void button検査履歴表示_Click(object sender, EventArgs e)
        {
            //ここで変更履歴を作成
            string FilePath = Folder + System.IO.Path.DirectorySeparatorChar + "変更履歴.txt";
            bool closed = CloseNotepad(FilePath);
            if (closed)
            {
                OpenNotepad(FilePath);
            }
            else
            {
                Console.WriteLine("CloseNotepad: 終了しません。");
            }
        }

    }
}
