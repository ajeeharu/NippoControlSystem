namespace NippoControlSystem.UI.Views
{
    partial class TopEditView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopEditView));
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.mnuSaveClose = new System.Windows.Forms.ToolStripMenuItem();
            this.button_SaveClose = new System.Windows.Forms.Button();
            this.button_Edit = new System.Windows.Forms.Button();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.号機へ変換ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.変換iDhiDbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.変換iDbiDhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInput = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDown = new System.Windows.Forms.ToolStripMenuItem();
            this.Frame1 = new System.Windows.Forms.GroupBox();
            this.buttongetMenuFromFolder = new System.Windows.Forms.Button();
            this.radioButton_Edaban = new System.Windows.Forms.RadioButton();
            this.radioButton_Zuban = new System.Windows.Forms.RadioButton();
            this.button_Insert = new System.Windows.Forms.Button();
            this.button_Delete = new System.Windows.Forms.Button();
            this.button_Copy = new System.Windows.Forms.Button();
            this.button_MoveUp = new System.Windows.Forms.Button();
            this.button_MoveDown = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.MainMenu1 = new System.Windows.Forms.MenuStrip();
            this.dataGridView_Zuban = new System.Windows.Forms.DataGridView();
            this.ColumnTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMainID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuMainBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetTopMenu = new NippoControlSystem.DataSetTopMenu();
            this.dataGridView_Edaban = new System.Windows.Forms.DataGridView();
            this.ColumnSubTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSubmainID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSubID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuSubBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Frame1.SuspendLayout();
            this.MainMenu1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Zuban)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuMainBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetTopMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Edaban)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuSubBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuSaveClose
            // 
            this.mnuSaveClose.Name = "mnuSaveClose";
            this.mnuSaveClose.Size = new System.Drawing.Size(172, 22);
            this.mnuSaveClose.Text = "保存して閉じる(&X)";
            this.mnuSaveClose.Click += new System.EventHandler(this.mnuSaveClose_Click);
            // 
            // button_SaveClose
            // 
            this.button_SaveClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_SaveClose.BackColor = System.Drawing.SystemColors.Control;
            this.button_SaveClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_SaveClose.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_SaveClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_SaveClose.Location = new System.Drawing.Point(16, 471);
            this.button_SaveClose.Name = "button_SaveClose";
            this.button_SaveClose.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_SaveClose.Size = new System.Drawing.Size(129, 41);
            this.button_SaveClose.TabIndex = 15;
            this.button_SaveClose.Text = "保存して閉じる(&Q)";
            this.button_SaveClose.UseVisualStyleBackColor = false;
            this.button_SaveClose.Click += new System.EventHandler(this.button_SaveClose_Click);
            // 
            // button_Edit
            // 
            this.button_Edit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Edit.BackColor = System.Drawing.SystemColors.Control;
            this.button_Edit.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_Edit.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Edit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Edit.Location = new System.Drawing.Point(16, 415);
            this.button_Edit.Name = "button_Edit";
            this.button_Edit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_Edit.Size = new System.Drawing.Size(129, 41);
            this.button_Edit.TabIndex = 14;
            this.button_Edit.Text = "編集(&I)";
            this.button_Edit.UseVisualStyleBackColor = false;
            this.button_Edit.Click += new System.EventHandler(this.button_Edit_Click);
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSaveClose,
            this.号機へ変換ToolStripMenuItem,
            this.変換iDhiDbToolStripMenuItem,
            this.変換iDbiDhToolStripMenuItem});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(78, 20);
            this.mnuFile.Text = "ファイル(&F)";
            // 
            // 号機へ変換ToolStripMenuItem
            // 
            this.号機へ変換ToolStripMenuItem.Name = "号機へ変換ToolStripMenuItem";
            this.号機へ変換ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.号機へ変換ToolStripMenuItem.Text = "1号機へ変換";
            this.号機へ変換ToolStripMenuItem.Click += new System.EventHandler(this.mnuSaveOldVer_Click);
            // 
            // 変換iDhiDbToolStripMenuItem
            // 
            this.変換iDhiDbToolStripMenuItem.Name = "変換iDhiDbToolStripMenuItem";
            this.変換iDhiDbToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.変換iDhiDbToolStripMenuItem.Text = "変換iDh→iDb";
            this.変換iDhiDbToolStripMenuItem.Click += new System.EventHandler(this.変換iDhiDbToolStripMenuItem_Click);
            // 
            // 変換iDbiDhToolStripMenuItem
            // 
            this.変換iDbiDhToolStripMenuItem.Name = "変換iDbiDhToolStripMenuItem";
            this.変換iDbiDhToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.変換iDbiDhToolStripMenuItem.Text = "変換iDb→iDh";
            this.変換iDbiDhToolStripMenuItem.Click += new System.EventHandler(this.変換iDhiDbToolStripMenuItem_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInput,
            this.mnuAdd,
            this.mnuDel,
            this.mnuCopy,
            this.mnuUp,
            this.mnuDown});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(60, 20);
            this.mnuEdit.Text = "編集(&E)";
            // 
            // mnuInput
            // 
            this.mnuInput.Name = "mnuInput";
            this.mnuInput.Size = new System.Drawing.Size(141, 22);
            this.mnuInput.Text = "編集(&E)";
            this.mnuInput.Click += new System.EventHandler(this.mnuInput_Click);
            // 
            // mnuAdd
            // 
            this.mnuAdd.Name = "mnuAdd";
            this.mnuAdd.Size = new System.Drawing.Size(141, 22);
            this.mnuAdd.Text = "挿入(&I)";
            this.mnuAdd.Click += new System.EventHandler(this.mnuAdd_Click);
            // 
            // mnuDel
            // 
            this.mnuDel.Name = "mnuDel";
            this.mnuDel.Size = new System.Drawing.Size(141, 22);
            this.mnuDel.Text = "削除(&D)";
            this.mnuDel.Click += new System.EventHandler(this.mnuDel_Click);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.Size = new System.Drawing.Size(141, 22);
            this.mnuCopy.Text = "複製(&C)";
            this.mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
            // 
            // mnuUp
            // 
            this.mnuUp.Name = "mnuUp";
            this.mnuUp.Size = new System.Drawing.Size(141, 22);
            this.mnuUp.Text = "上に移動(&U)";
            this.mnuUp.Click += new System.EventHandler(this.mnuUp_Click);
            // 
            // mnuDown
            // 
            this.mnuDown.Name = "mnuDown";
            this.mnuDown.Size = new System.Drawing.Size(141, 22);
            this.mnuDown.Text = "下に移動(&L)";
            this.mnuDown.Click += new System.EventHandler(this.mnuDown_Click);
            // 
            // Frame1
            // 
            this.Frame1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Frame1.BackColor = System.Drawing.SystemColors.Control;
            this.Frame1.Controls.Add(this.buttongetMenuFromFolder);
            this.Frame1.Controls.Add(this.radioButton_Edaban);
            this.Frame1.Controls.Add(this.radioButton_Zuban);
            this.Frame1.Controls.Add(this.button_Insert);
            this.Frame1.Controls.Add(this.button_Delete);
            this.Frame1.Controls.Add(this.button_Copy);
            this.Frame1.Controls.Add(this.button_MoveUp);
            this.Frame1.Controls.Add(this.button_MoveDown);
            this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Frame1.Location = new System.Drawing.Point(176, 407);
            this.Frame1.Name = "Frame1";
            this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame1.Size = new System.Drawing.Size(617, 113);
            this.Frame1.TabIndex = 16;
            this.Frame1.TabStop = false;
            this.Frame1.Text = "ツールBOX";
            // 
            // buttongetMenuFromFolder
            // 
            this.buttongetMenuFromFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttongetMenuFromFolder.BackColor = System.Drawing.SystemColors.Control;
            this.buttongetMenuFromFolder.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttongetMenuFromFolder.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttongetMenuFromFolder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttongetMenuFromFolder.Location = new System.Drawing.Point(376, 18);
            this.buttongetMenuFromFolder.Name = "buttongetMenuFromFolder";
            this.buttongetMenuFromFolder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttongetMenuFromFolder.Size = new System.Drawing.Size(225, 25);
            this.buttongetMenuFromFolder.TabIndex = 21;
            this.buttongetMenuFromFolder.Text = "フォルダから新規検索";
            this.buttongetMenuFromFolder.UseVisualStyleBackColor = false;
            this.buttongetMenuFromFolder.Visible = false;
            this.buttongetMenuFromFolder.Click += new System.EventHandler(this.buttongetMenuFromFolder_Click);
            // 
            // radioButton_Edaban
            // 
            this.radioButton_Edaban.BackColor = System.Drawing.SystemColors.Control;
            this.radioButton_Edaban.Checked = true;
            this.radioButton_Edaban.Cursor = System.Windows.Forms.Cursors.Default;
            this.radioButton_Edaban.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButton_Edaban.ForeColor = System.Drawing.Color.Red;
            this.radioButton_Edaban.Location = new System.Drawing.Point(136, 24);
            this.radioButton_Edaban.Name = "radioButton_Edaban";
            this.radioButton_Edaban.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radioButton_Edaban.Size = new System.Drawing.Size(217, 18);
            this.radioButton_Edaban.TabIndex = 5;
            this.radioButton_Edaban.TabStop = true;
            this.radioButton_Edaban.Text = "枝番とフォルダ";
            this.radioButton_Edaban.UseVisualStyleBackColor = false;
            this.radioButton_Edaban.CheckedChanged += new System.EventHandler(this.radioButton_Edaban_CheckedChanged);
            // 
            // radioButton_Zuban
            // 
            this.radioButton_Zuban.BackColor = System.Drawing.SystemColors.Control;
            this.radioButton_Zuban.Cursor = System.Windows.Forms.Cursors.Default;
            this.radioButton_Zuban.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButton_Zuban.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButton_Zuban.Location = new System.Drawing.Point(16, 24);
            this.radioButton_Zuban.Name = "radioButton_Zuban";
            this.radioButton_Zuban.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radioButton_Zuban.Size = new System.Drawing.Size(105, 18);
            this.radioButton_Zuban.TabIndex = 4;
            this.radioButton_Zuban.TabStop = true;
            this.radioButton_Zuban.Text = "図番";
            this.radioButton_Zuban.UseVisualStyleBackColor = false;
            this.radioButton_Zuban.CheckedChanged += new System.EventHandler(this.radioButton_Zuban_CheckedChanged);
            // 
            // button_Insert
            // 
            this.button_Insert.BackColor = System.Drawing.SystemColors.Control;
            this.button_Insert.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_Insert.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Insert.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Insert.Location = new System.Drawing.Point(16, 56);
            this.button_Insert.Name = "button_Insert";
            this.button_Insert.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_Insert.Size = new System.Drawing.Size(105, 41);
            this.button_Insert.TabIndex = 6;
            this.button_Insert.Text = "挿入(&A)";
            this.button_Insert.UseVisualStyleBackColor = false;
            this.button_Insert.Click += new System.EventHandler(this.button_Insert_Click);
            // 
            // button_Delete
            // 
            this.button_Delete.BackColor = System.Drawing.SystemColors.Control;
            this.button_Delete.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_Delete.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Delete.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Delete.Location = new System.Drawing.Point(136, 56);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_Delete.Size = new System.Drawing.Size(105, 41);
            this.button_Delete.TabIndex = 7;
            this.button_Delete.Text = "削除(&D)";
            this.button_Delete.UseVisualStyleBackColor = false;
            this.button_Delete.Click += new System.EventHandler(this.button_Delete_Click);
            // 
            // button_Copy
            // 
            this.button_Copy.BackColor = System.Drawing.SystemColors.Control;
            this.button_Copy.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_Copy.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Copy.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Copy.Location = new System.Drawing.Point(256, 56);
            this.button_Copy.Name = "button_Copy";
            this.button_Copy.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_Copy.Size = new System.Drawing.Size(105, 41);
            this.button_Copy.TabIndex = 8;
            this.button_Copy.Text = "複製(&C)";
            this.button_Copy.UseVisualStyleBackColor = false;
            this.button_Copy.Click += new System.EventHandler(this.button_Copy_Click);
            // 
            // button_MoveUp
            // 
            this.button_MoveUp.BackColor = System.Drawing.SystemColors.Control;
            this.button_MoveUp.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_MoveUp.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_MoveUp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_MoveUp.Location = new System.Drawing.Point(376, 56);
            this.button_MoveUp.Name = "button_MoveUp";
            this.button_MoveUp.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_MoveUp.Size = new System.Drawing.Size(105, 41);
            this.button_MoveUp.TabIndex = 9;
            this.button_MoveUp.Text = "上に移動(&U)";
            this.button_MoveUp.UseVisualStyleBackColor = false;
            this.button_MoveUp.Click += new System.EventHandler(this.button_MoveUp_Click);
            // 
            // button_MoveDown
            // 
            this.button_MoveDown.BackColor = System.Drawing.SystemColors.Control;
            this.button_MoveDown.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_MoveDown.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_MoveDown.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_MoveDown.Location = new System.Drawing.Point(496, 56);
            this.button_MoveDown.Name = "button_MoveDown";
            this.button_MoveDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_MoveDown.Size = new System.Drawing.Size(105, 41);
            this.button_MoveDown.TabIndex = 10;
            this.button_MoveDown.Text = "下に移動(&L)";
            this.button_MoveDown.UseVisualStyleBackColor = false;
            this.button_MoveDown.Click += new System.EventHandler(this.button_MoveDown_Click);
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label1.ForeColor = System.Drawing.Color.Red;
            this.Label1.Location = new System.Drawing.Point(16, 377);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(777, 17);
            this.Label1.TabIndex = 17;
            this.Label1.Text = "※ 各検査毎に別々のフォルダを作成し、指定して下さい。　フォルダが重複した場合、定義ファイルが書き換わることがあります。";
            // 
            // MainMenu1
            // 
            this.MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit});
            this.MainMenu1.Location = new System.Drawing.Point(0, 0);
            this.MainMenu1.Name = "MainMenu1";
            this.MainMenu1.Size = new System.Drawing.Size(807, 24);
            this.MainMenu1.TabIndex = 18;
            // 
            // dataGridView_Zuban
            // 
            this.dataGridView_Zuban.AllowUserToAddRows = false;
            this.dataGridView_Zuban.AllowUserToDeleteRows = false;
            this.dataGridView_Zuban.AllowUserToResizeColumns = false;
            this.dataGridView_Zuban.AllowUserToResizeRows = false;
            this.dataGridView_Zuban.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView_Zuban.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Zuban.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_Zuban.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView_Zuban.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnTitle,
            this.ColumnMainID});
            this.dataGridView_Zuban.DataSource = this.menuMainBindingSource;
            this.dataGridView_Zuban.Location = new System.Drawing.Point(20, 27);
            this.dataGridView_Zuban.MultiSelect = false;
            this.dataGridView_Zuban.Name = "dataGridView_Zuban";
            this.dataGridView_Zuban.ReadOnly = true;
            this.dataGridView_Zuban.RowHeadersVisible = false;
            this.dataGridView_Zuban.RowTemplate.Height = 23;
            this.dataGridView_Zuban.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_Zuban.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Zuban.Size = new System.Drawing.Size(182, 345);
            this.dataGridView_Zuban.TabIndex = 19;
            this.dataGridView_Zuban.Click += new System.EventHandler(this.dataGridView_Zuban_Click);
            // 
            // ColumnTitle
            // 
            this.ColumnTitle.DataPropertyName = "Title";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ColumnTitle.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnTitle.HeaderText = "Title";
            this.ColumnTitle.Name = "ColumnTitle";
            this.ColumnTitle.ReadOnly = true;
            this.ColumnTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnMainID
            // 
            this.ColumnMainID.DataPropertyName = "MainID";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ColumnMainID.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnMainID.HeaderText = "MainID";
            this.ColumnMainID.Name = "ColumnMainID";
            this.ColumnMainID.ReadOnly = true;
            this.ColumnMainID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnMainID.Visible = false;
            // 
            // menuMainBindingSource
            // 
            this.menuMainBindingSource.DataMember = "menuMain";
            this.menuMainBindingSource.DataSource = this.dataSetTopMenu;
            // 
            // dataSetTopMenu
            // 
            this.dataSetTopMenu.DataSetName = "DataSetTopMenu";
            this.dataSetTopMenu.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGridView_Edaban
            // 
            this.dataGridView_Edaban.AllowUserToAddRows = false;
            this.dataGridView_Edaban.AllowUserToResizeColumns = false;
            this.dataGridView_Edaban.AllowUserToResizeRows = false;
            this.dataGridView_Edaban.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Edaban.AutoGenerateColumns = false;
            this.dataGridView_Edaban.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Edaban.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView_Edaban.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView_Edaban.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSubTitle,
            this.ColumnFolder,
            this.ColumnSubmainID,
            this.ColumnSubID});
            this.dataGridView_Edaban.DataMember = "menuMain_menuSub";
            this.dataGridView_Edaban.DataSource = this.menuSubBindingSource;
            this.dataGridView_Edaban.Location = new System.Drawing.Point(219, 27);
            this.dataGridView_Edaban.Name = "dataGridView_Edaban";
            this.dataGridView_Edaban.ReadOnly = true;
            this.dataGridView_Edaban.RowHeadersVisible = false;
            this.dataGridView_Edaban.RowTemplate.Height = 23;
            this.dataGridView_Edaban.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_Edaban.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Edaban.Size = new System.Drawing.Size(570, 345);
            this.dataGridView_Edaban.TabIndex = 20;
            this.dataGridView_Edaban.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Edaban_CellDoubleClick);
            this.dataGridView_Edaban.Click += new System.EventHandler(this.dataGridView_Edaban_Click);
            // 
            // ColumnSubTitle
            // 
            this.ColumnSubTitle.DataPropertyName = "SubTitle";
            dataGridViewCellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ColumnSubTitle.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnSubTitle.HeaderText = "SubTitle";
            this.ColumnSubTitle.Name = "ColumnSubTitle";
            this.ColumnSubTitle.ReadOnly = true;
            this.ColumnSubTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnFolder
            // 
            this.ColumnFolder.DataPropertyName = "Folder";
            dataGridViewCellStyle6.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ColumnFolder.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColumnFolder.HeaderText = "Folder";
            this.ColumnFolder.Name = "ColumnFolder";
            this.ColumnFolder.ReadOnly = true;
            this.ColumnFolder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnSubmainID
            // 
            this.ColumnSubmainID.DataPropertyName = "MainID";
            this.ColumnSubmainID.HeaderText = "MainID";
            this.ColumnSubmainID.Name = "ColumnSubmainID";
            this.ColumnSubmainID.ReadOnly = true;
            this.ColumnSubmainID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnSubmainID.Visible = false;
            // 
            // ColumnSubID
            // 
            this.ColumnSubID.DataPropertyName = "SubID";
            this.ColumnSubID.HeaderText = "SubID";
            this.ColumnSubID.Name = "ColumnSubID";
            this.ColumnSubID.ReadOnly = true;
            this.ColumnSubID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnSubID.Visible = false;
            // 
            // menuSubBindingSource
            // 
            this.menuSubBindingSource.DataMember = "menuMain";
            this.menuSubBindingSource.DataSource = this.dataSetTopMenu;
            // 
            // frmTopEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(807, 534);
            this.Controls.Add(this.dataGridView_Edaban);
            this.Controls.Add(this.dataGridView_Zuban);
            this.Controls.Add(this.button_SaveClose);
            this.Controls.Add(this.button_Edit);
            this.Controls.Add(this.Frame1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.MainMenu1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTopEdit";
            this.Text = "配線チェッカー（トップ画面の編集）";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTopEdit_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTopEdit_FormClosed);
            this.Load += new System.EventHandler(this.frmTopEdit_Load);
            this.Frame1.ResumeLayout(false);
            this.MainMenu1.ResumeLayout(false);
            this.MainMenu1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Zuban)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuMainBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetTopMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Edaban)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuSubBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolTip ToolTip1;
        public System.Windows.Forms.ToolStripMenuItem mnuSaveClose;
        public System.Windows.Forms.Button button_SaveClose;
        public System.Windows.Forms.Button button_Edit;
        public System.Windows.Forms.ToolStripMenuItem mnuFile;
        public System.Windows.Forms.ToolStripMenuItem mnuEdit;
        public System.Windows.Forms.ToolStripMenuItem mnuInput;
        public System.Windows.Forms.ToolStripMenuItem mnuAdd;
        public System.Windows.Forms.ToolStripMenuItem mnuDel;
        public System.Windows.Forms.ToolStripMenuItem mnuCopy;
        public System.Windows.Forms.ToolStripMenuItem mnuUp;
        public System.Windows.Forms.ToolStripMenuItem mnuDown;
        public System.Windows.Forms.GroupBox Frame1;
        public System.Windows.Forms.RadioButton radioButton_Edaban;
        public System.Windows.Forms.RadioButton radioButton_Zuban;
        public System.Windows.Forms.Button button_Insert;
        public System.Windows.Forms.Button button_Delete;
        public System.Windows.Forms.Button button_Copy;
        public System.Windows.Forms.Button button_MoveUp;
        public System.Windows.Forms.Button button_MoveDown;
        public System.Windows.Forms.Label Label1;
        public System.Windows.Forms.MenuStrip MainMenu1;
        private System.Windows.Forms.DataGridView dataGridView_Zuban;
        private System.Windows.Forms.DataGridView dataGridView_Edaban;
        private System.Windows.Forms.BindingSource menuSubBindingSource;
        private DataSetTopMenu dataSetTopMenu;
        private System.Windows.Forms.BindingSource menuMainBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMainID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSubTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSubmainID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSubID;
        public System.Windows.Forms.Button buttongetMenuFromFolder;
        private System.Windows.Forms.ToolStripMenuItem 号機へ変換ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 変換iDhiDbToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 変換iDbiDhToolStripMenuItem;
    }
}