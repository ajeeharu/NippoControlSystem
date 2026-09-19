using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem
{
    public partial class frmDebugNo : Form
    {
        Cyc.IO.Settings Default = Cyc.IO.Settings.GetInstance();
        Cyc.IO.cDio dio = Cyc.IO.cDio.GetInstance();
        Cyc.IO.NippoDIO nio = Cyc.IO.NippoDIO.GetInstance();
        Cyc.IO.Aio aio = Cyc.IO.Aio.GetInstance();
        MeasureCondition mc = MeasureCondition.GetInstance();
        Forms mForms = Forms.GetInstance();

        //Lamp
        //const bool WorkingLampON = true;    //稼働時点灯
        const bool WorkingLampON = false;    //稼働時点灯 20160907
        const int OpSwLampOn = (WorkingLampON ? 0 : 1);
        const int OpSwLampOff = (WorkingLampON ? 1 : 0);

        public frmDebugNo()
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

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CurrentTNo
        {
            get;
            set;
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ start_stat
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MeasureCondition.enumInspectStat start_stat
        {
            get;
            set;
        }

        private void frmDebugNo_Load(object sender, EventArgs e)
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmDebugNo"]);
            Forms.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmDebugNo"]);

            int ListItemsCount = myDataSetItems.ListDat.Count;
            this.comboBox_TNo.Items.Clear();
            for (int i=0; i< ListItemsCount; i++)
            {
                this.comboBox_TNo.Items.Add((i + 1).ToString());
            }
            this.comboBox_TNo.SelectedIndex = CurrentTNo;
            TimerReadSw.Enabled = true;

        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            if (this.comboBox_TNo.SelectedIndex == -1)
            {
                System.Windows.Forms.MessageBox.Show("T#が範囲外です。", Default.ApplicationName);
                return;
            }
            this.CurrentTNo = this.comboBox_TNo.SelectedIndex;
            this.start_stat = MeasureCondition.enumInspectStat.Stat_MidStarted;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            TimerReadSw.Enabled = false;
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.start_stat = MeasureCondition.enumInspectStat.Stat_STOP;
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            TimerReadSw.Enabled = false;
            this.Close();
        }

        private void frmDebugNo_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Lamp消灯
            aio.setGreenLamp(0);
            aio.setRedLamp(0);
        }

        private void frmDebugNo_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmDebugNo_FormClosing");
        }

        private void TimerReadSw_Tick(object sender, EventArgs e)
        {
            if (GreenSwitchDown())
            {
                //GreenSwが押された
                button_Start_Click(this.button_Start, new EventArgs());
            }
            else if (RedSwitchDown())
            {
                //RedSwが押された
                button_Cancel_Click(button_Cancel, new EventArgs());
            }
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
            //if (stopButtonClick)
            //{
            //    stopButtonClick = false;
            //    return true;
            //}
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
            this.switchLabelRedSwitch.LampValue = 0;
            return true;
        }

        private void comboBox_TNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_TNo_Validating(object sender, CancelEventArgs e)
        {
           System.Windows.Forms.ComboBox comboBox1 = (System.Windows.Forms.ComboBox)sender;

           System.Diagnostics.Debug.WriteLine(string.Format("comboBox_TNo_Validating:\"{0}\":{1}", comboBox1.Text, comboBox1.SelectedIndex));
           for (int i = 0; i < comboBox1.Items.Count; i++)
           {
               if (comboBox1.Text.ToString() == comboBox1.Items[i].ToString())
               {
                   comboBox1.SelectedIndex = i;
                   return;
               }
           }
           e.Cancel = false;
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
    }
}