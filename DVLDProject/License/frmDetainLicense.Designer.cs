namespace DVLDProject
{
    partial class frmDetainLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDetainLicense));
            this.labTitle = new System.Windows.Forms.Label();
            this.gpApplicationInfo = new System.Windows.Forms.GroupBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.labLicenseID = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDetain = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnClose = new Guna.UI2.WinForms.Guna2CircleButton();
            this.linkLabelShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.linkLabelShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.labCreatedBy = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.label14 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.labDetainDate = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labDetainID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFineFees = new System.Windows.Forms.TextBox();
            this.ctrlFilterLicense1 = new DVLDProject.ctrlFilterLicense();
            this.gpApplicationInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labTitle
            // 
            this.labTitle.AutoSize = true;
            this.labTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTitle.ForeColor = System.Drawing.Color.Red;
            this.labTitle.Location = new System.Drawing.Point(592, 9);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(241, 37);
            this.labTitle.TabIndex = 10;
            this.labTitle.Text = "Detain License";
            // 
            // gpApplicationInfo
            // 
            this.gpApplicationInfo.Controls.Add(this.txtFineFees);
            this.gpApplicationInfo.Controls.Add(this.pictureBox7);
            this.gpApplicationInfo.Controls.Add(this.labLicenseID);
            this.gpApplicationInfo.Controls.Add(this.label6);
            this.gpApplicationInfo.Controls.Add(this.btnDetain);
            this.gpApplicationInfo.Controls.Add(this.btnClose);
            this.gpApplicationInfo.Controls.Add(this.linkLabelShowLicenseInfo);
            this.gpApplicationInfo.Controls.Add(this.linkLabelShowLicenseHistory);
            this.gpApplicationInfo.Controls.Add(this.pictureBox11);
            this.gpApplicationInfo.Controls.Add(this.labCreatedBy);
            this.gpApplicationInfo.Controls.Add(this.label20);
            this.gpApplicationInfo.Controls.Add(this.pictureBox8);
            this.gpApplicationInfo.Controls.Add(this.label14);
            this.gpApplicationInfo.Controls.Add(this.pictureBox5);
            this.gpApplicationInfo.Controls.Add(this.labDetainDate);
            this.gpApplicationInfo.Controls.Add(this.label99);
            this.gpApplicationInfo.Controls.Add(this.pictureBox1);
            this.gpApplicationInfo.Controls.Add(this.labDetainID);
            this.gpApplicationInfo.Controls.Add(this.label2);
            this.gpApplicationInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.gpApplicationInfo.Location = new System.Drawing.Point(999, 79);
            this.gpApplicationInfo.Name = "gpApplicationInfo";
            this.gpApplicationInfo.Size = new System.Drawing.Size(416, 559);
            this.gpApplicationInfo.TabIndex = 12;
            this.gpApplicationInfo.TabStop = false;
            this.gpApplicationInfo.Text = "Detain Info";
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::DVLDProject.Properties.Resources.Number_32;
            this.pictureBox7.Location = new System.Drawing.Point(188, 92);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(27, 20);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 37;
            this.pictureBox7.TabStop = false;
            // 
            // labLicenseID
            // 
            this.labLicenseID.AutoSize = true;
            this.labLicenseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labLicenseID.Location = new System.Drawing.Point(221, 88);
            this.labLicenseID.Name = "labLicenseID";
            this.labLicenseID.Size = new System.Drawing.Size(45, 24);
            this.labLicenseID.TabIndex = 36;
            this.labLicenseID.Text = "N\\A";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(50, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 24);
            this.label6.TabIndex = 35;
            this.label6.Text = "License ID:";
            // 
            // btnDetain
            // 
            this.btnDetain.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDetain.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDetain.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDetain.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDetain.Enabled = false;
            this.btnDetain.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetain.ForeColor = System.Drawing.Color.White;
            this.btnDetain.Location = new System.Drawing.Point(319, 481);
            this.btnDetain.Name = "btnDetain";
            this.btnDetain.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnDetain.Size = new System.Drawing.Size(79, 72);
            this.btnDetain.TabIndex = 34;
            this.btnDetain.Text = "Detain";
            this.btnDetain.Click += new System.EventHandler(this.btnDetain_Click);
            // 
            // btnClose
            // 
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(225, 481);
            this.btnClose.Name = "btnClose";
            this.btnClose.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnClose.Size = new System.Drawing.Size(79, 72);
            this.btnClose.TabIndex = 33;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // linkLabelShowLicenseInfo
            // 
            this.linkLabelShowLicenseInfo.AutoSize = true;
            this.linkLabelShowLicenseInfo.Enabled = false;
            this.linkLabelShowLicenseInfo.Location = new System.Drawing.Point(60, 358);
            this.linkLabelShowLicenseInfo.Name = "linkLabelShowLicenseInfo";
            this.linkLabelShowLicenseInfo.Size = new System.Drawing.Size(187, 25);
            this.linkLabelShowLicenseInfo.TabIndex = 32;
            this.linkLabelShowLicenseInfo.TabStop = true;
            this.linkLabelShowLicenseInfo.Text = "Show License Info";
            this.linkLabelShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelShowLicenseInfo_LinkClicked);
            // 
            // linkLabelShowLicenseHistory
            // 
            this.linkLabelShowLicenseHistory.AutoSize = true;
            this.linkLabelShowLicenseHistory.Enabled = false;
            this.linkLabelShowLicenseHistory.Location = new System.Drawing.Point(28, 315);
            this.linkLabelShowLicenseHistory.Name = "linkLabelShowLicenseHistory";
            this.linkLabelShowLicenseHistory.Size = new System.Drawing.Size(219, 25);
            this.linkLabelShowLicenseHistory.TabIndex = 31;
            this.linkLabelShowLicenseHistory.TabStop = true;
            this.linkLabelShowLicenseHistory.Text = "Show License History";
            this.linkLabelShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelShowLicenseHistory_LinkClicked);
            // 
            // pictureBox11
            // 
            this.pictureBox11.Image = global::DVLDProject.Properties.Resources.User_32__2;
            this.pictureBox11.Location = new System.Drawing.Point(188, 227);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(27, 20);
            this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox11.TabIndex = 30;
            this.pictureBox11.TabStop = false;
            // 
            // labCreatedBy
            // 
            this.labCreatedBy.AutoSize = true;
            this.labCreatedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCreatedBy.Location = new System.Drawing.Point(221, 223);
            this.labCreatedBy.Name = "labCreatedBy";
            this.labCreatedBy.Size = new System.Drawing.Size(45, 24);
            this.labCreatedBy.TabIndex = 29;
            this.labCreatedBy.Text = "N\\A";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(46, 223);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(118, 24);
            this.label20.TabIndex = 28;
            this.label20.Text = "Created By:";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::DVLDProject.Properties.Resources.money_32;
            this.pictureBox8.Location = new System.Drawing.Point(188, 181);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(27, 20);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 18;
            this.pictureBox8.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(53, 177);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(111, 24);
            this.label14.TabIndex = 16;
            this.label14.Text = "Fine Fees:";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::DVLDProject.Properties.Resources.Calendar_32;
            this.pictureBox5.Location = new System.Drawing.Point(188, 135);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(27, 20);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 12;
            this.pictureBox5.TabStop = false;
            // 
            // labDetainDate
            // 
            this.labDetainDate.AutoSize = true;
            this.labDetainDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDetainDate.Location = new System.Drawing.Point(221, 131);
            this.labDetainDate.Name = "labDetainDate";
            this.labDetainDate.Size = new System.Drawing.Size(45, 24);
            this.labDetainDate.TabIndex = 11;
            this.labDetainDate.Text = "N\\A";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label99.Location = new System.Drawing.Point(42, 131);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(122, 24);
            this.label99.TabIndex = 10;
            this.label99.Text = "Detain Data:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLDProject.Properties.Resources.Number_32;
            this.pictureBox1.Location = new System.Drawing.Point(188, 46);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(27, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // labDetainID
            // 
            this.labDetainID.AutoSize = true;
            this.labDetainID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDetainID.Location = new System.Drawing.Point(221, 42);
            this.labDetainID.Name = "labDetainID";
            this.labDetainID.Size = new System.Drawing.Size(45, 24);
            this.labDetainID.TabIndex = 8;
            this.labDetainID.Text = "N\\A";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(64, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "Detain ID:";
            // 
            // txtFineFees
            // 
            this.txtFineFees.Location = new System.Drawing.Point(225, 173);
            this.txtFineFees.Name = "txtFineFees";
            this.txtFineFees.Size = new System.Drawing.Size(188, 31);
            this.txtFineFees.TabIndex = 38;
            this.txtFineFees.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFineFees_KeyPress);
            // 
            // ctrlFilterLicense1
            // 
            this.ctrlFilterLicense1.LicenseID = 0;
            this.ctrlFilterLicense1.Location = new System.Drawing.Point(12, 62);
            this.ctrlFilterLicense1.Name = "ctrlFilterLicense1";
            this.ctrlFilterLicense1.Size = new System.Drawing.Size(976, 570);
            this.ctrlFilterLicense1.TabIndex = 11;
            this.ctrlFilterLicense1.MyEventWhenBtnSearchClick += new System.EventHandler(this.ctrlFilterLicense1_MyEventWhenBtnSearchClick);
            // 
            // frmDetainLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1424, 650);
            this.Controls.Add(this.ctrlFilterLicense1);
            this.Controls.Add(this.labTitle);
            this.Controls.Add(this.gpApplicationInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDetainLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detain License";
            this.Load += new System.EventHandler(this.frmDetainLicense_Load);
            this.gpApplicationInfo.ResumeLayout(false);
            this.gpApplicationInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlFilterLicense ctrlFilterLicense1;
        private System.Windows.Forms.Label labTitle;
        private System.Windows.Forms.GroupBox gpApplicationInfo;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label labLicenseID;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2CircleButton btnDetain;
        private Guna.UI2.WinForms.Guna2CircleButton btnClose;
        private System.Windows.Forms.LinkLabel linkLabelShowLicenseInfo;
        private System.Windows.Forms.LinkLabel linkLabelShowLicenseHistory;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.Label labCreatedBy;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label labDetainDate;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labDetainID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFineFees;
    }
}