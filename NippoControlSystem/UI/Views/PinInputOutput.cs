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
    public partial class PinInputOutput : Form
    {
        //　------　MVVMパターン用にリファクタリングしたコード　------

        //　------　MVVM化のためにリファクタリングする前のコード　------

        Settings Default = Settings.GetInstance();
        MeasureCondition mc = MeasureCondition.GetInstance();

        public PinInputOutput()
        {
            InitializeComponent();
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ SelectPin
        int m_SelectPin = 0;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectPin
        {
            get { return m_SelectPin; }
            set
            {
                m_SelectPin = value;
                //Update_PinName();
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ PinIO
        string m_PinIO = "";
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PinIO
        {
            get { return m_PinIO; }
            set
            {
                m_PinIO = value;
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ PinIO
        string m_iDpH = "";
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string iDpH
        {
            get { return m_iDpH; }
            set
            {
                m_iDpH = value;
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ PinIO
        string m_iDpL = "";
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string iDpL
        {
            get { return m_iDpL; }
            set
            {
                m_iDpL = value;
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ PinIO
        string m_iDsH = "";
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string iDsH
        {
            get { return m_iDsH; }
            set
            {
                m_iDsH = value;
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ PinIO
        string m_iDsL = "";
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string iDsL
        {
            get { return m_iDsL; }
            set
            {
                m_iDsL = value;
            }
        }

        private void frmPinIO_Load(object sender, EventArgs e)
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmPinIO"]);
            Views.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmPinIO"]);

            this.Text = "デジタル入出力";
            //comboBoxの定義
            comboBox_PinIO.Items.Clear();
            for (int i = 0; i < Default.DioStatGuide.Length; i++)
            {
                comboBox_PinIO.Items.Add(Default.DioStatGuide[i]);
            }

            //ドロップダウンリストの表示項目数を変更する
            comboBox_PinIO.MaxDropDownItems = comboBox_PinIO.Items.Count;

            //初期値をセット
            textBoxiDp電流下限値.Text = m_iDpL;
            textBoxiDp電流上限値.Text = m_iDpH;
            textBoxiDs電流下限値.Text = m_iDsL;
            textBoxiDs電流上限値.Text = m_iDsH;

            //ここは毎回実行
            Update_PinIO();
        }

        private void Update_PinIO()
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Update_PinIO()"));
            int SelectedIndex = Default.DioStat.Length - 1;
            for (int i = 0; i < Default.DioStat.Length; i++)
            {
                if (this.m_PinIO == Default.DioStat[i])
                {
                    SelectedIndex = i;
                    this.comboBox_PinIO.SelectedIndex = SelectedIndex;
                    break;
                }
            }
            // ロード時にフォーカスを設定する
            this.ActiveControl = this.comboBox_PinIO;
        }

        private void Update_PiniDp_enable()
        {
            switch (this.m_PinIO)
            {
                case "iDp":
                case "nDp":
                    groupBoxiDp.Enabled = true;
                    groupBoxiDs.Enabled = false;
                    break;
                case "iDs":
                case "nDs":
                    groupBoxiDp.Enabled = false;
                    groupBoxiDs.Enabled = true;
                    break;
                default:
                    groupBoxiDp.Enabled = false;
                    groupBoxiDs.Enabled = false;
                    break;
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmPinIO_button_OK_Click");
            if (this.comboBox_PinIO.SelectedIndex == -1)
            {
                this.m_PinIO = null;
            }
            else
            {
                this.m_PinIO = Default.DioStat[this.comboBox_PinIO.SelectedIndex];
                switch (this.m_PinIO)
                {
                    case "iDp":
                    case "nDp":
                        m_iDpL = textBoxiDp電流下限値.Text;
                        m_iDpH = textBoxiDp電流上限値.Text;
                        break;
                    case "iDs":
                    case "nDs":
                        m_iDsL = textBoxiDs電流下限値.Text;
                        m_iDsH = textBoxiDs電流上限値.Text;
                        break;
                    default:
                        m_iDpL = "";
                        m_iDpH = "";
                        m_iDsL = "";
                        m_iDsH = "";
                        break;
                }

            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        private void frmPinIO_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmPinIO_FormClosing");
        }

        private void comboBox_PinIO_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_PinIO = Default.DioStat[this.comboBox_PinIO.SelectedIndex];
            switch (this.m_PinIO)
            {
                case "iDp":
                case "nDp":
                    groupBoxiDp.Enabled = true;
                    groupBoxiDs.Enabled = false;
                    break;
                case "iDs":
                case "nDs":
                    groupBoxiDp.Enabled = false;
                    groupBoxiDs.Enabled = true;
                    break;
                default:
                    groupBoxiDp.Enabled = false;
                    groupBoxiDs.Enabled = false;
                    break;
            }
            Update_PiniDp_enable();
        }

        private void frmPinIO_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void comboBox_PinIO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Enter))
            {
                button_OK_Click(sender, e);
            }
            else if (e.KeyData == (Keys.Escape))
            {
                this.Close();
            }
        }
    }
}