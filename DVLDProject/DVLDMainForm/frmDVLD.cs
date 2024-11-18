
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject
{
    public partial class frmDVLD : Form
    {
        public frmDVLD()
        {
            InitializeComponent();
        }

        
        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
           //MessageBox.Show("People screen will be here!");
            Form frm  = new frmManagePeople();

            frm.ShowDialog();
            
            
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsSetting.CloseApp = false;
            this.Close();
            
        }

        
        private void driversToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //MessageBox.Show("Drivers screen will be here!");
            frmManageDrivers frm = new frmManageDrivers();
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //MessageBox.Show("Users screen will be here!");

            Form frm = new frmManageUsers();
            frm.ShowDialog();

        }

        
        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
             Form frm = new frmUserInfo(clsSetting.UserInfo.UserID);
            frm.ShowDialog();

            
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangePassword(clsSetting.UserInfo.UserID);
            frm.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm  = new frmManageApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes ();
            frm.ShowDialog();
            
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewAndUpdateLocalDrivingLicenseApplication frm = new frmNewAndUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationsList frm = new frmLocalDrivingLicenseApplicationsList();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void iToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageInternationalLicense frm = new frmManageInternationalLicense();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLicense frm = new frmRenewLicense();
            frm.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceForDamageOrLostLicense frm = new frmReplaceForDamageOrLostLicense();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationsList frm = new frmLocalDrivingLicenseApplicationsList();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseLicense frm = new frmReleaseLicense();    
            frm.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseLicense frm = new frmReleaseLicense();
            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicense frm = new frmListDetainedLicense();
            frm.ShowDialog();
        }
    }
}
