using NippoControlSystem.Infrastructure.Configuration;
using NippoControlSystem.UI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NippoControlSystem.UI.Views
{
    public partial class PinAnalogInputView : Form
    {
        //　------　MVVMパターン用にリファクタリングしたコード　------

        //　------　MVVM化のためにリファクタリングする前のコード　------

        Settings Default = Settings.GetInstance();
        MeasureCondition mc = MeasureCondition.GetInstance();

        public PinAnalogInputView()
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
        //プロパティ LowerValue
        string m_LowerValue = "";
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string LowerValue
        {
            get { return m_LowerValue; }
            set
            {
                m_LowerValue = value;
            }
        }

        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ UpperValue
        string m_UpperValue = "";
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string UpperValue
        {
            get { return m_UpperValue; }
            set
            {
                m_UpperValue = value;
            }
        }

        private void frmPinAi_Load(object sender, EventArgs e)
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmPinAi"]);
            Views.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmPinAi"]);

            this.Text = "アナログ入力";
            this.label_Notice.Text = string.Format("アナログ入力の期待値 範囲（電圧の上限と下限）を入力してください。 （{0}～{1}V)", Default.AiVoltMin, Default.AiVoltMax);
            Update_PinAI();

        }
        private void Update_PinAI()
        {
            System.Diagnostics.Debug.WriteLine(string.Format("button_OK.Focus()"));
            this.textBox_LowerValue.Text = this.m_LowerValue;
            this.textBox_UpperValue.Text = this.m_UpperValue;
            // ロード時にフォーカスを設定する
            this.ActiveControl = this.textBox_LowerValue;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmPinAi_button_OK_Click");
            double LowerValue = double.MinValue;
            double UpperValue = double.MaxValue;
            if (!string.IsNullOrEmpty(this.textBox_LowerValue.Text))
            {
                if (!double.TryParse(this.textBox_LowerValue.Text, out LowerValue))
                {
                    System.Windows.Forms.MessageBox.Show("下限値に正しい数値を入力してください。", Default.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                        //電圧入力
                        if (!(LowerValue >= Default.AiVoltMin && LowerValue <= Default.AiVoltMax))
                        {
                            System.Windows.Forms.MessageBox.Show(string.Format("下限値は {0} から {1} までの数値を入力してください。", Default.AiVoltMin, Default.AiVoltMax), Default.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                }
            }
            if (!string.IsNullOrEmpty(this.textBox_UpperValue.Text))
            {
                if (!double.TryParse(this.textBox_UpperValue.Text, out UpperValue))
                {
                    System.Windows.Forms.MessageBox.Show("上限値に正しい数値を入力してください。", Default.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                        //電圧入力
                        if (!(UpperValue >= Default.AiVoltMin && UpperValue <= Default.AiVoltMax))
                        {
                            System.Windows.Forms.MessageBox.Show(string.Format("上限値は {0} から {1} までの数値を入力してください。", Default.AiVoltMin, Default.AiVoltMax), Default.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                }
            }
            if (!(LowerValue == double.MinValue && UpperValue == double.MaxValue))
            {
                if (LowerValue > UpperValue)
                {
                    System.Windows.Forms.MessageBox.Show("上限値は、下限値より大きい数値を入力してください。", Default.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            this.m_LowerValue = this.textBox_LowerValue.Text;
            this.m_UpperValue = this.textBox_UpperValue.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void frmPinAi_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmPinAi_FormClosing");
        }

        private void frmPinAi_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void textBox_LowerValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Enter))
            {
                this.textBox_UpperValue.Focus();
            }
            else if (e.KeyData == (Keys.Escape))
            {
                this.Close();
            }

        }

        private void textBox_UpperValue_KeyDown(object sender, KeyEventArgs e)
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