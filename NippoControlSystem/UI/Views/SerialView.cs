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
    public partial class SerialView : Form

    {
        //　------　MVVMパターン用にリファクタリングしたコード　------

        //　------　MVVM化のためにリファクタリングする前のコード　------

        Cyc.IO.Settings Default = Cyc.IO.Settings.GetInstance();
        Cyc.IO.cDio dio = Cyc.IO.cDio.GetInstance();
        Cyc.IO.NippoDIO nio = Cyc.IO.NippoDIO.GetInstance();
        Cyc.IO.Aio aio = Cyc.IO.Aio.GetInstance();
        MeasureCondition mc = MeasureCondition.GetInstance();
        Views mForms = Views.GetInstance();
        Cyc.Windows.Forms.screenShot screen = Cyc.Windows.Forms.screenShot.GetInstance();

        //Local 変数
        int newGreenSwitch = 0;
        int newRedSwitch = 0;
        int lastGreenSwitch = 0;
        int lastRedSwitch = 0;

        //Lamp
        //const bool WorkingLampON = true;    //稼働時点灯
        const bool WorkingLampON = false;    //稼働時点灯 20160907
        const int OpSwLampOn = (WorkingLampON ? 0 : 1);
        const int OpSwLampOff = (WorkingLampON ? 1 : 0);

        public SerialView()
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
        //プロパティ myDataSetItems
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public DataSetItems myDataSetItems
        {
            get;
            set;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ SelectedNo
        private void frmSerial_Load(object sender, EventArgs e)
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmSerial"]);
            Views.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmSerial"]);

            //this.Text = string.Format("{0} と {1} の入力", Default.SerialTitle, Default.GokiTitle);
            this.Text = string.Format("検査結果保存", Default.SerialTitle, Default.GokiTitle);
            this.lblSerialTitle.Text = string.Format("{0}を入力して下さい。", Default.SerialTitle); //製造番号
            this.lblGoTitle.Text = string.Format("{0}を入力して下さい。", Default.GokiTitle); //号機

            this.textBox_Serial.Text = mc.SerialNo; //製造番号
            this.textBox_Serial.SelectionStart = 0;
            this.textBox_Serial.SelectionLength = 0;

            this.textBox_GoNo.Text = mc.GoNo;       //号機番号

            this.TimerReadSw.Enabled = true;
        }

        private void button_SaveClose_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox_Serial.Text))
            {
                System.Windows.Forms.MessageBox.Show(string.Format("{0}を入力して下さい。", Default.SerialTitle), Default.ApplicationName);
                return;
            }
            if (string.IsNullOrEmpty(this.textBox_GoNo.Text))
            {
                System.Windows.Forms.MessageBox.Show(string.Format("{0}を入力して下さい。", Default.GokiTitle), Default.ApplicationName);
                return;
            }
            mc.SerialNo = this.textBox_Serial.Text;
            mc.GoNo = this.textBox_GoNo.Text;

            Bitmap bm = screen.CaptureControl(mForms.fmMain);
            string resultBitMapPass = string.Format("{0}A{1:yyyyMMdd_HHmmss}.bmp", this.Folder + System.IO.Path.DirectorySeparatorChar, mc.TestEndDT);
            bm.Save(resultBitMapPass);

            saveResults();

            if (mc.InspecStat == MeasureCondition.enumInspectStat.Stat_NormalEND)   //成功したときのみAddする
            {
                mForms.fmMain.Serial = libSerialNo.AddSerialNo(Default.Serial, Default.SerialSuffixLength, mc.SerialNo);
            }
            mForms.fmMain.GoNo = mc.GoNo;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void saveResults()
        {
            mc.saveResultCsv(myDataSetItems, Folder);
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void TimerReadSw_Tick(object sender, EventArgs e)
        {
            //Aio-Dio
            newGreenSwitch = aio.getGreenSw();
            newRedSwitch = aio.getRedSw();

            aio.setGreenLamp(newGreenSwitch ^ (WorkingLampON ? 1 : 0));
            aio.setRedLamp(newRedSwitch ^ (WorkingLampON ? 1 : 0));

            if (newGreenSwitch == 0 && lastGreenSwitch == 1)
            {
                //Pushed Green button
                if (!string.IsNullOrEmpty(this.textBox_Serial.Text) && !string.IsNullOrEmpty(this.textBox_GoNo.Text))
                {
                    button_SaveClose_Click(this.button_SaveClose, new EventArgs());
                }
            }
            if (newRedSwitch == 0 && lastRedSwitch == 1)
            {
                button_Cancel_Click(button_Cancel, new EventArgs());
            }
            if (newRedSwitch == 0 && lastRedSwitch == 1)
            {
                //Pushed Green button
            }
            //save 
            lastGreenSwitch = newGreenSwitch;
            lastRedSwitch = newRedSwitch;

        }

        private void frmSerial_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmSerial_FormClosing");
        }

        private void frmSerial_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.TimerReadSw.Enabled = false;
            aio.setGreenLamp(0);
            aio.setRedLamp(0);
        }

    }
}