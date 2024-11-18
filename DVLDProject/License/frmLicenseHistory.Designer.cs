namespace DVLDProject
{
    partial class frmLicenseHistory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLicenseHistory));
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabLocal = new System.Windows.Forms.TabPage();
            this.dgvLocalLicense = new System.Windows.Forms.DataGridView();
            this.tabInternational = new System.Windows.Forms.TabPage();
            this.dvgInternationalLicense = new System.Windows.Forms.DataGridView();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.labNumberOfRecords = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlPersonInfo1 = new DVLDProject.People.ctrlPersonInfo();
            this.contextMenuStripForLocalLicense = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripForInternationalLicense = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showLicenseInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicense)).BeginInit();
            this.tabInternational.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgInternationalLicense)).BeginInit();
            this.contextMenuStripForLocalLicense.SuspendLayout();
            this.contextMenuStripForInternationalLicense.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.White;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.Red;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(418, 12);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(346, 57);
            this.guna2HtmlLabel1.TabIndex = 14;
            this.guna2HtmlLabel1.Text = "License History";
            // 
            // guna2CirclePictureBox1
            // 
            this.guna2CirclePictureBox1.Image = global::DVLDProject.Properties.Resources.PersonLicenseHistory_512;
            this.guna2CirclePictureBox1.ImageRotate = 0F;
            this.guna2CirclePictureBox1.Location = new System.Drawing.Point(32, 123);
            this.guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            this.guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox1.Size = new System.Drawing.Size(155, 236);
            this.guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2CirclePictureBox1.TabIndex = 13;
            this.guna2CirclePictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::DVLDProject.Properties.Resources.Close_64;
            this.btnClose.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnClose.Location = new System.Drawing.Point(1079, 626);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 45);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabLocal);
            this.tabControl1.Controls.Add(this.tabInternational);
            this.tabControl1.Location = new System.Drawing.Point(32, 419);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1159, 205);
            this.tabControl1.TabIndex = 22;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabLocal
            // 
            this.tabLocal.Controls.Add(this.dgvLocalLicense);
            this.tabLocal.Location = new System.Drawing.Point(4, 22);
            this.tabLocal.Name = "tabLocal";
            this.tabLocal.Padding = new System.Windows.Forms.Padding(3);
            this.tabLocal.Size = new System.Drawing.Size(1151, 179);
            this.tabLocal.TabIndex = 0;
            this.tabLocal.Text = "Local";
            this.tabLocal.UseVisualStyleBackColor = true;
            // 
            // dgvLocalLicense
            // 
            this.dgvLocalLicense.AllowUserToAddRows = false;
            this.dgvLocalLicense.AllowUserToDeleteRows = false;
            this.dgvLocalLicense.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLocalLicense.BackgroundColor = System.Drawing.Color.White;
            this.dgvLocalLicense.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalLicense.ContextMenuStrip = this.contextMenuStripForLocalLicense;
            this.dgvLocalLicense.Location = new System.Drawing.Point(6, 6);
            this.dgvLocalLicense.Name = "dgvLocalLicense";
            this.dgvLocalLicense.ReadOnly = true;
            this.dgvLocalLicense.Size = new System.Drawing.Size(1139, 167);
            this.dgvLocalLicense.TabIndex = 0;
            // 
            // tabInternational
            // 
            this.tabInternational.Controls.Add(this.dvgInternationalLicense);
            this.tabInternational.Location = new System.Drawing.Point(4, 22);
            this.tabInternational.Name = "tabInternational";
            this.tabInternational.Padding = new System.Windows.Forms.Padding(3);
            this.tabInternational.Size = new System.Drawing.Size(1151, 179);
            this.tabInternational.TabIndex = 1;
            this.tabInternational.Text = "International";
            this.tabInternational.UseVisualStyleBackColor = true;
            // 
            // dvgInternationalLicense
            // 
            this.dvgInternationalLicense.AllowUserToAddRows = false;
            this.dvgInternationalLicense.AllowUserToDeleteRows = false;
            this.dvgInternationalLicense.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dvgInternationalLicense.BackgroundColor = System.Drawing.Color.White;
            this.dvgInternationalLicense.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgInternationalLicense.ContextMenuStrip = this.contextMenuStripForInternationalLicense;
            this.dvgInternationalLicense.Location = new System.Drawing.Point(6, 6);
            this.dvgInternationalLicense.Name = "dvgInternationalLicense";
            this.dvgInternationalLicense.ReadOnly = true;
            this.dvgInternationalLicense.Size = new System.Drawing.Size(1139, 167);
            this.dvgInternationalLicense.TabIndex = 1;
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.White;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(36, 386);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(155, 27);
            this.guna2HtmlLabel2.TabIndex = 23;
            this.guna2HtmlLabel2.Text = "Driver License";
            // 
            // labNumberOfRecords
            // 
            this.labNumberOfRecords.AutoSize = true;
            this.labNumberOfRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.labNumberOfRecords.Location = new System.Drawing.Point(162, 630);
            this.labNumberOfRecords.Name = "labNumberOfRecords";
            this.labNumberOfRecords.Size = new System.Drawing.Size(25, 25);
            this.labNumberOfRecords.TabIndex = 27;
            this.labNumberOfRecords.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(31, 630);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 25);
            this.label2.TabIndex = 26;
            this.label2.Text = "# Records:";
            // 
            // ctrlPersonInfo1
            // 
            this.ctrlPersonInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlPersonInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlPersonInfo1.Location = new System.Drawing.Point(214, 85);
            this.ctrlPersonInfo1.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlPersonInfo1.Name = "ctrlPersonInfo1";
            this.ctrlPersonInfo1.PersonID = 0;
            this.ctrlPersonInfo1.Size = new System.Drawing.Size(977, 338);
            this.ctrlPersonInfo1.TabIndex = 15;
            // 
            // contextMenuStripForLocalLicense
            // 
            this.contextMenuStripForLocalLicense.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.contextMenuStripForLocalLicense.Name = "contextMenuStripForLocalLicense";
            this.contextMenuStripForLocalLicense.Size = new System.Drawing.Size(170, 26);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = global::DVLDProject.Properties.Resources.Driver_License_48;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItem2.Text = "Show License Info";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // contextMenuStripForInternationalLicense
            // 
            this.contextMenuStripForInternationalLicense.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLicenseInfoToolStripMenuItem});
            this.contextMenuStripForInternationalLicense.Name = "contextMenuStripForInternationalLicense";
            this.contextMenuStripForInternationalLicense.Size = new System.Drawing.Size(181, 48);
            // 
            // showLicenseInfoToolStripMenuItem
            // 
            this.showLicenseInfoToolStripMenuItem.Image = global::DVLDProject.Properties.Resources.International_32;
            this.showLicenseInfoToolStripMenuItem.Name = "showLicenseInfoToolStripMenuItem";
            this.showLicenseInfoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showLicenseInfoToolStripMenuItem.Text = "Show License Info";
            this.showLicenseInfoToolStripMenuItem.Click += new System.EventHandler(this.showLicenseInfoToolStripMenuItem_Click);
            // 
            // frmLicenseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 675);
            this.Controls.Add(this.labNumberOfRecords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.guna2HtmlLabel2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlPersonInfo1);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.guna2CirclePictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLicenseHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "License History";
            this.Load += new System.EventHandler(this.frmLicenseHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabLocal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicense)).EndInit();
            this.tabInternational.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvgInternationalLicense)).EndInit();
            this.contextMenuStripForLocalLicense.ResumeLayout(false);
            this.contextMenuStripForInternationalLicense.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private People.ctrlPersonInfo ctrlPersonInfo1;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabLocal;
        private System.Windows.Forms.TabPage tabInternational;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private System.Windows.Forms.Label labNumberOfRecords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvLocalLicense;
        private System.Windows.Forms.DataGridView dvgInternationalLicense;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForLocalLicense;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForInternationalLicense;
        private System.Windows.Forms.ToolStripMenuItem showLicenseInfoToolStripMenuItem;
    }
}