using NippoControlSystem.Infrastructure.Configuration;
using NippoControlSystem.Infrastructure.Devices;
using System.ComponentModel;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.UI.Views
{
    public partial class DebugView : Form
    {
        //　------　MVVMパターン用にリファクタリングしたコード　------

        //　------　MVVM化のためにリファクタリングする前のコード　------

        Settings Default = Settings.GetInstance();
        cDio dio = cDio.GetInstance();
        NippoDIO nio = NippoDIO.GetInstance();
        private readonly Aio _aio;
        MeasureCondition mc = MeasureCondition.GetInstance();
        Views mForms = Views.GetInstance();

        //Lamp
        //const bool WorkingLampON = true;    //稼働時点灯
        const bool WorkingLampON = false;    //稼働時点灯 20160907
        const int OpSwLampOn = (WorkingLampON ? 0 : 1);
        const int OpSwLampOff = (WorkingLampON ? 1 : 0);

        public DebugView(Aio aio)
        {
            _aio = aio;
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
        //プロパティ CurrentTNo
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


        private void frmDebug_Load(object sender, EventArgs e)
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmDebug"]);
            Views.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmDebug"]);

            TimerReadSw.Enabled = true;

        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            if (radioButton1_0.Checked)
            {
                //強制的に最後の項目まで検査する。
                this.CurrentTNo = 0;       //最初から
                this.DialogResult = System.Windows.Forms.DialogResult.OK;   //20160907
                this.start_stat = MeasureCondition.enumInspectStat.Stat_ForceToEnd;
                TimerReadSw.Enabled = false;
                this.Close();
            }
            else if (radioButton1_1.Checked)
            {
                //現在のカーソル行から検査を開始する。
                this.DialogResult = System.Windows.Forms.DialogResult.OK;   //20160907
                this.start_stat = MeasureCondition.enumInspectStat.Stat_MidStarted;
                TimerReadSw.Enabled = false;
                this.Close();
            }
            else if (radioButton1_2.Checked)
            {
                TimerReadSw.Enabled = false;
                //開始位置を指定して検査を開始する。
                DebugNoView fmDebugNo = mForms.fmDebugNo;
                fmDebugNo.myDataSetItems = myDataSetItems;
                fmDebugNo.ShowDialog();
                if (fmDebugNo.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    this.CurrentTNo = fmDebugNo.CurrentTNo;
                    this.start_stat = fmDebugNo.start_stat;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    TimerReadSw.Enabled = false;     //20160907
                    this.Close();
                }
                //else
                //{
                //    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                //    this.Close();
                //}
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;   //20160907
            this.start_stat = MeasureCondition.enumInspectStat.Stat_STOP;
            this.Close();
        }

        private void frmDebug_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Lamp
            _aio.setGreenLamp(0);
            _aio.setRedLamp(0);
        }

        private void frmDebug_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmDebug_FormClosing");
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
                if (this.switchLabelGreenSwitch.LampValue == 0 && _aio.getGreenSw() == 0)
                {
                    mc.GreenSwitchStat = 0;
                }
            }
            if (mc.RedSwitchStat != 0)    //OFFしたら、RedSwitchStat(ON中)をリセットする 20170126
            {
                //一度ONしたら、OFFするまで、無視する
                if (this.switchLabelRedSwitch.LampValue == 0 && _aio.getRedSw() == 0)
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
                if (this.switchLabelGreenSwitch.LampValue == 0 && _aio.getGreenSw() == 0)
                {
                    mc.GreenSwitchStat = 0;
                }
                return false;
            }
            else
            {
                //switchLabelGreenSwitch.LampValueは、ON=1、_aio.getGreenSw()はON=1
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

        private const int SlLampOn = 1;
        private const int SlLampOff = 0;

        private void switchLabelGreenSwitch_MouseDown(object sender, MouseEventArgs e)
        {
            this.switchLabelGreenSwitch.LampValue = 0;
            _aio.setGreenLamp(OpSwLampOn);
        }

        private void switchLabelGreenSwitch_MouseLeave(object sender, EventArgs e)
        {
            this.switchLabelGreenSwitch.LampValue = 1;
            _aio.setGreenLamp(0);
        }

        private void switchLabelGreenSwitch_MouseUp(object sender, MouseEventArgs e)
        {
            this.switchLabelGreenSwitch.LampValue = 1;
            _aio.setGreenLamp(0);
        }

        private void switchLabelRedSwitch_MouseDown(object sender, MouseEventArgs e)
        {
            this.switchLabelRedSwitch.LampValue = 0;
            _aio.setRedLamp(OpSwLampOn);
        }

        private void switchLabelRedSwitch_MouseLeave(object sender, EventArgs e)
        {
            this.switchLabelRedSwitch.LampValue = 1;
            _aio.setRedLamp(0);
        }

        private void switchLabelRedSwitch_MouseUp(object sender, MouseEventArgs e)
        {
            this.switchLabelRedSwitch.LampValue = 1;
            _aio.setRedLamp(0);
        }
    }
}