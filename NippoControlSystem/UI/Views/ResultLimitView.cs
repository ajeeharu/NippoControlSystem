using NippoControlSystem.Infrastructure.Configuration;
using NippoControlSystem.Infrastructure.Devices;
using NippoControlSystem.UI.Views;
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
    public partial class ResultLimitView : Form
    {
        //　------　MVVMパターン用にリファクタリングしたコード　------

        //　------　MVVM化のためにリファクタリングする前のコード　------

        Settings Default = Settings.GetInstance();
        cDio dio = cDio.GetInstance();
        NippoDIO nio = NippoDIO.GetInstance();
        Aio aio = Aio.GetInstance();
        MeasureCondition mc = MeasureCondition.GetInstance();
        Views mForms = Views.GetInstance();

        // 未使用のフィールド dataGridView_DIO を削除しました
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
        //int delayedViewNo = 0;
        //int delayedViewRowIndex = 0;


        public ResultLimitView()
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

        //--------1--------
        // デザイナで割り当てられているが定義が欠けているイベントハンドラを追加
        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView_DA_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            //?Eclick????ALeave????????????A??????Leave?????????
            if (dataGridView_Entered != null)
            {
                dataGridView_Entered.ClearSelection();
                dataGridView_Entered = null;
            }
            dataGridView_Entered = dataGridView1;

            if (e.RowIndex < 0)
            {
                // ヘッダなどでは何もしない
            }
            else if (e.ColumnIndex < 0)
            {
                // 列ヘッダでは何もしない
            }
            else if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                dataGridView_Entered[e.ColumnIndex, e.RowIndex].Selected = true;
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                // 必要ならコンテキストメニューを設定: e.ContextMenuStrip = this.contextMenuStripAO;
            }
        }

        // TNo property (同等のfrmResultViewと同様)
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int TNo
        {
            get;
            set;
        }

        private void button_Next_Click(object sender, EventArgs e)
        {
            this.label_Busy.Visible = true;
            this.label_Busy.Refresh();

            if (myDataSetItems != null && TNo < (myDataSetItems.ListDat.Rows.Count - 1))
            {
                TNo++;
                this.textBox_TNo.Text = string.Format("{0}", TNo + 1);
                this.textBox_TNo.Refresh();
            }
            this.label_Busy.Visible = false;
            this.label_Busy.Refresh();
        }

        private void button_Priv_Click(object sender, EventArgs e)
        {
            this.label_Busy.Visible = true;
            this.label_Busy.Refresh();

            if (myDataSetItems != null && TNo > 0)
            {
                TNo--;
                this.textBox_TNo.Text = string.Format("{0}", TNo + 1);
                this.textBox_TNo.Refresh();
            }
            this.label_Busy.Visible = false;
            this.label_Busy.Refresh();
        }

        private void mnuBefore_Click(object sender, EventArgs e)
        {
            button_Priv_Click(sender, e);
        }

        private void nmuNext_Click(object sender, EventArgs e)
        {
            button_Next_Click(sender, e);
        }

        // SelectionCell (frmResultView 側から設定されることがある)
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string SelectionCell
        {
            get;
            set;
        }

        // 以下は designer から参照されるイベントハンドラのスタブ実装
        private void dataGridView_DA_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 最小実装: 必要なら frmResultView の実装を移植してください
        }

        private void dataGridView_DA_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // スタブ
        }

        private void dataGridView_DA_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            // スタブ
        }

        private void dataGridView_DA_Enter(object sender, EventArgs e)
        {
            try { dataGridView_Entered = (DataGridView)sender; } catch { }
        }

        private void dataGridView_DA_KeyDown(object sender, KeyEventArgs e)
        {
            // スタブ
        }

        private void dataGridView_DA_Leave(object sender, EventArgs e)
        {
            if (dataGridView_Entered == sender) dataGridView_Entered = null;
        }

        private void frmResultViewLMT_FormClosed(object sender, FormClosedEventArgs e)
        {
            // スタブ
        }

        private void frmResultViewLMT_FormClosing(object sender, FormClosingEventArgs e)
        {
            // スタブ
        }

        private void frmResultViewLMT_Load(object sender, EventArgs e)
        {
            // スタブ: 実際の初期化が必要なら frmDataInputLMT/frmResultView の実装を参照して移植してください
        }

        private void frmResultViewLMT_Shown(object sender, EventArgs e)
        {
            // スタブ
        }

        private void mnuPrint_Click(object sender, EventArgs e)
        {
            // スタブ
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            button_Close_Click(sender, e);
        }

        private void pictureBoxPage1_Click(object sender, EventArgs e)
        {
            // スタブ
        }

        private void pictureBoxPage2_Click(object sender, EventArgs e)
        {
            // スタブ
        }

        private void textBox_TNo_TextChanged(object sender, EventArgs e)
        {
            // スタブ
        }

        private void textBox_TNo_Validated(object sender, EventArgs e)
        {
            // スタブ
        }

        private void textBox_TNo_Validating(object sender, CancelEventArgs e)
        {
            // スタブ
        }

        private void TimerReadSw_Tick(object sender, EventArgs e)
        {
            // スタブ
        }

        private void timer_selectionView_Tick(object sender, EventArgs e)
        {
            // スタブ
        }
    }
}
