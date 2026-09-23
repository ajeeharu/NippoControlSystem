using NippoControlSystem.UI.Controls;

namespace NippoControlSystem.UI.Views
{
    partial class PinInputOutput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PinInputOutput));
            this.comboBox_PinIO = new System.Windows.Forms.ComboBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetItems = new NippoControlSystem.DataSetItems();
            this.button_OK = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.groupBoxiDp = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxiDp電流上限値 = new NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxiDp電流下限値 = new NumericTextBox();
            this.groupBoxiDs = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxiDs電流上限値 = new NumericTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxiDs電流下限値 = new NumericTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetItems)).BeginInit();
            this.groupBoxiDp.SuspendLayout();
            this.groupBoxiDs.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_PinIO
            // 
            this.comboBox_PinIO.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox_PinIO.Cursor = System.Windows.Forms.Cursors.Default;
            this.comboBox_PinIO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_PinIO.Font = new System.Drawing.Font("ＭＳ ゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox_PinIO.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox_PinIO.Items.AddRange(new object[] {
            "出力High",
            "出力GND",
            "入力High",
            "入力GND",
            "入力Open",
            "指定なし"});
            this.comboBox_PinIO.Location = new System.Drawing.Point(18, 42);
            this.comboBox_PinIO.MaxDropDownItems = 12;
            this.comboBox_PinIO.Name = "comboBox_PinIO";
            this.comboBox_PinIO.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboBox_PinIO.Size = new System.Drawing.Size(204, 29);
            this.comboBox_PinIO.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.comboBox_PinIO, "選択してください。");
            this.comboBox_PinIO.SelectedIndexChanged += new System.EventHandler(this.comboBox_PinIO_SelectedIndexChanged);
            this.comboBox_PinIO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_PinIO_KeyDown);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataMember = "View_DIO";
            this.bindingSource1.DataSource = this.dataSetItems;
            // 
            // dataSetItems
            // 
            this.dataSetItems.DataSetName = "DataSetItems";
            this.dataSetItems.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button_OK
            // 
            this.button_OK.BackColor = System.Drawing.SystemColors.Control;
            this.button_OK.Cursor = System.Windows.Forms.Cursors.Default;
            this.button_OK.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_OK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_OK.Location = new System.Drawing.Point(74, 239);
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
            this.Label1.Location = new System.Drawing.Point(10, 18);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(324, 21);
            this.Label1.TabIndex = 5;
            this.Label1.Text = "出力または入力（期待値）を選択してください。";
            // 
            // groupBoxiDp
            // 
            this.groupBoxiDp.Controls.Add(this.label9);
            this.groupBoxiDp.Controls.Add(this.label10);
            this.groupBoxiDp.Controls.Add(this.label11);
            this.groupBoxiDp.Controls.Add(this.textBoxiDp電流上限値);
            this.groupBoxiDp.Controls.Add(this.label6);
            this.groupBoxiDp.Controls.Add(this.label7);
            this.groupBoxiDp.Controls.Add(this.label8);
            this.groupBoxiDp.Controls.Add(this.textBoxiDp電流下限値);
            this.groupBoxiDp.Location = new System.Drawing.Point(21, 78);
            this.groupBoxiDp.Name = "groupBoxiDp";
            this.groupBoxiDp.Size = new System.Drawing.Size(246, 74);
            this.groupBoxiDp.TabIndex = 40;
            this.groupBoxiDp.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.Control;
            this.label9.Cursor = System.Windows.Forms.Cursors.Default;
            this.label9.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(148, 20);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label9.Size = new System.Drawing.Size(82, 15);
            this.label9.TabIndex = 40;
            this.label9.Text = "電流上限値";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.Control;
            this.label10.Cursor = System.Windows.Forms.Cursors.Default;
            this.label10.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(203, 44);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label10.Size = new System.Drawing.Size(27, 15);
            this.label10.TabIndex = 39;
            this.label10.Text = "mA";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.Control;
            this.label11.Cursor = System.Windows.Forms.Cursors.Default;
            this.label11.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label11.Location = new System.Drawing.Point(123, 20);
            this.label11.Name = "label11";
            this.label11.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label11.Size = new System.Drawing.Size(30, 15);
            this.label11.TabIndex = 38;
            this.label11.Text = "iDp";
            // 
            // textBoxiDp電流上限値
            // 
            this.textBoxiDp電流上限値.AcceptsReturn = true;
            this.textBoxiDp電流上限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxiDp電流上限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxiDp電流上限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBoxiDp電流上限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxiDp電流上限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxiDp電流上限値.LimitLower = 0D;
            this.textBoxiDp電流上限値.LimitUpper = 60D;
            this.textBoxiDp電流上限値.Location = new System.Drawing.Point(126, 40);
            this.textBoxiDp電流上限値.MaxLength = 0;
            this.textBoxiDp電流上限値.Name = "textBoxiDp電流上限値";
            this.textBoxiDp電流上限値.NumericFormat = "F1";
            this.textBoxiDp電流上限値.Size = new System.Drawing.Size(73, 22);
            this.textBoxiDp電流上限値.TabIndex = 37;
            this.textBoxiDp電流上限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxiDp電流上限値.Value = 0D;
            this.textBoxiDp電流上限値.ValueChanged = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Cursor = System.Windows.Forms.Cursors.Default;
            this.label6.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(31, 20);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(82, 15);
            this.label6.TabIndex = 36;
            this.label6.Text = "電流下限値";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Cursor = System.Windows.Forms.Cursors.Default;
            this.label7.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(86, 44);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(27, 15);
            this.label7.TabIndex = 35;
            this.label7.Text = "mA";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Cursor = System.Windows.Forms.Cursors.Default;
            this.label8.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(6, 20);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(30, 15);
            this.label8.TabIndex = 34;
            this.label8.Text = "iDp";
            // 
            // textBoxiDp電流下限値
            // 
            this.textBoxiDp電流下限値.AcceptsReturn = true;
            this.textBoxiDp電流下限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxiDp電流下限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxiDp電流下限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBoxiDp電流下限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxiDp電流下限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxiDp電流下限値.LimitLower = 0D;
            this.textBoxiDp電流下限値.LimitUpper = 60D;
            this.textBoxiDp電流下限値.Location = new System.Drawing.Point(9, 40);
            this.textBoxiDp電流下限値.MaxLength = 0;
            this.textBoxiDp電流下限値.Name = "textBoxiDp電流下限値";
            this.textBoxiDp電流下限値.NumericFormat = "F1";
            this.textBoxiDp電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxiDp電流下限値.Size = new System.Drawing.Size(73, 22);
            this.textBoxiDp電流下限値.TabIndex = 22;
            this.textBoxiDp電流下限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxiDp電流下限値.Value = 0D;
            this.textBoxiDp電流下限値.ValueChanged = false;
            // 
            // groupBoxiDs
            // 
            this.groupBoxiDs.Controls.Add(this.label2);
            this.groupBoxiDs.Controls.Add(this.label3);
            this.groupBoxiDs.Controls.Add(this.label4);
            this.groupBoxiDs.Controls.Add(this.textBoxiDs電流上限値);
            this.groupBoxiDs.Controls.Add(this.label5);
            this.groupBoxiDs.Controls.Add(this.label12);
            this.groupBoxiDs.Controls.Add(this.label13);
            this.groupBoxiDs.Controls.Add(this.textBoxiDs電流下限値);
            this.groupBoxiDs.Location = new System.Drawing.Point(21, 153);
            this.groupBoxiDs.Name = "groupBoxiDs";
            this.groupBoxiDs.Size = new System.Drawing.Size(246, 74);
            this.groupBoxiDs.TabIndex = 41;
            this.groupBoxiDs.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(148, 20);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 40;
            this.label2.Text = "電流上限値";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(203, 44);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(27, 15);
            this.label3.TabIndex = 39;
            this.label3.Text = "mA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(123, 20);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(30, 15);
            this.label4.TabIndex = 38;
            this.label4.Text = "iDs";
            // 
            // textBoxiDs電流上限値
            // 
            this.textBoxiDs電流上限値.AcceptsReturn = true;
            this.textBoxiDs電流上限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxiDs電流上限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxiDs電流上限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBoxiDs電流上限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxiDs電流上限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxiDs電流上限値.LimitLower = 0D;
            this.textBoxiDs電流上限値.LimitUpper = 60D;
            this.textBoxiDs電流上限値.Location = new System.Drawing.Point(126, 40);
            this.textBoxiDs電流上限値.MaxLength = 0;
            this.textBoxiDs電流上限値.Name = "textBoxiDs電流上限値";
            this.textBoxiDs電流上限値.NumericFormat = "F1";
            this.textBoxiDs電流上限値.Size = new System.Drawing.Size(73, 22);
            this.textBoxiDs電流上限値.TabIndex = 37;
            this.textBoxiDs電流上限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxiDs電流上限値.Value = 0D;
            this.textBoxiDs電流上限値.ValueChanged = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.label5.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(31, 20);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 36;
            this.label5.Text = "電流下限値";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.SystemColors.Control;
            this.label12.Cursor = System.Windows.Forms.Cursors.Default;
            this.label12.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(86, 44);
            this.label12.Name = "label12";
            this.label12.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label12.Size = new System.Drawing.Size(27, 15);
            this.label12.TabIndex = 35;
            this.label12.Text = "mA";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.SystemColors.Control;
            this.label13.Cursor = System.Windows.Forms.Cursors.Default;
            this.label13.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(6, 20);
            this.label13.Name = "label13";
            this.label13.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label13.Size = new System.Drawing.Size(30, 15);
            this.label13.TabIndex = 34;
            this.label13.Text = "iDs";
            // 
            // textBoxiDs電流下限値
            // 
            this.textBoxiDs電流下限値.AcceptsReturn = true;
            this.textBoxiDs電流下限値.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxiDs電流下限値.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxiDs電流下限値.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.textBoxiDs電流下限値.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxiDs電流下限値.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxiDs電流下限値.LimitLower = 0D;
            this.textBoxiDs電流下限値.LimitUpper = 60D;
            this.textBoxiDs電流下限値.Location = new System.Drawing.Point(9, 40);
            this.textBoxiDs電流下限値.MaxLength = 0;
            this.textBoxiDs電流下限値.Name = "textBoxiDs電流下限値";
            this.textBoxiDs電流下限値.NumericFormat = "F1";
            this.textBoxiDs電流下限値.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxiDs電流下限値.Size = new System.Drawing.Size(73, 22);
            this.textBoxiDs電流下限値.TabIndex = 22;
            this.textBoxiDs電流下限値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxiDs電流下限値.Value = 0D;
            this.textBoxiDs電流下限値.ValueChanged = false;
            // 
            // frmPinIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(333, 292);
            this.Controls.Add(this.groupBoxiDs);
            this.Controls.Add(this.groupBoxiDp);
            this.Controls.Add(this.comboBox_PinIO);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPinIO";
            this.Text = "デジタル入出力";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPinIO_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPinIO_FormClosed);
            this.Load += new System.EventHandler(this.frmPinIO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetItems)).EndInit();
            this.groupBoxiDp.ResumeLayout(false);
            this.groupBoxiDp.PerformLayout();
            this.groupBoxiDs.ResumeLayout(false);
            this.groupBoxiDs.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox comboBox_PinIO;
        public System.Windows.Forms.ToolTip ToolTip1;
        public System.Windows.Forms.Button button_OK;
        public System.Windows.Forms.Label Label1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DataSetItems dataSetItems;
        private System.Windows.Forms.GroupBox groupBoxiDp;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label label11;
        public NumericTextBox textBoxiDp電流上限値;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label8;
        public NumericTextBox textBoxiDp電流下限値;
        private System.Windows.Forms.GroupBox groupBoxiDs;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public NumericTextBox textBoxiDs電流上限値;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label12;
        public System.Windows.Forms.Label label13;
        public NumericTextBox textBoxiDs電流下限値;
    }
}