namespace NippoControlSystem
{
    partial class frmTopEditInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTopEditInput));
            this.button_SelectFolder = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_OK = new System.Windows.Forms.Button();
            this.textBox_Folder = new System.Windows.Forms.TextBox();
            this.textBox_Edaban = new System.Windows.Forms.TextBox();
            this.textBox_Zuban = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.lblSubNo = new System.Windows.Forms.Label();
            this.lblMainNo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_SelectFolder
            // 
            this.button_SelectFolder.BackColor = System.Drawing.SystemColors.Control;
            this.button_SelectFolder.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_SelectFolder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_SelectFolder.Image = ((System.Drawing.Image)(resources.GetObject("button_SelectFolder.Image")));
            this.button_SelectFolder.Location = new System.Drawing.Point(447, 111);
            this.button_SelectFolder.Name = "button_SelectFolder";
            this.button_SelectFolder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_SelectFolder.Size = new System.Drawing.Size(49, 49);
            this.button_SelectFolder.TabIndex = 11;
            this.button_SelectFolder.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button_SelectFolder.UseVisualStyleBackColor = false;
            this.button_SelectFolder.Click += new System.EventHandler(this.button_SelectFolder_Click);
            // 
            // button_OK
            // 
            this.button_OK.BackColor = System.Drawing.SystemColors.Control;
            this.button_OK.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_OK.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_OK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_OK.Location = new System.Drawing.Point(151, 191);
            this.button_OK.Name = "button_OK";
            this.button_OK.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_OK.Size = new System.Drawing.Size(185, 57);
            this.button_OK.TabIndex = 12;
            this.button_OK.Text = "&OK";
            this.button_OK.UseVisualStyleBackColor = false;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // textBox_Folder
            // 
            this.textBox_Folder.AcceptsReturn = true;
            this.textBox_Folder.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Folder.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Folder.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Folder.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_Folder.Location = new System.Drawing.Point(23, 127);
            this.textBox_Folder.MaxLength = 0;
            this.textBox_Folder.Name = "textBox_Folder";
            this.textBox_Folder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Folder.Size = new System.Drawing.Size(417, 26);
            this.textBox_Folder.TabIndex = 10;
            // 
            // textBox_Edaban
            // 
            this.textBox_Edaban.AcceptsReturn = true;
            this.textBox_Edaban.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Edaban.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Edaban.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Edaban.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_Edaban.Location = new System.Drawing.Point(359, 55);
            this.textBox_Edaban.MaxLength = 0;
            this.textBox_Edaban.Name = "textBox_Edaban";
            this.textBox_Edaban.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Edaban.Size = new System.Drawing.Size(121, 26);
            this.textBox_Edaban.TabIndex = 9;
            // 
            // textBox_Zuban
            // 
            this.textBox_Zuban.AcceptsReturn = true;
            this.textBox_Zuban.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Zuban.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Zuban.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Zuban.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_Zuban.Location = new System.Drawing.Point(23, 55);
            this.textBox_Zuban.MaxLength = 0;
            this.textBox_Zuban.Name = "textBox_Zuban";
            this.textBox_Zuban.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Zuban.Size = new System.Drawing.Size(313, 26);
            this.textBox_Zuban.TabIndex = 8;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.SystemColors.Control;
            this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label3.Location = new System.Drawing.Point(23, 103);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label3.Size = new System.Drawing.Size(81, 25);
            this.Label3.TabIndex = 15;
            this.Label3.Text = "フォルダ";
            // 
            // lblSubNo
            // 
            this.lblSubNo.BackColor = System.Drawing.SystemColors.Control;
            this.lblSubNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblSubNo.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSubNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSubNo.Location = new System.Drawing.Point(359, 31);
            this.lblSubNo.Name = "lblSubNo";
            this.lblSubNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSubNo.Size = new System.Drawing.Size(121, 25);
            this.lblSubNo.TabIndex = 14;
            this.lblSubNo.Text = "Edaban";
            // 
            // lblMainNo
            // 
            this.lblMainNo.BackColor = System.Drawing.SystemColors.Control;
            this.lblMainNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblMainNo.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMainNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMainNo.Location = new System.Drawing.Point(23, 31);
            this.lblMainNo.Name = "lblMainNo";
            this.lblMainNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblMainNo.Size = new System.Drawing.Size(313, 25);
            this.lblMainNo.TabIndex = 13;
            this.lblMainNo.Text = "Zuban";
            // 
            // frmTopEditInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(519, 279);
            this.Controls.Add(this.button_SelectFolder);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.textBox_Folder);
            this.Controls.Add(this.textBox_Edaban);
            this.Controls.Add(this.textBox_Zuban);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.lblSubNo);
            this.Controls.Add(this.lblMainNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTopEditInput";
            this.Text = "配線チェッカー（編集）";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTopEditInput_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTopEditInput_FormClosed);
            this.Load += new System.EventHandler(this.frmTopEditInput_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button button_SelectFolder;
        public System.Windows.Forms.ToolTip ToolTip1;
        public System.Windows.Forms.Button button_OK;
        public System.Windows.Forms.TextBox textBox_Folder;
        public System.Windows.Forms.TextBox textBox_Edaban;
        public System.Windows.Forms.TextBox textBox_Zuban;
        public System.Windows.Forms.Label Label3;
        public System.Windows.Forms.Label lblSubNo;
        public System.Windows.Forms.Label lblMainNo;
    }
}