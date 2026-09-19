using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NippoControlSystem
{
    public partial class frmPinName : Form
    {
        MeasureCondition mc = MeasureCondition.GetInstance();

        public frmPinName()
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
            }
        }
        //--------1---------2---------3---------4---------5---------6---------7---------8
        //プロパティ SelectPin
        string m_PinName = "";
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PinName
        {
            get { return m_PinName; }
            set
            {
                m_PinName = value;
            }
        }

        private void frmPinName_Load(object sender, EventArgs e)
        {
            //Locationを設定、ConfigurationManagerは、System.Configuration.dll への参照設定が必要
            Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["loc_frmPinName"]);
            Forms.DesktopLocation((Form)this, System.Configuration.ConfigurationManager.AppSettings["loc_frmPinName"]);

            this.Text = "端子名の記入";
            Update_PinName();
            //this.textBox_PinName.SelectionStart = 0;
            //this.textBox_PinName.SelectionLength = 0;

        }

        private void Update_PinName()
        {
            this.textBox_PinName.Text = this.m_PinName;
            // ロード時にフォーカスを設定する
            this.ActiveControl = this.textBox_PinName;

            System.Diagnostics.Debug.WriteLine(string.Format("button_OK.Focus()"));
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            this.m_PinName = this.textBox_PinName.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        private void frmPinName_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmPinName_FormClosing");
        }

        private void frmPinName_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void textBox_PinName_KeyDown(object sender, KeyEventArgs e)
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