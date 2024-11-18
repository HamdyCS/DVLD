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
    public partial class frmDetainLicense : Form
    {
        private clsDetainLicense _DetainLicense;
        public frmDetainLicense()
        {
            InitializeComponent();
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlFilterLicense1_MyEventWhenBtnSearchClick(object sender, EventArgs e)
        {
            linkLabelShowLicenseHistory.Enabled = true;
            btnDetain.Enabled = false;

            clsLicense license = clsLicense.Find(ctrlFilterLicense1.LicenseID);
            if (license == null)
            {
                linkLabelShowLicenseHistory.Enabled = false;
            }

            int DetainID = 0;

            if (clsDetainLicense.CheckIfLicenseIsdetainedAndNotRelease(ref DetainID, ctrlFilterLicense1.LicenseID))
            {
                MessageBox.Show("Selected license is already detained, " +
                    "Choose another one", 
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }

            btnDetain.Enabled = true;
            labLicenseID.Text = ctrlFilterLicense1.LicenseID.ToString();
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

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the key pressed is a digit or a control key (like backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If the key is not a digit or control key, ignore the key press
                e.Handled = true;
            }
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            labDetainDate.Text = DateTime.Now.ToShortDateString();
            labCreatedBy.Text = clsSetting.UserInfo.UserName;
        }

        private bool _AddNewDetainLicense()
        {
            _DetainLicense = new clsDetainLicense();
            _DetainLicense.LicenseID = ctrlFilterLicense1.LicenseID;

            if(txtFineFees.Text.Length > 0)
            {

                _DetainLicense.FineFees = Convert.ToDouble(txtFineFees.Text);
            }
            else
            {
                _DetainLicense.FineFees = 0;
            }

            _DetainLicense.CreatedByUserID = clsSetting.UserInfo.UserID;

            if(!_DetainLicense.Save())
            {
                return false;
            }

            
            return true;
        }
        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Detain License?", "Quection", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            if (!_AddNewDetainLicense())
            {
                MessageBox.Show("Data didnot save successfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"License detained successfuly with ID = {_DetainLicense.DetainID}" ,"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            labDetainID.Text = _DetainLicense.DetainID.ToString();

            btnDetain.Enabled = false;

            linkLabelShowLicenseInfo.Enabled = true;
        }
    }
}
