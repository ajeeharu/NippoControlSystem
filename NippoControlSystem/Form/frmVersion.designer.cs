namespace NippoControlSystem
{
    partial class frmVersion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVersion));
            this.label_CompanyName = new System.Windows.Forms.Label();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.label_Version = new System.Windows.Forms.Label();
            this.label_ProductName = new System.Windows.Forms.Label();
            this.label_Copyright = new System.Windows.Forms.Label();
            this.label_Description = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // label_CompanyName
            // 
            this.label_CompanyName.BackColor = System.Drawing.SystemColors.Control;
            this.label_CompanyName.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_CompanyName.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_CompanyName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_CompanyName.Location = new System.Drawing.Point(76, 19);
            this.label_CompanyName.Name = "label_CompanyName";
            this.label_CompanyName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_CompanyName.Size = new System.Drawing.Size(186, 13);
            this.label_CompanyName.TabIndex = 12;
            this.label_CompanyName.Text = "CompanyName";
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Image = global::NippoControlSystem.UI.Properties.Resources.nippo3;
            this.pictureBoxIcon.Location = new System.Drawing.Point(16, 19);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxIcon.TabIndex = 13;
            this.pictureBoxIcon.TabStop = false;
            // 
            // label_Version
            // 
            this.label_Version.BackColor = System.Drawing.SystemColors.Control;
            this.label_Version.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_Version.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Version.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Version.Location = new System.Drawing.Point(76, 46);
            this.label_Version.Name = "label_Version";
            this.label_Version.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_Version.Size = new System.Drawing.Size(438, 13);
            this.label_Version.TabIndex = 14;
            this.label_Version.Text = "Version";
            // 
            // label_ProductName
            // 
            this.label_ProductName.BackColor = System.Drawing.SystemColors.Control;
            this.label_ProductName.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_ProductName.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_ProductName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_ProductName.Location = new System.Drawing.Point(274, 19);
            this.label_ProductName.Name = "label_ProductName";
            this.label_ProductName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_ProductName.Size = new System.Drawing.Size(186, 13);
            this.label_ProductName.TabIndex = 16;
            this.label_ProductName.Text = "ProductName";
            // 
            // label_Copyright
            // 
            this.label_Copyright.BackColor = System.Drawing.SystemColors.Control;
            this.label_Copyright.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_Copyright.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Copyright.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Copyright.Location = new System.Drawing.Point(76, 73);
            this.label_Copyright.Name = "label_Copyright";
            this.label_Copyright.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_Copyright.Size = new System.Drawing.Size(438, 13);
            this.label_Copyright.TabIndex = 17;
            this.label_Copyright.Text = "Copyright";
            // 
            // label_Description
            // 
            this.label_Description.BackColor = System.Drawing.SystemColors.Control;
            this.label_Description.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_Description.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Description.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Description.Location = new System.Drawing.Point(76, 100);
            this.label_Description.Name = "label_Description";
            this.label_Description.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_Description.Size = new System.Drawing.Size(438, 13);
            this.label_Description.TabIndex = 18;
            this.label_Description.Text = "Description";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(446, 140);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(68, 28);
            this.buttonOK.TabIndex = 19;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // frmVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(522, 180);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label_Description);
            this.Controls.Add(this.label_Copyright);
            this.Controls.Add(this.label_ProductName);
            this.Controls.Add(this.label_Version);
            this.Controls.Add(this.pictureBoxIcon);
            this.Controls.Add(this.label_CompanyName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVersion";
            this.Text = "frmVersion.cs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVersion_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmVersion_FormClosed);
            this.Load += new System.EventHandler(this.frmVersion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label label_CompanyName;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        public System.Windows.Forms.Label label_Version;
        public System.Windows.Forms.Label label_ProductName;
        public System.Windows.Forms.Label label_Copyright;
        public System.Windows.Forms.Label label_Description;
        private System.Windows.Forms.Button buttonOK;
    }
}

