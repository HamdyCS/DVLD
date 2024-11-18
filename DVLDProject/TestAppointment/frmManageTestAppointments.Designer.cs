namespace DVLDProject
{
    partial class frmManageTestAppointments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageTestAppointments));
            this.labHeader = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.picTestPhoto = new System.Windows.Forms.PictureBox();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.labNumberOfRecords = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnAddNewAppointments = new Guna.UI2.WinForms.Guna2Button();
            this.ctrlLDLApplicationInfo1 = new DVLDProject.ctrlLDLApplicationInfo();
            ((System.ComponentModel.ISupportInitialize)(this.picTestPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labHeader
            // 
            this.labHeader.BackColor = System.Drawing.Color.White;
            this.labHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labHeader.ForeColor = System.Drawing.Color.Red;
            this.labHeader.Location = new System.Drawing.Point(321, 112);
            this.labHeader.Name = "labHeader";
            this.labHeader.Size = new System.Drawing.Size(358, 35);
            this.labHeader.TabIndex = 24;
            this.labHeader.Text = "Vision Test Appointments";
            // 
            // picTestPhoto
            // 
            this.picTestPhoto.Image = global::DVLDProject.Properties.Resources.Vision_512;
            this.picTestPhoto.Location = new System.Drawing.Point(395, 3);
            this.picTestPhoto.Name = "picTestPhoto";
            this.picTestPhoto.Size = new System.Drawing.Size(202, 112);
            this.picTestPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTestPhoto.TabIndex = 25;
            this.picTestPhoto.TabStop = false;
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
            this.btnClose.Location = new System.Drawing.Point(871, 761);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 37);
            this.btnClose.TabIndex = 30;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // labNumberOfRecords
            // 
            this.labNumberOfRecords.AutoSize = true;
            this.labNumberOfRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labNumberOfRecords.Location = new System.Drawing.Point(168, 761);
            this.labNumberOfRecords.Name = "labNumberOfRecords";
            this.labNumberOfRecords.Size = new System.Drawing.Size(27, 29);
            this.labNumberOfRecords.TabIndex = 29;
            this.labNumberOfRecords.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 761);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 29);
            this.label2.TabIndex = 28;
            this.label2.Text = "# Records:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(12, 599);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(958, 156);
            this.dataGridView1.TabIndex = 27;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.takeTestToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::DVLDProject.Properties.Resources.edit_32;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // takeTestToolStripMenuItem
            // 
            this.takeTestToolStripMenuItem.Image = global::DVLDProject.Properties.Resources.Test_32;
            this.takeTestToolStripMenuItem.Name = "takeTestToolStripMenuItem";
            this.takeTestToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.takeTestToolStripMenuItem.Text = "Take Test";
            this.takeTestToolStripMenuItem.Click += new System.EventHandler(this.takeTestToolStripMenuItem_Click);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.White;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(15, 548);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(169, 31);
            this.guna2HtmlLabel1.TabIndex = 31;
            this.guna2HtmlLabel1.Text = "Appointments:";
            // 
            // btnAddNewAppointments
            // 
            this.btnAddNewAppointments.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddNewAppointments.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddNewAppointments.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddNewAppointments.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddNewAppointments.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAddNewAppointments.ForeColor = System.Drawing.Color.White;
            this.btnAddNewAppointments.Image = global::DVLDProject.Properties.Resources.AddAppointment_32;
            this.btnAddNewAppointments.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnAddNewAppointments.Location = new System.Drawing.Point(870, 556);
            this.btnAddNewAppointments.Name = "btnAddNewAppointments";
            this.btnAddNewAppointments.Size = new System.Drawing.Size(99, 37);
            this.btnAddNewAppointments.TabIndex = 32;
            this.btnAddNewAppointments.Text = "Add";
            this.btnAddNewAppointments.Click += new System.EventHandler(this.btnAddNewAppointments_Click);
            // 
            // ctrlLDLApplicationInfo1
            // 
            this.ctrlLDLApplicationInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlLDLApplicationInfo1.LDLApplicationID = 0;
            this.ctrlLDLApplicationInfo1.Location = new System.Drawing.Point(4, 153);
            this.ctrlLDLApplicationInfo1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ctrlLDLApplicationInfo1.Name = "ctrlLDLApplicationInfo1";
            this.ctrlLDLApplicationInfo1.Size = new System.Drawing.Size(957, 396);
            this.ctrlLDLApplicationInfo1.TabIndex = 26;
            // 
            // frmManageTestAppointments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 802);
            this.Controls.Add(this.btnAddNewAppointments);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.labNumberOfRecords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ctrlLDLApplicationInfo1);
            this.Controls.Add(this.picTestPhoto);
            this.Controls.Add(this.labHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmManageTestAppointments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vision Test Appointments";
            this.Load += new System.EventHandler(this.frmManageTestAppointments_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.picTestPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel labHeader;
        private System.Windows.Forms.PictureBox picTestPhoto;
        private ctrlLDLApplicationInfo ctrlLDLApplicationInfo1;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private System.Windows.Forms.Label labNumberOfRecords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Button btnAddNewAppointments;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeTestToolStripMenuItem;
    }
}