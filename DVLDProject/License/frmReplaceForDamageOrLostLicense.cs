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
    public partial class frmReplaceForDamageOrLostLicense : Form
    {
        private clsLicense _OldLicense;
        private clsLicense _NewLicense;

        private enum enMode { eLost = 3, eDamaged = 4 }

        enMode _Mode = enMode.eDamaged;

        public frmReplaceForDamageOrLostLicense()
        {
            InitializeComponent();
        }

        private void ctrlFilterLicense1_MyEvent(object sender, EventArgs e)
        {
            _OldLicense = clsLicense.Find(ctrlFilterLicense1.LicenseID);

            btnReplace.Enabled = false;
            linkLabelShowLicenseHistory.Enabled = false;
            linkLabelShowLicenseInfo.Enabled = false;


            if (_OldLicense == null)
            {
                MessageBox.Show("License not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (clsRenewLicense.CheckIfLicenseIsExpired(_OldLicense.LicenseID))
            {
                MessageBox.Show($"Selected license is " +
                    "expired, choose another one" , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!clsRenewLicense.CheckIfLicenseIsActive(ctrlFilterLicense1.LicenseID))
            {
                MessageBox.Show("License is not Active, choose another one", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            labOLDLicenseID.Text = _OldLicense.LicenseID.ToString();

           
            btnReplace.Enabled = true;
            linkLabelShowLicenseHistory.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _ChangeFormToDamagedMode()
        {
            if(_Mode!=enMode.eDamaged)
            {
                return;
            }

            clsApplicationTypes ApplicationType = clsApplicationTypes.FindApplicationTypeByID(4);

            if (ApplicationType==null)
            {
                return;
            }

            labApplicationFees.Text = ApplicationType.ApplicationFees.ToString();

            labTitle.Text = "Replacement For Damaged License";
            this.Text = "Replacement For Damaged License";
        }

        private void _ChangeFormToLostMode()
        {
            if (_Mode != enMode.eLost)
            {
                return;
            }

            clsApplicationTypes ApplicationType = clsApplicationTypes.FindApplicationTypeByID(3);

            if (ApplicationType == null)
            {
                return;
            }

            labApplicationFees.Text = ApplicationType.ApplicationFees.ToString();

            labTitle.Text = "Replacement For Lost License";
            this.Text = "Replacement For Lost License";
        }
        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            if(rbDamagedLicense.Checked) 
            {
                _Mode = enMode.eDamaged;
                _ChangeFormToDamagedMode();
                
            }
        }


        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            if(rbLostLicense.Checked) 
            {
                _Mode = enMode.eLost;
                _ChangeFormToLostMode();
            }
        }

        private void frmReplaceForDamageOrLostLicense_Load(object sender, EventArgs e)
        {
            _ChangeFormToDamagedMode();
            labCreatedBy.Text = clsSetting.UserInfo.UserName;
            labApplicationDate.Text = DateTime.Now.ToShortDateString();

        }

        private void linkLabelShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //if (_OldLicense == null)
            //{
            //    return;
            //}


            //clsDrivers driver = clsDrivers.FindDriverByDriverID(_OldLicense.DriverID);

            //if (driver == null)
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

        private void linkLabelShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_NewLicense == null)
            {
                return;
            }

            frmLicenseInfo frm = new frmLicenseInfo(_NewLicense.LicenseID);
            frm.ShowDialog();
        }

        private bool _ReplaceLicense()
        {
            if (_OldLicense == null)
            {
                return false;

            }
            clsReplaceForDamageOrLostLicense ReplaceLicense;

            if (_Mode==enMode.eLost) 
            {
                ReplaceLicense = new clsReplaceForDamageOrLostLicense(true);

            }
            else 
            {
                ReplaceLicense = new clsReplaceForDamageOrLostLicense(false);

            }

            

            ReplaceLicense.OldLicenseID = _OldLicense.LicenseID;
            ReplaceLicense.CreatedByUserID = clsSetting.UserInfo.UserID;

            if (!ReplaceLicense.Save())
            {
                return false;
            }

            _NewLicense = clsLicense.Find(ReplaceLicense.NewLicenseID);
            if (_NewLicense == null)
            {
                return false;
            }

            return true;
        }
        private void btnReplace_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to replace License?","Quection",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.Cancel)
            {
                return;
            }

            if (!_ReplaceLicense())
            {
                MessageBox.Show("Data didnot save successfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Data saved successfuly, the new license ID is: {_NewLicense.LicenseID}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            labRLicenseApplicationID.Text = _NewLicense.ApplicationID.ToString();
            
            labNewLicenseID.Text = _NewLicense.LicenseID.ToString();

            linkLabelShowLicenseInfo.Enabled = true;
        }
    }
}
