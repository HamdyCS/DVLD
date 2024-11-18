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
    public partial class frmRenewLicense : Form
    {
        private clsLicense _OldLicense;
        private clsLicense _NewLicense;
        public frmRenewLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRenewLicense_Load(object sender, EventArgs e)
        {
            labApplicationDate.Text = DateTime.Now.ToShortDateString();
            labIssuaDate.Text = DateTime.Now.ToShortDateString();

            clsApplicationTypes ApplicationType = clsApplicationTypes.FindApplicationTypeByID(2);

            if(ApplicationType != null ) 
            {
                labApplicationFees.Text = ApplicationType.ApplicationFees.ToString();
            }
            labCreatedBy.Text = clsSetting.UserInfo.UserName;
        }

        private void ctrlFilterLicense1_MyEvent(object sender, EventArgs e)
        {
            _OldLicense = clsLicense.Find(ctrlFilterLicense1.LicenseID);

            btnRenew.Enabled = false;
            linkLabelShowLicenseHistory.Enabled = false;
            linkLabelShowLicenseInfo.Enabled = false;


            if ( _OldLicense == null ) 
            {
                MessageBox.Show("License not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }

            if(!clsRenewLicense.CheckIfLicenseIsExpired(_OldLicense.LicenseID))
            {
                MessageBox.Show($"Selected license is not yet " +
                    $"expired, it will expired on: " +
                    $"{_OldLicense.ExpirationDate.ToShortDateString()} ", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!clsRenewLicense.CheckIfLicenseIsActive(ctrlFilterLicense1.LicenseID))
            {
                MessageBox.Show("License is not Active", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            
           labOLDLicenseID.Text = _OldLicense.LicenseID.ToString();

            clsLicenseClasses LicenseClass = clsLicenseClasses.Find(_OldLicense.LicenseClass);

            if (LicenseClass != null)
            {
                labLiceenseFees.Text = LicenseClass.ClassFees.ToString();
                labTotalFees.Text = (Convert.ToInt32(labApplicationFees.Text.ToString()) + LicenseClass.ClassFees).ToString();
            }
            else
            {
                labTotalFees.Text = labApplicationFees.Text.ToString();
            }

            btnRenew.Enabled = true;
            linkLabelShowLicenseHistory.Enabled = true;
        }

        private void linkLabelShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //if(_OldLicense==null)
            //{
            //    return;
            //}


            //clsDrivers driver = clsDrivers.FindDriverByDriverID(_OldLicense.DriverID);

            //if(driver == null) 
            //{
            //    return;
            //}
            //frmLicenseHistory frm = new frmLicenseHistory(driver.PersonID);
            //frm.ShowDialog();

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

        private bool _RenewLicense()
        {
            if (_OldLicense == null)
            {
                return false;

            }

            clsRenewLicense RenewLicense = new clsRenewLicense ();
            RenewLicense.OldLicenseID = _OldLicense.LicenseID;
            RenewLicense.CreatedByUserID = clsSetting.UserInfo.UserID;

            if(!RenewLicense.Save())
            {
                return false;
            }

            _NewLicense = clsLicense.Find(RenewLicense.NewLicenseID);
            if(_NewLicense == null) 
            {
                return false;
            }

            return true;
        }
        private void btnRenew_Click(object sender, EventArgs e)
        {
            if(!_RenewLicense())
            {
                MessageBox.Show("Data didnot save successfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Data saved successfuly, the new license ID is: {_NewLicense.LicenseID}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            labRLicenseApplicationID.Text = _NewLicense.ApplicationID.ToString();
            labExpritionDate.Text = _NewLicense.ExpirationDate.ToShortDateString();
            labNewLicenseID.Text = _NewLicense.LicenseID.ToString();

            linkLabelShowLicenseInfo.Enabled = true;
        }

        private void linkLabelShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(_NewLicense==null)
            {
                return;
            }

            frmLicenseInfo frm = new frmLicenseInfo(_NewLicense.LicenseID);
            frm.ShowDialog();
        }
    }
}
