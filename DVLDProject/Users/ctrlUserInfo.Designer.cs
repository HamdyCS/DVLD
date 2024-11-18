namespace DVLDProject
{
    partial class ctrlUserInfo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labIsActive = new System.Windows.Forms.Label();
            this.labUserName = new System.Windows.Forms.Label();
            this.labUserID = new System.Windows.Forms.Label();
            this.ctrlPersonInfo1 = new DVLDProject.People.ctrlPersonInfo();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.labIsActive);
            this.groupBox1.Controls.Add(this.labUserName);
            this.groupBox1.Controls.Add(this.labUserID);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 343);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(981, 186);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login Information:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(319, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 29);
            this.label4.TabIndex = 52;
            this.label4.Text = "Is Active:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(319, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 29);
            this.label3.TabIndex = 51;
            this.label3.Text = "User Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(319, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 29);
            this.label2.TabIndex = 50;
            this.label2.Text = "User ID:";
            // 
            // labIsActive
            // 
            this.labIsActive.AutoSize = true;
            this.labIsActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labIsActive.ForeColor = System.Drawing.Color.Blue;
            this.labIsActive.Location = new System.Drawing.Point(537, 125);
            this.labIsActive.Name = "labIsActive";
            this.labIsActive.Size = new System.Drawing.Size(47, 25);
            this.labIsActive.TabIndex = 48;
            this.labIsActive.Text = "N/A";
            // 
            // labUserName
            // 
            this.labUserName.AutoSize = true;
            this.labUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labUserName.ForeColor = System.Drawing.Color.Blue;
            this.labUserName.Location = new System.Drawing.Point(537, 76);
            this.labUserName.Name = "labUserName";
            this.labUserName.Size = new System.Drawing.Size(47, 25);
            this.labUserName.TabIndex = 47;
            this.labUserName.Text = "N/A";
            // 
            // labUserID
            // 
            this.labUserID.AutoSize = true;
            this.labUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labUserID.ForeColor = System.Drawing.Color.Blue;
            this.labUserID.Location = new System.Drawing.Point(537, 27);
            this.labUserID.Name = "labUserID";
            this.labUserID.Size = new System.Drawing.Size(47, 25);
            this.labUserID.TabIndex = 46;
            this.labUserID.Text = "N/A";
            // 
            // ctrlPersonInfo1
            // 
            this.ctrlPersonInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlPersonInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlPersonInfo1.Location = new System.Drawing.Point(1, 5);
            this.ctrlPersonInfo1.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlPersonInfo1.Name = "ctrlPersonInfo1";
            this.ctrlPersonInfo1.PersonID = 0;
            this.ctrlPersonInfo1.Size = new System.Drawing.Size(983, 330);
            this.ctrlPersonInfo1.TabIndex = 38;
            // 
            // ctrlUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ctrlPersonInfo1);
            this.Name = "ctrlUserInfo";
            this.Size = new System.Drawing.Size(989, 543);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private People.ctrlPersonInfo ctrlPersonInfo1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labIsActive;
        private System.Windows.Forms.Label labUserName;
        private System.Windows.Forms.Label labUserID;
    }
}
