namespace NippoControlSystem
{
    partial class frmSerial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSerial));
            this.textBox_GoNo = new System.Windows.Forms.TextBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_SaveClose = new System.Windows.Forms.Button();
            this.textBox_Serial = new System.Windows.Forms.TextBox();
            this.lblGoTitle = new System.Windows.Forms.Label();
            this._Label3_0 = new System.Windows.Forms.Label();
            this.TimerReadSw = new System.Windows.Forms.Timer(this.components);
            this._Label3_4 = new System.Windows.Forms.Label();
            this._Label2_1 = new System.Windows.Forms.Label();
            this._Label2_0 = new System.Windows.Forms.Label();
            this.lblSerialTitle = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // textBox_GoNo
            // 
            this.textBox_GoNo.AcceptsReturn = true;
            this.textBox_GoNo.BackColor = System.Drawing.Color.Black;
            this.textBox_GoNo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_GoNo.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_GoNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.textBox_GoNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_GoNo.Location = new System.Drawing.Point(42, 145);
            this.textBox_GoNo.MaxLength = 0;
            this.textBox_GoNo.Name = "textBox_GoNo";
            this.textBox_GoNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_GoNo.Size = new System.Drawing.Size(377, 39);
            this.textBox_GoNo.TabIndex = 11;
            // 
            // button_Cancel
            // 
            this.button_Cancel.BackColor = System.Drawing.SystemColors.Control;
            this.button_Cancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_Cancel.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Cancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Cancel.Location = new System.Drawing.Point(250, 233);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_Cancel.Size = new System.Drawing.Size(161, 49);
            this.button_Cancel.TabIndex = 13;
            this.button_Cancel.Text = "保存せず終了";
            this.button_Cancel.UseVisualStyleBackColor = false;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_SaveClose
            // 
            this.button_SaveClose.BackColor = System.Drawing.SystemColors.Control;
            this.button_SaveClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_SaveClose.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_SaveClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_SaveClose.Location = new System.Drawing.Point(34, 233);
            this.button_SaveClose.Name = "button_SaveClose";
            this.button_SaveClose.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_SaveClose.Size = new System.Drawing.Size(161, 49);
            this.button_SaveClose.TabIndex = 12;
            this.button_SaveClose.Text = "保存して終了";
            this.button_SaveClose.UseVisualStyleBackColor = false;
            this.button_SaveClose.Click += new System.EventHandler(this.button_SaveClose_Click);
            // 
            // textBox_Serial
            // 
            this.textBox_Serial.AcceptsReturn = true;
            this.textBox_Serial.BackColor = System.Drawing.Color.Black;
            this.textBox_Serial.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Serial.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Serial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.textBox_Serial.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_Serial.Location = new System.Drawing.Point(42, 57);
            this.textBox_Serial.MaxLength = 0;
            this.textBox_Serial.Name = "textBox_Serial";
            this.textBox_Serial.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Serial.Size = new System.Drawing.Size(377, 39);
            this.textBox_Serial.TabIndex = 10;
            // 
            // lblGoTitle
            // 
            this.lblGoTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblGoTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblGoTitle.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblGoTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblGoTitle.Location = new System.Drawing.Point(42, 121);
            this.lblGoTitle.Name = "lblGoTitle";
            this.lblGoTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblGoTitle.Size = new System.Drawing.Size(353, 25);
            this.lblGoTitle.TabIndex = 19;
            this.lblGoTitle.Text = "Number入力して下さい。";
            // 
            // _Label3_0
            // 
            this._Label3_0.BackColor = System.Drawing.SystemColors.Control;
            this._Label3_0.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label3_0.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label3_0.ForeColor = System.Drawing.SystemColors.MenuText;
            this._Label3_0.Location = new System.Drawing.Point(290, 289);
            this._Label3_0.Name = "_Label3_0";
            this._Label3_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label3_0.Size = new System.Drawing.Size(97, 17);
            this._Label3_0.TabIndex = 18;
            this._Label3_0.Text = "操作BOX";
            // 
            // TimerReadSw
            // 
            this.TimerReadSw.Interval = 10;
            this.TimerReadSw.Tick += new System.EventHandler(this.TimerReadSw_Tick);
            // 
            // _Label3_4
            // 
            this._Label3_4.BackColor = System.Drawing.SystemColors.Control;
            this._Label3_4.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label3_4.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label3_4.ForeColor = System.Drawing.SystemColors.MenuText;
            this._Label3_4.Location = new System.Drawing.Point(66, 289);
            this._Label3_4.Name = "_Label3_4";
            this._Label3_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label3_4.Size = new System.Drawing.Size(97, 17);
            this._Label3_4.TabIndex = 17;
            this._Label3_4.Text = "操作BOX";
            // 
            // _Label2_1
            // 
            this._Label2_1.BackColor = System.Drawing.SystemColors.Control;
            this._Label2_1.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label2_1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label2_1.ForeColor = System.Drawing.Color.Red;
            this._Label2_1.Location = new System.Drawing.Point(290, 313);
            this._Label2_1.Name = "_Label2_1";
            this._Label2_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label2_1.Size = new System.Drawing.Size(105, 25);
            this._Label2_1.TabIndex = 16;
            this._Label2_1.Text = "【停止】";
            // 
            // _Label2_0
            // 
            this._Label2_0.BackColor = System.Drawing.SystemColors.Control;
            this._Label2_0.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label2_0.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label2_0.ForeColor = System.Drawing.Color.Red;
            this._Label2_0.Location = new System.Drawing.Point(50, 313);
            this._Label2_0.Name = "_Label2_0";
            this._Label2_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label2_0.Size = new System.Drawing.Size(136, 25);
            this._Label2_0.TabIndex = 15;
            this._Label2_0.Text = "【開始/次へ】";
            // 
            // lblSerialTitle
            // 
            this.lblSerialTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblSerialTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblSerialTitle.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSerialTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSerialTitle.Location = new System.Drawing.Point(42, 25);
            this.lblSerialTitle.Name = "lblSerialTitle";
            this.lblSerialTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSerialTitle.Size = new System.Drawing.Size(353, 25);
            this.lblSerialTitle.TabIndex = 14;
            this.lblSerialTitle.Text = "Number入力して下さい。";
            // 
            // frmSerial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(453, 362);
            this.ControlBox = false;
            this.Controls.Add(this.textBox_GoNo);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_SaveClose);
            this.Controls.Add(this.textBox_Serial);
            this.Controls.Add(this.lblGoTitle);
            this.Controls.Add(this._Label3_0);
            this.Controls.Add(this._Label3_4);
            this.Controls.Add(this._Label2_1);
            this.Controls.Add(this._Label2_0);
            this.Controls.Add(this.lblSerialTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSerial";
            this.Text = "製造番号";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSerial_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSerial_FormClosed);
            this.Load += new System.EventHandler(this.frmSerial_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox_GoNo;
        public System.Windows.Forms.Button button_Cancel;
        public System.Windows.Forms.Button button_SaveClose;
        public System.Windows.Forms.TextBox textBox_Serial;
        public System.Windows.Forms.Label lblGoTitle;
        public System.Windows.Forms.Label _Label3_0;
        public System.Windows.Forms.Timer TimerReadSw;
        public System.Windows.Forms.Label _Label3_4;
        public System.Windows.Forms.Label _Label2_1;
        public System.Windows.Forms.Label _Label2_0;
        public System.Windows.Forms.Label lblSerialTitle;
        public System.Windows.Forms.ToolTip ToolTip1;
    }
}