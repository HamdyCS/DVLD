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
    public partial class frmLogin : Form
    {
        private bool _ShowPassword = false;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            btnShowAndHidePassword.Image = imageList1.Images[0];
            _ShowPassword = false;

            string UserName = string.Empty;
            string password = string.Empty;

            if (clsUsers.GetUserInfoFromWindowsRegistry(ref UserName, ref password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = password;
                chkRemeberMe.Checked = true;
                btnLogin.PerformClick();
            }
        }

        private void btnShowAndHidePassword_Click(object sender, EventArgs e)
        {
            if(!_ShowPassword) 
            {
                btnShowAndHidePassword.Image = imageList1.Images[1];
                txtPassword.PasswordChar = '\0';// لالغاء ال password char
                _ShowPassword = true;
            }
            else
            {
                btnShowAndHidePassword.Image = imageList1.Images[0];
                txtPassword.PasswordChar = '*';
                _ShowPassword = false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string UserName = txtUserName.Text;
            string Password = txtPassword.Text;

            if(!clsUsers.CheckIfUserNameInSystem(UserName))
            {
                MessageBox.Show("Invalid user name!", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!clsUsers.CheckIfUserNameAndPasswordIsTrue(UserName,clsUtility.ComputeHash(Password)))
            {
                MessageBox.Show("Wrong password!", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!clsUsers.CheckIfUserIsActive(UserName))
            {
                MessageBox.Show("Your Account is not active!", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //clsUsers.DeleteAllRecordInRemeberUserFile();

            if(chkRemeberMe.Checked)
            {
                clsUsers.SaveUserInfoInWindowsRegistry(UserName,Password);
            }

            //DataTable dtUserInfo = clsUsers.GetUserInfo(UserName);

            //var Row = dtUserInfo.Rows[0];

            //clsSetting.UserInfo.UserID =Convert.ToInt32( Row["UserID"]);
            //clsSetting.UserInfo.PersonID = Convert.ToInt32(Row["PersonID"]);
            //clsSetting.UserInfo.UserName = Row["UserName"].ToString();
            //clsSetting.UserInfo.Password = Row["Password"].ToString();
            //clsSetting.UserInfo.IsActive = Convert.ToBoolean(Row["IsActive"]);

            clsUsers User = clsUsers.FindUserByUserName(UserName);
            if (User == null)
            {
                MessageBox.Show("User Not Found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                clsSetting.UserInfo.UserID = User.UserID;
                clsSetting.UserInfo.UserName = User. UserName;
                clsSetting.UserInfo.Password = User.Password;
                clsSetting.UserInfo.IsActive = User.IsActive;

            }

            Form frm = new frmDVLD();
            this.Hide();
            frm.ShowDialog();
            
            if(clsSetting.CloseApp)
            {
                Application.Exit();
            }
            else
            {
                clsSetting.CloseApp = true;
                this.Show();
               
            }

        }
    }
}
