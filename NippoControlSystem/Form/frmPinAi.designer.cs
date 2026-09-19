namespace NippoControlSystem
{
    partial class frmPinAi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPinAi));
            this.textBox_UpperValue = new System.Windows.Forms.TextBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBox_LowerValue = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this._Label2_3 = new System.Windows.Forms.Label();
            this._Label2_2 = new System.Windows.Forms.Label();
            this._Label2_1 = new System.Windows.Forms.Label();
            this._Label2_0 = new System.Windows.Forms.Label();
            this.label_Notice = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_UpperValue
            // 
            this.textBox_UpperValue.AcceptsReturn = true;
            this.textBox_UpperValue.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_UpperValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_UpperValue.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_UpperValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_UpperValue.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_UpperValue.Location = new System.Drawing.Point(150, 74);
            this.textBox_UpperValue.MaxLength = 0;
            this.textBox_UpperValue.Name = "textBox_UpperValue";
            this.textBox_UpperValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_UpperValue.Size = new System.Drawing.Size(97, 28);
            this.textBox_UpperValue.TabIndex = 1;
            this.textBox_UpperValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ToolTip1.SetToolTip(this.textBox_UpperValue, "0.00～30.00");
            this.textBox_UpperValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_UpperValue_KeyDown);
            // 
            // textBox_LowerValue
            // 
            this.textBox_LowerValue.AcceptsReturn = true;
            this.textBox_LowerValue.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_LowerValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_LowerValue.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_LowerValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_LowerValue.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_LowerValue.Location = new System.Drawing.Point(14, 74);
            this.textBox_LowerValue.MaxLength = 0;
            this.textBox_LowerValue.Name = "textBox_LowerValue";
            this.textBox_LowerValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_LowerValue.Size = new System.Drawing.Size(97, 28);
            this.textBox_LowerValue.TabIndex = 0;
            this.textBox_LowerValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ToolTip1.SetToolTip(this.textBox_LowerValue, "0.0～15.0");
            this.textBox_LowerValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_LowerValue_KeyDown);
            // 
            // button_OK
            // 
            this.button_OK.BackColor = System.Drawing.SystemColors.Control;
            this.button_OK.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_OK.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_OK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_OK.Location = new System.Drawing.Point(62, 122);
            this.button_OK.Name = "button_OK";
            this.button_OK.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_OK.Size = new System.Drawing.Size(161, 41);
            this.button_OK.TabIndex = 2;
            this.button_OK.Text = "&OK";
            this.button_OK.UseVisualStyleBackColor = false;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // _Label2_3
            // 
            this._Label2_3.BackColor = System.Drawing.SystemColors.Control;
            this._Label2_3.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label2_3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label2_3.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Label2_3.Location = new System.Drawing.Point(150, 58);
            this._Label2_3.Name = "_Label2_3";
            this._Label2_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label2_3.Size = new System.Drawing.Size(57, 17);
            this._Label2_3.TabIndex = 15;
            this._Label2_3.Text = "上限";
            // 
            // _Label2_2
            // 
            this._Label2_2.BackColor = System.Drawing.SystemColors.Control;
            this._Label2_2.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label2_2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label2_2.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Label2_2.Location = new System.Drawing.Point(14, 58);
            this._Label2_2.Name = "_Label2_2";
            this._Label2_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label2_2.Size = new System.Drawing.Size(81, 17);
            this._Label2_2.TabIndex = 14;
            this._Label2_2.Text = "下限";
            // 
            // _Label2_1
            // 
            this._Label2_1.BackColor = System.Drawing.SystemColors.Control;
            this._Label2_1.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label2_1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label2_1.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Label2_1.Location = new System.Drawing.Point(246, 82);
            this._Label2_1.Name = "_Label2_1";
            this._Label2_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label2_1.Size = new System.Drawing.Size(17, 17);
            this._Label2_1.TabIndex = 13;
            this._Label2_1.Text = "V";
            // 
            // _Label2_0
            // 
            this._Label2_0.BackColor = System.Drawing.SystemColors.Control;
            this._Label2_0.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label2_0.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label2_0.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Label2_0.Location = new System.Drawing.Point(118, 82);
            this._Label2_0.Name = "_Label2_0";
            this._Label2_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label2_0.Size = new System.Drawing.Size(17, 17);
            this._Label2_0.TabIndex = 12;
            this._Label2_0.Text = "V";
            // 
            // label_Notice
            // 
            this.label_Notice.BackColor = System.Drawing.SystemColors.Control;
            this.label_Notice.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_Notice.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Notice.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Notice.Location = new System.Drawing.Point(14, 18);
            this.label_Notice.Name = "label_Notice";
            this.label_Notice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_Notice.Size = new System.Drawing.Size(305, 33);
            this.label_Notice.TabIndex = 11;
            this.label_Notice.Text = "アナログ入力の期待値 範囲（電圧の上限と下限）を入力してください。 （0～15V)";
            // 
            // frmPinAi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(333, 180);
            this.Controls.Add(this.textBox_UpperValue);
            this.Controls.Add(this.textBox_LowerValue);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this._Label2_3);
            this.Controls.Add(this._Label2_2);
            this.Controls.Add(this._Label2_1);
            this.Controls.Add(this._Label2_0);
            this.Controls.Add(this.label_Notice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPinAi";
            this.Text = "frmPinAi.cs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPinAi_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPinAi_FormClosed);
            this.Load += new System.EventHandler(this.frmPinAi_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox_UpperValue;
        public System.Windows.Forms.ToolTip ToolTip1;
        public System.Windows.Forms.TextBox textBox_LowerValue;
        public System.Windows.Forms.Button button_OK;
        public System.Windows.Forms.Label _Label2_3;
        public System.Windows.Forms.Label _Label2_2;
        public System.Windows.Forms.Label _Label2_1;
        public System.Windows.Forms.Label _Label2_0;
        public System.Windows.Forms.Label label_Notice;
    }
}

