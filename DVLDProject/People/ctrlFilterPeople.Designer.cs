namespace DVLDProject
{
    partial class ctrlFilterPeople
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.btnAddNewPerson = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnSearch = new Guna.UI2.WinForms.Guna2CircleButton();
            this.txtFilterBy = new Guna.UI2.WinForms.Guna2TextBox();
            this.comFilterBy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labFilterBY = new System.Windows.Forms.Label();
            this.ctrlPersonInfo1 = new DVLDProject.People.ctrlPersonInfo();
            this.gbFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.btnAddNewPerson);
            this.gbFilter.Controls.Add(this.btnSearch);
            this.gbFilter.Controls.Add(this.txtFilterBy);
            this.gbFilter.Controls.Add(this.comFilterBy);
            this.gbFilter.Controls.Add(this.labFilterBY);
            this.gbFilter.Location = new System.Drawing.Point(5, 12);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(969, 100);
            this.gbFilter.TabIndex = 3;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // btnAddNewPerson
            // 
            this.btnAddNewPerson.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddNewPerson.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddNewPerson.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddNewPerson.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddNewPerson.FillColor = System.Drawing.Color.White;
            this.btnAddNewPerson.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAddNewPerson.ForeColor = System.Drawing.Color.White;
            this.btnAddNewPerson.Image = global::DVLDProject.Properties.Resources.Add_Person_40;
            this.btnAddNewPerson.ImageSize = new System.Drawing.Size(50, 50);
            this.btnAddNewPerson.Location = new System.Drawing.Point(852, 22);
            this.btnAddNewPerson.Name = "btnAddNewPerson";
            this.btnAddNewPerson.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnAddNewPerson.Size = new System.Drawing.Size(92, 72);
            this.btnAddNewPerson.TabIndex = 29;
            this.btnAddNewPerson.Click += new System.EventHandler(this.btnAddNewPerson_Click_1);
            // 
            // btnSearch
            // 
            this.btnSearch.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSearch.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSearch.FillColor = System.Drawing.Color.White;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Image = global::DVLDProject.Properties.Resources.SearchPerson1;
            this.btnSearch.ImageSize = new System.Drawing.Size(50, 50);
            this.btnSearch.Location = new System.Drawing.Point(745, 22);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnSearch.Size = new System.Drawing.Size(92, 72);
            this.btnSearch.TabIndex = 28;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click_1);
            // 
            // txtFilterBy
            // 
            this.txtFilterBy.BackColor = System.Drawing.Color.White;
            this.txtFilterBy.BorderColor = System.Drawing.Color.Black;
            this.txtFilterBy.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.txtFilterBy.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFilterBy.DefaultText = "";
            this.txtFilterBy.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtFilterBy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtFilterBy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFilterBy.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFilterBy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFilterBy.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtFilterBy.ForeColor = System.Drawing.Color.Black;
            this.txtFilterBy.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFilterBy.Location = new System.Drawing.Point(407, 36);
            this.txtFilterBy.Margin = new System.Windows.Forms.Padding(6);
            this.txtFilterBy.Name = "txtFilterBy";
            this.txtFilterBy.PasswordChar = '\0';
            this.txtFilterBy.PlaceholderText = "";
            this.txtFilterBy.SelectedText = "";
            this.txtFilterBy.Size = new System.Drawing.Size(300, 34);
            this.txtFilterBy.TabIndex = 27;
            this.txtFilterBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterBy_KeyPress_1);
            // 
            // comFilterBy
            // 
            this.comFilterBy.BackColor = System.Drawing.Color.Transparent;
            this.comFilterBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comFilterBy.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comFilterBy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.comFilterBy.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.comFilterBy.ForeColor = System.Drawing.Color.Black;
            this.comFilterBy.ItemHeight = 30;
            this.comFilterBy.Items.AddRange(new object[] {
            "Person ID",
            "National No"});
            this.comFilterBy.Location = new System.Drawing.Point(134, 34);
            this.comFilterBy.Name = "comFilterBy";
            this.comFilterBy.Size = new System.Drawing.Size(260, 36);
            this.comFilterBy.StartIndex = 1;
            this.comFilterBy.TabIndex = 26;
            this.comFilterBy.SelectedIndexChanged += new System.EventHandler(this.comFilterBy_SelectedIndexChanged);
            // 
            // labFilterBY
            // 
            this.labFilterBY.AutoSize = true;
            this.labFilterBY.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labFilterBY.ForeColor = System.Drawing.Color.Black;
            this.labFilterBY.Location = new System.Drawing.Point(8, 34);
            this.labFilterBY.Name = "labFilterBY";
            this.labFilterBY.Size = new System.Drawing.Size(107, 29);
            this.labFilterBY.TabIndex = 25;
            this.labFilterBY.Text = "Filter By:";
            // 
            // ctrlPersonInfo1
            // 
            this.ctrlPersonInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlPersonInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlPersonInfo1.Location = new System.Drawing.Point(5, 120);
            this.ctrlPersonInfo1.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlPersonInfo1.Name = "ctrlPersonInfo1";
            this.ctrlPersonInfo1.PersonID = 0;
            this.ctrlPersonInfo1.Size = new System.Drawing.Size(983, 330);
            this.ctrlPersonInfo1.TabIndex = 4;
            // 
            // ctrlFilterPeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrlPersonInfo1);
            this.Controls.Add(this.gbFilter);
            this.Name = "ctrlFilterPeople";
            this.Size = new System.Drawing.Size(996, 453);
            this.Load += new System.EventHandler(this.ctrlFilterPeople_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ctrlFilterPeople_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ctrlFilterPeople_PreviewKeyDown);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFilter;
        private Guna.UI2.WinForms.Guna2CircleButton btnAddNewPerson;
        private Guna.UI2.WinForms.Guna2CircleButton btnSearch;
        private Guna.UI2.WinForms.Guna2TextBox txtFilterBy;
        private Guna.UI2.WinForms.Guna2ComboBox comFilterBy;
        private System.Windows.Forms.Label labFilterBY;
        private People.ctrlPersonInfo ctrlPersonInfo1;
    }
}
