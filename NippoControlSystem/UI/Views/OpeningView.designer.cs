namespace NippoControlSystem.UI.Views
{
    partial class OpeningView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpeningView));
            this.textBox_Filter = new System.Windows.Forms.TextBox();
            this.button_ViewManual = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.listBox_SubNo = new System.Windows.Forms.ListBox();
            this.menuSubBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuMainBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSetTopMenu = new NippoControlSystem.DataSetTopMenu();
            this.listBox_MainNo = new System.Windows.Forms.ListBox();
            this.button_frmSetting = new System.Windows.Forms.Button();
            this.button_Exit = new System.Windows.Forms.Button();
            this.button_frmTopEdit = new System.Windows.Forms.Button();
            this.button_frmMain = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.lblSubNo = new System.Windows.Forms.Label();
            this.lblMainNo = new System.Windows.Forms.Label();
            this.timerInitalUpdate = new System.Windows.Forms.Timer(this.components);
            this.button_AnalogMonitor = new System.Windows.Forms.Button();
            this.buttonHistory = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.menuSubBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuMainBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetTopMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_Filter
            // 
            this.textBox_Filter.AcceptsReturn = true;
            this.textBox_Filter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox_Filter.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Filter.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Filter.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_Filter.Location = new System.Drawing.Point(31, 533);
            this.textBox_Filter.MaxLength = 0;
            this.textBox_Filter.Name = "textBox_Filter";
            this.textBox_Filter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Filter.Size = new System.Drawing.Size(265, 31);
            this.textBox_Filter.TabIndex = 16;
            this.textBox_Filter.TextChanged += new System.EventHandler(this.textBox_Filter_TextChanged);
            // 
            // button_ViewManual
            // 
            this.button_ViewManual.BackColor = System.Drawing.SystemColors.Control;
            this.button_ViewManual.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_ViewManual.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_ViewManual.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_ViewManual.Location = new System.Drawing.Point(463, 419);
            this.button_ViewManual.Name = "button_ViewManual";
            this.button_ViewManual.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_ViewManual.Size = new System.Drawing.Size(169, 65);
            this.button_ViewManual.TabIndex = 20;
            this.button_ViewManual.Text = "操作マニュアル(&M)";
            this.button_ViewManual.UseVisualStyleBackColor = false;
            this.button_ViewManual.Click += new System.EventHandler(this.button_ViewManual_Click);
            // 
            // listBox_SubNo
            // 
            this.listBox_SubNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listBox_SubNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBox_SubNo.DataSource = this.menuSubBindingSource;
            this.listBox_SubNo.DisplayMember = "SubTitle";
            this.listBox_SubNo.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox_SubNo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listBox_SubNo.ItemHeight = 24;
            this.listBox_SubNo.Location = new System.Drawing.Point(303, 93);
            this.listBox_SubNo.Name = "listBox_SubNo";
            this.listBox_SubNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBox_SubNo.Size = new System.Drawing.Size(129, 412);
            this.listBox_SubNo.TabIndex = 15;
            this.ToolTip1.SetToolTip(this.listBox_SubNo, "選択してください。");
            this.listBox_SubNo.ValueMember = "SubID";
            this.listBox_SubNo.DoubleClick += new System.EventHandler(this.listBox_SubNo_DoubleClick);
            // 
            // menuSubBindingSource
            // 
            this.menuSubBindingSource.DataMember = "menuMain_menuSub";
            this.menuSubBindingSource.DataSource = this.menuMainBindingSource;
            // 
            // menuMainBindingSource
            // 
            this.menuMainBindingSource.DataMember = "menuMain";
            this.menuMainBindingSource.DataSource = this.DataSetTopMenu;
            // 
            // DataSetTopMenu
            // 
            this.DataSetTopMenu.DataSetName = "DataSetTopMenu";
            this.DataSetTopMenu.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // listBox_MainNo
            // 
            this.listBox_MainNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listBox_MainNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBox_MainNo.DataSource = this.menuMainBindingSource;
            this.listBox_MainNo.DisplayMember = "Title";
            this.listBox_MainNo.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox_MainNo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listBox_MainNo.ItemHeight = 24;
            this.listBox_MainNo.Location = new System.Drawing.Point(31, 93);
            this.listBox_MainNo.Name = "listBox_MainNo";
            this.listBox_MainNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBox_MainNo.Size = new System.Drawing.Size(265, 412);
            this.listBox_MainNo.TabIndex = 14;
            this.ToolTip1.SetToolTip(this.listBox_MainNo, "選択してください。");
            this.listBox_MainNo.ValueMember = "MainID";
            this.listBox_MainNo.SelectedIndexChanged += new System.EventHandler(this.listBox_MainNo_SelectedIndexChanged);
            // 
            // button_frmSetting
            // 
            this.button_frmSetting.BackColor = System.Drawing.SystemColors.Control;
            this.button_frmSetting.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_frmSetting.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_frmSetting.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_frmSetting.Location = new System.Drawing.Point(463, 339);
            this.button_frmSetting.Name = "button_frmSetting";
            this.button_frmSetting.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_frmSetting.Size = new System.Drawing.Size(169, 65);
            this.button_frmSetting.TabIndex = 19;
            this.button_frmSetting.Text = "検査定義の編集(&I)";
            this.button_frmSetting.UseVisualStyleBackColor = false;
            this.button_frmSetting.Click += new System.EventHandler(this.button_SettingView_Click);
            // 
            // button_Exit
            // 
            this.button_Exit.BackColor = System.Drawing.SystemColors.Control;
            this.button_Exit.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_Exit.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Exit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Exit.Location = new System.Drawing.Point(463, 509);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_Exit.Size = new System.Drawing.Size(169, 65);
            this.button_Exit.TabIndex = 21;
            this.button_Exit.Text = "終了(&Q)";
            this.button_Exit.UseVisualStyleBackColor = false;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // button_frmTopEdit
            // 
            this.button_frmTopEdit.BackColor = System.Drawing.SystemColors.Control;
            this.button_frmTopEdit.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_frmTopEdit.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_frmTopEdit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_frmTopEdit.Location = new System.Drawing.Point(463, 259);
            this.button_frmTopEdit.Name = "button_frmTopEdit";
            this.button_frmTopEdit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_frmTopEdit.Size = new System.Drawing.Size(169, 65);
            this.button_frmTopEdit.TabIndex = 18;
            this.button_frmTopEdit.Text = "この画面の編集(&E)";
            this.button_frmTopEdit.UseVisualStyleBackColor = false;
            this.button_frmTopEdit.Click += new System.EventHandler(this.button_TopEditView_Click);
            // 
            // button_frmMain
            // 
            this.button_frmMain.BackColor = System.Drawing.SystemColors.Control;
            this.button_frmMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_frmMain.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_frmMain.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_frmMain.Location = new System.Drawing.Point(463, 77);
            this.button_frmMain.Name = "button_frmMain";
            this.button_frmMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_frmMain.Size = new System.Drawing.Size(169, 65);
            this.button_frmMain.TabIndex = 17;
            this.button_frmMain.Text = "検査(&C)";
            this.button_frmMain.UseVisualStyleBackColor = false;
            this.button_frmMain.Click += new System.EventHandler(this.button_MainView_Click);
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.Location = new System.Drawing.Point(31, 509);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(257, 17);
            this.Label2.TabIndex = 27;
            this.Label2.Text = "フィルター";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Label1.Location = new System.Drawing.Point(23, 21);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(234, 33);
            this.Label1.TabIndex = 26;
            this.Label1.Text = "配線チェッカーV2";
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.SystemColors.Control;
            this.lblVersion.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblVersion.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblVersion.Location = new System.Drawing.Point(443, 1);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblVersion.Size = new System.Drawing.Size(215, 20);
            this.lblVersion.TabIndex = 25;
            this.lblVersion.Text = "lblVersion";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblVersion.Click += new System.EventHandler(this.lblVersion_Click);
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.SystemColors.Control;
            this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Label3.Location = new System.Drawing.Point(272, 21);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label3.Size = new System.Drawing.Size(296, 33);
            this.Label3.TabIndex = 24;
            this.Label3.Text = "選択画面（トップ画面）  ";
            // 
            // lblSubNo
            // 
            this.lblSubNo.BackColor = System.Drawing.SystemColors.Control;
            this.lblSubNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblSubNo.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSubNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSubNo.Location = new System.Drawing.Point(303, 69);
            this.lblSubNo.Name = "lblSubNo";
            this.lblSubNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSubNo.Size = new System.Drawing.Size(113, 17);
            this.lblSubNo.TabIndex = 23;
            this.lblSubNo.Text = "lblSubNo";
            // 
            // lblMainNo
            // 
            this.lblMainNo.BackColor = System.Drawing.SystemColors.Control;
            this.lblMainNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblMainNo.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMainNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMainNo.Location = new System.Drawing.Point(31, 69);
            this.lblMainNo.Name = "lblMainNo";
            this.lblMainNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblMainNo.Size = new System.Drawing.Size(257, 17);
            this.lblMainNo.TabIndex = 22;
            this.lblMainNo.Text = "lblMainNo";
            // 
            // timerInitalUpdate
            // 
            this.timerInitalUpdate.Tick += new System.EventHandler(this.timerInitalUpdate_Tick);
            // 
            // button_AnalogMonitor
            // 
            this.button_AnalogMonitor.BackColor = System.Drawing.SystemColors.Control;
            this.button_AnalogMonitor.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_AnalogMonitor.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_AnalogMonitor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_AnalogMonitor.Location = new System.Drawing.Point(303, 519);
            this.button_AnalogMonitor.Name = "button_AnalogMonitor";
            this.button_AnalogMonitor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_AnalogMonitor.Size = new System.Drawing.Size(129, 56);
            this.button_AnalogMonitor.TabIndex = 28;
            this.button_AnalogMonitor.Text = "アナログ入出力モニタ(&A)";
            this.button_AnalogMonitor.UseVisualStyleBackColor = false;
            this.button_AnalogMonitor.Visible = false;
            this.button_AnalogMonitor.Click += new System.EventHandler(this.button_AnalogMonitor_Click);
            // 
            // buttonHistory
            // 
            this.buttonHistory.BackColor = System.Drawing.SystemColors.Control;
            this.buttonHistory.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonHistory.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonHistory.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonHistory.Location = new System.Drawing.Point(463, 159);
            this.buttonHistory.Name = "buttonHistory";
            this.buttonHistory.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonHistory.Size = new System.Drawing.Size(169, 65);
            this.buttonHistory.TabIndex = 29;
            this.buttonHistory.Text = "検査履歴(&D)";
            this.buttonHistory.UseVisualStyleBackColor = false;
            this.buttonHistory.Click += new System.EventHandler(this.button_HistoryView_Click);
            // 
            // frmOpenning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(662, 587);
            this.ControlBox = false;
            this.Controls.Add(this.buttonHistory);
            this.Controls.Add(this.button_AnalogMonitor);
            this.Controls.Add(this.textBox_Filter);
            this.Controls.Add(this.button_ViewManual);
            this.Controls.Add(this.button_frmSetting);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.button_frmTopEdit);
            this.Controls.Add(this.button_frmMain);
            this.Controls.Add(this.listBox_SubNo);
            this.Controls.Add(this.listBox_MainNo);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.lblSubNo);
            this.Controls.Add(this.lblMainNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOpenning";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配線チェッカー V2 ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOpenning_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmOpenning_FormClosed);
            this.Load += new System.EventHandler(this.frmOpenning_Load);
            ((System.ComponentModel.ISupportInitialize)(this.menuSubBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuMainBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetTopMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox_Filter;
        public System.Windows.Forms.Button button_ViewManual;
        public System.Windows.Forms.ToolTip ToolTip1;
        public System.Windows.Forms.ListBox listBox_SubNo;
        public System.Windows.Forms.ListBox listBox_MainNo;
        public System.Windows.Forms.Button button_frmSetting;
        public System.Windows.Forms.Button button_Exit;
        public System.Windows.Forms.Button button_frmTopEdit;
        public System.Windows.Forms.Button button_frmMain;
        public System.Windows.Forms.Label Label2;
        public System.Windows.Forms.Label Label1;
        public System.Windows.Forms.Label lblVersion;
        public System.Windows.Forms.Label Label3;
        public System.Windows.Forms.Label lblSubNo;
        public System.Windows.Forms.Label lblMainNo;
        private DataSetTopMenu DataSetTopMenu;
        private System.Windows.Forms.Timer timerInitalUpdate;
        public System.Windows.Forms.Button button_AnalogMonitor;
        public System.Windows.Forms.Button buttonHistory;
        private System.Windows.Forms.BindingSource menuSubBindingSource;
        private System.Windows.Forms.BindingSource menuMainBindingSource;
    }
}