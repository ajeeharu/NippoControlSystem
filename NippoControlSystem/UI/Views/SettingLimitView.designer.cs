using NippoControlSystem.UI.Controls;

namespace NippoControlSystem.UI.Views
{
    partial class SettingLimitView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingLimitView));
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox_Edaban = new System.Windows.Forms.TextBox();
            this.bindingSourceCheckDat = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetItems = new NippoControlSystem.DataSetItems();
            this.textBox_Zuban = new System.Windows.Forms.TextBox();
            this.textBox_Item = new System.Windows.Forms.TextBox();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.Label3 = new System.Windows.Forms.Label();
            this.lblSubNo = new System.Windows.Forms.Label();
            this.lblMainNo = new System.Windows.Forms.Label();
            this.lblItem = new System.Windows.Forms.Label();
            this.MainMenu1 = new System.Windows.Forms.MenuStrip();
            this.dataSetItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button_SaveClose = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_iOp電流上限値 = new NumericTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox_iOp電流下限値 = new NumericTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox_iGN電流上限値 = new NumericTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox_iGN電流下限値 = new NumericTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.textBox_iHi電流上限値 = new NumericTextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.textBox_iHi電流下限値 = new NumericTextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.textBox_iDo電流上限値 = new NumericTextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label_iDo電流下限値 = new System.Windows.Forms.Label();
            this.textBox_iDo電流下限値 = new NumericTextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.textBox_iDh電流上限値 = new NumericTextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label_iDh電流下限値 = new System.Windows.Forms.Label();
            this.textBox_iDh電流下限値 = new NumericTextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.textBox_iDb電流上限値 = new NumericTextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label_iDb電流下限値 = new System.Windows.Forms.Label();
            this.textBox_iDb電流下限値 = new NumericTextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.textBox_iDc電流上限値 = new NumericTextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label_iDc電流下限値 = new System.Windows.Forms.Label();
            this.textBox_iDc電流下限値 = new NumericTextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.textBox_iTo電流上限値 = new NumericTextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label_iTo電流下限値 = new System.Windows.Forms.Label();
            this.textBox_iTo電流下限値 = new NumericTextBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.textBox_iDp電流上限値 = new NumericTextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label_iDp電流下限値 = new System.Windows.Forms.Label();
            this.textBox_iDp電流下限値 = new NumericTextBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.textBox_iDs電流上限値 = new NumericTextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label_iDs電流下限値 = new System.Windows.Forms.Label();
            this.textBox_iDs電流下限値 = new NumericTextBox();
            this.textBox_Volt = new System.Windows.Forms.TextBox();
            this.buttonChangeVolt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCheckDat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetItems)).BeginInit();
            this.MainMenu1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetItemsBindingSource)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(124, 22);
            this.mnuExit.Text = "閉じる(&X)";
            // 
            // mnuSave
            // 
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.Size = new System.Drawing.Size(124, 22);
            this.mnuSave.Text = "保存(&S)";
            // 
            // textBox_Edaban
            // 
            this.textBox_Edaban.AcceptsReturn = true;
            this.textBox_Edaban.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Edaban.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Edaban.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "SubTitle", true));
            this.textBox_Edaban.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_Edaban.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_Edaban.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_Edaban.Location = new System.Drawing.Point(400, 44);
            this.textBox_Edaban.MaxLength = 0;
            this.textBox_Edaban.Name = "textBox_Edaban";
            this.textBox_Edaban.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Edaban.Size = new System.Drawing.Size(73, 22);
            this.textBox_Edaban.TabIndex = 21;
            this.textBox_Edaban.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            // textBox_Zuban
            // 
            this.textBox_Zuban.AcceptsReturn = true;
            this.textBox_Zuban.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Zuban.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Zuban.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "Title", true));
            this.textBox_Zuban.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Zuban.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_Zuban.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_Zuban.Location = new System.Drawing.Point(216, 44);
            this.textBox_Zuban.MaxLength = 0;
            this.textBox_Zuban.Name = "textBox_Zuban";
            this.textBox_Zuban.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Zuban.Size = new System.Drawing.Size(169, 22);
            this.textBox_Zuban.TabIndex = 20;
            this.textBox_Zuban.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Item
            // 
            this.textBox_Item.AcceptsReturn = true;
            this.textBox_Item.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Item.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Item.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "Item", true));
            this.textBox_Item.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_Item.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_Item.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_Item.Location = new System.Drawing.Point(8, 44);
            this.textBox_Item.MaxLength = 0;
            this.textBox_Item.Name = "textBox_Item";
            this.textBox_Item.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Item.Size = new System.Drawing.Size(193, 22);
            this.textBox_Item.TabIndex = 19;
            this.textBox_Item.Text = "12345678901234567890123";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSave,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(78, 20);
            this.mnuFile.Text = "ファイル(&F)";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.SystemColors.Control;
            this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label3.Location = new System.Drawing.Point(488, 28);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label3.Size = new System.Drawing.Size(49, 17);
            this.Label3.TabIndex = 34;
            this.Label3.Text = "電圧";
            // 
            // lblSubNo
            // 
            this.lblSubNo.BackColor = System.Drawing.SystemColors.Control;
            this.lblSubNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblSubNo.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSubNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSubNo.Location = new System.Drawing.Point(400, 28);
            this.lblSubNo.Name = "lblSubNo";
            this.lblSubNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSubNo.Size = new System.Drawing.Size(73, 17);
            this.lblSubNo.TabIndex = 33;
            this.lblSubNo.Text = "Edaban";
            // 
            // lblMainNo
            // 
            this.lblMainNo.BackColor = System.Drawing.SystemColors.Control;
            this.lblMainNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblMainNo.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMainNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMainNo.Location = new System.Drawing.Point(216, 28);
            this.lblMainNo.Name = "lblMainNo";
            this.lblMainNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblMainNo.Size = new System.Drawing.Size(169, 17);
            this.lblMainNo.TabIndex = 32;
            this.lblMainNo.Text = "Zuban";
            // 
            // lblItem
            // 
            this.lblItem.BackColor = System.Drawing.SystemColors.Control;
            this.lblItem.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblItem.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblItem.Location = new System.Drawing.Point(8, 28);
            this.lblItem.Name = "lblItem";
            this.lblItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblItem.Size = new System.Drawing.Size(169, 17);
            this.lblItem.TabIndex = 31;
            this.lblItem.Text = "Hinmei";
            // 
            // MainMenu1
            // 
            this.MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.MainMenu1.Location = new System.Drawing.Point(0, 0);
            this.MainMenu1.Name = "MainMenu1";
            this.MainMenu1.Size = new System.Drawing.Size(850, 24);
            this.MainMenu1.TabIndex = 36;
            // 
            // dataSetItemsBindingSource
            // 
            this.dataSetItemsBindingSource.DataSource = this.dataSetItems;
            this.dataSetItemsBindingSource.Position = 0;
            // 
            // button_SaveClose
            // 
            this.button_SaveClose.BackColor = System.Drawing.SystemColors.Control;
            this.button_SaveClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_SaveClose.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_SaveClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_SaveClose.Location = new System.Drawing.Point(650, 36);
            this.button_SaveClose.Name = "button_SaveClose";
            this.button_SaveClose.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_SaveClose.Size = new System.Drawing.Size(114, 33);
            this.button_SaveClose.TabIndex = 40;
            this.button_SaveClose.Text = "閉じる(&Q)";
            this.button_SaveClose.UseVisualStyleBackColor = false;
            this.button_SaveClose.Click += new System.EventHandler(this.button_SaveClose_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.textBox_iOp電流上限値);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.textBox_iOp電流下限値);
            this.groupBox3.Location = new System.Drawing.Point(88, 88);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(269, 74);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(148, 20);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 40;
            this.label4.Text = "電圧上限値";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.SystemColors.Control;
            this.label12.Cursor = System.Windows.Forms.Cursors.Default;
            this.label12.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(239, 44);
            this.label12.Name = "label12";
            this.label12.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label12.Size = new System.Drawing.Size(16, 15);
            this.label12.TabIndex = 39;
            this.label12.Text = "V";
            // 
            // textBox_iOp電流上限値
            // 
            this.textBox_iOp電流上限値.AcceptsReturn = true;
            this.textBox_iOp電流上限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iOp電流上限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iOp電流上限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iOP-HiLMT", true));
            this.textBox_iOp電流上限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iOp電流上限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iOp電流上限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iOp電流上限値.LimitLower = 0D;
            this.textBox_iOp電流上限値.LimitUpper = 60D;
            this.textBox_iOp電流上限値.Location = new System.Drawing.Point(162, 40);
            this.textBox_iOp電流上限値.MaxLength = 0;
            this.textBox_iOp電流上限値.Name = "textBox_iOp電流上限値";
            this.textBox_iOp電流上限値.NumericFormat = "F1";
            this.textBox_iOp電流上限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iOp電流上限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iOp電流上限値.TabIndex = 37;
            this.textBox_iOp電流上限値.Text = "0.0";
            this.textBox_iOp電流上限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iOp電流上限値.Value = 0D;
            this.textBox_iOp電流上限値.ValueChanged = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.SystemColors.Control;
            this.label14.Cursor = System.Windows.Forms.Cursors.Default;
            this.label14.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(40, 20);
            this.label14.Name = "label14";
            this.label14.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label14.Size = new System.Drawing.Size(82, 15);
            this.label14.TabIndex = 36;
            this.label14.Text = "電圧下限値";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.SystemColors.Control;
            this.label15.Cursor = System.Windows.Forms.Cursors.Default;
            this.label15.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label15.Location = new System.Drawing.Point(122, 44);
            this.label15.Name = "label15";
            this.label15.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label15.Size = new System.Drawing.Size(16, 15);
            this.label15.TabIndex = 35;
            this.label15.Text = "V";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.SystemColors.Control;
            this.label16.Cursor = System.Windows.Forms.Cursors.Default;
            this.label16.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label16.Location = new System.Drawing.Point(6, 20);
            this.label16.Name = "label16";
            this.label16.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label16.Size = new System.Drawing.Size(31, 15);
            this.label16.TabIndex = 34;
            this.label16.Text = "iOp";
            // 
            // textBox_iOp電流下限値
            // 
            this.textBox_iOp電流下限値.AcceptsReturn = true;
            this.textBox_iOp電流下限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iOp電流下限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iOp電流下限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iOP-LoLMT", true));
            this.textBox_iOp電流下限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iOp電流下限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iOp電流下限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iOp電流下限値.LimitLower = 0D;
            this.textBox_iOp電流下限値.LimitUpper = 60D;
            this.textBox_iOp電流下限値.Location = new System.Drawing.Point(45, 40);
            this.textBox_iOp電流下限値.MaxLength = 0;
            this.textBox_iOp電流下限値.Name = "textBox_iOp電流下限値";
            this.textBox_iOp電流下限値.NumericFormat = "F1";
            this.textBox_iOp電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iOp電流下限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iOp電流下限値.TabIndex = 22;
            this.textBox_iOp電流下限値.Text = "0.0";
            this.textBox_iOp電流下限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iOp電流下限値.Value = 0D;
            this.textBox_iOp電流下限値.ValueChanged = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.textBox_iGN電流上限値);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.textBox_iGN電流下限値);
            this.groupBox4.Location = new System.Drawing.Point(88, 169);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(269, 74);
            this.groupBox4.TabIndex = 42;
            this.groupBox4.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.SystemColors.Control;
            this.label13.Cursor = System.Windows.Forms.Cursors.Default;
            this.label13.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(148, 20);
            this.label13.Name = "label13";
            this.label13.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label13.Size = new System.Drawing.Size(82, 15);
            this.label13.TabIndex = 40;
            this.label13.Text = "電圧上限値";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.SystemColors.Control;
            this.label17.Cursor = System.Windows.Forms.Cursors.Default;
            this.label17.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label17.Location = new System.Drawing.Point(239, 44);
            this.label17.Name = "label17";
            this.label17.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label17.Size = new System.Drawing.Size(16, 15);
            this.label17.TabIndex = 39;
            this.label17.Text = "V";
            // 
            // textBox_iGN電流上限値
            // 
            this.textBox_iGN電流上限値.AcceptsReturn = true;
            this.textBox_iGN電流上限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iGN電流上限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iGN電流上限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iGN-HiLMT", true));
            this.textBox_iGN電流上限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iGN電流上限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iGN電流上限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iGN電流上限値.LimitLower = 0D;
            this.textBox_iGN電流上限値.LimitUpper = 60D;
            this.textBox_iGN電流上限値.Location = new System.Drawing.Point(162, 40);
            this.textBox_iGN電流上限値.MaxLength = 0;
            this.textBox_iGN電流上限値.Name = "textBox_iGN電流上限値";
            this.textBox_iGN電流上限値.NumericFormat = "F1";
            this.textBox_iGN電流上限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iGN電流上限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iGN電流上限値.TabIndex = 37;
            this.textBox_iGN電流上限値.Text = "0.0";
            this.textBox_iGN電流上限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iGN電流上限値.Value = 0D;
            this.textBox_iGN電流上限値.ValueChanged = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.SystemColors.Control;
            this.label18.Cursor = System.Windows.Forms.Cursors.Default;
            this.label18.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label18.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label18.Location = new System.Drawing.Point(40, 20);
            this.label18.Name = "label18";
            this.label18.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label18.Size = new System.Drawing.Size(82, 15);
            this.label18.TabIndex = 36;
            this.label18.Text = "電圧下限値";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.SystemColors.Control;
            this.label19.Cursor = System.Windows.Forms.Cursors.Default;
            this.label19.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label19.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label19.Location = new System.Drawing.Point(122, 44);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label19.Size = new System.Drawing.Size(16, 15);
            this.label19.TabIndex = 35;
            this.label19.Text = "V";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.SystemColors.Control;
            this.label20.Cursor = System.Windows.Forms.Cursors.Default;
            this.label20.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label20.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label20.Location = new System.Drawing.Point(6, 20);
            this.label20.Name = "label20";
            this.label20.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label20.Size = new System.Drawing.Size(33, 15);
            this.label20.TabIndex = 34;
            this.label20.Text = "iGN";
            // 
            // textBox_iGN電流下限値
            // 
            this.textBox_iGN電流下限値.AcceptsReturn = true;
            this.textBox_iGN電流下限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iGN電流下限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iGN電流下限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iGN-LoLMT", true));
            this.textBox_iGN電流下限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iGN電流下限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iGN電流下限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iGN電流下限値.LimitLower = 0D;
            this.textBox_iGN電流下限値.LimitUpper = 60D;
            this.textBox_iGN電流下限値.Location = new System.Drawing.Point(45, 40);
            this.textBox_iGN電流下限値.MaxLength = 0;
            this.textBox_iGN電流下限値.Name = "textBox_iGN電流下限値";
            this.textBox_iGN電流下限値.NumericFormat = "F1";
            this.textBox_iGN電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iGN電流下限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iGN電流下限値.TabIndex = 22;
            this.textBox_iGN電流下限値.Text = "0.0";
            this.textBox_iGN電流下限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iGN電流下限値.Value = 0D;
            this.textBox_iGN電流下限値.ValueChanged = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.textBox_iHi電流上限値);
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Controls.Add(this.label25);
            this.groupBox5.Controls.Add(this.textBox_iHi電流下限値);
            this.groupBox5.Location = new System.Drawing.Point(88, 250);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(269, 74);
            this.groupBox5.TabIndex = 43;
            this.groupBox5.TabStop = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.SystemColors.Control;
            this.label21.Cursor = System.Windows.Forms.Cursors.Default;
            this.label21.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label21.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label21.Location = new System.Drawing.Point(148, 20);
            this.label21.Name = "label21";
            this.label21.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label21.Size = new System.Drawing.Size(82, 15);
            this.label21.TabIndex = 40;
            this.label21.Text = "電圧上限値";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.SystemColors.Control;
            this.label22.Cursor = System.Windows.Forms.Cursors.Default;
            this.label22.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label22.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label22.Location = new System.Drawing.Point(239, 44);
            this.label22.Name = "label22";
            this.label22.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label22.Size = new System.Drawing.Size(16, 15);
            this.label22.TabIndex = 39;
            this.label22.Text = "V";
            // 
            // textBox_iHi電流上限値
            // 
            this.textBox_iHi電流上限値.AcceptsReturn = true;
            this.textBox_iHi電流上限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iHi電流上限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iHi電流上限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iHi-HiLMT", true));
            this.textBox_iHi電流上限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iHi電流上限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iHi電流上限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iHi電流上限値.LimitLower = 0D;
            this.textBox_iHi電流上限値.LimitUpper = 60D;
            this.textBox_iHi電流上限値.Location = new System.Drawing.Point(162, 40);
            this.textBox_iHi電流上限値.MaxLength = 0;
            this.textBox_iHi電流上限値.Name = "textBox_iHi電流上限値";
            this.textBox_iHi電流上限値.NumericFormat = "F1";
            this.textBox_iHi電流上限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iHi電流上限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iHi電流上限値.TabIndex = 37;
            this.textBox_iHi電流上限値.Text = "0.0";
            this.textBox_iHi電流上限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iHi電流上限値.Value = 0D;
            this.textBox_iHi電流上限値.ValueChanged = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.SystemColors.Control;
            this.label23.Cursor = System.Windows.Forms.Cursors.Default;
            this.label23.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label23.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label23.Location = new System.Drawing.Point(40, 20);
            this.label23.Name = "label23";
            this.label23.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label23.Size = new System.Drawing.Size(82, 15);
            this.label23.TabIndex = 36;
            this.label23.Text = "電圧下限値";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.SystemColors.Control;
            this.label24.Cursor = System.Windows.Forms.Cursors.Default;
            this.label24.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label24.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label24.Location = new System.Drawing.Point(122, 44);
            this.label24.Name = "label24";
            this.label24.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label24.Size = new System.Drawing.Size(16, 15);
            this.label24.TabIndex = 35;
            this.label24.Text = "V";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.SystemColors.Control;
            this.label25.Cursor = System.Windows.Forms.Cursors.Default;
            this.label25.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label25.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label25.Location = new System.Drawing.Point(6, 20);
            this.label25.Name = "label25";
            this.label25.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label25.Size = new System.Drawing.Size(26, 15);
            this.label25.TabIndex = 34;
            this.label25.Text = "iHi";
            // 
            // textBox_iHi電流下限値
            // 
            this.textBox_iHi電流下限値.AcceptsReturn = true;
            this.textBox_iHi電流下限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iHi電流下限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iHi電流下限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iHi-LoLMT", true));
            this.textBox_iHi電流下限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iHi電流下限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iHi電流下限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iHi電流下限値.LimitLower = 0D;
            this.textBox_iHi電流下限値.LimitUpper = 60D;
            this.textBox_iHi電流下限値.Location = new System.Drawing.Point(45, 40);
            this.textBox_iHi電流下限値.MaxLength = 0;
            this.textBox_iHi電流下限値.Name = "textBox_iHi電流下限値";
            this.textBox_iHi電流下限値.NumericFormat = "F1";
            this.textBox_iHi電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iHi電流下限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iHi電流下限値.TabIndex = 22;
            this.textBox_iHi電流下限値.Text = "0.0";
            this.textBox_iHi電流下限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iHi電流下限値.Value = 0D;
            this.textBox_iHi電流下限値.ValueChanged = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label26);
            this.groupBox6.Controls.Add(this.label27);
            this.groupBox6.Controls.Add(this.textBox_iDo電流上限値);
            this.groupBox6.Controls.Add(this.label28);
            this.groupBox6.Controls.Add(this.label29);
            this.groupBox6.Controls.Add(this.label_iDo電流下限値);
            this.groupBox6.Controls.Add(this.textBox_iDo電流下限値);
            this.groupBox6.Location = new System.Drawing.Point(450, 88);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(269, 74);
            this.groupBox6.TabIndex = 44;
            this.groupBox6.TabStop = false;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.SystemColors.Control;
            this.label26.Cursor = System.Windows.Forms.Cursors.Default;
            this.label26.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label26.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label26.Location = new System.Drawing.Point(148, 20);
            this.label26.Name = "label26";
            this.label26.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label26.Size = new System.Drawing.Size(82, 15);
            this.label26.TabIndex = 40;
            this.label26.Text = "電流上限値";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.SystemColors.Control;
            this.label27.Cursor = System.Windows.Forms.Cursors.Default;
            this.label27.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label27.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label27.Location = new System.Drawing.Point(239, 44);
            this.label27.Name = "label27";
            this.label27.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label27.Size = new System.Drawing.Size(27, 15);
            this.label27.TabIndex = 39;
            this.label27.Text = "mA";
            // 
            // textBox_iDo電流上限値
            // 
            this.textBox_iDo電流上限値.AcceptsReturn = true;
            this.textBox_iDo電流上限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iDo電流上限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iDo電流上限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iDo-HiLMT", true));
            this.textBox_iDo電流上限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iDo電流上限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iDo電流上限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iDo電流上限値.LimitLower = 0D;
            this.textBox_iDo電流上限値.LimitUpper = 60D;
            this.textBox_iDo電流上限値.Location = new System.Drawing.Point(162, 40);
            this.textBox_iDo電流上限値.MaxLength = 0;
            this.textBox_iDo電流上限値.Name = "textBox_iDo電流上限値";
            this.textBox_iDo電流上限値.NumericFormat = "F1";
            this.textBox_iDo電流上限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iDo電流上限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iDo電流上限値.TabIndex = 37;
            this.textBox_iDo電流上限値.Text = "0.0";
            this.textBox_iDo電流上限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iDo電流上限値.Value = 0D;
            this.textBox_iDo電流上限値.ValueChanged = true;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.SystemColors.Control;
            this.label28.Cursor = System.Windows.Forms.Cursors.Default;
            this.label28.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label28.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label28.Location = new System.Drawing.Point(40, 20);
            this.label28.Name = "label28";
            this.label28.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label28.Size = new System.Drawing.Size(82, 15);
            this.label28.TabIndex = 36;
            this.label28.Text = "電流下限値";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.SystemColors.Control;
            this.label29.Cursor = System.Windows.Forms.Cursors.Default;
            this.label29.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label29.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label29.Location = new System.Drawing.Point(122, 44);
            this.label29.Name = "label29";
            this.label29.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label29.Size = new System.Drawing.Size(27, 15);
            this.label29.TabIndex = 35;
            this.label29.Text = "mA";
            // 
            // label_iDo電流下限値
            // 
            this.label_iDo電流下限値.AutoSize = true;
            this.label_iDo電流下限値.BackColor = System.Drawing.SystemColors.Control;
            this.label_iDo電流下限値.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_iDo電流下限値.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_iDo電流下限値.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_iDo電流下限値.Location = new System.Drawing.Point(6, 20);
            this.label_iDo電流下限値.Name = "label_iDo電流下限値";
            this.label_iDo電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_iDo電流下限値.Size = new System.Drawing.Size(31, 15);
            this.label_iDo電流下限値.TabIndex = 34;
            this.label_iDo電流下限値.Text = "iDo";
            // 
            // textBox_iDo電流下限値
            // 
            this.textBox_iDo電流下限値.AcceptsReturn = true;
            this.textBox_iDo電流下限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iDo電流下限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iDo電流下限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iDo-LoLMT", true));
            this.textBox_iDo電流下限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iDo電流下限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iDo電流下限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iDo電流下限値.LimitLower = 0D;
            this.textBox_iDo電流下限値.LimitUpper = 60D;
            this.textBox_iDo電流下限値.Location = new System.Drawing.Point(45, 40);
            this.textBox_iDo電流下限値.MaxLength = 0;
            this.textBox_iDo電流下限値.Name = "textBox_iDo電流下限値";
            this.textBox_iDo電流下限値.NumericFormat = "F1";
            this.textBox_iDo電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iDo電流下限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iDo電流下限値.TabIndex = 22;
            this.textBox_iDo電流下限値.Text = "0.0";
            this.textBox_iDo電流下限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iDo電流下限値.Value = 0D;
            this.textBox_iDo電流下限値.ValueChanged = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label31);
            this.groupBox7.Controls.Add(this.label32);
            this.groupBox7.Controls.Add(this.textBox_iDh電流上限値);
            this.groupBox7.Controls.Add(this.label33);
            this.groupBox7.Controls.Add(this.label34);
            this.groupBox7.Controls.Add(this.label_iDh電流下限値);
            this.groupBox7.Controls.Add(this.textBox_iDh電流下限値);
            this.groupBox7.Location = new System.Drawing.Point(450, 169);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(269, 74);
            this.groupBox7.TabIndex = 45;
            this.groupBox7.TabStop = false;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.SystemColors.Control;
            this.label31.Cursor = System.Windows.Forms.Cursors.Default;
            this.label31.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label31.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label31.Location = new System.Drawing.Point(148, 20);
            this.label31.Name = "label31";
            this.label31.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label31.Size = new System.Drawing.Size(82, 15);
            this.label31.TabIndex = 40;
            this.label31.Text = "電流上限値";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.SystemColors.Control;
            this.label32.Cursor = System.Windows.Forms.Cursors.Default;
            this.label32.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label32.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label32.Location = new System.Drawing.Point(239, 44);
            this.label32.Name = "label32";
            this.label32.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label32.Size = new System.Drawing.Size(27, 15);
            this.label32.TabIndex = 39;
            this.label32.Text = "mA";
            // 
            // textBox_iDh電流上限値
            // 
            this.textBox_iDh電流上限値.AcceptsReturn = true;
            this.textBox_iDh電流上限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iDh電流上限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iDh電流上限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iDh-HiLMT", true));
            this.textBox_iDh電流上限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iDh電流上限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iDh電流上限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iDh電流上限値.LimitLower = 0D;
            this.textBox_iDh電流上限値.LimitUpper = 60D;
            this.textBox_iDh電流上限値.Location = new System.Drawing.Point(162, 40);
            this.textBox_iDh電流上限値.MaxLength = 0;
            this.textBox_iDh電流上限値.Name = "textBox_iDh電流上限値";
            this.textBox_iDh電流上限値.NumericFormat = "F1";
            this.textBox_iDh電流上限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iDh電流上限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iDh電流上限値.TabIndex = 37;
            this.textBox_iDh電流上限値.Text = "0.0";
            this.textBox_iDh電流上限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iDh電流上限値.Value = 0D;
            this.textBox_iDh電流上限値.ValueChanged = true;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.BackColor = System.Drawing.SystemColors.Control;
            this.label33.Cursor = System.Windows.Forms.Cursors.Default;
            this.label33.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label33.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label33.Location = new System.Drawing.Point(40, 20);
            this.label33.Name = "label33";
            this.label33.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label33.Size = new System.Drawing.Size(82, 15);
            this.label33.TabIndex = 36;
            this.label33.Text = "電流下限値";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.SystemColors.Control;
            this.label34.Cursor = System.Windows.Forms.Cursors.Default;
            this.label34.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label34.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label34.Location = new System.Drawing.Point(122, 44);
            this.label34.Name = "label34";
            this.label34.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label34.Size = new System.Drawing.Size(27, 15);
            this.label34.TabIndex = 35;
            this.label34.Text = "mA";
            // 
            // label_iDh電流下限値
            // 
            this.label_iDh電流下限値.AutoSize = true;
            this.label_iDh電流下限値.BackColor = System.Drawing.SystemColors.Control;
            this.label_iDh電流下限値.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_iDh電流下限値.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_iDh電流下限値.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_iDh電流下限値.Location = new System.Drawing.Point(6, 20);
            this.label_iDh電流下限値.Name = "label_iDh電流下限値";
            this.label_iDh電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_iDh電流下限値.Size = new System.Drawing.Size(31, 15);
            this.label_iDh電流下限値.TabIndex = 34;
            this.label_iDh電流下限値.Text = "iDh";
            // 
            // textBox_iDh電流下限値
            // 
            this.textBox_iDh電流下限値.AcceptsReturn = true;
            this.textBox_iDh電流下限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iDh電流下限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iDh電流下限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iDh-LoLMT", true));
            this.textBox_iDh電流下限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iDh電流下限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iDh電流下限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iDh電流下限値.LimitLower = 0D;
            this.textBox_iDh電流下限値.LimitUpper = 60D;
            this.textBox_iDh電流下限値.Location = new System.Drawing.Point(45, 40);
            this.textBox_iDh電流下限値.MaxLength = 0;
            this.textBox_iDh電流下限値.Name = "textBox_iDh電流下限値";
            this.textBox_iDh電流下限値.NumericFormat = "F1";
            this.textBox_iDh電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iDh電流下限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iDh電流下限値.TabIndex = 22;
            this.textBox_iDh電流下限値.Text = "0.0";
            this.textBox_iDh電流下限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iDh電流下限値.Value = 0D;
            this.textBox_iDh電流下限値.ValueChanged = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label36);
            this.groupBox8.Controls.Add(this.label37);
            this.groupBox8.Controls.Add(this.textBox_iDb電流上限値);
            this.groupBox8.Controls.Add(this.label38);
            this.groupBox8.Controls.Add(this.label39);
            this.groupBox8.Controls.Add(this.label_iDb電流下限値);
            this.groupBox8.Controls.Add(this.textBox_iDb電流下限値);
            this.groupBox8.Location = new System.Drawing.Point(450, 250);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(269, 74);
            this.groupBox8.TabIndex = 46;
            this.groupBox8.TabStop = false;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.BackColor = System.Drawing.SystemColors.Control;
            this.label36.Cursor = System.Windows.Forms.Cursors.Default;
            this.label36.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label36.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label36.Location = new System.Drawing.Point(148, 20);
            this.label36.Name = "label36";
            this.label36.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label36.Size = new System.Drawing.Size(82, 15);
            this.label36.TabIndex = 40;
            this.label36.Text = "電流上限値";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.SystemColors.Control;
            this.label37.Cursor = System.Windows.Forms.Cursors.Default;
            this.label37.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label37.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label37.Location = new System.Drawing.Point(239, 44);
            this.label37.Name = "label37";
            this.label37.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label37.Size = new System.Drawing.Size(27, 15);
            this.label37.TabIndex = 39;
            this.label37.Text = "mA";
            // 
            // textBox_iDb電流上限値
            // 
            this.textBox_iDb電流上限値.AcceptsReturn = true;
            this.textBox_iDb電流上限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iDb電流上限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iDb電流上限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iDb-HiLMT", true));
            this.textBox_iDb電流上限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iDb電流上限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iDb電流上限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iDb電流上限値.LimitLower = 0D;
            this.textBox_iDb電流上限値.LimitUpper = 60D;
            this.textBox_iDb電流上限値.Location = new System.Drawing.Point(162, 40);
            this.textBox_iDb電流上限値.MaxLength = 0;
            this.textBox_iDb電流上限値.Name = "textBox_iDb電流上限値";
            this.textBox_iDb電流上限値.NumericFormat = "F1";
            this.textBox_iDb電流上限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iDb電流上限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iDb電流上限値.TabIndex = 37;
            this.textBox_iDb電流上限値.Text = "0.0";
            this.textBox_iDb電流上限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iDb電流上限値.Value = 0D;
            this.textBox_iDb電流上限値.ValueChanged = true;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.BackColor = System.Drawing.SystemColors.Control;
            this.label38.Cursor = System.Windows.Forms.Cursors.Default;
            this.label38.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label38.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label38.Location = new System.Drawing.Point(40, 20);
            this.label38.Name = "label38";
            this.label38.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label38.Size = new System.Drawing.Size(82, 15);
            this.label38.TabIndex = 36;
            this.label38.Text = "電流下限値";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.SystemColors.Control;
            this.label39.Cursor = System.Windows.Forms.Cursors.Default;
            this.label39.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label39.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label39.Location = new System.Drawing.Point(122, 44);
            this.label39.Name = "label39";
            this.label39.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label39.Size = new System.Drawing.Size(27, 15);
            this.label39.TabIndex = 35;
            this.label39.Text = "mA";
            // 
            // label_iDb電流下限値
            // 
            this.label_iDb電流下限値.AutoSize = true;
            this.label_iDb電流下限値.BackColor = System.Drawing.SystemColors.Control;
            this.label_iDb電流下限値.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_iDb電流下限値.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_iDb電流下限値.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.label_iDb電流下限値.Location = new System.Drawing.Point(6, 20);
            this.label_iDb電流下限値.Name = "label_iDb電流下限値";
            this.label_iDb電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_iDb電流下限値.Size = new System.Drawing.Size(30, 15);
            this.label_iDb電流下限値.TabIndex = 34;
            this.label_iDb電流下限値.Text = "iDb";
            // 
            // textBox_iDb電流下限値
            // 
            this.textBox_iDb電流下限値.AcceptsReturn = true;
            this.textBox_iDb電流下限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iDb電流下限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iDb電流下限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iDb-LoLMT", true));
            this.textBox_iDb電流下限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iDb電流下限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iDb電流下限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iDb電流下限値.LimitLower = 0D;
            this.textBox_iDb電流下限値.LimitUpper = 60D;
            this.textBox_iDb電流下限値.Location = new System.Drawing.Point(45, 40);
            this.textBox_iDb電流下限値.MaxLength = 0;
            this.textBox_iDb電流下限値.Name = "textBox_iDb電流下限値";
            this.textBox_iDb電流下限値.NumericFormat = "F1";
            this.textBox_iDb電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iDb電流下限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iDb電流下限値.TabIndex = 22;
            this.textBox_iDb電流下限値.Text = "0.0";
            this.textBox_iDb電流下限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iDb電流下限値.Value = 0D;
            this.textBox_iDb電流下限値.ValueChanged = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label41);
            this.groupBox9.Controls.Add(this.label42);
            this.groupBox9.Controls.Add(this.textBox_iDc電流上限値);
            this.groupBox9.Controls.Add(this.label43);
            this.groupBox9.Controls.Add(this.label44);
            this.groupBox9.Controls.Add(this.label_iDc電流下限値);
            this.groupBox9.Controls.Add(this.textBox_iDc電流下限値);
            this.groupBox9.Location = new System.Drawing.Point(450, 331);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(269, 74);
            this.groupBox9.TabIndex = 47;
            this.groupBox9.TabStop = false;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.SystemColors.Control;
            this.label41.Cursor = System.Windows.Forms.Cursors.Default;
            this.label41.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label41.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label41.Location = new System.Drawing.Point(148, 20);
            this.label41.Name = "label41";
            this.label41.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label41.Size = new System.Drawing.Size(82, 15);
            this.label41.TabIndex = 40;
            this.label41.Text = "電流上限値";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.SystemColors.Control;
            this.label42.Cursor = System.Windows.Forms.Cursors.Default;
            this.label42.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label42.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label42.Location = new System.Drawing.Point(239, 44);
            this.label42.Name = "label42";
            this.label42.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label42.Size = new System.Drawing.Size(27, 15);
            this.label42.TabIndex = 39;
            this.label42.Text = "mA";
            // 
            // textBox_iDc電流上限値
            // 
            this.textBox_iDc電流上限値.AcceptsReturn = true;
            this.textBox_iDc電流上限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iDc電流上限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iDc電流上限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iDc-HiLMT", true));
            this.textBox_iDc電流上限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iDc電流上限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iDc電流上限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iDc電流上限値.LimitLower = 0D;
            this.textBox_iDc電流上限値.LimitUpper = 60D;
            this.textBox_iDc電流上限値.Location = new System.Drawing.Point(162, 40);
            this.textBox_iDc電流上限値.MaxLength = 0;
            this.textBox_iDc電流上限値.Name = "textBox_iDc電流上限値";
            this.textBox_iDc電流上限値.NumericFormat = "F1";
            this.textBox_iDc電流上限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iDc電流上限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iDc電流上限値.TabIndex = 37;
            this.textBox_iDc電流上限値.Text = "0.0";
            this.textBox_iDc電流上限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iDc電流上限値.Value = 0D;
            this.textBox_iDc電流上限値.ValueChanged = true;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.BackColor = System.Drawing.SystemColors.Control;
            this.label43.Cursor = System.Windows.Forms.Cursors.Default;
            this.label43.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label43.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label43.Location = new System.Drawing.Point(40, 20);
            this.label43.Name = "label43";
            this.label43.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label43.Size = new System.Drawing.Size(82, 15);
            this.label43.TabIndex = 36;
            this.label43.Text = "電流下限値";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.BackColor = System.Drawing.SystemColors.Control;
            this.label44.Cursor = System.Windows.Forms.Cursors.Default;
            this.label44.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label44.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label44.Location = new System.Drawing.Point(122, 44);
            this.label44.Name = "label44";
            this.label44.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label44.Size = new System.Drawing.Size(27, 15);
            this.label44.TabIndex = 35;
            this.label44.Text = "mA";
            // 
            // label_iDc電流下限値
            // 
            this.label_iDc電流下限値.AutoSize = true;
            this.label_iDc電流下限値.BackColor = System.Drawing.SystemColors.Control;
            this.label_iDc電流下限値.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_iDc電流下限値.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_iDc電流下限値.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label_iDc電流下限値.Location = new System.Drawing.Point(6, 20);
            this.label_iDc電流下限値.Name = "label_iDc電流下限値";
            this.label_iDc電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_iDc電流下限値.Size = new System.Drawing.Size(31, 15);
            this.label_iDc電流下限値.TabIndex = 34;
            this.label_iDc電流下限値.Text = "iDc";
            // 
            // textBox_iDc電流下限値
            // 
            this.textBox_iDc電流下限値.AcceptsReturn = true;
            this.textBox_iDc電流下限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iDc電流下限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iDc電流下限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iDc-LoLMT", true));
            this.textBox_iDc電流下限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iDc電流下限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iDc電流下限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iDc電流下限値.LimitLower = 0D;
            this.textBox_iDc電流下限値.LimitUpper = 60D;
            this.textBox_iDc電流下限値.Location = new System.Drawing.Point(45, 40);
            this.textBox_iDc電流下限値.MaxLength = 0;
            this.textBox_iDc電流下限値.Name = "textBox_iDc電流下限値";
            this.textBox_iDc電流下限値.NumericFormat = "F1";
            this.textBox_iDc電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iDc電流下限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iDc電流下限値.TabIndex = 22;
            this.textBox_iDc電流下限値.Text = "0.0";
            this.textBox_iDc電流下限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iDc電流下限値.Value = 0D;
            this.textBox_iDc電流下限値.ValueChanged = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.label46);
            this.groupBox10.Controls.Add(this.label47);
            this.groupBox10.Controls.Add(this.textBox_iTo電流上限値);
            this.groupBox10.Controls.Add(this.label48);
            this.groupBox10.Controls.Add(this.label49);
            this.groupBox10.Controls.Add(this.label_iTo電流下限値);
            this.groupBox10.Controls.Add(this.textBox_iTo電流下限値);
            this.groupBox10.Location = new System.Drawing.Point(450, 574);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(269, 74);
            this.groupBox10.TabIndex = 50;
            this.groupBox10.TabStop = false;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.BackColor = System.Drawing.SystemColors.Control;
            this.label46.Cursor = System.Windows.Forms.Cursors.Default;
            this.label46.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label46.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label46.Location = new System.Drawing.Point(148, 20);
            this.label46.Name = "label46";
            this.label46.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label46.Size = new System.Drawing.Size(82, 15);
            this.label46.TabIndex = 40;
            this.label46.Text = "電流上限値";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.BackColor = System.Drawing.SystemColors.Control;
            this.label47.Cursor = System.Windows.Forms.Cursors.Default;
            this.label47.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label47.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label47.Location = new System.Drawing.Point(239, 44);
            this.label47.Name = "label47";
            this.label47.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label47.Size = new System.Drawing.Size(27, 15);
            this.label47.TabIndex = 39;
            this.label47.Text = "mA";
            // 
            // textBox_iTo電流上限値
            // 
            this.textBox_iTo電流上限値.AcceptsReturn = true;
            this.textBox_iTo電流上限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iTo電流上限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iTo電流上限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iTo-HiLMT", true));
            this.textBox_iTo電流上限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iTo電流上限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iTo電流上限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iTo電流上限値.LimitLower = 0D;
            this.textBox_iTo電流上限値.LimitUpper = 60D;
            this.textBox_iTo電流上限値.Location = new System.Drawing.Point(162, 40);
            this.textBox_iTo電流上限値.MaxLength = 0;
            this.textBox_iTo電流上限値.Name = "textBox_iTo電流上限値";
            this.textBox_iTo電流上限値.NumericFormat = "F1";
            this.textBox_iTo電流上限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iTo電流上限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iTo電流上限値.TabIndex = 37;
            this.textBox_iTo電流上限値.Text = "0.0";
            this.textBox_iTo電流上限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iTo電流上限値.Value = 0D;
            this.textBox_iTo電流上限値.ValueChanged = true;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.BackColor = System.Drawing.SystemColors.Control;
            this.label48.Cursor = System.Windows.Forms.Cursors.Default;
            this.label48.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label48.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label48.Location = new System.Drawing.Point(40, 20);
            this.label48.Name = "label48";
            this.label48.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label48.Size = new System.Drawing.Size(82, 15);
            this.label48.TabIndex = 36;
            this.label48.Text = "電流下限値";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.BackColor = System.Drawing.SystemColors.Control;
            this.label49.Cursor = System.Windows.Forms.Cursors.Default;
            this.label49.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label49.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label49.Location = new System.Drawing.Point(122, 44);
            this.label49.Name = "label49";
            this.label49.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label49.Size = new System.Drawing.Size(27, 15);
            this.label49.TabIndex = 35;
            this.label49.Text = "mA";
            // 
            // label_iTo電流下限値
            // 
            this.label_iTo電流下限値.AutoSize = true;
            this.label_iTo電流下限値.BackColor = System.Drawing.SystemColors.Control;
            this.label_iTo電流下限値.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_iTo電流下限値.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_iTo電流下限値.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_iTo電流下限値.Location = new System.Drawing.Point(6, 20);
            this.label_iTo電流下限値.Name = "label_iTo電流下限値";
            this.label_iTo電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_iTo電流下限値.Size = new System.Drawing.Size(30, 15);
            this.label_iTo電流下限値.TabIndex = 34;
            this.label_iTo電流下限値.Text = "iTo";
            // 
            // textBox_iTo電流下限値
            // 
            this.textBox_iTo電流下限値.AcceptsReturn = true;
            this.textBox_iTo電流下限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iTo電流下限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iTo電流下限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iTo-LoLMT", true));
            this.textBox_iTo電流下限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iTo電流下限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iTo電流下限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iTo電流下限値.LimitLower = 0D;
            this.textBox_iTo電流下限値.LimitUpper = 60D;
            this.textBox_iTo電流下限値.Location = new System.Drawing.Point(45, 40);
            this.textBox_iTo電流下限値.MaxLength = 0;
            this.textBox_iTo電流下限値.Name = "textBox_iTo電流下限値";
            this.textBox_iTo電流下限値.NumericFormat = "F1";
            this.textBox_iTo電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iTo電流下限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iTo電流下限値.TabIndex = 22;
            this.textBox_iTo電流下限値.Text = "0.0";
            this.textBox_iTo電流下限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iTo電流下限値.Value = 0D;
            this.textBox_iTo電流下限値.ValueChanged = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.label51);
            this.groupBox11.Controls.Add(this.label52);
            this.groupBox11.Controls.Add(this.textBox_iDp電流上限値);
            this.groupBox11.Controls.Add(this.label53);
            this.groupBox11.Controls.Add(this.label54);
            this.groupBox11.Controls.Add(this.label_iDp電流下限値);
            this.groupBox11.Controls.Add(this.textBox_iDp電流下限値);
            this.groupBox11.Location = new System.Drawing.Point(450, 493);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(269, 74);
            this.groupBox11.TabIndex = 49;
            this.groupBox11.TabStop = false;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.BackColor = System.Drawing.SystemColors.Control;
            this.label51.Cursor = System.Windows.Forms.Cursors.Default;
            this.label51.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label51.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label51.Location = new System.Drawing.Point(148, 20);
            this.label51.Name = "label51";
            this.label51.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label51.Size = new System.Drawing.Size(82, 15);
            this.label51.TabIndex = 40;
            this.label51.Text = "電流上限値";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.BackColor = System.Drawing.SystemColors.Control;
            this.label52.Cursor = System.Windows.Forms.Cursors.Default;
            this.label52.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label52.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label52.Location = new System.Drawing.Point(239, 44);
            this.label52.Name = "label52";
            this.label52.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label52.Size = new System.Drawing.Size(27, 15);
            this.label52.TabIndex = 39;
            this.label52.Text = "mA";
            // 
            // textBox_iDp電流上限値
            // 
            this.textBox_iDp電流上限値.AcceptsReturn = true;
            this.textBox_iDp電流上限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iDp電流上限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iDp電流上限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iDp-HiLMT", true));
            this.textBox_iDp電流上限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iDp電流上限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iDp電流上限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iDp電流上限値.LimitLower = 0D;
            this.textBox_iDp電流上限値.LimitUpper = 60D;
            this.textBox_iDp電流上限値.Location = new System.Drawing.Point(162, 40);
            this.textBox_iDp電流上限値.MaxLength = 0;
            this.textBox_iDp電流上限値.Name = "textBox_iDp電流上限値";
            this.textBox_iDp電流上限値.NumericFormat = "F1";
            this.textBox_iDp電流上限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iDp電流上限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iDp電流上限値.TabIndex = 37;
            this.textBox_iDp電流上限値.Text = "0.0";
            this.textBox_iDp電流上限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iDp電流上限値.Value = 0D;
            this.textBox_iDp電流上限値.ValueChanged = true;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.BackColor = System.Drawing.SystemColors.Control;
            this.label53.Cursor = System.Windows.Forms.Cursors.Default;
            this.label53.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label53.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label53.Location = new System.Drawing.Point(40, 20);
            this.label53.Name = "label53";
            this.label53.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label53.Size = new System.Drawing.Size(82, 15);
            this.label53.TabIndex = 36;
            this.label53.Text = "電流下限値";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.BackColor = System.Drawing.SystemColors.Control;
            this.label54.Cursor = System.Windows.Forms.Cursors.Default;
            this.label54.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label54.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label54.Location = new System.Drawing.Point(122, 44);
            this.label54.Name = "label54";
            this.label54.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label54.Size = new System.Drawing.Size(27, 15);
            this.label54.TabIndex = 35;
            this.label54.Text = "mA";
            // 
            // label_iDp電流下限値
            // 
            this.label_iDp電流下限値.AutoSize = true;
            this.label_iDp電流下限値.BackColor = System.Drawing.SystemColors.Control;
            this.label_iDp電流下限値.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_iDp電流下限値.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_iDp電流下限値.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_iDp電流下限値.Location = new System.Drawing.Point(6, 20);
            this.label_iDp電流下限値.Name = "label_iDp電流下限値";
            this.label_iDp電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_iDp電流下限値.Size = new System.Drawing.Size(30, 15);
            this.label_iDp電流下限値.TabIndex = 34;
            this.label_iDp電流下限値.Text = "iDp";
            // 
            // textBox_iDp電流下限値
            // 
            this.textBox_iDp電流下限値.AcceptsReturn = true;
            this.textBox_iDp電流下限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iDp電流下限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iDp電流下限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iDp-LoLMT", true));
            this.textBox_iDp電流下限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iDp電流下限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iDp電流下限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iDp電流下限値.LimitLower = 0D;
            this.textBox_iDp電流下限値.LimitUpper = 60D;
            this.textBox_iDp電流下限値.Location = new System.Drawing.Point(45, 40);
            this.textBox_iDp電流下限値.MaxLength = 0;
            this.textBox_iDp電流下限値.Name = "textBox_iDp電流下限値";
            this.textBox_iDp電流下限値.NumericFormat = "F1";
            this.textBox_iDp電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iDp電流下限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iDp電流下限値.TabIndex = 22;
            this.textBox_iDp電流下限値.Text = "0.0";
            this.textBox_iDp電流下限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iDp電流下限値.Value = 0D;
            this.textBox_iDp電流下限値.ValueChanged = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.label56);
            this.groupBox12.Controls.Add(this.label57);
            this.groupBox12.Controls.Add(this.textBox_iDs電流上限値);
            this.groupBox12.Controls.Add(this.label58);
            this.groupBox12.Controls.Add(this.label59);
            this.groupBox12.Controls.Add(this.label_iDs電流下限値);
            this.groupBox12.Controls.Add(this.textBox_iDs電流下限値);
            this.groupBox12.Location = new System.Drawing.Point(450, 412);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(269, 74);
            this.groupBox12.TabIndex = 48;
            this.groupBox12.TabStop = false;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.BackColor = System.Drawing.SystemColors.Control;
            this.label56.Cursor = System.Windows.Forms.Cursors.Default;
            this.label56.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label56.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label56.Location = new System.Drawing.Point(148, 20);
            this.label56.Name = "label56";
            this.label56.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label56.Size = new System.Drawing.Size(82, 15);
            this.label56.TabIndex = 40;
            this.label56.Text = "電流上限値";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.BackColor = System.Drawing.SystemColors.Control;
            this.label57.Cursor = System.Windows.Forms.Cursors.Default;
            this.label57.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label57.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label57.Location = new System.Drawing.Point(239, 44);
            this.label57.Name = "label57";
            this.label57.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label57.Size = new System.Drawing.Size(27, 15);
            this.label57.TabIndex = 39;
            this.label57.Text = "mA";
            // 
            // textBox_iDs電流上限値
            // 
            this.textBox_iDs電流上限値.AcceptsReturn = true;
            this.textBox_iDs電流上限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iDs電流上限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iDs電流上限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iDs-HiLMT", true));
            this.textBox_iDs電流上限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iDs電流上限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iDs電流上限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iDs電流上限値.LimitLower = 0D;
            this.textBox_iDs電流上限値.LimitUpper = 60D;
            this.textBox_iDs電流上限値.Location = new System.Drawing.Point(162, 40);
            this.textBox_iDs電流上限値.MaxLength = 0;
            this.textBox_iDs電流上限値.Name = "textBox_iDs電流上限値";
            this.textBox_iDs電流上限値.NumericFormat = "F1";
            this.textBox_iDs電流上限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iDs電流上限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iDs電流上限値.TabIndex = 37;
            this.textBox_iDs電流上限値.Text = "0.0";
            this.textBox_iDs電流上限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iDs電流上限値.Value = 0D;
            this.textBox_iDs電流上限値.ValueChanged = true;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.BackColor = System.Drawing.SystemColors.Control;
            this.label58.Cursor = System.Windows.Forms.Cursors.Default;
            this.label58.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label58.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label58.Location = new System.Drawing.Point(40, 20);
            this.label58.Name = "label58";
            this.label58.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label58.Size = new System.Drawing.Size(82, 15);
            this.label58.TabIndex = 36;
            this.label58.Text = "電流下限値";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.BackColor = System.Drawing.SystemColors.Control;
            this.label59.Cursor = System.Windows.Forms.Cursors.Default;
            this.label59.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label59.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label59.Location = new System.Drawing.Point(122, 44);
            this.label59.Name = "label59";
            this.label59.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label59.Size = new System.Drawing.Size(27, 15);
            this.label59.TabIndex = 35;
            this.label59.Text = "mA";
            // 
            // label_iDs電流下限値
            // 
            this.label_iDs電流下限値.AutoSize = true;
            this.label_iDs電流下限値.BackColor = System.Drawing.SystemColors.Control;
            this.label_iDs電流下限値.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_iDs電流下限値.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_iDs電流下限値.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_iDs電流下限値.Location = new System.Drawing.Point(6, 20);
            this.label_iDs電流下限値.Name = "label_iDs電流下限値";
            this.label_iDs電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_iDs電流下限値.Size = new System.Drawing.Size(30, 15);
            this.label_iDs電流下限値.TabIndex = 34;
            this.label_iDs電流下限値.Text = "iDs";
            // 
            // textBox_iDs電流下限値
            // 
            this.textBox_iDs電流下限値.AcceptsReturn = true;
            this.textBox_iDs電流下限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_iDs電流下限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_iDs電流下限値.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceCheckDat, "iDs-LoLMT", true));
            this.textBox_iDs電流下限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_iDs電流下限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_iDs電流下限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_iDs電流下限値.LimitLower = 0D;
            this.textBox_iDs電流下限値.LimitUpper = 60D;
            this.textBox_iDs電流下限値.Location = new System.Drawing.Point(45, 40);
            this.textBox_iDs電流下限値.MaxLength = 0;
            this.textBox_iDs電流下限値.Name = "textBox_iDs電流下限値";
            this.textBox_iDs電流下限値.NumericFormat = "F1";
            this.textBox_iDs電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_iDs電流下限値.Size = new System.Drawing.Size(73, 22);
            this.textBox_iDs電流下限値.TabIndex = 22;
            this.textBox_iDs電流下限値.Text = "0.0";
            this.textBox_iDs電流下限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_iDs電流下限値.Value = 0D;
            this.textBox_iDs電流下限値.ValueChanged = true;
            // 
            // textBox_Volt
            // 
            this.textBox_Volt.AcceptsReturn = true;
            this.textBox_Volt.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Volt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox_Volt.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBox_Volt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_Volt.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_Volt.Location = new System.Drawing.Point(488, 44);
            this.textBox_Volt.MaxLength = 0;
            this.textBox_Volt.Name = "textBox_Volt";
            this.textBox_Volt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox_Volt.Size = new System.Drawing.Size(73, 22);
            this.textBox_Volt.TabIndex = 51;
            this.textBox_Volt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonChangeVolt
            // 
            this.buttonChangeVolt.BackColor = System.Drawing.SystemColors.Control;
            this.buttonChangeVolt.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonChangeVolt.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonChangeVolt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonChangeVolt.Location = new System.Drawing.Point(561, 43);
            this.buttonChangeVolt.Name = "buttonChangeVolt";
            this.buttonChangeVolt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonChangeVolt.Size = new System.Drawing.Size(73, 24);
            this.buttonChangeVolt.TabIndex = 52;
            this.buttonChangeVolt.Text = "電圧変更";
            this.buttonChangeVolt.UseVisualStyleBackColor = false;
            this.buttonChangeVolt.Click += new System.EventHandler(this.buttonChangeVolt_Click);
            // 
            // frmSettingLMT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(850, 666);
            this.Controls.Add(this.buttonChangeVolt);
            this.Controls.Add(this.textBox_Volt);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.groupBox12);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button_SaveClose);
            this.Controls.Add(this.textBox_Edaban);
            this.Controls.Add(this.textBox_Zuban);
            this.Controls.Add(this.textBox_Item);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.lblSubNo);
            this.Controls.Add(this.lblMainNo);
            this.Controls.Add(this.lblItem);
            this.Controls.Add(this.MainMenu1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSettingLMT";
            this.Text = "配線チェッカー（検査定義上下限値の編集）";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSettingLMT_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSettingLMT_FormClosed);
            this.Load += new System.EventHandler(this.frmSettingLMT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCheckDat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetItems)).EndInit();
            this.MainMenu1.ResumeLayout(false);
            this.MainMenu1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetItemsBindingSource)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolTip ToolTip1;
        public System.Windows.Forms.ToolStripMenuItem mnuExit;
        public System.Windows.Forms.ToolStripMenuItem mnuSave;
        public System.Windows.Forms.TextBox textBox_Edaban;
        public System.Windows.Forms.TextBox textBox_Zuban;
        public System.Windows.Forms.TextBox textBox_Item;
        public System.Windows.Forms.ToolStripMenuItem mnuFile;
        public System.Windows.Forms.Label Label3;
        public System.Windows.Forms.Label lblSubNo;
        public System.Windows.Forms.Label lblMainNo;
        public System.Windows.Forms.Label lblItem;
        public System.Windows.Forms.MenuStrip MainMenu1;
        private System.Windows.Forms.BindingSource bindingSourceCheckDat;
        private DataSetItems dataSetItems;
        private System.Windows.Forms.BindingSource dataSetItemsBindingSource;
        public System.Windows.Forms.Button button_SaveClose;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label12;
        public NumericTextBox textBox_iOp電流上限値;
        public System.Windows.Forms.Label label14;
        public System.Windows.Forms.Label label15;
        public System.Windows.Forms.Label label16;
        public NumericTextBox textBox_iOp電流下限値;
        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.Label label13;
        public System.Windows.Forms.Label label17;
        public NumericTextBox textBox_iGN電流上限値;
        public System.Windows.Forms.Label label18;
        public System.Windows.Forms.Label label19;
        public System.Windows.Forms.Label label20;
        public NumericTextBox textBox_iGN電流下限値;
        private System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.Label label21;
        public System.Windows.Forms.Label label22;
        public NumericTextBox textBox_iHi電流上限値;
        public System.Windows.Forms.Label label23;
        public System.Windows.Forms.Label label24;
        public System.Windows.Forms.Label label25;
        public NumericTextBox textBox_iHi電流下限値;
        private System.Windows.Forms.GroupBox groupBox6;
        public System.Windows.Forms.Label label26;
        public System.Windows.Forms.Label label27;
        public NumericTextBox textBox_iDo電流上限値;
        public System.Windows.Forms.Label label28;
        public System.Windows.Forms.Label label29;
        public System.Windows.Forms.Label label_iDo電流下限値;
        public NumericTextBox textBox_iDo電流下限値;
        private System.Windows.Forms.GroupBox groupBox7;
        public System.Windows.Forms.Label label31;
        public System.Windows.Forms.Label label32;
        public NumericTextBox textBox_iDh電流上限値;
        public System.Windows.Forms.Label label33;
        public System.Windows.Forms.Label label34;
        public System.Windows.Forms.Label label_iDh電流下限値;
        public NumericTextBox textBox_iDh電流下限値;
        private System.Windows.Forms.GroupBox groupBox8;
        public System.Windows.Forms.Label label36;
        public System.Windows.Forms.Label label37;
        public NumericTextBox textBox_iDb電流上限値;
        public System.Windows.Forms.Label label38;
        public System.Windows.Forms.Label label39;
        public System.Windows.Forms.Label label_iDb電流下限値;
        public NumericTextBox textBox_iDb電流下限値;
        private System.Windows.Forms.GroupBox groupBox9;
        public System.Windows.Forms.Label label41;
        public System.Windows.Forms.Label label42;
        public NumericTextBox textBox_iDc電流上限値;
        public System.Windows.Forms.Label label43;
        public System.Windows.Forms.Label label44;
        public System.Windows.Forms.Label label_iDc電流下限値;
        public NumericTextBox textBox_iDc電流下限値;
        private System.Windows.Forms.GroupBox groupBox10;
        public System.Windows.Forms.Label label46;
        public System.Windows.Forms.Label label47;
        public System.Windows.Forms.Label label48;
        public System.Windows.Forms.Label label49;
        public System.Windows.Forms.Label label_iTo電流下限値;
        private System.Windows.Forms.GroupBox groupBox11;
        public System.Windows.Forms.Label label51;
        public System.Windows.Forms.Label label52;
        public System.Windows.Forms.Label label53;
        public System.Windows.Forms.Label label54;
        public System.Windows.Forms.Label label_iDp電流下限値;
        private System.Windows.Forms.GroupBox groupBox12;
        public System.Windows.Forms.Label label56;
        public System.Windows.Forms.Label label57;
        public System.Windows.Forms.Label label58;
        public System.Windows.Forms.Label label59;
        public System.Windows.Forms.Label label_iDs電流下限値;
        public NumericTextBox textBox_iDs電流下限値;
        public NumericTextBox textBox_iDs電流上限値;
        public NumericTextBox textBox_iTo電流上限値;
        public NumericTextBox textBox_iTo電流下限値;
        public NumericTextBox textBox_iDp電流上限値;
        public NumericTextBox textBox_iDp電流下限値;
        public System.Windows.Forms.TextBox textBox_Volt;
        public System.Windows.Forms.Button buttonChangeVolt;
    }
}