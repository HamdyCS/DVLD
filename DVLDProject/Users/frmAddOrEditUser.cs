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
    public partial class frmAddOrEditUser : Form
    {
        DataTable dt;
        DataView dv;

        enum enMode {eAddNewUser = 1,eUpdateUser=2 };
        enMode _Mode;

        clsUsers _User = null;

        private void ChangeFormToUpdateMode()
        {
            _Mode = enMode.eUpdateUser;
            this.Text = "Update User";

            if(_User!=null)
            {
                labHeader.Text = "Update User";
                gbFilter.Enabled = false;
                ctrlPersonInfo1.SetPersonID(_User.PersonID);

                labUserID.Text = _User.UserID.ToString();

                txtUserName.Text = _User.UserName;
                txtPassword .Text = _User.Password;
                txtConfirmPassword.Text = _User.Password;

                ckIsActive.Checked = _User.IsActive;
            }
        }

        public frmAddOrEditUser()
        {
            InitializeComponent();
            _Mode = enMode.eAddNewUser;
        }

        public frmAddOrEditUser(int UserID)
        {
            InitializeComponent();
            _User = clsUsers.FindUserByUserID(UserID);
            ChangeFormToUpdateMode();
        }
        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if(e.KeyChar == (char)Keys.Enter)
            {
                btnSearch.PerformClick();
            }

            if ((comFilterBy.Text == "Person ID"))
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    // إذا كان ليس رقماً، فقم بإلغاء الحرف المكتوب
                    e.Handled = true;

                }
            }
        }

        private void frmAddOrEditUser_Load(object sender, EventArgs e)
        {
            if(_Mode== enMode.eAddNewUser) 
            {
                dt = clsPeople.GetLittleInfoAboutPeople();
                dv = dt.DefaultView;
                txtFilterBy.Focus();
                btnSave.Enabled = false;
            }
            else if(_Mode == enMode.eUpdateUser)
            {
                gbFilter.Enabled = false;
            }


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sq;

            if((comFilterBy.Text == "Person ID"))
            {
                sq = "";
            }
            else
            {
                sq = "'";
            }

            try
            {
            dv.RowFilter = $"[{comFilterBy.Text}] =  {sq}{txtFilterBy.Text}{sq} ";
                ctrlPersonInfo1.SetPersonID(Convert.ToInt32(dv[0][0]));
                
            }
            catch(Exception ex)
            {
                ctrlPersonInfo1.SetPersonID(-1);
               
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(_Mode==enMode.eAddNewUser)
            {

            if(clsUsers.CheckIfThisPersonIsUserByPersonID(Convert.ToInt32(dv[0][0])))
            {
                MessageBox.Show("Selected Person already has a user choose another one", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            }

            tabControl1.SelectTab(1);
            btnSave.Enabled = true;
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddOrEditPerson frm = new frmAddOrEditPerson();
            frm.DataBack += FormAddNewPersonDataBack;
            frm.ShowDialog();
        }

        private void FormAddNewPersonDataBack(int PersonID)
        {
            comFilterBy.SelectedIndex = 0;
            txtFilterBy.Text = PersonID.ToString();
            //ctrlPersonInfo1.SetPersonID(PersonID);

            dt = clsPeople.GetLittleInfoAboutPeople();
            dv = dt.DefaultView;
            try
            {
                dv.RowFilter = $"[{comFilterBy.Text}] =  {txtFilterBy.Text} ";
                ctrlPersonInfo1.SetPersonID(Convert.ToInt32(dv[0][0]));

            }
            catch (Exception ex)
            {
                ctrlPersonInfo1.SetPersonID(-1);

            }
        }

        private void txtUserName_Validated(object sender, EventArgs e)
        {

        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (_Mode == enMode.eAddNewUser)
            {
                if (clsUsers.CheckIfUserNameInSystem(txtUserName.Text))
                {
                    errorProvider1.SetError(txtUserName, "User Name aready in system choose another one");
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtUserName, "");

                }
            }
            else if(_Mode == enMode.eUpdateUser)
            {
                if (txtUserName.Text == _User.UserName) 
                {
                    return;
                }

                if (clsUsers.CheckIfUserNameInSystem(txtUserName.Text))
                {
                    errorProvider1.SetError(txtUserName, "User Name aready in system choose another one");
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtUserName, "");

                }
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text==string.Empty)
            {
                errorProvider1.SetError(txtPassword, "Please set password");
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPassword, "");

            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text == string.Empty || txtConfirmPassword.Text != txtPassword.Text)
            {
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password shoud be same New password");
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");

            }
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Form not vaild", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtConfirmPassword.Text==string.Empty||txtUserName.Text==string.Empty)
            {
                MessageBox.Show("Please set user name and password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            if(_Mode==enMode.eAddNewUser)
            {
                _User = new clsUsers(ctrlPersonInfo1.PersonID);
                _User.UserName = txtUserName.Text;
                _User.Password = txtPassword.Text;
                
                _User.IsActive = ckIsActive.Checked;
             
            }
            else if(_Mode==enMode.eUpdateUser) 
            {
                _User.UserName=txtUserName.Text;
                _User.Password=txtPassword.Text;
                _User.IsActive = ckIsActive.Checked;
            }

            if (_User.save())
            {
                MessageBox.Show("Data saved successfuly", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChangeFormToUpdateMode();
            }
            else
            {
                MessageBox.Show("Data didnot save successfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Focus();
        }

       
    }
}
