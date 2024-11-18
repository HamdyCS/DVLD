using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLDProject
{
    public partial class frmChangePassword : Form
    {
        clsUsers UserInfo;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            ctrlUserInfo1.SetUserID(UserID);

            UserInfo = clsUsers.FindUserByUserID(UserID);
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            if(clsUtility.ComputeHash( txtCurrentPassword.Text) == UserInfo.Password)
            {
                txtNewPassword.Visible = true;
                txtConfirmPassword.Visible = true;
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if(txtConfirmPassword.Text!= txtNewPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password shoud be same New password");

            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, "");
                e.Cancel = false;
            }
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Form not vaild", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtNewPassword.Text==UserInfo.Password|| txtNewPassword.Text == string.Empty)
            {
                MessageBox.Show("Password didnot changed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UserInfo.Password =clsUtility.ComputeHash( txtNewPassword.Text);

            if(UserInfo.save())
            {
                MessageBox.Show("Password update successfuly", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Password didnot changed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
