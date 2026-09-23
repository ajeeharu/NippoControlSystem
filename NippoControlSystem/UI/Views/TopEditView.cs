using NippoControlSystem.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.UI.Views
{
    public partial class TopEditView : Form
    {
        //　------　MVVMパターン用にリファクタリングしたコード　------

        //　------　MVVM化のためにリファクタリングする前のコード　------

        Settings Default = Settings.GetInstance();
        //Cyc.IO.cDio dio = Cyc.IO.cDio.GetInstance();
        //Cyc.IO.NippoDIO nio = Cyc.IO.NippoDIO.GetInstance();
        MeasureCondition mc = MeasureCondition.GetInstance();
        Views mForms = Views.GetInstance();

        public TopEditView()
        {
            InitializeComponent();
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ DataSetTopMenu
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataSetTopMenu myDataSetTopMenu
        {
            get;
            set;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ SelectedNo
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedMainNo
        {
            get;
            set;
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ SelectedSubNo
        int m_SelectSubNo = 0;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedSubNo
        {
            get { return m_SelectSubNo; }
            set
            {
                m_SelectSubNo = value;
                //updateCurrentCell();  20161203
            }
        }

        private void frmTopEdit_Load(object sender, EventArgs e)
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmTopEdit"]);
            Views.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmTopEdit"]);

            //ここからはFormの更新
            this.radioButton_Zuban.Text = Default.MainNo;   //"仕様書番号";
            this.radioButton_Edaban.Text = string.Format("{0}とフォルダ", Default.SubNo);     //"追番";
            this.radioButton_Zuban.Checked = true;
            //dataGridView
            this.dataGridView_Zuban.Columns["ColumnTitle"].HeaderText = Default.MainNo;
            //this.dataGridView_Zuban.Columns["ColumnTitle"].Width = this.dataGridView_Zuban.Width - 3;
            this.dataGridView_Zuban.Columns["ColumnTitle"].Width = 1000;
            this.dataGridView_Edaban.Columns["ColumnFolder"].HeaderText = "フォルダ";
            //this.dataGridView_Edaban.Columns["ColumnFolder"].Width = this.dataGridView_Edaban.Width - this.dataGridView_Edaban.Columns["ColumnSubTitle"].Width - 3;
            this.dataGridView_Edaban.Columns["ColumnFolder"].Width = 1000;  //幅がきっちりだと、文字数オーバ時に...になるため
            this.dataGridView_Edaban.Columns["ColumnSubTitle"].HeaderText = Default.SubNo;
            this.dataGridView_Edaban.Columns["ColumnSubTitle"].Width = 100;

            //DataSet
            this.dataGridView_Zuban.DataSource = myDataSetTopMenu;
            this.dataGridView_Zuban.DataMember = "menuMain";
            //this.dataGridView_Zuban.Columns["ColumnMainID"].DataPropertyName = "MainID";
            //this.dataGridView_Zuban.Columns["ColumnTitle"].DataPropertyName = "Title";
            //DataGridViewColumn MainID = new DataGridViewTextBoxColumn();
            //MainID.Name = "ColumnMainID";
            //MainID.DataPropertyName = "MainID";
            //MainID.Visible = false;
            //this.dataGridView_Zuban.Columns.Add(MainID);

            this.dataGridView_Edaban.DataSource = myDataSetTopMenu;
            this.dataGridView_Edaban.DataMember = "menuMain.menuMain_menuSub";  //menuMain_menuSub
                                                                                //this.dataGridView_Edaban.Columns["ColumnSubMainID"].DataPropertyName = "MainID";
                                                                                //this.dataGridView_Edaban.Columns["ColumnSubID"].DataPropertyName = "SubID";
                                                                                //this.dataGridView_Edaban.Columns["ColumnSubTitle"].DataPropertyName = "SubTitle";
                                                                                //this.dataGridView_Edaban.Columns["ColumnFolder"].DataPropertyName = "Folder";

            //行を選択
            updateCurrentCell();

        }
        private void updateCurrentCell()
        {
            //行を選択
            if (this.dataGridView_Zuban.Rows.Count > 0)
                this.dataGridView_Zuban.CurrentCell = this.dataGridView_Zuban[0, SelectedMainNo];
            if (this.dataGridView_Edaban.Rows.Count > 0)
                this.dataGridView_Edaban.CurrentCell = this.dataGridView_Edaban[0, SelectedSubNo];
        }


        private void radioButton_Edaban_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Edaban.Checked)
            {
                this.radioButton_Zuban.ForeColor = Color.Black;
                this.radioButton_Edaban.ForeColor = Color.Red;
            }
        }

        private void radioButton_Zuban_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Zuban.Checked)
            {
                this.radioButton_Zuban.ForeColor = Color.Red;
                this.radioButton_Edaban.ForeColor = Color.Black;
            }
        }

        private void button_SaveClose_Click(object sender, EventArgs e)
        {
            //if (myDataSetTopMenu.GetChanges() != null)
            {
                //mc.DataSetTopMenu = (DataSetTopMenu)myDataSetTopMenu.Copy();
                myDataSetTopMenu.AcceptChanges();
                string menuCsvPass = Default.ApplicationFloder + Default.MenuFolder + Default.Menucsv;
                mc.saveMenuCsv(myDataSetTopMenu, menuCsvPass);
            }
            System.Diagnostics.Debug.WriteLine("button_SaveClose_Click");
            this.Close();
        }

        private void frmTopEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (myDataSetTopMenu.GetChanges() != null)
            {
                //Saveされないとき（キャンセル時）
                myDataSetTopMenu.RejectChanges();
            }
            System.Diagnostics.Debug.WriteLine("frmTopEdit_FormClosing");
        }

        private void frmTopEdit_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button_Edit_Click(object sender, EventArgs e)
        {
            string MainTitle;
            string SubTitle;
            string Folder;

            int SelectMainNo = 0;
            int SelectSubNo = 0;
            if (dataGridView_Zuban.Rows.Count > 0 && dataGridView_Zuban.CurrentCell != null)
            {
                this.radioButton_Edaban.Checked = true;
                if (dataGridView_Edaban.Rows.Count == 0)
                {
                    button_Insert_Click(sender, e);
                }
                SelectMainNo = (int)dataGridView_Edaban.CurrentRow.Cells["ColumnSubMainID"].Value;
                SelectSubNo = (int)dataGridView_Edaban.CurrentRow.Cells["ColumnSubID"].Value;
                TopEditInputView fmTopEditInput = mForms.fmTopEditInput;
                MainTitle = dataGridView_Zuban.CurrentRow.Cells["ColumnTitle"].Value.ToString();
                SubTitle = dataGridView_Edaban.CurrentRow.Cells["ColumnSubTitle"].Value.ToString();
                Folder = dataGridView_Edaban.CurrentRow.Cells["ColumnFolder"].Value.ToString();
                fmTopEditInput.MainTitle = MainTitle;
                fmTopEditInput.SubTitle = SubTitle;
                fmTopEditInput.Folder = Folder;
                fmTopEditInput.ShowDialog();
                if (MainTitle != fmTopEditInput.MainTitle || SubTitle != fmTopEditInput.SubTitle || Folder != fmTopEditInput.Folder)
                {
                    if (MainTitle != fmTopEditInput.MainTitle)
                    {
                        dataGridView_Zuban.CurrentRow.Cells["ColumnTitle"].Value = fmTopEditInput.MainTitle;
                    }
                    if (SubTitle != fmTopEditInput.SubTitle)
                    {
                        dataGridView_Edaban.CurrentRow.Cells["ColumnSubTitle"].Value = fmTopEditInput.SubTitle;
                    }
                    if (Folder != fmTopEditInput.Folder)
                    {
                        dataGridView_Edaban.CurrentRow.Cells["ColumnFolder"].Value = fmTopEditInput.Folder;
                    }
                    dataGridView_Edaban.Refresh();
                    this.Refresh();
                }

            }
        }
        private void button_Insert_Click(object sender, EventArgs e)
        {
            DataTable dtMain = myDataSetTopMenu.menuMain;
            DataTable dtSubMain = myDataSetTopMenu.menuSub;
            int CurrentIndexof = -1;
            int CurrentRowIndex = 0;
            int NewMainID = -1;

            if (this.radioButton_Zuban.Checked)
            {
                DataRow dtMainRowNew = dtMain.NewRow();
                if (dataGridView_Zuban.Rows.Count > 0 && dataGridView_Zuban.CurrentCell != null)
                {
                    CurrentIndexof = dataGridView_Zuban.CurrentRow.Index;
                    dtMain.Rows.InsertAt(dtMainRowNew, CurrentIndexof + 1); //InsertAtを使うことで、その位置に挿入される（ので）後にSortとかいらない
                    NewMainID = (int)dtMain.Rows[CurrentIndexof + 1]["MainID"];
                    dtMain.AcceptChanges();
                    CurrentIndexof = dataGridView_Zuban.CurrentRow.Index;
                    libDataGridView.dataGridView_SelectionClear(dataGridView_Zuban);
                    this.dataGridView_Zuban.CurrentCell = this.dataGridView_Zuban[0, CurrentIndexof + 1];
                    this.dataGridView_Zuban.Rows[CurrentIndexof + 1].Selected = true;
                }
                else
                {
                    //CurrentIndexof = dataGridView_Zuban.CurrentRow.Index;
                    dtMain.Rows.Add(dtMainRowNew); //InsertAtを使うことで、その位置に挿入される（ので）後にSortとかいらない
                    NewMainID = (int)dtMainRowNew["MainID"];
                    dtMain.AcceptChanges();
                    CurrentIndexof = 0;
                    libDataGridView.dataGridView_SelectionClear(dataGridView_Zuban);
                    this.dataGridView_Zuban.CurrentCell = this.dataGridView_Zuban[0, CurrentIndexof];
                    this.dataGridView_Zuban.Rows[CurrentIndexof].Selected = true;
                }

                //Subにも追加
                DataRow dtSubMainRowNew = dtSubMain.NewRow();
                dtSubMainRowNew["MainID"] = NewMainID;//MainIDに連動させる
                dtSubMainRowNew["SubTitle"] = "";   // CurrentRowIndex.ToString();
                dtSubMainRowNew["Folder"] = "";
                dtSubMain.Rows.Add(dtSubMainRowNew);

            }
            else
            {
                if (dataGridView_Zuban.Rows.Count > 0 && dataGridView_Zuban.CurrentCell != null)
                {
                    DataRow dtSubMainRowNew = dtSubMain.NewRow();
                    dtSubMainRowNew["MainID"] = dataGridView_Zuban.CurrentRow.Cells["ColumnMainID"].Value;//MainIDに連動させる
                    dtSubMainRowNew["SubTitle"] = "";   // CurrentRowIndex.ToString();
                    dtSubMainRowNew["Folder"] = "";

                    if (dataGridView_Edaban.CurrentRow == null)
                    {
                        //Currentがないので、適当に追加する
                        dtSubMain.Rows.Add(dtSubMainRowNew);
                    }
                    else
                    {
                        //どうしても、選択行の前に追加されるので、最下位行のときは、Addでやる
                        CurrentRowIndex = dataGridView_Edaban.CurrentCell.RowIndex;
                        int ColumnSubID = int.Parse(dataGridView_Edaban.Rows[CurrentRowIndex].Cells["ColumnSubID"].Value.ToString());
                        DataRow[] dtSubRowCurr = dtSubMain.Select(string.Format("SubID='{0}'", ColumnSubID));
                        if (dtSubRowCurr.Length > 0)
                        {
                            CurrentIndexof = dtSubMain.Rows.IndexOf(dtSubRowCurr[0]);
                            //this.dataGridView_Edaban.Rows.Insert(dataGridView_Edaban.CurrentCell.RowIndex,"","","","");   //bindのあるDatagridviewには、Add/Insertできません
                            // なのでTableに追加する
                            if ((dataGridView_Edaban.Rows.Count - 1) == CurrentIndexof)
                            {
                                dtSubMain.Rows.Add(dtSubMainRowNew);
                                this.dataGridView_Edaban.CurrentCell = this.dataGridView_Edaban[0, dataGridView_Edaban.Rows.Count - 1];
                            }
                            else
                            {
                                dtSubMain.Rows.InsertAt(dtSubMainRowNew, CurrentIndexof + 1); //InsertAtを使うことで、その位置に挿入される（ので）後にSortとかいらない
                                this.dataGridView_Edaban.CurrentCell = this.dataGridView_Edaban[0, CurrentRowIndex + 1];
                            }
                        }
                    }
                }
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            DataTable dtMain = myDataSetTopMenu.menuMain;
            DataTable dtSubMain = myDataSetTopMenu.menuSub;
            int CurrentRowIndex = 0;
            int CurrentIndexof = -1;

            if (this.radioButton_Zuban.Checked)
            {
                if (dataGridView_Zuban.SelectedRows.Count > 1)
                {
                    System.Windows.Forms.MessageBox.Show("複数行の削除はできません", Default.ApplicationName);
                    return;
                }
                if (dataGridView_Zuban.Rows.Count > 0 && dataGridView_Zuban.CurrentCell != null)
                {
                    CurrentRowIndex = dataGridView_Zuban.CurrentCell.RowIndex;
                    int ColumnMainID = int.Parse(dataGridView_Zuban.Rows[CurrentRowIndex].Cells["ColumnMainID"].Value.ToString());
                    DataRow[] dtMainRowCurr = dtMain.Select(string.Format("MainID='{0}'", ColumnMainID));
                    if (dtMainRowCurr.Length > 0)
                    {
                        //削除後だとカーソルの移動が変なので、先に移動しておく
                        if (dataGridView_Zuban.Rows.Count > 1)
                        {
                            libDataGridView.dataGridView_SelectionClear(dataGridView_Zuban);
                            libDataGridView.dataGridView_SelectionClear(dataGridView_Edaban);
                            if (CurrentRowIndex == (dataGridView_Zuban.Rows.Count - 1))
                            {
                                CurrentIndexof = CurrentRowIndex > 0 ? CurrentRowIndex - 1 : 0;
                                //削除行は最終なので、自分より前の行にカーソルを移動する
                                this.dataGridView_Zuban.CurrentCell = this.dataGridView_Zuban[0, CurrentIndexof];
                                this.dataGridView_Zuban.Rows[CurrentIndexof].Selected = true;
                            }
                            else
                            {
                                //削除行は、最終でないので、削除行より、うしろの行にカーソルを移動する
                                CurrentIndexof = CurrentRowIndex;
                                //削除行は最終なので、自分より前の行にカーソルを移動する
                                this.dataGridView_Zuban.CurrentCell = this.dataGridView_Zuban[0, CurrentIndexof];
                                this.dataGridView_Zuban.Rows[CurrentIndexof].Selected = true;
                            }
                        }
                        //実際に削除する
                        int MainID = int.Parse(dtMainRowCurr[0]["MainID"].ToString());
                        dtMainRowCurr[0].Delete();

                        //dtSubのMainIDを削除する。MainID→NewMainID
                        foreach (DataRow currentRow in dtSubMain.Select(string.Format("MainID='{0}'", MainID)))
                        {
                            //currentRow["MainID"] = NewMainID;
                            currentRow.Delete();
                        }
                    }
                }
            }
            else
            {
                if (dataGridView_Edaban.SelectedRows.Count > 1)
                {
                    System.Windows.Forms.MessageBox.Show("複数行の削除はできません", Default.ApplicationName);
                    return;
                }
                if (dataGridView_Edaban.Rows.Count > 0 && dataGridView_Edaban.CurrentRow != null)
                {
                    CurrentIndexof = dataGridView_Edaban.CurrentRow.Index;
                    CurrentRowIndex = dataGridView_Edaban.CurrentCell.RowIndex;
                    int ColumnSubID = int.Parse(dataGridView_Edaban.Rows[CurrentRowIndex].Cells["ColumnSubID"].Value.ToString());
                    DataRow[] dtsubMainRowCurr = dtSubMain.Select(string.Format("SubID='{0}'", ColumnSubID));
                    if (dtsubMainRowCurr.Length > 0)
                    {
                        //該当の行があったので、削除する
                        //削除後だとカーソルの移動が変なので、削除前に移動しておく
                        if (dataGridView_Edaban.Rows.Count > 1)
                        {
                            libDataGridView.dataGridView_SelectionClear(dataGridView_Edaban);
                            if (CurrentRowIndex == (dataGridView_Edaban.Rows.Count - 1))
                            {
                                CurrentIndexof = CurrentRowIndex > 0 ? CurrentRowIndex - 1 : 0;
                                //削除行は最終なので、自分より前の行にカーソルを移動する
                                this.dataGridView_Edaban.CurrentCell = this.dataGridView_Edaban[0, CurrentIndexof];
                                this.dataGridView_Edaban.Rows[CurrentIndexof].Selected = true;
                            }
                            else
                            {
                                //削除行は、最終でないので、削除行より、うしろの行にカーソルを移動する
                                CurrentIndexof = CurrentRowIndex;
                                //削除行は最終なので、自分より前の行にカーソルを移動する
                                this.dataGridView_Edaban.CurrentCell = this.dataGridView_Edaban[0, CurrentIndexof];
                                this.dataGridView_Edaban.Rows[CurrentIndexof].Selected = true;
                            }
                        }
                        //実際に削除する
                        dtsubMainRowCurr[0].Delete();
                    }
                }
            }
        }

        private void button_MoveUp_Click(object sender, EventArgs e)
        {
            DataTable dtMain = myDataSetTopMenu.menuMain;
            DataTable dtSubMain = myDataSetTopMenu.menuSub;
            int CurrentRowIndex = 0;
            int CurrentIndexof = -1;

            if (this.radioButton_Zuban.Checked)
            {
                if (dataGridView_Zuban.Rows.Count > 0 && dataGridView_Zuban.CurrentCell != null)
                {
                    CurrentRowIndex = dataGridView_Zuban.CurrentCell.RowIndex;
                    int ColumnMainID = int.Parse(dataGridView_Zuban.Rows[CurrentRowIndex].Cells["ColumnMainID"].Value.ToString());
                    DataRow [] dtMainRowCurr = dtMain.Select(string.Format("MainID='{0}'", ColumnMainID));
                    if (CurrentRowIndex > 0 && dtMainRowCurr.Length > 0)
                    {
                        CurrentIndexof = dtMain.Rows.IndexOf(dtMainRowCurr[0]);
                        int MainID = int.Parse(dtMainRowCurr[0]["MainID"].ToString());
                        DataRow dtMainRowNew = dtMain.NewRow();
                        dtMainRowNew["Title"] = dtMainRowCurr[0]["Title"];
                        dtMain.Rows.InsertAt(dtMainRowNew, CurrentIndexof - 1); //InsertAtを使うことで、その位置に挿入される（ので）後にSortとかいらない
                        int NewMainID = int.Parse(dtMain.Rows[CurrentIndexof - 1]["MainID"].ToString());
                        dtMainRowCurr[0].Delete();

                        //dtSubのMainIDを更新する。MainID→NewMainID
                        foreach (DataRow currentRow in dtSubMain.Select(string.Format("MainID='{0}'", MainID)))
                        {
                            currentRow["MainID"] = NewMainID;
                        }

                        //dataGridView_Edaban.Sort(this.dataGridView_Edaban.Columns["ColumnSubSortID"], sortDirection);
                        this.dataGridView_Zuban.CurrentCell = this.dataGridView_Zuban[0, CurrentRowIndex - 1];
                    }
                }
            }
            else
            {
                if (dataGridView_Edaban.Rows.Count > 0 && dataGridView_Edaban.CurrentCell != null)
                {
                    ////this.dataGridView_Edaban.Rows.Insert(dataGridView_Edaban.CurrentCell.RowIndex,"","","","");   //bindのあるDatagridviewには、Add/Insertできません
                    //// なのでTableに追加する
                    //CurrentRowIndex = dataGridView_Edaban.CurrentCell.RowIndex;
                    //int ColumnSubID = int.Parse(dataGridView_Edaban.Rows[CurrentRowIndex].Cells["ColumnSubID"].Value.ToString());
                    //DataRow [] dtSubMainRowCurr = dtSubMain.Select(string.Format("SubID='{0}'", ColumnSubID));
                    //if (CurrentRowIndex > 0 && dtSubMainRowCurr.Length > 0)
                    //{
                    //    CurrentIndexof = dtSubMain.Rows.IndexOf(dtSubMainRowCurr[0]);
                    //    int SubID = int.Parse(dtSubMainRowCurr[0]["SubID"].ToString());
                    //    DataRow dtSubMainRowNew = dtSubMain.NewRow();
                    //    dtSubMainRowNew["MainID"] = dtSubMainRowCurr[0]["MainID"];//MainIDに連動させる
                    //    dtSubMainRowNew["SubTitle"] = dtSubMainRowCurr[0]["SubTitle"];
                    //    dtSubMainRowNew["Folder"] = dtSubMainRowCurr[0]["Folder"];
                    //    //dtSubMain.Rows.Add(dtSubMainRow);
                    //    dtSubMain.Rows.InsertAt(dtSubMainRowNew, CurrentIndexof - 1); //InsertAtを使うことで、その位置に挿入される（ので）後にSortとかいらない
                    //    dtSubMainRowCurr[0].Delete();

                    //    //dataGridView_Edaban.Sort(this.dataGridView_Edaban.Columns["ColumnSubSortID"], sortDirection);
                    //    this.dataGridView_Edaban.CurrentCell = this.dataGridView_Edaban[0, CurrentRowIndex - 1];
                    //}
                    if (dataGridView_Edaban.CurrentCell.RowIndex > 0)
                    {
                        CurrentRowIndex = dataGridView_Edaban.CurrentCell.RowIndex;
                        int ColumnMainID = int.Parse(dataGridView_Zuban.CurrentRow.Cells["ColumnMainID"].Value.ToString());
                        DataRow[] dtSubMainRowCurr = dtSubMain.Select(string.Format("MainID='{0}'", ColumnMainID));
                        object[] obj1 = dtSubMainRowCurr[CurrentRowIndex].ItemArray;
                        object[] obj2 = dtSubMainRowCurr[CurrentRowIndex - 1].ItemArray;
                        for (int i = 2; i < obj1.Length; i++)
                        {
                            dtSubMainRowCurr[CurrentRowIndex][i] = obj2[i];
                            dtSubMainRowCurr[CurrentRowIndex - 1][i] = obj1[i];
                        }
                        this.dataGridView_Edaban.CurrentCell = this.dataGridView_Edaban[0, CurrentRowIndex - 1];
                    }
                }
            }
        }

        private void button_MoveDown_Click(object sender, EventArgs e)
        {
            DataTable dtMain = myDataSetTopMenu.menuMain;
            DataTable dtSubMain = myDataSetTopMenu.menuSub;
            int CurrentRowIndex = 0;
            int CurrentIndexof = -1;

            if (this.radioButton_Zuban.Checked)
            {
                if (dataGridView_Zuban.Rows.Count > 0 && dataGridView_Zuban.CurrentCell != null)
                {
                    CurrentRowIndex = dataGridView_Zuban.CurrentCell.RowIndex;
                    int ColumnMainID = int.Parse(dataGridView_Zuban.Rows[CurrentRowIndex].Cells["ColumnMainID"].Value.ToString());
                    DataRow[] dtMainRowCurr = dtMain.Select(string.Format("MainID='{0}'", ColumnMainID));
                    if ((CurrentRowIndex+1) < dataGridView_Zuban.Rows.Count && dtMainRowCurr.Length > 0)
                    {
                        CurrentIndexof = dtMain.Rows.IndexOf(dtMainRowCurr[0]);
                        int MainID = int.Parse(dtMainRowCurr[0]["MainID"].ToString());
                        DataRow dtMainRowNew = dtMain.NewRow();
                        dtMainRowNew["Title"] = dtMainRowCurr[0]["Title"];
                        dtMain.Rows.InsertAt(dtMainRowNew, CurrentIndexof + 2); //InsertAtを使うことで、その位置に挿入される（ので）後にSortとかいらない
                        int NewMainID = int.Parse(dtMain.Rows[CurrentIndexof +2]["MainID"].ToString());
                        dtMainRowCurr[0].Delete();

                        //dtSubのMainIDを更新する。MainID→NewMainID
                        foreach (DataRow currentRow in dtSubMain.Select(string.Format("MainID='{0}'", MainID)))
                        {
                            currentRow["MainID"] = NewMainID;
                        }

                        //dataGridView_Edaban.Sort(this.dataGridView_Edaban.Columns["ColumnSubSortID"], sortDirection);
                        this.dataGridView_Zuban.CurrentCell = this.dataGridView_Zuban[0, CurrentRowIndex + 1];
                    }
                }
            }
            else
            {
                if (dataGridView_Edaban.Rows.Count > 0 && dataGridView_Edaban.CurrentCell != null)
                {
                    ////this.dataGridView_Edaban.Rows.Insert(dataGridView_Edaban.CurrentCell.RowIndex,"","","","");   //bindのあるDatagridviewには、Add/Insertできません
                    //// なのでTableに追加する
                    //CurrentRowIndex = dataGridView_Edaban.CurrentCell.RowIndex;
                    //int ColumnSubID = int.Parse(dataGridView_Edaban.Rows[CurrentRowIndex].Cells["ColumnSubID"].Value.ToString());
                    //DataRow[] dtSubMainRowCurr = dtSubMain.Select(string.Format("SubID='{0}'", ColumnSubID));
                    //if ((CurrentRowIndex+1) < dataGridView_Edaban.Rows.Count && dtSubMainRowCurr.Length > 0)
                    //{
                    //    CurrentIndexof = dtSubMain.Rows.IndexOf(dtSubMainRowCurr[0]);
                    //    int SubID = int.Parse(dtSubMainRowCurr[0]["SubID"].ToString());
                    //    DataRow dtSubMainRowNew = dtSubMain.NewRow();
                    //    dtSubMainRowNew["MainID"] = dtSubMainRowCurr[0]["MainID"];//MainIDに連動させる
                    //    dtSubMainRowNew["SubTitle"] = dtSubMainRowCurr[0]["SubTitle"];
                    //    dtSubMainRowNew["Folder"] = dtSubMainRowCurr[0]["Folder"];
                    //    //dtSubMain.Rows.Add(dtSubMainRow);
                    //    dtSubMain.Rows.InsertAt(dtSubMainRowNew, CurrentIndexof + 2); //InsertAtを使うことで、その位置に挿入される（ので）後にSortとかいらない
                    //    dtSubMainRowCurr[0].Delete();

                    //    //dataGridView_Edaban.Sort(this.dataGridView_Edaban.Columns["ColumnSubSortID"], sortDirection);
                    //    this.dataGridView_Edaban.CurrentCell = this.dataGridView_Edaban[0, CurrentRowIndex + 1];
                    //}
                    if (dataGridView_Edaban.CurrentCell.RowIndex < (dataGridView_Edaban.Rows.Count-1))
                    {
                        CurrentRowIndex = dataGridView_Edaban.CurrentCell.RowIndex;
                        int ColumnMainID = int.Parse(dataGridView_Zuban.CurrentRow.Cells["ColumnMainID"].Value.ToString());
                        DataRow[] dtSubMainRowCurr = dtSubMain.Select(string.Format("MainID='{0}'", ColumnMainID));
                        object[] obj1 = dtSubMainRowCurr[CurrentRowIndex].ItemArray;
                        object[] obj2 = dtSubMainRowCurr[CurrentRowIndex + 1].ItemArray;
                        for (int i = 2; i < obj1.Length; i++)
                        {
                            dtSubMainRowCurr[CurrentRowIndex][i] = obj2[i];
                            dtSubMainRowCurr[CurrentRowIndex + 1][i] = obj1[i];
                        }
                        this.dataGridView_Edaban.CurrentCell = this.dataGridView_Edaban[0, CurrentRowIndex + 1];
                    }
                }
            }

        }
        private void dataGridView_Zuban_Click(object sender, EventArgs e)
        {
            this.radioButton_Zuban.Checked = true;
        }

        private void dataGridView_Edaban_Click(object sender, EventArgs e)
        {
            this.radioButton_Edaban.Checked = true;
        }

        private void button_Copy_Click(object sender, EventArgs e)
        {
            DataTable dtSubMain = myDataSetTopMenu.menuSub;

            if (dataGridView_Zuban.Rows.Count > 0 && dataGridView_Zuban.CurrentCell != null && dataGridView_Edaban.Rows.Count > 0 && dataGridView_Edaban.CurrentCell != null)
            {
                string ColumnTitle = dataGridView_Zuban.CurrentRow.Cells["ColumnTitle"].Value.ToString();
                string ColumnSubTitle = dataGridView_Edaban.CurrentRow.Cells["ColumnSubTitle"].Value.ToString();
                string ColumnFolder = dataGridView_Edaban.CurrentRow.Cells["ColumnFolder"].Value.ToString();

                if (string.IsNullOrEmpty(ColumnTitle) || string.IsNullOrEmpty(ColumnSubTitle) || string.IsNullOrEmpty(ColumnFolder))
                {
                    System.Windows.Forms.MessageBox.Show("仕様書番号と追番を指定して下さい。", Default.ApplicationName);
                    return;
                }

                //int SelectMainNo = (int)dataGridView_Edaban.CurrentRow.Cells["ColumnSubMainID"].Value;
                //int SelectSubNo = (int)dataGridView_Edaban.CurrentRow.Cells["ColumnSubID"].Value;
                DataRow dtSubMainRowNew = dtSubMain.NewRow();
                dtSubMainRowNew["MainID"] = dataGridView_Zuban.CurrentRow.Cells["ColumnMainID"].Value;//MainIDに連動させる
                dtSubMainRowNew["SubTitle"] = "";   // CurrentRowIndex.ToString();
                dtSubMainRowNew["Folder"] = "";

                //従来に合わせるため、先に行を追加する
                int CurrentRowIndex = dataGridView_Edaban.CurrentRow.Index;
                int ColumnSubID = int.Parse(dataGridView_Edaban.CurrentRow.Cells["ColumnSubID"].Value.ToString());
                DataRow[] dtSubRowCurr = dtSubMain.Select(string.Format("SubID='{0}'", ColumnSubID));
                if (dtSubRowCurr.Length > 0)
                {
                    int CurrentIndexof = dtSubMain.Rows.IndexOf(dtSubRowCurr[0]);
                    //this.dataGridView_Edaban.Rows.Insert(dataGridView_Edaban.CurrentCell.RowIndex,"","","","");   //bindのあるDatagridviewには、Add/Insertできません
                    // なのでTableに追加する
                    if (CurrentIndexof == -1)
                    {
                        dtSubMain.Rows.Add(dtSubMainRowNew);
                        this.dataGridView_Edaban.CurrentCell = this.dataGridView_Edaban[0, dataGridView_Edaban.Rows.Count - 1];
                    }
                    else
                    {
                        dtSubMain.Rows.InsertAt(dtSubMainRowNew, CurrentIndexof + 1); //InsertAtを使うことで、その位置に挿入される（ので）後にSortとかいらない
                        this.dataGridView_Edaban.CurrentCell = this.dataGridView_Edaban[0, CurrentRowIndex + 1];
                    }
                }

                this.radioButton_Edaban.Checked = true;
                //button_Insert_Click(sender, e);
                //SelectSubNoNew = (int)dataGridView_Edaban.CurrentRow.Cells["ColumnSubID"].Value;

                //frmTopEditCopy fmTopEditCopy = new frmTopEditCopy();
                TopEditCopyView fmTopEditCopy = mForms.fmTopEditCopy;
                fmTopEditCopy.MainTitle = ColumnTitle;
                fmTopEditCopy.SubTitle = ColumnSubTitle;
                fmTopEditCopy.Folder = ColumnFolder;
                //fmTopEditCopy.SelectSubNoNew = SelectSubNoNew;
                fmTopEditCopy.ShowDialog();
                if (fmTopEditCopy.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    dtSubMainRowNew["SubTitle"] = fmTopEditCopy.SubTitle;   // CurrentRowIndex.ToString();
                    dtSubMainRowNew["Folder"] = fmTopEditCopy.Folder;
                }
                this.Refresh();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("仕様書番号と追番を指定して下さい。", Default.ApplicationName);
            }
        }

        private void mnuFromFolder_Click(object sender, EventArgs e)
        {
            getMenuFromFolder_(true);
        }

        private void getMenuFromFolder_(bool boolClearDS)
        {
            //FolderBrowserDialogクラスのインスタンスを作成
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            //上部に表示する説明テキストを指定する
            fbd.Description = "フォルダを指定してください。";
            //ルートフォルダを指定する
            //デフォルトでDesktop
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            //最初に選択するフォルダを指定する
            //RootFolder以下にあるフォルダである必要がある
            //fbd.SelectedPath = @"C:\Windows";
            //fbd.SelectedPath = Default.ApplicationFloder + Default.DataFloder;
            fbd.SelectedPath = Default.DataFolder;  //2
            //ユーザーが新しいフォルダを作成できるようにする
            //デフォルトでTrue
            fbd.ShowNewFolderButton = true;

            //ダイアログを表示する
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                //選択されたフォルダを表示する
                Console.WriteLine(fbd.SelectedPath);
                DataSetTopMenu DataSetTopMenuEdit = myDataSetTopMenu;
                if (boolClearDS)
                {
                    DataSetTopMenuEdit.menuMain.Rows.Clear();
                    DataSetTopMenuEdit.menuSub.Rows.Clear();
                }
                mc.topMenuFromFolder(DataSetTopMenuEdit, fbd.SelectedPath);
                //更新する
                this.dataGridView_Zuban.DataSource = myDataSetTopMenu;
                this.dataGridView_Edaban.DataSource = myDataSetTopMenu;
            }
            fbd.Dispose();
        }

        private void mnuSaveClose_Click(object sender, EventArgs e)
        {
            button_SaveClose_Click(sender, e);
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            //http://dobon.net/vb/dotnet/form/openfiledialog.html
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            //ofd.FileName = "default.html";
            ofd.FileName = Default.Menucsv;
            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            //ofd.InitialDirectory = @"C:\";
            ofd.InitialDirectory = Default.ApplicationFloder + Default.MenuFolder;
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter =
                "hcmファイル(*.hcm;*.csv)|*.hcm;*.csv|すべてのファイル(*.*)|*.*";   //hcm:配線チェッカーメニュー
            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 2;
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
                //OKボタンがクリックされたとき
                //選択されたファイル名を表示する
                Console.WriteLine(ofd.FileName);
                mc.loadMenuCsv(myDataSetTopMenu, ofd.FileName);
                //更新する
                this.dataGridView_Zuban.DataSource = myDataSetTopMenu;
                this.dataGridView_Edaban.DataSource = myDataSetTopMenu;
            }
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            //http://dobon.net/vb/dotnet/form/savefiledialog.html
            //SaveFileDialogクラスのインスタンスを作成
            SaveFileDialog sfd = new SaveFileDialog();

            //はじめのファイル名を指定する
            //sfd.FileName = "新しいファイル.html";
            sfd.FileName = Default.Menucsv;
            //はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = Default.ApplicationFloder + Default.MenuFolder;
            //[ファイルの種類]に表示される選択肢を指定する
            sfd.Filter =
                "hcmファイル(*.hcm;*.csv)|*.hcm;*.csv|すべてのファイル(*.*)|*.*";   //hcm:配線チェッカーメニュー
            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            sfd.FilterIndex = 2;
            //タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;
            //既に存在するファイル名を指定したとき警告する
            //デフォルトでTrueなので指定する必要はない
            sfd.OverwritePrompt = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            sfd.CheckPathExists = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき
                //選択されたファイル名を表示する
                Console.WriteLine(sfd.FileName);
                mc.saveMenuCsv(myDataSetTopMenu, sfd.FileName);
            }
        }

        private void mnuAddFolder_Click(object sender, EventArgs e)
        {
            getMenuFromFolder_(false);

        }

        private void mnuInput_Click(object sender, EventArgs e)
        {
            button_Edit_Click(sender, e);
        }

        private void mnuAdd_Click(object sender, EventArgs e)
        {
            button_Insert_Click(sender, e);
        }

        private void mnuDel_Click(object sender, EventArgs e)
        {
            button_Delete_Click(sender, e);
        }

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            button_Copy_Click(sender, e);
        }

        private void mnuUp_Click(object sender, EventArgs e)
        {
            button_MoveUp_Click(sender, e);
        }

        private void mnuDown_Click(object sender, EventArgs e)
        {
            button_MoveDown_Click(sender, e);
        }

        private void mnuSaveOldVer_Click(object sender, EventArgs e)
        {
            int SelectMainNo = 0;
            int SelectSubNo = 0;
            string iMainTitle =null;
            string iSubTitle = null;
            string iFolder = null;
            string oFolder = null;
            if (dataGridView_Zuban.Rows.Count > 0 && dataGridView_Zuban.CurrentCell != null && dataGridView_Edaban.Rows.Count > 0 && dataGridView_Edaban.CurrentCell != null)
            {
                SelectMainNo = (int)dataGridView_Edaban.CurrentRow.Cells["ColumnSubMainID"].Value;
                SelectSubNo = (int)dataGridView_Edaban.CurrentRow.Cells["ColumnSubID"].Value;
                iMainTitle = dataGridView_Zuban.CurrentRow.Cells["ColumnTitle"].Value.ToString();
                iSubTitle = dataGridView_Edaban.CurrentRow.Cells["ColumnSubTitle"].Value.ToString();
                iFolder = dataGridView_Edaban.CurrentRow.Cells["ColumnFolder"].Value.ToString();

                oFolder = getSaveOldVerFolder_();
                if (!string.IsNullOrEmpty(oFolder))
                {
                    if(convToOldVer(iMainTitle, iSubTitle, iFolder, oFolder) == true)
                    {
                        System.Windows.Forms.MessageBox.Show(string.Format("変換は正常に終了しました。"), Default.ApplicationName);
                    }
                    else
                    {
                        string logfile = string.Format("{0}\\conv{1:yyyyMMdd}.log", oFolder, DateTime.Now);
                        System.Windows.Forms.MessageBox.Show(string.Format("変換エラーがありました。変換ログを確認してください。\n{0}", logfile), Default.ApplicationName);
                    }
                }
            }
        }

        private string getSaveOldVerFolder_()
        {
            string SelectedPath = null;

            //FolderBrowserDialogクラスのインスタンスを作成
            FolderBrowserDialog fbd = new FolderBrowserDialog();


            //上部に表示する説明テキストを指定する
            fbd.Description = string.Format("フォルダを指定してください。({0}/{1}は自動的に付加されます)", Default.MainNo, Default.SubNo);
            //ルートフォルダを指定する
            //デフォルトでDesktop
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            //最初に選択するフォルダを指定する
            //RootFolder以下にあるフォルダである必要がある
            //fbd.SelectedPath = @"C:\Windows";
            //fbd.SelectedPath = Default.ApplicationFloder + Default.DataFloder;
            fbd.SelectedPath = Default.DataFolder;  //1
            //ユーザーが新しいフォルダを作成できるようにする
            //デフォルトでTrue
            fbd.ShowNewFolderButton = true;

            //ダイアログを表示する
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                //選択されたフォルダを表示する
                Console.WriteLine(fbd.SelectedPath);
                SelectedPath = fbd.SelectedPath;
            }
            fbd.Dispose();

            return SelectedPath;
        }

        private void mnuSaveOldVerAll_Click(object sender, EventArgs e)
        {
            string iMainTitle =null;
            string iSubTitle = null;
            string iFolder = null;
            string oFolder = null;
            bool bRet = true;

            oFolder = getSaveOldVerFolder_();
            if (!string.IsNullOrEmpty(oFolder))
            {
                foreach (DataRow mainRow in myDataSetTopMenu.menuMain)
                {
                    foreach (DataRow subtitleRow in mainRow.GetChildRows("menuMain_menuSub"))
                    {
                        iMainTitle = mainRow["Title"].ToString();
                        iSubTitle = subtitleRow["SubTitle"].ToString();
                        iFolder = subtitleRow["Folder"].ToString();

                        bRet |= convToOldVer(iMainTitle, iSubTitle, iFolder, oFolder);
                    }
                }
                if (bRet == true)
                {
                    System.Windows.Forms.MessageBox.Show(string.Format("変換は正常に終了しました。"), Default.ApplicationName);
                }
                else
                {
                    string logfile = string.Format("{0}\\conv{1:yyyyMMdd}.log", oFolder, DateTime.Now);
                    System.Windows.Forms.MessageBox.Show(string.Format("変換エラーがありました。変換ログを確認してください。\n{0}", logfile), Default.ApplicationName);
                }
            }
        }

        private string folderCheck(string iMainTitle, string iSubTitle, string iFolder, string oFolder)
        {
            //新設定ファイルを旧設定ファイルに書き出す。
            string oFulPass = oFolder + System.IO.Path.DirectorySeparatorChar + iMainTitle + System.IO.Path.DirectorySeparatorChar + iSubTitle + System.IO.Path.DirectorySeparatorChar;
            if (System.IO.File.Exists(oFulPass + Default.Checkdat))
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show(string.Format("{0}が存在します。上書きしますか？", oFulPass + Default.Checkdat), Default.ApplicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (dr == System.Windows.Forms.DialogResult.Cancel) return "Cancel";
            }
            if (System.IO.File.Exists(oFulPass + Default.Listdat))
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show(string.Format("{0}が存在します。上書きしますか？", oFulPass + Default.Listdat), Default.ApplicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (dr == System.Windows.Forms.DialogResult.Cancel) return "Cancel";
            }
            if (System.IO.File.Exists(oFulPass + Default.Portdat))
            {
                DialogResult dr = System.Windows.Forms.MessageBox.Show(string.Format("{0}が存在します。上書きしますか？", oFulPass + Default.Portdat), Default.ApplicationName, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (dr == System.Windows.Forms.DialogResult.Cancel) return "Cancel";
            }

            string CheckdatFile = iFolder + System.IO.Path.DirectorySeparatorChar + Default.Checkdat;
            string ListdatFile = iFolder + System.IO.Path.DirectorySeparatorChar + Default.Listdat;
            string PortdatFile = iFolder + System.IO.Path.DirectorySeparatorChar + Default.Portdat;
            if (!System.IO.File.Exists(CheckdatFile))
            {
                System.Windows.Forms.MessageBox.Show(string.Format("Check.datファイルが見つかりません。({0})", CheckdatFile), Default.ApplicationName);
                return "NoFile - Check.dat";
            }
            if (!System.IO.File.Exists(ListdatFile))
            {
                System.Windows.Forms.MessageBox.Show(string.Format("List.datファイルが見つかりません。({0})", ListdatFile), Default.ApplicationName);
                return "NoFile - List.dat";
            }
            if (!System.IO.File.Exists(PortdatFile))
            {
                System.Windows.Forms.MessageBox.Show(string.Format("Port.datファイルが見つかりません。({0})", PortdatFile), Default.ApplicationName);
                return "NoFile - Port.dat";
            }


            if (System.IO.File.Exists(oFulPass + Default.Checkdat))
            {
                System.IO.File.Delete(oFulPass + Default.Checkdat);
            }
            if (System.IO.File.Exists(oFulPass + Default.Listdat))
            {
                System.IO.File.Delete(oFulPass + Default.Listdat);
            }
            if (System.IO.File.Exists(oFulPass + Default.Portdat))
            {
                System.IO.File.Delete(oFulPass + Default.Portdat);
            }

            //Folderを作成 Home
            if (!System.IO.Directory.Exists(oFolder))
            {
                System.IO.Directory.CreateDirectory(oFolder);
            }
            //Folderを作成 iMainTitle
            if (!System.IO.Directory.Exists(oFolder + System.IO.Path.DirectorySeparatorChar + iMainTitle))
            {
                System.IO.Directory.CreateDirectory(oFolder + System.IO.Path.DirectorySeparatorChar + iMainTitle);
            }
            //Folderを作成 iMainTitle
            if (!System.IO.Directory.Exists(oFolder + System.IO.Path.DirectorySeparatorChar + iMainTitle + System.IO.Path.DirectorySeparatorChar + iSubTitle))
            {
                System.IO.Directory.CreateDirectory(oFolder + System.IO.Path.DirectorySeparatorChar + iMainTitle + System.IO.Path.DirectorySeparatorChar + iSubTitle);
            }
            return "";
        }

        private bool convToOldVer(string iMainTitle, string iSubTitle, string iFolder, string oFolder)
        {
            string errmsg = null;
            bool bRet = false;
            //?iMainTitle="329-00620"
            //?iSubTitle="T"
            //?iFolder="C:\\Users\\Nippo_Shibukawa\\Documents\\Application Floder\\Checker\\329-00620\\T"
            //?oFolder="C:\\Users\\Nippo_Shibukawa\\Documents\\Application Floder\\OldVer"
            string oFulPass = oFolder + System.IO.Path.DirectorySeparatorChar + iMainTitle + System.IO.Path.DirectorySeparatorChar + iSubTitle + System.IO.Path.DirectorySeparatorChar;
            string CheckdatFile = iFolder + System.IO.Path.DirectorySeparatorChar + Default.Checkdat;
            string ListdatFile = iFolder + System.IO.Path.DirectorySeparatorChar + Default.Listdat;
            string PortdatFile = iFolder + System.IO.Path.DirectorySeparatorChar + Default.Portdat;

            errmsg = folderCheck(iMainTitle, iSubTitle, iFolder, oFolder);
            if (errmsg == "")
            {
                //Checkdatはそのままコピー
                //第3項にTrueを指定すると、コピー先が存在している時、上書きする
                //上書きするファイルが読み取り専用などで上書きできない場合は、
                //  UnauthorizedAccessExceptionが発生
                //System.IO.File.Copy(CheckdatFile, oFulPass + Default.Checkdat, true);
                errmsg = mc.convToOldVerCheckdat(CheckdatFile, oFulPass + Default.Checkdat);
                System.IO.File.Move(oFulPass + Default.Checkdat + mc.Part, oFulPass + Default.Checkdat);    //.partを取る
                if (errmsg == "")
                {
                    //Portdatはそのままコピー
                    //第3項にTrueを指定すると、コピー先が存在している時、上書きする
                    //上書きするファイルが読み取り専用などで上書きできない場合は、
                    //  UnauthorizedAccessExceptionが発生
                    System.IO.File.Copy(PortdatFile, oFulPass + Default.Portdat, true);

                    errmsg = mc.convToOldVerListdat(ListdatFile, oFulPass + Default.Listdat);
                    System.IO.File.Move(oFulPass + Default.Listdat + mc.Part, oFulPass + Default.Listdat);
                }
            }
            if (errmsg == "")
            {
                bRet = true;
            }
            else
            {
                //エラー時は、変換ログとして、ファイルに書き込む
                string logfile = string.Format("{0}\\conv{1:yyyyMMdd}.log", oFolder, DateTime.Now);
                using (System.IO.StreamWriter sr = new System.IO.StreamWriter(logfile, true, mc.enc))   //allways append
                {
                    sr.WriteLine(string.Format("{0}-{1} NG", iMainTitle, iSubTitle));
                    sr.Write(errmsg);
                }
                //System.Windows.Forms.MessageBox.Show(string.Format("変換エラーがありました。変換ログを確認してください。\n{0}", logfile), Default.ApplicationName);
            }
            return bRet;
        }

        private void dataGridView_Edaban_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button_Edit_Click(sender, e);
        }

        private void buttongetMenuFromFolder_Click(object sender, EventArgs e)
        {
            getMenuFromFolder_(true);
        }

        private void 変換iDhiDbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripMenuItem iTem = (System.Windows.Forms.ToolStripMenuItem)sender;

            int SelectMainNo = 0;
            int SelectSubNo = 0;
            string iMainTitle = null;
            string iSubTitle = null;
            string iFolder = null;
            bool returnStat = false;

            if (dataGridView_Zuban.Rows.Count > 0 && dataGridView_Zuban.CurrentCell != null && dataGridView_Edaban.Rows.Count > 0 && dataGridView_Edaban.CurrentCell != null)
            {
                SelectMainNo = (int)dataGridView_Edaban.CurrentRow.Cells["ColumnSubMainID"].Value;
                SelectSubNo = (int)dataGridView_Edaban.CurrentRow.Cells["ColumnSubID"].Value;
                iMainTitle = dataGridView_Zuban.CurrentRow.Cells["ColumnTitle"].Value.ToString();
                iSubTitle = dataGridView_Edaban.CurrentRow.Cells["ColumnSubTitle"].Value.ToString();
                iFolder = dataGridView_Edaban.CurrentRow.Cells["ColumnFolder"].Value.ToString();

                string CheckdatFile = iFolder + System.IO.Path.DirectorySeparatorChar + Default.Checkdat;
                string ListdatFile = iFolder + System.IO.Path.DirectorySeparatorChar + Default.Listdat;
                string PortdatFile = iFolder + System.IO.Path.DirectorySeparatorChar + Default.Portdat;

                if (!System.IO.File.Exists(ListdatFile))
                {
                    System.Windows.Forms.MessageBox.Show(string.Format("List.datファイルが見つかりません。({0})", ListdatFile), Default.ApplicationName);
                    return;
                }


                if (iTem.Equals(mnuFile.DropDownItems[2]))
                {
                    returnStat = mc.executeiDhiDb(ListdatFile);
                }
                else if (iTem.Equals(mnuFile.DropDownItems[3]))
                {
                    returnStat = mc.executeiDbiDh(ListdatFile);
                }
                if (returnStat == true)
                {
                    System.Windows.Forms.MessageBox.Show(string.Format("変換は正常に終了しました。"), Default.ApplicationName);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(string.Format("変換できませんでした。\n該当の項目がないか、ファイルが存在しません"), Default.ApplicationName);
                }
            }
        }
    }
}