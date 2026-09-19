namespace NippoControlSystem
{
    partial class frmPinName
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPinName));
            this.textBox_PinName = new System.Windows.Forms.TextBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_OK = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_PinName
            // 
            this.textBox_PinName.AcceptsReturn = true;
            this.textBox_PinName.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_PinName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_PinName.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_PinName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_PinName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_PinName.Location = new System.Drawing.Point(18, 60);
            this.textBox_PinName.MaxLength = 0;
            this.textBox_PinName.Name = "textBox_PinName";
            this.textBox_PinName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_PinName.Size = new System.Drawing.Size(129, 28);
            this.textBox_PinName.TabIndex = 3;
            this.textBox_PinName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip1.SetToolTip(this.textBox_PinName, "端子名の入力");
            this.textBox_PinName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_PinName_KeyDown);
            // 
            // button_OK
            // 
            this.button_OK.BackColor = System.Drawing.SystemColors.Control;
            this.button_OK.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_OK.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_OK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_OK.Location = new System.Drawing.Point(66, 108);
            this.button_OK.Name = "button_OK";
            this.button_OK.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_OK.Size = new System.Drawing.Size(161, 41);
            this.button_OK.TabIndex = 4;
            this.button_OK.Text = "&OK";
            this.button_OK.UseVisualStyleBackColor = false;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(10, 20);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(289, 17);
            this.Label1.TabIndex = 5;
            this.Label1.Text = "接続した端子の名称を入力してください。";
            // 
            // frmPinName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(309, 169);
            this.Controls.Add(this.textBox_PinName);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPinName";
            this.Text = "デジタル入出力";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPinName_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPinName_FormClosed);
            this.Load += new System.EventHandler(this.frmPinName_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox_PinName;
        public System.Windows.Forms.ToolTip ToolTip1;
        public System.Windows.Forms.Button button_OK;
        public System.Windows.Forms.Label Label1;
    }
}