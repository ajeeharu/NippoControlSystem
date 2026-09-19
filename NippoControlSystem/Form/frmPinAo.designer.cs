namespace NippoControlSystem
{
    partial class frmPinAo
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPinAo));
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBox_OutVoltValue = new System.Windows.Forms.TextBox();
            this.comboBox_PinAO = new System.Windows.Forms.ComboBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.label_NoticeVolt = new System.Windows.Forms.Label();
            this.label2_OutVoltUnit = new System.Windows.Forms.Label();
            this.label_Notice = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_OutVoltValue
            // 
            this.textBox_OutVoltValue.AcceptsReturn = true;
            this.textBox_OutVoltValue.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_OutVoltValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_OutVoltValue.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_OutVoltValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_OutVoltValue.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_OutVoltValue.Location = new System.Drawing.Point(10, 66);
            this.textBox_OutVoltValue.MaxLength = 0;
            this.textBox_OutVoltValue.Name = "textBox_OutVoltValue";
            this.textBox_OutVoltValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_OutVoltValue.Size = new System.Drawing.Size(97, 28);
            this.textBox_OutVoltValue.TabIndex = 0;
            this.textBox_OutVoltValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ToolTip1.SetToolTip(this.textBox_OutVoltValue, "0.0～15.0");
            this.textBox_OutVoltValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_OutVoltValue_KeyDown);
            // 
            // comboBox_PinAO
            // 
            this.comboBox_PinAO.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox_PinAO.Cursor = System.Windows.Forms.Cursors.Default;
            this.comboBox_PinAO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_PinAO.Font = new System.Drawing.Font("ＭＳ ゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox_PinAO.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox_PinAO.Items.AddRange(new object[] {
            "無効(空白)",
            "有効"});
            this.comboBox_PinAO.Location = new System.Drawing.Point(152, 66);
            this.comboBox_PinAO.Name = "comboBox_PinAO";
            this.comboBox_PinAO.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboBox_PinAO.Size = new System.Drawing.Size(137, 29);
            this.comboBox_PinAO.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.comboBox_PinAO, "選択してください。");
            this.comboBox_PinAO.SelectedIndexChanged += new System.EventHandler(this.comboBox_PinAO_SelectedIndexChanged);
            this.comboBox_PinAO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_PinAO_KeyDown);
            // 
            // button_OK
            // 
            this.button_OK.BackColor = System.Drawing.SystemColors.Control;
            this.button_OK.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_OK.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_OK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_OK.Location = new System.Drawing.Point(66, 122);
            this.button_OK.Name = "button_OK";
            this.button_OK.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_OK.Size = new System.Drawing.Size(161, 41);
            this.button_OK.TabIndex = 2;
            this.button_OK.Text = "&OK";
            this.button_OK.UseVisualStyleBackColor = false;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // label_NoticeVolt
            // 
            this.label_NoticeVolt.BackColor = System.Drawing.SystemColors.Control;
            this.label_NoticeVolt.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_NoticeVolt.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_NoticeVolt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_NoticeVolt.Location = new System.Drawing.Point(10, 50);
            this.label_NoticeVolt.Name = "label_NoticeVolt";
            this.label_NoticeVolt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_NoticeVolt.Size = new System.Drawing.Size(137, 17);
            this.label_NoticeVolt.TabIndex = 11;
            this.label_NoticeVolt.Text = "出力電圧(0～15V)";
            // 
            // label2_OutVoltUnit
            // 
            this.label2_OutVoltUnit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.label2_OutVoltUnit.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2_OutVoltUnit.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2_OutVoltUnit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2_OutVoltUnit.Location = new System.Drawing.Point(114, 74);
            this.label2_OutVoltUnit.Name = "label2_OutVoltUnit";
            this.label2_OutVoltUnit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2_OutVoltUnit.Size = new System.Drawing.Size(17, 17);
            this.label2_OutVoltUnit.TabIndex = 10;
            this.label2_OutVoltUnit.Text = "V";
            // 
            // label_Notice
            // 
            this.label_Notice.BackColor = System.Drawing.SystemColors.Control;
            this.label_Notice.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_Notice.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Notice.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Notice.Location = new System.Drawing.Point(10, 18);
            this.label_Notice.Name = "label_Notice";
            this.label_Notice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_Notice.Size = new System.Drawing.Size(313, 17);
            this.label_Notice.TabIndex = 9;
            this.label_Notice.Text = "アナログ出力（電圧）を入力してください。";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(152, 50);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(137, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "出力有効";
            // 
            // frmPinAo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(333, 180);
            this.Controls.Add(this.label_NoticeVolt);
            this.Controls.Add(this.comboBox_PinAO);
            this.Controls.Add(this.textBox_OutVoltValue);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.label2_OutVoltUnit);
            this.Controls.Add(this.label_Notice);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPinAo";
            this.Text = "アナログ入力";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPinAo_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPinAo_FormClosed);
            this.Load += new System.EventHandler(this.frmPinAo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolTip ToolTip1;
        public System.Windows.Forms.TextBox textBox_OutVoltValue;
        public System.Windows.Forms.Button button_OK;
        public System.Windows.Forms.Label label_NoticeVolt;
        public System.Windows.Forms.Label label2_OutVoltUnit;
        public System.Windows.Forms.Label label_Notice;
        public System.Windows.Forms.ComboBox comboBox_PinAO;
        public System.Windows.Forms.Label label2;
    }
}