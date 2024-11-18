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
    public partial class frmReleaseLicense : Form
    {
        clsDetainLicense _DetainLicense;
        int _DetainID;
        private void _FillDetainInfo()
        {
            if(_DetainLicense==null)
            {
                return;
            }

            labDetainID.Text = _DetainLicense.DetainID.ToString();
            labLicenseID.Text = ctrlFilterLicense1.LicenseID.ToString();
            labDetainDate.Text = _DetainLicense.DetainDate.ToShortDateString();
            labFineFees.Text = _DetainLicense.FineFees.ToString();

            clsApplicationTypes applicationType = clsApplicationTypes.FindApplicationTypeByID(5);


            labTotalFees.Text = Convert.ToString(_DetainLicense.FineFees + applicationType.ApplicationFees);

        }


        public frmReleaseLicense()
        {
            InitializeComponent();

        }

        public frmReleaseLicense(int DetainID)
        {
            InitializeComponent();

            _DetainLicense = clsDetainLicense.Find(DetainID);

            if( _DetainLicense==null )
            {
                return;
            }

            ctrlFilterLicense1.SetLicenseID(_DetainLicense.LicenseID); ;
            

            if (clsDetainLicense.CheckIfLicenseIsdetainedAndNotRelease(ref _DetainID, _DetainLicense.LicenseID))
            {
                btnRelease.Enabled = true;
            }

            _FillDetainInfo();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabelShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsLicense license = clsLicense.Find(ctrlFilterLicense1.LicenseID);
            if (license == null)
            {
                return;
            }

            clsDrivers driver = clsDrivers.FindDriverByDriverID(license.DriverID);

            if (driver == null)
            {
                return;
            }

            frmLicenseHistory frm = new frmLicenseHistory(driver.PersonID);
            frm.ShowDialog();
        }

        private void linkLabelShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (_DetainLicense == null)
            {
                return;
            }

            frmLicenseInfo frm = new frmLicenseInfo(_DetainLicense.LicenseID);
            frm.ShowDialog();
        }

        private void ctrlFilterLicense1_MyEventWhenBtnSearchClick(object sender, EventArgs e)
        {
            linkLabelShowLicenseHistory.Enabled = true;
            btnRelease.Enabled = false;

            clsLicense license = clsLicense.Find(ctrlFilterLicense1.LicenseID);
            if (license == null)
            {
                linkLabelShowLicenseHistory.Enabled = false;
            }

            

            if (!clsDetainLicense.CheckIfLicenseIsdetainedAndNotRelease(ref _DetainID, ctrlFilterLicense1.LicenseID))
            {
                MessageBox.Show("Selected license is not detained, " +
                    "Choose another one",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            btnRelease.Enabled = true;
            labLicenseID.Text = ctrlFilterLicense1.LicenseID.ToString();

            _DetainLicense = clsDetainLicense.Find(_DetainID);

            

            _FillDetainInfo();
        }

        private void frmReleaseLicense_Load(object sender, EventArgs e)
        {
            clsApplicationTypes applicationType = clsApplicationTypes.FindApplicationTypeByID(5);

            labApplicationFees.Text = applicationType.ApplicationFees.ToString();

            labCreatedBy.Text = clsSetting.UserInfo.UserName;

        }

        private bool _ReleaseDetainLicense()
        {
            clsReleaseDetainedLicense ReleaseDetainedLicense = new clsReleaseDetainedLicense();

            ReleaseDetainedLicense.LicenseID = _DetainLicense.LicenseID;
            ReleaseDetainedLicense.CreatedByUserID = _DetainLicense.CreatedByUserID;

            return ReleaseDetainedLicense.Save();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if(_DetainLicense == null)
            {
                return;
            }

            if (MessageBox.Show("Are you sure you want to Release License?", "Quection", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            if (!_ReleaseDetainLicense())
            {
                MessageBox.Show("Data didnot save successfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("License Released successfuly ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _DetainLicense = null;
            _DetainLicense = clsDetainLicense.Find(_DetainID);

            if(_DetainLicense == null)
            {
                return;
            }

            labReleaseApplicationID.Text = _DetainLicense.ReleaseApplicationID.ToString();

            btnRelease.Enabled = false;

            linkLabelShowLicenseInfo.Enabled = true;
        }
    }
}
