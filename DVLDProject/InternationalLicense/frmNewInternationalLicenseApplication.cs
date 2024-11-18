using DVLDBusinessLayer;
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
    public partial class frmNewInternationalLicenseApplication : Form
    {
        clsLicense _License;
        clsDrivers _Driver;
        clsInternationalLicense _InternationalLicense;
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void linkLabelShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsLicense License = clsLicense.Find(ctrlFilterLicense1.LicenseID);

            if(License == null ) 
            {
                return;
            }

            clsDrivers Driver =  clsDrivers.FindDriverByDriverID(License.DriverID);

            if( Driver == null ) 
            {
                return; 
            }
            frmLicenseHistory frm = new frmLicenseHistory(Driver.PersonID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabelShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            frmInternationalLicenseInfo frm = new frmInternationalLicenseInfo(_InternationalLicense.InternationalLicenseID);
            frm.ShowDialog();
        }

        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            labApplicationDate.Text = DateTime.Now.ToShortDateString();
            labIssuaDate.Text = DateTime.Now.ToShortDateString();

            clsApplicationTypes applicationType = clsApplicationTypes.FindApplicationTypeByID(6);

            if(applicationType!= null)
            {
                labFees.Text = applicationType.ApplicationFees.ToString();
            }

            labExpritionDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            labCreatedBy.Text = clsSetting.UserInfo.UserName;

        }

        private bool _AddNewInternationalLicense()
        {
            _InternationalLicense = new clsInternationalLicense(); 
            if(_Driver == null)
            {
                return false;
            }
            _InternationalLicense.DriverID = _Driver.DriverID;
            _InternationalLicense.IssuedUsingLocalLicenseID = ctrlFilterLicense1.LicenseID;
            _InternationalLicense.CreatedByUserID = clsSetting.UserInfo.UserID;

            return _InternationalLicense.Save();
        }

        private void btnIssua_Click(object sender, EventArgs e)
        {
            _License = clsLicense.Find(ctrlFilterLicense1.LicenseID);

            if( _License == null ) 
            {
                MessageBox.Show("License Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Driver = clsDrivers.FindDriverByDriverID(_License.DriverID);

            if (_Driver == null)
            {
                MessageBox.Show("Driver Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int InternationalLicenseID = 0;

            if (!clsInternationalLicense.CheckIfThisPersonDonotHaveActiveInternationalLicense(ref InternationalLicenseID,_Driver.PersonID))
            {
                MessageBox.Show($"Person already have active" +
                    $" international license with ID = {InternationalLicenseID}", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(_AddNewInternationalLicense())
            {
                MessageBox.Show("Data saved successfuly", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                linkLabelShowLicenseInfo.Enabled = true;
            }
            else
            {
                MessageBox.Show("Data didnot save successfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void ctrlFilterLicense1_MyEvent(object sender, EventArgs e)
        {
            int InternationalLicenseID = 0;

            _License = clsLicense.Find(ctrlFilterLicense1.LicenseID);

            if (_License == null)
            {
                linkLabelShowLicenseHistory.Enabled = false;
                return;
            }

            linkLabelShowLicenseHistory.Enabled = true;

            _Driver = clsDrivers.FindDriverByDriverID(_License.DriverID);

            if (_Driver == null)
            {
              
                return;
            }

            if (!clsInternationalLicense.CheckIfThisPersonDonotHaveActiveInternationalLicense(ref InternationalLicenseID, _Driver.PersonID))
            {
                MessageBox.Show($"Person already have active" +
                    $" international license with ID = {InternationalLicenseID}", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

        }
    }
}
