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
    public partial class PinAnalogOutputView : Form
    {
        //bool button_OKed = false;
        Cyc.IO.Settings Default = Cyc.IO.Settings.GetInstance();
        MeasureCondition mc = MeasureCondition.GetInstance();

        public PinAnalogOutputView()
        {
            InitializeComponent();
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ SelectPin
        int m_SelectPin = 0;
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
        //プロパティ AoValue
        string m_AoValue = "";
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string AoValue
        {
            get { return m_AoValue; }
            set
            {
                m_AoValue = value;
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ AoSw
        string m_AoSw = "";
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string AoSw
        {
            get { return m_AoSw; }
            set
            {
                m_AoSw = value;
            }
        }

        private void frmPinAo_Load(object sender, EventArgs e)
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmPinAo"]);
            Views.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmPinAo"]);

            this.Text = "アナログ出力";
            this.label_NoticeVolt.Text = string.Format("出力電圧({0}～{1}V)", Default.AoVoltMin, Default.AoVoltMax);
            //comboBoxの定義
            comboBox_PinAO.Items.Clear();
            for (int i = 0; i < Default.AoSwichGuide.Length; i++)
            {
                comboBox_PinAO.Items.Add(Default.AoSwichGuide[i]);
            }

            //ここは毎回実行
            //button_OKed = false;
            //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            Update_PinAO();
            //this.comboBox_PinIO.SelectionStart = 0;
            //this.comboBox_PinIO.SelectionLength = 0;
            //this.comboBox_PinAO.SelectedIndex = (this.textBox_PinAO.Text == Default.AoSwichStat[0] ? 0 : 1);
        }

        private void Update_PinAO()
        {
            int SelectedIndex = 0;
            for (int i = 0; i < Default.AoSwichStat.Length; i++)
            {
                if (this.m_AoSw == Default.AoSwichStat[i])
                {
                    SelectedIndex = i;
                }
            }
            this.comboBox_PinAO.SelectedIndex = SelectedIndex;

            if (m_SelectPin < 2)
            {
                double OutVoltValue;
                if (string.IsNullOrEmpty(this.m_AoValue))
                {
                    this.textBox_OutVoltValue.Text = null;
                }
                else
                {
                    if (double.TryParse(this.m_AoValue, out OutVoltValue))
                    {
                        this.textBox_OutVoltValue.Text = this.m_AoValue;
                    }
                    else
                    {
                        this.textBox_OutVoltValue.Text = null;
                    }

                }
                this.textBox_OutVoltValue.Visible = true;
                this.label2_OutVoltUnit.Visible = true;
                this.label_NoticeVolt.Visible = true;
                // ロード時にフォーカスを設定する
                this.ActiveControl = this.textBox_OutVoltValue;
            }
            else
            {
                this.textBox_OutVoltValue.Visible = false;
                this.label2_OutVoltUnit.Visible = false;
                this.label_NoticeVolt.Visible = false;
                // ロード時にフォーカスを設定する
                this.ActiveControl = this.comboBox_PinAO;
            }

        }

        private void comboBox_PinAO_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.textBox_PinAO.Text = Default.AoSwichStat[this.comboBox_PinAO.SelectedIndex];
            //this.textBox_PinAO.Focus(); //Focusをしないと、DataSetが更新されない
            //this.comboBox_PinAO.Focus();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmPinAo:button_OK_Click");

            double OutVoltValue = double.MinValue;
            if (!string.IsNullOrEmpty(this.textBox_OutVoltValue.Text))
            {
                if (!double.TryParse(this.textBox_OutVoltValue.Text, out OutVoltValue))
                {
                    System.Windows.Forms.MessageBox.Show("出力電圧値に正しい数値を入力してください。", Default.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    //電圧出力
                    if (!(OutVoltValue >= Default.AoVoltMin && OutVoltValue <= Default.AoVoltMax))
                    {
                        System.Windows.Forms.MessageBox.Show(string.Format("出力電圧値 {0} から {1} までの数値を入力してください。", Default.AoVoltMin, Default.AoVoltMax), Default.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            //mc.DataSetItems.Tables[this.bindingSource1.DataMember].AcceptChanges();
            this.m_AoValue = this.textBox_OutVoltValue.Text;
            if (this.comboBox_PinAO.SelectedIndex == -1)
            {
                this.m_AoSw = null;
            }
            else
            {
                this.m_AoSw = Default.AoSwichStat[this.comboBox_PinAO.SelectedIndex];
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void frmPinAo_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmPinAo_FormClosing");
            //if (button_OKed)
            //{
            //    this.DialogResult = System.Windows.Forms.DialogResult.OK;
            //}
        }

        private void frmPinAo_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void textBox_OutVoltValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Enter))
            {
                this.comboBox_PinAO.Focus();
            }
            else if (e.KeyData == (Keys.Escape))
            {
                this.Close();
            }

        }

        private void comboBox_PinAO_KeyDown(object sender, KeyEventArgs e)
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