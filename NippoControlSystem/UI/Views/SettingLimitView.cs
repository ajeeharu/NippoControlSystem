using NippoControlSystem.Infrastructure.Configuration;
using NippoControlSystem.Infrastructure.Devices;
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
    public partial class SettingLimitView : Form
    {
        //　------　MVVMパターン用にリファクタリングしたコード　------

        //　------　MVVM化のためにリファクタリングする前のコード　------

        Settings Default = Settings.GetInstance();
        //Cyc.IO.cDio dio = Cyc.IO.cDio.GetInstance();
        //Cyc.IO.NippoDIO nio = Cyc.IO.NippoDIO.GetInstance();
        MeasureCondition mc = MeasureCondition.GetInstance();
        Views mForms = Views.GetInstance();
        //DataSetItems myDataSetItems = null;

        public SettingLimitView()
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

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ DataSetItems
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataSetItems myDataSetItems
        {
            get;
            set;
        }

        private void frmSettingLMT_Load(object sender, EventArgs e)
        {
            ////Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            //Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmSettingLMT"]);
            //Forms.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmSettingLMT"]);

            //ここからはFormの更新
            this.lblMainNo.Text = Default.MainNo;   //"仕様書番号";
            this.lblSubNo.Text = Default.SubNo;     //"追番";
            this.lblItem.Text = Default.Item;  //品名
            //
            //if ((myDataSetItems = mc.createDataSetItems(Folder, MainTitle, SubTitle)) == null)
            //{
            //    this.Close();
            //    return;
            //}

            //DataSet
            DataTable dtCheckDat = myDataSetItems.CheckDat;

            //Checkdat
            this.bindingSourceCheckDat.DataSource = myDataSetItems;
            //this.bindingSource1.DataMember = "CheckDat";
            int itemFound = this.bindingSourceCheckDat.Find("Title", myDataSetItems.CheckDat.Rows[0]["Title"]);
            //int itemFound = this.bindingSource1.Find("Title", "5C0-00700");
            if (itemFound >= 0)
            {
                bindingSourceCheckDat.Position = itemFound;
            }

            //this.comboBox_Volt.DataBindings.Add("SelectedIndex", this.bindingSourceCheckDat, "Volt");
            //string strVolt = this.textBox_Volt.Text.ToString();
            string strVolt = myDataSetItems.CheckDat.Rows[itemFound]["Volt"].ToString();
            this.textBox_Volt.Text = getVoltString(strVolt);

            //this.lblFolderDir.Text = string.Format("フォルダ = {0}", this.Folder);
            if (!string.IsNullOrEmpty(this.textBox_Item.Text))
            {
                //文字が選択されるのを防ぐ
                this.textBox_Item.SelectionStart = 0;
                this.textBox_Item.SelectionLength = 0;
            }

            //Color 20180830
            this.label_iDo電流下限値.ForeColor = Default.CellStyles[(int)NippoDIO.IO_STAT.iDo - (int)NippoDIO.IO_STAT.oOP].ForeColor;
            this.label_iDh電流下限値.ForeColor = Default.CellStyles[(int)NippoDIO.IO_STAT.iDh - (int)NippoDIO.IO_STAT.oOP].ForeColor;
            this.label_iDb電流下限値.ForeColor = Default.CellStyles[(int)NippoDIO.IO_STAT.iDb - (int)NippoDIO.IO_STAT.oOP].ForeColor;
            this.label_iDc電流下限値.ForeColor = Default.CellStyles[(int)NippoDIO.IO_STAT.iDc - (int)NippoDIO.IO_STAT.oOP].ForeColor;
            this.label_iDs電流下限値.ForeColor = Default.CellStyles[(int)NippoDIO.IO_STAT.iDs - (int)NippoDIO.IO_STAT.oOP].ForeColor;
            this.label_iDp電流下限値.ForeColor = Default.CellStyles[(int)NippoDIO.IO_STAT.iDp - (int)NippoDIO.IO_STAT.oOP].ForeColor;
            this.label_iTo電流下限値.ForeColor = Default.CellStyles[(int)NippoDIO.IO_STAT.iTo - (int)NippoDIO.IO_STAT.oOP].ForeColor;

            this.dataSetItemsBindingSource.DataSource = myDataSetItems;

        }

        private void button_SaveClose_Click(object sender, EventArgs e)
        {
            //mForms.Hide(this);
            this.Close();
        }

        private void button_EditTanshi_Click(object sender, EventArgs e)
        {
            PortView fmPort = mForms.fmPort;
            fmPort.myDataSetItems = myDataSetItems;

            fmPort.ShowDialog();
        }


        private void frmSettingLMT_FormClosing(object sender, FormClosingEventArgs e)
        {
            //mForms.Hide(this);
            System.Diagnostics.Debug.WriteLine("frmSettingLMT_FormClosing");
        }
        private void frmSettingLMT_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private string getVoltString(string valueString)
        {
            string[] voltstring = { "12V", "24V" };
            return (valueString == "1" ? voltstring[1] : voltstring[0]);
        }

        private void ChangeVolt()
        {
            int itemFound = 0;
            Console.WriteLine(System.DateTime.Now.ToLongTimeString() + " ChangeVolt:");

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

            int iDo_Length = (int)NippoDIO.IO_STAT.iTo - (int)NippoDIO.IO_STAT.iOP + 1;
            //12V 24V
            int mVoltIdx = (newVolt == "1" ? 2 : 0); //0:12V 2:24V

            string dtCheckDatFields = null;
            for (int i = 0; i < iDo_Length; i++)
            {
                dtCheckDatFields = ((NippoDIO.IO_STAT)((int)NippoDIO.IO_STAT.iOP + i)).ToString() + Default.CheckDatLMTFields[0];     //Hi
                dtCheckDatRow[dtCheckDatFields] = Default.CheckDatLMT[mVoltIdx][i].ToString("F1");    //iOP-HiLMT～iTo-HiLMT
                dtCheckDatFields = ((NippoDIO.IO_STAT)((int)NippoDIO.IO_STAT.iOP + i)).ToString() + Default.CheckDatLMTFields[1];     //Lo
                dtCheckDatRow[dtCheckDatFields] = Default.CheckDatLMT[mVoltIdx + 1][i].ToString("F1");    //iOP-LoLMT～iTo-LoLMT
            }
        }

        private void buttonChangeVolt_Click(object sender, EventArgs e)
        {
            ChangeVolt();
        }

    }
}
