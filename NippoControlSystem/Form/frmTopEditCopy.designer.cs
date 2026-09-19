namespace NippoControlSystem
{
    partial class frmTopEditCopy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTopEditCopy));
            this.textBox_Zuban = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.textBox_Edaban = new System.Windows.Forms.TextBox();
            this.textBox_Folder = new System.Windows.Forms.TextBox();
            this.lblMainNo = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Label1 = new System.Windows.Forms.Label();
            this.textBox_Zuban_Copyed = new System.Windows.Forms.ComboBox();
            this.textBox_Folder_Copyed = new System.Windows.Forms.TextBox();
            this.textBox_Edaban_Copyed = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.Frame2 = new System.Windows.Forms.GroupBox();
            this.CommandFolderBrowser = new System.Windows.Forms.Button();
            this.lblMainNo_Copyed = new System.Windows.Forms.Label();
            this.lblSubNo_Coped = new System.Windows.Forms.Label();
            this._Label3_1 = new System.Windows.Forms.Label();
            this.lblSubNo = new System.Windows.Forms.Label();
            this._Label3_0 = new System.Windows.Forms.Label();
            this.Frame1 = new System.Windows.Forms.GroupBox();
            this.Line1 = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.Frame2.SuspendLayout();
            this.Frame1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_Zuban
            // 
            this.textBox_Zuban.AcceptsReturn = true;
            this.textBox_Zuban.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.textBox_Zuban.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Zuban.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Zuban.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_Zuban.Location = new System.Drawing.Point(16, 48);
            this.textBox_Zuban.MaxLength = 0;
            this.textBox_Zuban.Name = "textBox_Zuban";
            this.textBox_Zuban.ReadOnly = true;
            this.textBox_Zuban.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Zuban.Size = new System.Drawing.Size(313, 26);
            this.textBox_Zuban.TabIndex = 12;
            this.textBox_Zuban.TabStop = false;
            this.textBox_Zuban.Text = "textBox_Zuban";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label2.ForeColor = System.Drawing.Color.Red;
            this.Label2.Location = new System.Drawing.Point(24, 436);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(505, 41);
            this.Label2.TabIndex = 28;
            this.Label2.Text = "※ 各検査毎に別々のフォルダを作成し、指定して下さい。　フォルダが重複した場合、定義ファイルが書き換わることがあります。";
            // 
            // textBox_Edaban
            // 
            this.textBox_Edaban.AcceptsReturn = true;
            this.textBox_Edaban.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.textBox_Edaban.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Edaban.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Edaban.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_Edaban.Location = new System.Drawing.Point(352, 48);
            this.textBox_Edaban.MaxLength = 0;
            this.textBox_Edaban.Name = "textBox_Edaban";
            this.textBox_Edaban.ReadOnly = true;
            this.textBox_Edaban.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Edaban.Size = new System.Drawing.Size(121, 26);
            this.textBox_Edaban.TabIndex = 11;
            this.textBox_Edaban.TabStop = false;
            this.textBox_Edaban.Text = "textBox_Edaban";
            // 
            // textBox_Folder
            // 
            this.textBox_Folder.AcceptsReturn = true;
            this.textBox_Folder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.textBox_Folder.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Folder.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Folder.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_Folder.Location = new System.Drawing.Point(16, 104);
            this.textBox_Folder.MaxLength = 0;
            this.textBox_Folder.Name = "textBox_Folder";
            this.textBox_Folder.ReadOnly = true;
            this.textBox_Folder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Folder.Size = new System.Drawing.Size(417, 26);
            this.textBox_Folder.TabIndex = 10;
            this.textBox_Folder.TabStop = false;
            this.textBox_Folder.Text = "textBox_Folder";
            // 
            // lblMainNo
            // 
            this.lblMainNo.BackColor = System.Drawing.SystemColors.Control;
            this.lblMainNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblMainNo.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMainNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMainNo.Location = new System.Drawing.Point(16, 24);
            this.lblMainNo.Name = "lblMainNo";
            this.lblMainNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblMainNo.Size = new System.Drawing.Size(313, 25);
            this.lblMainNo.TabIndex = 15;
            this.lblMainNo.Text = "Zuban";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Label1.Location = new System.Drawing.Point(296, 212);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(65, 25);
            this.Label1.TabIndex = 27;
            this.Label1.Text = "複製";
            // 
            // textBox_Zuban_Copyed
            // 
            this.textBox_Zuban_Copyed.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Zuban_Copyed.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox_Zuban_Copyed.Enabled = false;
            this.textBox_Zuban_Copyed.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Zuban_Copyed.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_Zuban_Copyed.Location = new System.Drawing.Point(16, 48);
            this.textBox_Zuban_Copyed.Name = "textBox_Zuban_Copyed";
            this.textBox_Zuban_Copyed.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Zuban_Copyed.Size = new System.Drawing.Size(313, 27);
            this.textBox_Zuban_Copyed.TabIndex = 0;
            // 
            // textBox_Folder_Copyed
            // 
            this.textBox_Folder_Copyed.AcceptsReturn = true;
            this.textBox_Folder_Copyed.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Folder_Copyed.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Folder_Copyed.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Folder_Copyed.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_Folder_Copyed.Location = new System.Drawing.Point(16, 104);
            this.textBox_Folder_Copyed.MaxLength = 0;
            this.textBox_Folder_Copyed.Name = "textBox_Folder_Copyed";
            this.textBox_Folder_Copyed.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Folder_Copyed.Size = new System.Drawing.Size(417, 26);
            this.textBox_Folder_Copyed.TabIndex = 2;
            // 
            // textBox_Edaban_Copyed
            // 
            this.textBox_Edaban_Copyed.AcceptsReturn = true;
            this.textBox_Edaban_Copyed.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Edaban_Copyed.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Edaban_Copyed.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Edaban_Copyed.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_Edaban_Copyed.Location = new System.Drawing.Point(352, 48);
            this.textBox_Edaban_Copyed.MaxLength = 0;
            this.textBox_Edaban_Copyed.Name = "textBox_Edaban_Copyed";
            this.textBox_Edaban_Copyed.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Edaban_Copyed.Size = new System.Drawing.Size(121, 26);
            this.textBox_Edaban_Copyed.TabIndex = 1;
            // 
            // button_OK
            // 
            this.button_OK.BackColor = System.Drawing.SystemColors.Control;
            this.button_OK.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_OK.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_OK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_OK.Location = new System.Drawing.Point(176, 492);
            this.button_OK.Name = "button_OK";
            this.button_OK.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_OK.Size = new System.Drawing.Size(185, 49);
            this.button_OK.TabIndex = 24;
            this.button_OK.Text = "&OK";
            this.button_OK.UseVisualStyleBackColor = false;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // Frame2
            // 
            this.Frame2.BackColor = System.Drawing.SystemColors.Control;
            this.Frame2.Controls.Add(this.textBox_Zuban_Copyed);
            this.Frame2.Controls.Add(this.textBox_Edaban_Copyed);
            this.Frame2.Controls.Add(this.textBox_Folder_Copyed);
            this.Frame2.Controls.Add(this.CommandFolderBrowser);
            this.Frame2.Controls.Add(this.lblMainNo_Copyed);
            this.Frame2.Controls.Add(this.lblSubNo_Coped);
            this.Frame2.Controls.Add(this._Label3_1);
            this.Frame2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Frame2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Frame2.Location = new System.Drawing.Point(16, 284);
            this.Frame2.Name = "Frame2";
            this.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame2.Size = new System.Drawing.Size(513, 145);
            this.Frame2.TabIndex = 26;
            this.Frame2.TabStop = false;
            this.Frame2.Text = "複製先";
            // 
            // CommandFolderBrowser
            // 
            this.CommandFolderBrowser.BackColor = System.Drawing.SystemColors.Control;
            this.CommandFolderBrowser.Cursor = System.Windows.Forms.Cursors.Default;
            this.CommandFolderBrowser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CommandFolderBrowser.Image = ((System.Drawing.Image)(resources.GetObject("CommandFolderBrowser.Image")));
            this.CommandFolderBrowser.Location = new System.Drawing.Point(440, 88);
            this.CommandFolderBrowser.Name = "CommandFolderBrowser";
            this.CommandFolderBrowser.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CommandFolderBrowser.Size = new System.Drawing.Size(49, 49);
            this.CommandFolderBrowser.TabIndex = 3;
            this.CommandFolderBrowser.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.CommandFolderBrowser.UseVisualStyleBackColor = false;
            this.CommandFolderBrowser.Click += new System.EventHandler(this.CommandFolderBrowser_Click);
            // 
            // lblMainNo_Copyed
            // 
            this.lblMainNo_Copyed.BackColor = System.Drawing.SystemColors.Control;
            this.lblMainNo_Copyed.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblMainNo_Copyed.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMainNo_Copyed.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMainNo_Copyed.Location = new System.Drawing.Point(16, 24);
            this.lblMainNo_Copyed.Name = "lblMainNo_Copyed";
            this.lblMainNo_Copyed.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblMainNo_Copyed.Size = new System.Drawing.Size(313, 25);
            this.lblMainNo_Copyed.TabIndex = 9;
            this.lblMainNo_Copyed.Text = "Zuban";
            // 
            // lblSubNo_Coped
            // 
            this.lblSubNo_Coped.BackColor = System.Drawing.SystemColors.Control;
            this.lblSubNo_Coped.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblSubNo_Coped.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSubNo_Coped.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSubNo_Coped.Location = new System.Drawing.Point(352, 24);
            this.lblSubNo_Coped.Name = "lblSubNo_Coped";
            this.lblSubNo_Coped.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSubNo_Coped.Size = new System.Drawing.Size(121, 25);
            this.lblSubNo_Coped.TabIndex = 8;
            this.lblSubNo_Coped.Text = "Edaban";
            // 
            // _Label3_1
            // 
            this._Label3_1.BackColor = System.Drawing.SystemColors.Control;
            this._Label3_1.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label3_1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label3_1.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Label3_1.Location = new System.Drawing.Point(16, 80);
            this._Label3_1.Name = "_Label3_1";
            this._Label3_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label3_1.Size = new System.Drawing.Size(81, 25);
            this._Label3_1.TabIndex = 7;
            this._Label3_1.Text = "フォルダ";
            // 
            // lblSubNo
            // 
            this.lblSubNo.BackColor = System.Drawing.SystemColors.Control;
            this.lblSubNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblSubNo.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSubNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSubNo.Location = new System.Drawing.Point(352, 24);
            this.lblSubNo.Name = "lblSubNo";
            this.lblSubNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSubNo.Size = new System.Drawing.Size(121, 25);
            this.lblSubNo.TabIndex = 14;
            this.lblSubNo.Text = "Edaban";
            // 
            // _Label3_0
            // 
            this._Label3_0.BackColor = System.Drawing.SystemColors.Control;
            this._Label3_0.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label3_0.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label3_0.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Label3_0.Location = new System.Drawing.Point(16, 80);
            this._Label3_0.Name = "_Label3_0";
            this._Label3_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label3_0.Size = new System.Drawing.Size(81, 25);
            this._Label3_0.TabIndex = 13;
            this._Label3_0.Text = "フォルダ";
            // 
            // Frame1
            // 
            this.Frame1.BackColor = System.Drawing.SystemColors.Control;
            this.Frame1.Controls.Add(this.textBox_Zuban);
            this.Frame1.Controls.Add(this.textBox_Edaban);
            this.Frame1.Controls.Add(this.textBox_Folder);
            this.Frame1.Controls.Add(this.lblMainNo);
            this.Frame1.Controls.Add(this.lblSubNo);
            this.Frame1.Controls.Add(this._Label3_0);
            this.Frame1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Frame1.Location = new System.Drawing.Point(16, 20);
            this.Frame1.Name = "Frame1";
            this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame1.Size = new System.Drawing.Size(513, 145);
            this.Frame1.TabIndex = 25;
            this.Frame1.TabStop = false;
            this.Frame1.Text = "複製元";
            // 
            // Line1
            // 
            this.Line1.BackColor = System.Drawing.Color.Transparent;
            this.Line1.ForeColor = System.Drawing.Color.Black;
            this.Line1.Image = global::NippoControlSystem.UI.Properties.Resources.arrow_down;
            this.Line1.Location = new System.Drawing.Point(240, 180);
            this.Line1.Name = "Line1";
            this.Line1.Size = new System.Drawing.Size(52, 101);
            this.Line1.TabIndex = 32;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataMember = "menuMain";
            // 
            // bindingSource2
            // 
            this.bindingSource2.DataMember = "menuSub";
            // 
            // frmTopEditCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(545, 560);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Line1);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.Frame2);
            this.Controls.Add(this.Frame1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTopEditCopy";
            this.Text = "配線チェッカー（複製）";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTopEditCopy_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTopEditCopy_FormClosed);
            this.Load += new System.EventHandler(this.frmTopEditCopy_Load);
            this.Frame2.ResumeLayout(false);
            this.Frame2.PerformLayout();
            this.Frame1.ResumeLayout(false);
            this.Frame1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TextBox textBox_Zuban;
        public System.Windows.Forms.Label Label2;
        public System.Windows.Forms.TextBox textBox_Edaban;
        public System.Windows.Forms.TextBox textBox_Folder;
        public System.Windows.Forms.Label lblMainNo;
        public System.Windows.Forms.ToolTip ToolTip1;
        public System.Windows.Forms.Label Label1;
        public System.Windows.Forms.Label Line1;
        public System.Windows.Forms.ComboBox textBox_Zuban_Copyed;
        public System.Windows.Forms.TextBox textBox_Folder_Copyed;
        public System.Windows.Forms.TextBox textBox_Edaban_Copyed;
        public System.Windows.Forms.Button button_OK;
        public System.Windows.Forms.GroupBox Frame2;
        public System.Windows.Forms.Button CommandFolderBrowser;
        public System.Windows.Forms.Label lblMainNo_Copyed;
        public System.Windows.Forms.Label lblSubNo_Coped;
        public System.Windows.Forms.Label _Label3_1;
        public System.Windows.Forms.Label lblSubNo;
        public System.Windows.Forms.Label _Label3_0;
        public System.Windows.Forms.GroupBox Frame1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource bindingSource2;
    }
}