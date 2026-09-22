namespace NippoControlSystem.UI.Views
{
    partial class MainView
    {
        //　------　MVVMパターン用にリファクタリングしたコード　------

        //　------　MVVM化のためにリファクタリングする前のコード　------

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
        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_Stop = new System.Windows.Forms.Button();
            this.button_Next = new System.Windows.Forms.Button();
            this.textBox_Zuban = new System.Windows.Forms.TextBox();
            this.bindingSourceCheckDat = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetItems = new NippoControlSystem.DataSetItems();
            this.lblGoTitle = new System.Windows.Forms.Label();
            this.lblSerialTitle = new System.Windows.Forms.Label();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.button_DataInput = new System.Windows.Forms.Button();
            this.button_Debug = new System.Windows.Forms.Button();
            this.textBox_GoNo = new System.Windows.Forms.TextBox();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnManual = new System.Windows.Forms.ToolStripMenuItem();
            this.button_ResultView = new System.Windows.Forms.Button();
            this.textBox_Serial = new System.Windows.Forms.TextBox();
            this.button_Close = new System.Windows.Forms.Button();
            this.textBox_Status = new System.Windows.Forms.TextBox();
            this.textBox_Edaban = new System.Windows.Forms.TextBox();
            this.textBox_Volt = new System.Windows.Forms.TextBox();
            this.textBox_Item = new System.Windows.Forms.TextBox();
            this.dataSetItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Frame1 = new System.Windows.Forms.GroupBox();
            this.label_ResultOK = new System.Windows.Forms.Label();
            this.label_ResultNG = new System.Windows.Forms.Label();
            this.lblMainNo = new System.Windows.Forms.Label();
            this.lblSubNo = new System.Windows.Forms.Label();
            this.lblItem = new System.Windows.Forms.Label();
            this._Label5_1 = new System.Windows.Forms.Label();
            this._Label5_0 = new System.Windows.Forms.Label();
            this._Label4_0 = new System.Windows.Forms.Label();
            this._Label3_1 = new System.Windows.Forms.Label();
            this.MainMenu1 = new System.Windows.Forms.MenuStrip();
            this.タイムアウト時間ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView_Inspection = new System.Windows.Forms.DataGridView();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inspectIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timerInspect = new System.Windows.Forms.Timer(this.components);
            this.timerDrawing = new System.Windows.Forms.Timer(this.components);
            this.switchLabelGreenSwitch = new Cyc.Forms.SwitchLabel();
            this.switchLabelRedSwitch = new Cyc.Forms.SwitchLabel();
            this.textBox_TestNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_CurrentResult = new System.Windows.Forms.TextBox();
            this.timerReadSw = new System.Windows.Forms.Timer(this.components);
            this.textBox_InspectType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelRemainTime = new System.Windows.Forms.Label();
            this.textBoxRemainTime = new System.Windows.Forms.TextBox();
            this.textBox_Guide = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCheckDat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetItemsBindingSource)).BeginInit();
            this.Frame1.SuspendLayout();
            this.MainMenu1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Inspection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Stop
            // 
            this.button_Stop.BackColor = System.Drawing.SystemColors.Control;
            this.button_Stop.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_Stop.Enabled = false;
            this.button_Stop.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Stop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Stop.Location = new System.Drawing.Point(200, 877);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_Stop.Size = new System.Drawing.Size(161, 57);
            this.button_Stop.TabIndex = 27;
            this.button_Stop.Text = "停止(&E)";
            this.button_Stop.UseVisualStyleBackColor = false;
            this.button_Stop.Click += new System.EventHandler(this.button_Stop_Click);
            // 
            // button_Next
            // 
            this.button_Next.BackColor = System.Drawing.SystemColors.Control;
            this.button_Next.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_Next.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Next.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Next.Location = new System.Drawing.Point(16, 877);
            this.button_Next.Name = "button_Next";
            this.button_Next.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_Next.Size = new System.Drawing.Size(153, 57);
            this.button_Next.TabIndex = 26;
            this.button_Next.Text = "開始/次へ(&G)";
            this.button_Next.UseVisualStyleBackColor = false;
            this.button_Next.Click += new System.EventHandler(this.button_Next_Click);
            // 
            // textBox_Zuban
            // 
            this.textBox_Zuban.AcceptsReturn = true;
            this.textBox_Zuban.BackColor = System.Drawing.Color.Black;
            this.textBox_Zuban.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Zuban.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "Title", true));
            this.textBox_Zuban.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Zuban.ForeColor = System.Drawing.Color.Cyan;
            this.textBox_Zuban.Location = new System.Drawing.Point(440, 52);
            this.textBox_Zuban.MaxLength = 0;
            this.textBox_Zuban.Name = "textBox_Zuban";
            this.textBox_Zuban.ReadOnly = true;
            this.textBox_Zuban.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Zuban.Size = new System.Drawing.Size(289, 39);
            this.textBox_Zuban.TabIndex = 33;
            this.textBox_Zuban.TabStop = false;
            this.textBox_Zuban.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bindingSourceCheckDat
            // 
            this.bindingSourceCheckDat.DataMember = "CheckDat";
            this.bindingSourceCheckDat.DataSource = this.dataSetItems;
            // 
            // dataSetItems
            // 
            this.dataSetItems.DataSetName = "DataSetItems";
            this.dataSetItems.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lblGoTitle
            // 
            this.lblGoTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblGoTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblGoTitle.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblGoTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblGoTitle.Location = new System.Drawing.Point(904, 476);
            this.lblGoTitle.Name = "lblGoTitle";
            this.lblGoTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblGoTitle.Size = new System.Drawing.Size(185, 25);
            this.lblGoTitle.TabIndex = 49;
            this.lblGoTitle.Text = "Goki";
            // 
            // lblSerialTitle
            // 
            this.lblSerialTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblSerialTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblSerialTitle.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSerialTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSerialTitle.Location = new System.Drawing.Point(904, 412);
            this.lblSerialTitle.Name = "lblSerialTitle";
            this.lblSerialTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSerialTitle.Size = new System.Drawing.Size(185, 25);
            this.lblSerialTitle.TabIndex = 47;
            this.lblSerialTitle.Text = "Serial";
            // 
            // mnuEdit
            // 
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(156, 22);
            this.mnuEdit.Text = "検査定義の編集";
            this.mnuEdit.Click += new System.EventHandler(this.mnuEdit_Click);
            // 
            // mnuStop
            // 
            this.mnuStop.Enabled = false;
            this.mnuStop.Name = "mnuStop";
            this.mnuStop.Size = new System.Drawing.Size(156, 22);
            this.mnuStop.Text = "停止";
            this.mnuStop.Click += new System.EventHandler(this.mnuStop_Click);
            // 
            // mnuStart
            // 
            this.mnuStart.Name = "mnuStart";
            this.mnuStart.Size = new System.Drawing.Size(156, 22);
            this.mnuStart.Text = "開始/次へ";
            this.mnuStart.Click += new System.EventHandler(this.mnuStart_Click);
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPrint,
            this.mnuEnd});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(67, 20);
            this.mnuFile.Text = "ファイル(&F)";
            // 
            // mnuPrint
            // 
            this.mnuPrint.Name = "mnuPrint";
            this.mnuPrint.Size = new System.Drawing.Size(119, 22);
            this.mnuPrint.Text = "印刷(&P)";
            this.mnuPrint.Click += new System.EventHandler(this.mnuPrint_Click);
            // 
            // mnuEnd
            // 
            this.mnuEnd.Name = "mnuEnd";
            this.mnuEnd.Size = new System.Drawing.Size(119, 22);
            this.mnuEnd.Text = "閉じる(&X)";
            this.mnuEnd.Click += new System.EventHandler(this.mnuEnd_Click);
            // 
            // mnuCheck
            // 
            this.mnuCheck.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStart,
            this.mnuStop,
            this.mnuEdit,
            this.mnuView});
            this.mnuCheck.Name = "mnuCheck";
            this.mnuCheck.Size = new System.Drawing.Size(58, 20);
            this.mnuCheck.Text = "検査(&C)";
            // 
            // mnuView
            // 
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(156, 22);
            this.mnuView.Text = "詳細表示";
            this.mnuView.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // button_DataInput
            // 
            this.button_DataInput.BackColor = System.Drawing.SystemColors.Control;
            this.button_DataInput.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_DataInput.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_DataInput.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_DataInput.Location = new System.Drawing.Point(400, 877);
            this.button_DataInput.Name = "button_DataInput";
            this.button_DataInput.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_DataInput.Size = new System.Drawing.Size(161, 57);
            this.button_DataInput.TabIndex = 28;
            this.button_DataInput.Text = "検査定義の編集(&I)";
            this.button_DataInput.UseVisualStyleBackColor = false;
            this.button_DataInput.Click += new System.EventHandler(this.button_DataInput_Click);
            // 
            // button_Debug
            // 
            this.button_Debug.BackColor = System.Drawing.SystemColors.Control;
            this.button_Debug.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_Debug.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Debug.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Debug.Location = new System.Drawing.Point(784, 877);
            this.button_Debug.Name = "button_Debug";
            this.button_Debug.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_Debug.Size = new System.Drawing.Size(161, 57);
            this.button_Debug.TabIndex = 30;
            this.button_Debug.Text = "デバッグ検査(&D)";
            this.button_Debug.UseVisualStyleBackColor = false;
            this.button_Debug.Click += new System.EventHandler(this.button_Debug_Click);
            // 
            // textBox_GoNo
            // 
            this.textBox_GoNo.AcceptsReturn = true;
            this.textBox_GoNo.BackColor = System.Drawing.Color.Black;
            this.textBox_GoNo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_GoNo.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_GoNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.textBox_GoNo.Location = new System.Drawing.Point(904, 500);
            this.textBox_GoNo.MaxLength = 0;
            this.textBox_GoNo.Name = "textBox_GoNo";
            this.textBox_GoNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_GoNo.Size = new System.Drawing.Size(273, 39);
            this.textBox_GoNo.TabIndex = 48;
            this.textBox_GoNo.TabStop = false;
            this.textBox_GoNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout,
            this.mnManual});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(65, 20);
            this.mnuHelp.Text = "ヘルプ(&H)";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(160, 22);
            this.mnuAbout.Text = "About(&a)";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // mnManual
            // 
            this.mnManual.Name = "mnManual";
            this.mnManual.Size = new System.Drawing.Size(160, 22);
            this.mnManual.Text = "操作マニュアル(&H)";
            this.mnManual.Click += new System.EventHandler(this.mnManual_Click);
            // 
            // button_ResultView
            // 
            this.button_ResultView.BackColor = System.Drawing.SystemColors.Control;
            this.button_ResultView.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_ResultView.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_ResultView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_ResultView.Location = new System.Drawing.Point(592, 877);
            this.button_ResultView.Name = "button_ResultView";
            this.button_ResultView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_ResultView.Size = new System.Drawing.Size(161, 57);
            this.button_ResultView.TabIndex = 29;
            this.button_ResultView.Text = "詳細表示(&V)";
            this.button_ResultView.UseVisualStyleBackColor = false;
            this.button_ResultView.Click += new System.EventHandler(this.button_ResultView_Click);
            // 
            // textBox_Serial
            // 
            this.textBox_Serial.AcceptsReturn = true;
            this.textBox_Serial.BackColor = System.Drawing.Color.Black;
            this.textBox_Serial.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Serial.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Serial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.textBox_Serial.Location = new System.Drawing.Point(904, 436);
            this.textBox_Serial.MaxLength = 0;
            this.textBox_Serial.Name = "textBox_Serial";
            this.textBox_Serial.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Serial.Size = new System.Drawing.Size(273, 39);
            this.textBox_Serial.TabIndex = 35;
            this.textBox_Serial.TabStop = false;
            this.textBox_Serial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_Close
            // 
            this.button_Close.BackColor = System.Drawing.SystemColors.Control;
            this.button_Close.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_Close.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Close.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Close.Location = new System.Drawing.Point(992, 877);
            this.button_Close.Name = "button_Close";
            this.button_Close.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_Close.Size = new System.Drawing.Size(185, 57);
            this.button_Close.TabIndex = 31;
            this.button_Close.Text = "保存/閉じる(&Q)";
            this.button_Close.UseVisualStyleBackColor = false;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // textBox_Status
            // 
            this.textBox_Status.AcceptsReturn = true;
            this.textBox_Status.BackColor = System.Drawing.Color.Black;
            this.textBox_Status.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Status.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Status.ForeColor = System.Drawing.Color.Cyan;
            this.textBox_Status.Location = new System.Drawing.Point(992, 52);
            this.textBox_Status.MaxLength = 0;
            this.textBox_Status.Name = "textBox_Status";
            this.textBox_Status.ReadOnly = true;
            this.textBox_Status.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Status.Size = new System.Drawing.Size(185, 39);
            this.textBox_Status.TabIndex = 38;
            this.textBox_Status.TabStop = false;
            this.textBox_Status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Edaban
            // 
            this.textBox_Edaban.AcceptsReturn = true;
            this.textBox_Edaban.BackColor = System.Drawing.Color.Black;
            this.textBox_Edaban.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Edaban.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "SubTitle", true));
            this.textBox_Edaban.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Edaban.ForeColor = System.Drawing.Color.Cyan;
            this.textBox_Edaban.Location = new System.Drawing.Point(736, 52);
            this.textBox_Edaban.MaxLength = 0;
            this.textBox_Edaban.Name = "textBox_Edaban";
            this.textBox_Edaban.ReadOnly = true;
            this.textBox_Edaban.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Edaban.Size = new System.Drawing.Size(129, 39);
            this.textBox_Edaban.TabIndex = 34;
            this.textBox_Edaban.TabStop = false;
            this.textBox_Edaban.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Volt
            // 
            this.textBox_Volt.AcceptsReturn = true;
            this.textBox_Volt.BackColor = System.Drawing.Color.Black;
            this.textBox_Volt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Volt.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Volt.ForeColor = System.Drawing.Color.Cyan;
            this.textBox_Volt.Location = new System.Drawing.Point(872, 52);
            this.textBox_Volt.MaxLength = 0;
            this.textBox_Volt.Name = "textBox_Volt";
            this.textBox_Volt.ReadOnly = true;
            this.textBox_Volt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Volt.Size = new System.Drawing.Size(113, 39);
            this.textBox_Volt.TabIndex = 37;
            this.textBox_Volt.TabStop = false;
            this.textBox_Volt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Item
            // 
            this.textBox_Item.AcceptsReturn = true;
            this.textBox_Item.BackColor = System.Drawing.Color.Black;
            this.textBox_Item.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Item.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "Item", true));
            this.textBox_Item.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Item.ForeColor = System.Drawing.Color.Cyan;
            this.textBox_Item.Location = new System.Drawing.Point(16, 52);
            this.textBox_Item.MaxLength = 0;
            this.textBox_Item.Name = "textBox_Item";
            this.textBox_Item.ReadOnly = true;
            this.textBox_Item.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Item.Size = new System.Drawing.Size(424, 39);
            this.textBox_Item.TabIndex = 32;
            this.textBox_Item.TabStop = false;
            this.textBox_Item.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dataSetItemsBindingSource
            // 
            this.dataSetItemsBindingSource.DataMember = "ListDat";
            this.dataSetItemsBindingSource.DataSource = this.dataSetItems;
            // 
            // Frame1
            // 
            this.Frame1.BackColor = System.Drawing.SystemColors.Control;
            this.Frame1.Controls.Add(this.label_ResultOK);
            this.Frame1.Controls.Add(this.label_ResultNG);
            this.Frame1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Frame1.Location = new System.Drawing.Point(904, 124);
            this.Frame1.Name = "Frame1";
            this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame1.Size = new System.Drawing.Size(273, 273);
            this.Frame1.TabIndex = 39;
            this.Frame1.TabStop = false;
            this.Frame1.Text = "結果";
            // 
            // label_ResultOK
            // 
            this.label_ResultOK.BackColor = System.Drawing.SystemColors.Control;
            this.label_ResultOK.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_ResultOK.ForeColor = System.Drawing.Color.Black;
            this.label_ResultOK.Image = global::NippoControlSystem.UI.Properties.Resources.OK;
            this.label_ResultOK.Location = new System.Drawing.Point(6, 20);
            this.label_ResultOK.Name = "label_ResultOK";
            this.label_ResultOK.Size = new System.Drawing.Size(261, 248);
            this.label_ResultOK.TabIndex = 0;
            // 
            // label_ResultNG
            // 
            this.label_ResultNG.BackColor = System.Drawing.SystemColors.Control;
            this.label_ResultNG.ForeColor = System.Drawing.Color.Black;
            this.label_ResultNG.Image = global::NippoControlSystem.UI.Properties.Resources.NG;
            this.label_ResultNG.Location = new System.Drawing.Point(6, 20);
            this.label_ResultNG.Name = "label_ResultNG";
            this.label_ResultNG.Size = new System.Drawing.Size(261, 248);
            this.label_ResultNG.TabIndex = 1;
            // 
            // lblMainNo
            // 
            this.lblMainNo.BackColor = System.Drawing.SystemColors.Control;
            this.lblMainNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblMainNo.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMainNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMainNo.Location = new System.Drawing.Point(440, 28);
            this.lblMainNo.Name = "lblMainNo";
            this.lblMainNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblMainNo.Size = new System.Drawing.Size(217, 25);
            this.lblMainNo.TabIndex = 45;
            this.lblMainNo.Text = "Zuban";
            // 
            // lblSubNo
            // 
            this.lblSubNo.BackColor = System.Drawing.SystemColors.Control;
            this.lblSubNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblSubNo.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSubNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSubNo.Location = new System.Drawing.Point(736, 28);
            this.lblSubNo.Name = "lblSubNo";
            this.lblSubNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSubNo.Size = new System.Drawing.Size(73, 25);
            this.lblSubNo.TabIndex = 46;
            this.lblSubNo.Text = "Edaban";
            // 
            // lblItem
            // 
            this.lblItem.BackColor = System.Drawing.SystemColors.Control;
            this.lblItem.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblItem.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblItem.Location = new System.Drawing.Point(16, 28);
            this.lblItem.Name = "lblItem";
            this.lblItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblItem.Size = new System.Drawing.Size(73, 25);
            this.lblItem.TabIndex = 44;
            this.lblItem.Text = "Hinmei";
            // 
            // _Label5_1
            // 
            this._Label5_1.BackColor = System.Drawing.SystemColors.Control;
            this._Label5_1.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label5_1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label5_1.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Label5_1.Location = new System.Drawing.Point(992, 28);
            this._Label5_1.Name = "_Label5_1";
            this._Label5_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label5_1.Size = new System.Drawing.Size(65, 25);
            this._Label5_1.TabIndex = 43;
            this._Label5_1.Text = "状態";
            // 
            // _Label5_0
            // 
            this._Label5_0.BackColor = System.Drawing.SystemColors.Control;
            this._Label5_0.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label5_0.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label5_0.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Label5_0.Location = new System.Drawing.Point(872, 28);
            this._Label5_0.Name = "_Label5_0";
            this._Label5_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label5_0.Size = new System.Drawing.Size(65, 25);
            this._Label5_0.TabIndex = 42;
            this._Label5_0.Text = "電圧";
            // 
            // _Label4_0
            // 
            this._Label4_0.BackColor = System.Drawing.SystemColors.Control;
            this._Label4_0.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label4_0.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label4_0.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Label4_0.Location = new System.Drawing.Point(16, 108);
            this._Label4_0.Name = "_Label4_0";
            this._Label4_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label4_0.Size = new System.Drawing.Size(345, 25);
            this._Label4_0.TabIndex = 41;
            this._Label4_0.Text = "検査内容";
            // 
            // _Label3_1
            // 
            this._Label3_1.BackColor = System.Drawing.SystemColors.Control;
            this._Label3_1.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label3_1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._Label3_1.ForeColor = System.Drawing.SystemColors.ControlText;
            this._Label3_1.Location = new System.Drawing.Point(16, 555);
            this._Label3_1.Name = "_Label3_1";
            this._Label3_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label3_1.Size = new System.Drawing.Size(105, 25);
            this._Label3_1.TabIndex = 40;
            this._Label3_1.Text = "操作ガイド";
            // 
            // MainMenu1
            // 
            this.MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuCheck,
            this.タイムアウト時間ToolStripMenuItem,
            this.mnuHelp});
            this.MainMenu1.Location = new System.Drawing.Point(0, 0);
            this.MainMenu1.Name = "MainMenu1";
            this.MainMenu1.Size = new System.Drawing.Size(1196, 24);
            this.MainMenu1.TabIndex = 50;
            // 
            // タイムアウト時間ToolStripMenuItem
            // 
            this.タイムアウト時間ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem1,
            this.ToolStripMenuItem2,
            this.ToolStripMenuItem3,
            this.ToolStripMenuItem4,
            this.ToolStripMenuItem5});
            this.タイムアウト時間ToolStripMenuItem.Name = "タイムアウト時間ToolStripMenuItem";
            this.タイムアウト時間ToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.タイムアウト時間ToolStripMenuItem.Text = "タイムアウト 1分";
            this.タイムアウト時間ToolStripMenuItem.DropDownOpening += new System.EventHandler(this.タイムアウト時間ToolStripMenuItem_DropDownOpening);
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            this.ToolStripMenuItem1.Text = "１分";
            this.ToolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // ToolStripMenuItem2
            // 
            this.ToolStripMenuItem2.Name = "ToolStripMenuItem2";
            this.ToolStripMenuItem2.Size = new System.Drawing.Size(98, 22);
            this.ToolStripMenuItem2.Text = "２分";
            this.ToolStripMenuItem2.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // ToolStripMenuItem3
            // 
            this.ToolStripMenuItem3.Name = "ToolStripMenuItem3";
            this.ToolStripMenuItem3.Size = new System.Drawing.Size(98, 22);
            this.ToolStripMenuItem3.Text = "３分";
            this.ToolStripMenuItem3.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // ToolStripMenuItem4
            // 
            this.ToolStripMenuItem4.Name = "ToolStripMenuItem4";
            this.ToolStripMenuItem4.Size = new System.Drawing.Size(98, 22);
            this.ToolStripMenuItem4.Text = "４分";
            this.ToolStripMenuItem4.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // ToolStripMenuItem5
            // 
            this.ToolStripMenuItem5.Name = "ToolStripMenuItem5";
            this.ToolStripMenuItem5.Size = new System.Drawing.Size(98, 22);
            this.ToolStripMenuItem5.Text = "５分";
            this.ToolStripMenuItem5.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // dataGridView_Inspection
            // 
            this.dataGridView_Inspection.AllowUserToAddRows = false;
            this.dataGridView_Inspection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Inspection.AutoGenerateColumns = false;
            this.dataGridView_Inspection.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView_Inspection.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Inspection.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_Inspection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Inspection.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.titleDataGridViewTextBoxColumn,
            this.resultDataGridViewTextBoxColumn,
            this.inspectIDDataGridViewTextBoxColumn});
            this.dataGridView_Inspection.DataSource = this.dataSetItemsBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Cyan;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Inspection.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_Inspection.Location = new System.Drawing.Point(16, 131);
            this.dataGridView_Inspection.Name = "dataGridView_Inspection";
            this.dataGridView_Inspection.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Inspection.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_Inspection.RowHeadersWidth = 60;
            this.dataGridView_Inspection.RowTemplate.Height = 27;
            this.dataGridView_Inspection.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Inspection.Size = new System.Drawing.Size(870, 407);
            this.dataGridView_Inspection.TabIndex = 51;
            this.dataGridView_Inspection.TabStop = false;
            this.dataGridView_Inspection.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Inspection_CellDoubleClick);
            this.dataGridView_Inspection.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_Inspection_CellFormatting);
            this.dataGridView_Inspection.CurrentCellChanged += new System.EventHandler(this.dataGridView_Inspection_CurrentCellChanged);
            this.dataGridView_Inspection.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dvResults_RowPostPaint);
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            this.titleDataGridViewTextBoxColumn.HeaderText = "検査タイトル";
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            this.titleDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.titleDataGridViewTextBoxColumn.Width = 590;
            // 
            // resultDataGridViewTextBoxColumn
            // 
            this.resultDataGridViewTextBoxColumn.DataPropertyName = "Result";
            this.resultDataGridViewTextBoxColumn.HeaderText = "結果";
            this.resultDataGridViewTextBoxColumn.Name = "resultDataGridViewTextBoxColumn";
            this.resultDataGridViewTextBoxColumn.Width = 202;
            // 
            // inspectIDDataGridViewTextBoxColumn
            // 
            this.inspectIDDataGridViewTextBoxColumn.DataPropertyName = "InspectID";
            this.inspectIDDataGridViewTextBoxColumn.HeaderText = "InspectID";
            this.inspectIDDataGridViewTextBoxColumn.Name = "inspectIDDataGridViewTextBoxColumn";
            this.inspectIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(485, 97);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 28);
            this.button1.TabIndex = 52;
            this.button1.TabStop = false;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(567, 97);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 28);
            this.button2.TabIndex = 53;
            this.button2.TabStop = false;
            this.button2.Text = "-";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timerInspect
            // 
            this.timerInspect.Interval = 50;
            this.timerInspect.Tick += new System.EventHandler(this.timerInspect_Tick);
            // 
            // timerDrawing
            // 
            this.timerDrawing.Interval = 50;
            this.timerDrawing.Tick += new System.EventHandler(this.timerDrawing_Tick);
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
            this.switchLabelGreenSwitch.Location = new System.Drawing.Point(1081, 543);
            this.switchLabelGreenSwitch.Name = "switchLabelGreenSwitch";
            this.switchLabelGreenSwitch.Size = new System.Drawing.Size(66, 32);
            this.switchLabelGreenSwitch.TabIndex = 420;
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
            this.switchLabelRedSwitch.Location = new System.Drawing.Point(1005, 543);
            this.switchLabelRedSwitch.Name = "switchLabelRedSwitch";
            this.switchLabelRedSwitch.Size = new System.Drawing.Size(66, 32);
            this.switchLabelRedSwitch.TabIndex = 419;
            this.switchLabelRedSwitch.Text = "Red\r\nSwitch";
            this.switchLabelRedSwitch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.switchLabelRedSwitch.Visible = false;
            this.switchLabelRedSwitch.Leave += new System.EventHandler(this.switchLabelRedSwitch_Leave);
            this.switchLabelRedSwitch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.switchLabelRedSwitch_MouseDown);
            this.switchLabelRedSwitch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.switchLabelRedSwitch_MouseUp);
            // 
            // textBox_TestNo
            // 
            this.textBox_TestNo.AcceptsReturn = true;
            this.textBox_TestNo.BackColor = System.Drawing.Color.Black;
            this.textBox_TestNo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_TestNo.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_TestNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.textBox_TestNo.Location = new System.Drawing.Point(200, 548);
            this.textBox_TestNo.MaxLength = 0;
            this.textBox_TestNo.Name = "textBox_TestNo";
            this.textBox_TestNo.ReadOnly = true;
            this.textBox_TestNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_TestNo.Size = new System.Drawing.Size(78, 36);
            this.textBox_TestNo.TabIndex = 421;
            this.textBox_TestNo.TabStop = false;
            this.textBox_TestNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_TestNo.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(161, 555);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(33, 21);
            this.label1.TabIndex = 422;
            this.label1.Text = "T#";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(906, 550);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(93, 21);
            this.label2.TabIndex = 423;
            this.label2.Text = "操作BOX";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(535, 555);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(94, 21);
            this.label3.TabIndex = 424;
            this.label3.Text = "現在状態";
            this.label3.Visible = false;
            // 
            // textBox_CurrentResult
            // 
            this.textBox_CurrentResult.AcceptsReturn = true;
            this.textBox_CurrentResult.BackColor = System.Drawing.Color.Black;
            this.textBox_CurrentResult.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_CurrentResult.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_CurrentResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.textBox_CurrentResult.Location = new System.Drawing.Point(635, 548);
            this.textBox_CurrentResult.MaxLength = 0;
            this.textBox_CurrentResult.Name = "textBox_CurrentResult";
            this.textBox_CurrentResult.ReadOnly = true;
            this.textBox_CurrentResult.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_CurrentResult.Size = new System.Drawing.Size(78, 36);
            this.textBox_CurrentResult.TabIndex = 425;
            this.textBox_CurrentResult.TabStop = false;
            this.textBox_CurrentResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_CurrentResult.Visible = false;
            // 
            // timerReadSw
            // 
            this.timerReadSw.Interval = 10;
            this.timerReadSw.Tick += new System.EventHandler(this.timerReadSw_Tick);
            // 
            // textBox_InspectType
            // 
            this.textBox_InspectType.AcceptsReturn = true;
            this.textBox_InspectType.BackColor = System.Drawing.Color.Black;
            this.textBox_InspectType.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_InspectType.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_InspectType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.textBox_InspectType.Location = new System.Drawing.Point(365, 548);
            this.textBox_InspectType.MaxLength = 0;
            this.textBox_InspectType.Name = "textBox_InspectType";
            this.textBox_InspectType.ReadOnly = true;
            this.textBox_InspectType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_InspectType.Size = new System.Drawing.Size(135, 36);
            this.textBox_InspectType.TabIndex = 427;
            this.textBox_InspectType.TabStop = false;
            this.textBox_InspectType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_InspectType.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(300, 555);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(60, 21);
            this.label4.TabIndex = 426;
            this.label4.Text = "タイプ";
            this.label4.Visible = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackgroundImage = global::NippoControlSystem.UI.Properties.Resources.panel00;
            this.pictureBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox8.ErrorImage = null;
            this.pictureBox8.InitialImage = null;
            this.pictureBox8.Location = new System.Drawing.Point(1037, 819);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(140, 55);
            this.pictureBox8.TabIndex = 69;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackgroundImage = global::NippoControlSystem.UI.Properties.Resources.panel00;
            this.pictureBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox7.ErrorImage = null;
            this.pictureBox7.InitialImage = null;
            this.pictureBox7.Location = new System.Drawing.Point(891, 819);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(140, 55);
            this.pictureBox7.TabIndex = 68;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackgroundImage = global::NippoControlSystem.UI.Properties.Resources.panel00;
            this.pictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox6.ErrorImage = null;
            this.pictureBox6.InitialImage = null;
            this.pictureBox6.Location = new System.Drawing.Point(746, 819);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(140, 55);
            this.pictureBox6.TabIndex = 67;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackgroundImage = global::NippoControlSystem.UI.Properties.Resources.panel00;
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox5.ErrorImage = null;
            this.pictureBox5.InitialImage = null;
            this.pictureBox5.Location = new System.Drawing.Point(600, 819);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(140, 55);
            this.pictureBox5.TabIndex = 66;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = global::NippoControlSystem.UI.Properties.Resources.panel00;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.ErrorImage = null;
            this.pictureBox4.InitialImage = null;
            this.pictureBox4.Location = new System.Drawing.Point(454, 819);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(140, 55);
            this.pictureBox4.TabIndex = 65;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::NippoControlSystem.UI.Properties.Resources.panel00;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.ErrorImage = null;
            this.pictureBox3.InitialImage = null;
            this.pictureBox3.Location = new System.Drawing.Point(308, 819);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(140, 55);
            this.pictureBox3.TabIndex = 64;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::NippoControlSystem.UI.Properties.Resources.panel00;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.ErrorImage = null;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(162, 819);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(140, 55);
            this.pictureBox2.TabIndex = 63;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::NippoControlSystem.UI.Properties.Resources.panel00;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(16, 819);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(140, 55);
            this.pictureBox1.TabIndex = 62;
            this.pictureBox1.TabStop = false;
            // 
            // labelRemainTime
            // 
            this.labelRemainTime.AutoSize = true;
            this.labelRemainTime.BackColor = System.Drawing.SystemColors.Control;
            this.labelRemainTime.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelRemainTime.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelRemainTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelRemainTime.Location = new System.Drawing.Point(716, 552);
            this.labelRemainTime.Name = "labelRemainTime";
            this.labelRemainTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelRemainTime.Size = new System.Drawing.Size(122, 21);
            this.labelRemainTime.TabIndex = 428;
            this.labelRemainTime.Text = "残り時間(秒)";
            this.labelRemainTime.Visible = false;
            // 
            // textBoxRemainTime
            // 
            this.textBoxRemainTime.AcceptsReturn = true;
            this.textBoxRemainTime.BackColor = System.Drawing.Color.White;
            this.textBoxRemainTime.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxRemainTime.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxRemainTime.ForeColor = System.Drawing.Color.Black;
            this.textBoxRemainTime.Location = new System.Drawing.Point(840, 544);
            this.textBoxRemainTime.MaxLength = 0;
            this.textBoxRemainTime.Name = "textBoxRemainTime";
            this.textBoxRemainTime.ReadOnly = true;
            this.textBoxRemainTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxRemainTime.Size = new System.Drawing.Size(78, 34);
            this.textBoxRemainTime.TabIndex = 429;
            this.textBoxRemainTime.TabStop = false;
            this.textBoxRemainTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxRemainTime.Visible = false;
            // 
            // textBox_Guide
            // 
            this.textBox_Guide.BackColor = System.Drawing.Color.Black;
            this.textBox_Guide.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 33.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Guide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.textBox_Guide.Location = new System.Drawing.Point(16, 586);
            this.textBox_Guide.Name = "textBox_Guide";
            this.textBox_Guide.Size = new System.Drawing.Size(1161, 231);
            this.textBox_Guide.TabIndex = 430;
            this.textBox_Guide.Text = "チェッカーのケーブル途中にあるLEDが点灯していますか？\n\n　点灯(OK): 【開始/次へ】ボタン\n　消灯(NG): 【停止】 　　ボタン\n　点灯(OK): 【開" +
    "始/次へ】ボタン\n　消灯(NG): 【停止】 　　ボタン";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1196, 943);
            this.Controls.Add(this.textBox_Guide);
            this.Controls.Add(this.textBoxRemainTime);
            this.Controls.Add(this.labelRemainTime);
            this.Controls.Add(this.textBox_InspectType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_CurrentResult);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_TestNo);
            this.Controls.Add(this.switchLabelGreenSwitch);
            this.Controls.Add(this.switchLabelRedSwitch);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView_Inspection);
            this.Controls.Add(this.button_Stop);
            this.Controls.Add(this.button_Next);
            this.Controls.Add(this.textBox_Zuban);
            this.Controls.Add(this.lblGoTitle);
            this.Controls.Add(this.lblSerialTitle);
            this.Controls.Add(this.button_DataInput);
            this.Controls.Add(this.button_Debug);
            this.Controls.Add(this.textBox_GoNo);
            this.Controls.Add(this.button_ResultView);
            this.Controls.Add(this.textBox_Serial);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.textBox_Status);
            this.Controls.Add(this.textBox_Edaban);
            this.Controls.Add(this.textBox_Volt);
            this.Controls.Add(this.textBox_Item);
            this.Controls.Add(this.Frame1);
            this.Controls.Add(this.lblMainNo);
            this.Controls.Add(this.lblSubNo);
            this.Controls.Add(this.lblItem);
            this.Controls.Add(this._Label5_1);
            this.Controls.Add(this._Label5_0);
            this.Controls.Add(this._Label4_0);
            this.Controls.Add(this._Label3_1);
            this.Controls.Add(this.MainMenu1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配線チェッカー（検査画面）";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCheckDat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetItemsBindingSource)).EndInit();
            this.Frame1.ResumeLayout(false);
            this.MainMenu1.ResumeLayout(false);
            this.MainMenu1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Inspection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolTip ToolTip1;
        public System.Windows.Forms.Button button_Stop;
        public System.Windows.Forms.Button button_Next;
        public System.Windows.Forms.TextBox textBox_Zuban;
        public System.Windows.Forms.Label lblGoTitle;
        public System.Windows.Forms.Label lblSerialTitle;
        public System.Windows.Forms.ToolStripMenuItem mnuEdit;
        public System.Windows.Forms.ToolStripMenuItem mnuStop;
        public System.Windows.Forms.ToolStripMenuItem mnuStart;
        public System.Windows.Forms.ToolStripMenuItem mnuFile;
        public System.Windows.Forms.ToolStripMenuItem mnuEnd;
        public System.Windows.Forms.ToolStripMenuItem mnuCheck;
        public System.Windows.Forms.ToolStripMenuItem mnuView;
        public System.Windows.Forms.Button button_DataInput;
        public System.Windows.Forms.Button button_Debug;
        public System.Windows.Forms.TextBox textBox_GoNo;
        public System.Windows.Forms.ToolStripMenuItem mnuHelp;
        public System.Windows.Forms.ToolStripMenuItem mnuAbout;
        public System.Windows.Forms.ToolStripMenuItem mnManual;
        public System.Windows.Forms.Button button_ResultView;
        public System.Windows.Forms.TextBox textBox_Serial;
        public System.Windows.Forms.Button button_Close;
        public System.Windows.Forms.TextBox textBox_Status;
        public System.Windows.Forms.TextBox textBox_Edaban;
        public System.Windows.Forms.TextBox textBox_Volt;
        public System.Windows.Forms.TextBox textBox_Item;
        public System.Windows.Forms.GroupBox Frame1;
        public System.Windows.Forms.Label label_ResultOK;
        public System.Windows.Forms.Label label_ResultNG;
        public System.Windows.Forms.Label lblMainNo;
        public System.Windows.Forms.Label lblSubNo;
        public System.Windows.Forms.Label lblItem;
        public System.Windows.Forms.Label _Label5_1;
        public System.Windows.Forms.Label _Label5_0;
        public System.Windows.Forms.Label _Label4_0;
        public System.Windows.Forms.Label _Label3_1;
        public System.Windows.Forms.MenuStrip MainMenu1;
        private System.Windows.Forms.BindingSource bindingSourceCheckDat;
        private System.Windows.Forms.DataGridView dataGridView_Inspection;
        private System.Windows.Forms.BindingSource dataSetItemsBindingSource;
        private DataSetItems dataSetItems;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timerInspect;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Timer timerDrawing;
        private Cyc.Forms.SwitchLabel switchLabelGreenSwitch;
        private Cyc.Forms.SwitchLabel switchLabelRedSwitch;
        public System.Windows.Forms.TextBox textBox_TestNo;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textBox_CurrentResult;
        private System.Windows.Forms.Timer timerReadSw;
        public System.Windows.Forms.TextBox textBox_InspectType;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem mnuPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn inspectIDDataGridViewTextBoxColumn;
        public System.Windows.Forms.Label labelRemainTime;
        public System.Windows.Forms.TextBox textBoxRemainTime;
        private System.Windows.Forms.ToolStripMenuItem タイムアウト時間ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem5;
        private System.Windows.Forms.RichTextBox textBox_Guide;
    }
}