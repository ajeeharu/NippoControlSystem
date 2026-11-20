using NippoControlSystem.Infrastructure.Persistence;
using System.Runtime.Versioning;

namespace NippoControlSystem.UI.Views
{
    [SupportedOSPlatform("windows")]

    public partial class InspectionMainView : Form
    {
        Cyc.IO.Settings Default = Cyc.IO.Settings.GetInstance();
        Cyc.IO.Log.LogLevel Main_LogLevel = Cyc.IO.Log.LogLevel.LOG_INFO;
        Cyc.IO.cDio dio = Cyc.IO.cDio.GetInstance();
        Cyc.IO.Aio aio = Cyc.IO.Aio.GetInstance();
        Cyc.IO.Ai2Di ai2di = Cyc.IO.Ai2Di.GetInstance();
        Cyc.IO.NippoDIO nio = Cyc.IO.NippoDIO.GetInstance();
        MeasureCondition mc = MeasureCondition.GetInstance();
        Forms mForms = Forms.GetInstance();
        System.Windows.Forms.PictureBox[] pictureBoxMeter;

        //Lamp
        //const bool WorkingLampON = true;    //稼働時点灯
        const bool WorkingLampON = false;    //稼働時点灯
        const int SlLampOn = 1;
        const int SlLampOff = 0;
        const int OpSwLampOn = (WorkingLampON ? 0 : 1);
        const int OpSwLampOff = (WorkingLampON ? 1 : 0);

        //// PictureBoxと同サイズのBitmapオブジェクトを作成
        //Bitmap[] pictureBoxBmp = null;

        //Local 変数
        int TNo = 0;
        int TNoRetryNo = 0;
        float[,] AiDataPop;
        float[] AiDataAve;
        float[] AiDataInput;
        //bool nextButtonClick = false;
        bool stopButtonClick = false;
        string PowerVolt = "";
        DataSetItems myDataSetItems;

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ Serial
        public string Serial
        {
            get { return this.textBox_Serial.Text; }
            set
            {
                this.textBox_Serial.Text = value;
                this.textBox_Serial.Refresh();
            }
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ GoNo
        public string GoNo
        {
            get { return this.textBox_GoNo.Text; }
            set
            {
                this.textBox_GoNo.Text = value;
                this.textBox_GoNo.Refresh();
            }
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ MainTitle
        public string MainTitle
        {
            get;
            set;
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ SubTitle
        public string SubTitle
        {
            get;
            set;
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ Folder
        public string Folder
        {
            get;
            set;
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ CurrentTNo
        int m_CurrentTNo = 0;
        public int CurrentTNo
        {
            get { return m_CurrentTNo; }
            set
            {
                m_CurrentTNo = value;
                System.Windows.Forms.DataGridView dv = dataGridView_Inspection;
                // ArrangedElementCollection.Count は Windows でのみサポートされる API のため実行時ガードを追加
                if (OperatingSystem.IsWindows())
                {
                    if (dv.Rows.Count > 0) dv.CurrentCell = dv[0, m_CurrentTNo];
                }
            }
        }
        private void button_Enable(bool enableStat)
        {
            //this.button_Next.Enabled = enableStat;
            this.button_Stop.Enabled = !enableStat;
            this.button_DataInput.Enabled = enableStat;
            this.button_ResultView.Enabled = enableStat;
            this.button_Debug.Enabled = enableStat;
            this.button_Close.Enabled = enableStat;
            //以下はメニューのボタン
            this.mnuStart.Enabled = enableStat;
            this.mnuStop.Enabled = !enableStat;
            this.mnuEdit.Enabled = enableStat;
            this.mnuView.Enabled = enableStat;
        }

    }
}
