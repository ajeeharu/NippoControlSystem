namespace NippoControlSystem
{
    partial class frmDebug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDebug));
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.radioButton1_2 = new System.Windows.Forms.RadioButton();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Start = new System.Windows.Forms.Button();
            this.radioButton1_1 = new System.Windows.Forms.RadioButton();
            this.radioButton1_0 = new System.Windows.Forms.RadioButton();
            this._Label2_0 = new System.Windows.Forms.Label();
            this._Label2_1 = new System.Windows.Forms.Label();
            this._Label3_4 = new System.Windows.Forms.Label();
            this._Label3_0 = new System.Windows.Forms.Label();
            this.TimerReadSw = new System.Windows.Forms.Timer(this.components);
            this.switchLabelGreenSwitch = new Cyc.Forms.SwitchLabel();
            this.switchLabelRedSwitch = new Cyc.Forms.SwitchLabel();
            this.SuspendLayout();
            // 
            // radioButton1_2
            // 
            this.radioButton1_2.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton1_2.BackColor = System.Drawing.SystemColors.Control;
            this.radioButton1_2.Cursor = System.Windows.Forms.Cursors.Default;
            this.radioButton1_2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButton1_2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.radioButton1_2.Location = new System.Drawing.Point(34, 146);
            this.radioButton1_2.Name = "radioButton1_2";
            this.radioButton1_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radioButton1_2.Size = new System.Drawing.Size(457, 49);
            this.radioButton1_2.TabIndex = 11;
            this.radioButton1_2.Text = "開始位置を指定して検査を開始する。";
            this.radioButton1_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton1_2.UseVisualStyleBackColor = false;
            // 
            // button_Cancel
            // 
            this.button_Cancel.BackColor = System.Drawing.SystemColors.Control;
            this.button_Cancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_Cancel.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Cancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Cancel.Location = new System.Drawing.Point(290, 242);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_Cancel.Size = new System.Drawing.Size(169, 65);
            this.button_Cancel.TabIndex = 13;
            this.button_Cancel.Text = "キャンセル(&C)";
            this.button_Cancel.UseVisualStyleBackColor = false;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Start
            // 
            this.button_Start.BackColor = System.Drawing.SystemColors.Control;
            this.button_Start.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_Start.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Start.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Start.Location = new System.Drawing.Point(58, 242);
            this.button_Start.Name = "button_Start";
            this.button_Start.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_Start.Size = new System.Drawing.Size(169, 65);
            this.button_Start.TabIndex = 12;
            this.button_Start.Text = "検査開始(&G)";
            this.button_Start.UseVisualStyleBackColor = false;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // radioButton1_1
            // 
            this.radioButton1_1.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton1_1.BackColor = System.Drawing.SystemColors.Control;
            this.radioButton1_1.Cursor = System.Windows.Forms.Cursors.Default;
            this.radioButton1_1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButton1_1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.radioButton1_1.Location = new System.Drawing.Point(34, 82);
            this.radioButton1_1.Name = "radioButton1_1";
            this.radioButton1_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radioButton1_1.Size = new System.Drawing.Size(457, 49);
            this.radioButton1_1.TabIndex = 10;
            this.radioButton1_1.Text = "現在のカーソル行から検査を開始する。";
            this.radioButton1_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton1_1.UseVisualStyleBackColor = false;
            // 
            // radioButton1_0
            // 
            this.radioButton1_0.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton1_0.BackColor = System.Drawing.SystemColors.Control;
            this.radioButton1_0.Checked = true;
            this.radioButton1_0.Cursor = System.Windows.Forms.Cursors.Default;
            this.radioButton1_0.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButton1_0.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.radioButton1_0.Location = new System.Drawing.Point(34, 18);
            this.radioButton1_0.Name = "radioButton1_0";
            this.radioButton1_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radioButton1_0.Size = new System.Drawing.Size(457, 49);
            this.radioButton1_0.TabIndex = 9;
            this.radioButton1_0.TabStop = true;
            this.radioButton1_0.Text = "強制的に最後の項目まで検査する。";
            this.radioButton1_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton1_0.UseVisualStyleBackColor = false;
            // 
            // _Label2_0
            // 
            this._Label2_0.BackColor = System.Drawing.SystemColors.Control;
            this._Label2_0.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label2_0.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label2_0.ForeColor = System.Drawing.Color.Red;
            this._Label2_0.Location = new System.Drawing.Point(82, 338);
            this._Label2_0.Name = "_Label2_0";
            this._Label2_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label2_0.Size = new System.Drawing.Size(145, 25);
            this._Label2_0.TabIndex = 17;
            this._Label2_0.Text = "【開始/次へ】";
            // 
            // _Label2_1
            // 
            this._Label2_1.BackColor = System.Drawing.SystemColors.Control;
            this._Label2_1.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label2_1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label2_1.ForeColor = System.Drawing.Color.Red;
            this._Label2_1.Location = new System.Drawing.Point(330, 338);
            this._Label2_1.Name = "_Label2_1";
            this._Label2_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label2_1.Size = new System.Drawing.Size(105, 25);
            this._Label2_1.TabIndex = 16;
            this._Label2_1.Text = "【停止】";
            // 
            // _Label3_4
            // 
            this._Label3_4.BackColor = System.Drawing.SystemColors.Control;
            this._Label3_4.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label3_4.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label3_4.ForeColor = System.Drawing.SystemColors.MenuText;
            this._Label3_4.Location = new System.Drawing.Point(98, 314);
            this._Label3_4.Name = "_Label3_4";
            this._Label3_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label3_4.Size = new System.Drawing.Size(97, 17);
            this._Label3_4.TabIndex = 15;
            this._Label3_4.Text = "操作BOX";
            // 
            // _Label3_0
            // 
            this._Label3_0.BackColor = System.Drawing.SystemColors.Control;
            this._Label3_0.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label3_0.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label3_0.ForeColor = System.Drawing.SystemColors.MenuText;
            this._Label3_0.Location = new System.Drawing.Point(330, 314);
            this._Label3_0.Name = "_Label3_0";
            this._Label3_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label3_0.Size = new System.Drawing.Size(97, 17);
            this._Label3_0.TabIndex = 14;
            this._Label3_0.Text = "操作BOX";
            // 
            // TimerReadSw
            // 
            this.TimerReadSw.Interval = 10;
            this.TimerReadSw.Tick += new System.EventHandler(this.TimerReadSw_Tick);
            // 
            // switchLabelGreenSwitch
            // 
            this.switchLabelGreenSwitch.BackColor = System.Drawing.Color.Silver;
            this.switchLabelGreenSwitch.BackOffColor = System.Drawing.Color.Silver;
            this.switchLabelGreenSwitch.BackOnColor = System.Drawing.Color.Lime;
            this.switchLabelGreenSwitch.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.switchLabelGreenSwitch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.switchLabelGreenSwitch.ForeOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.switchLabelGreenSwitch.ForeOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.switchLabelGreenSwitch.LampBitDevice = "A13";
            this.switchLabelGreenSwitch.LampValue = 0;
            this.switchLabelGreenSwitch.Location = new System.Drawing.Point(117, 207);
            this.switchLabelGreenSwitch.Name = "switchLabelGreenSwitch";
            this.switchLabelGreenSwitch.Size = new System.Drawing.Size(66, 32);
            this.switchLabelGreenSwitch.TabIndex = 428;
            this.switchLabelGreenSwitch.Text = "Green\r\nSwitch";
            this.switchLabelGreenSwitch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.switchLabelGreenSwitch.Visible = false;
            this.switchLabelGreenSwitch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.switchLabelGreenSwitch_MouseDown);
            this.switchLabelGreenSwitch.MouseLeave += new System.EventHandler(this.switchLabelGreenSwitch_MouseLeave);
            this.switchLabelGreenSwitch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.switchLabelGreenSwitch_MouseUp);
            // 
            // switchLabelRedSwitch
            // 
            this.switchLabelRedSwitch.BackColor = System.Drawing.Color.Silver;
            this.switchLabelRedSwitch.BackOffColor = System.Drawing.Color.Silver;
            this.switchLabelRedSwitch.BackOnColor = System.Drawing.Color.Red;
            this.switchLabelRedSwitch.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.switchLabelRedSwitch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.switchLabelRedSwitch.ForeOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.switchLabelRedSwitch.ForeOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.switchLabelRedSwitch.LampBitDevice = "A12";
            this.switchLabelRedSwitch.LampValue = 0;
            this.switchLabelRedSwitch.Location = new System.Drawing.Point(345, 207);
            this.switchLabelRedSwitch.Name = "switchLabelRedSwitch";
            this.switchLabelRedSwitch.Size = new System.Drawing.Size(66, 32);
            this.switchLabelRedSwitch.TabIndex = 427;
            this.switchLabelRedSwitch.Text = "Red\r\nSwitch";
            this.switchLabelRedSwitch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.switchLabelRedSwitch.Visible = false;
            this.switchLabelRedSwitch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.switchLabelRedSwitch_MouseDown);
            this.switchLabelRedSwitch.MouseLeave += new System.EventHandler(this.switchLabelRedSwitch_MouseLeave);
            this.switchLabelRedSwitch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.switchLabelRedSwitch_MouseUp);
            // 
            // frmDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(525, 380);
            this.Controls.Add(this.switchLabelGreenSwitch);
            this.Controls.Add(this.switchLabelRedSwitch);
            this.Controls.Add(this.radioButton1_2);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.radioButton1_1);
            this.Controls.Add(this.radioButton1_0);
            this.Controls.Add(this._Label2_0);
            this.Controls.Add(this._Label2_1);
            this.Controls.Add(this._Label3_4);
            this.Controls.Add(this._Label3_0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDebug";
            this.Text = "デバッグ検査選択";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDebug_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDebug_FormClosed);
            this.Load += new System.EventHandler(this.frmDebug_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ToolTip ToolTip1;
        public System.Windows.Forms.RadioButton radioButton1_2;
        public System.Windows.Forms.Button button_Cancel;
        public System.Windows.Forms.Button button_Start;
        public System.Windows.Forms.RadioButton radioButton1_1;
        public System.Windows.Forms.RadioButton radioButton1_0;
        public System.Windows.Forms.Label _Label2_0;
        public System.Windows.Forms.Label _Label2_1;
        public System.Windows.Forms.Label _Label3_4;
        public System.Windows.Forms.Label _Label3_0;
        public System.Windows.Forms.Timer TimerReadSw;
        private Cyc.Forms.SwitchLabel switchLabelGreenSwitch;
        private Cyc.Forms.SwitchLabel switchLabelRedSwitch;
    }
}