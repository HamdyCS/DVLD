
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
    public partial class frmIssuaDrivingLicenseFirstTime : Form
    {
        int _LocalDrivingLicenseID;
        clsLDLApplication _LDLApplication;
        public frmIssuaDrivingLicenseFirstTime(int LocalDrivingLicenseID)
        {
            InitializeComponent();

            _LocalDrivingLicenseID = LocalDrivingLicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmIssuaDrivingLicenseFirstTime_Load(object sender, EventArgs e)
        {
            _LDLApplication = clsLDLApplication.FindLDLApplication(_LocalDrivingLicenseID);

            if( _LDLApplication == null ) 
            {
                MessageBox.Show("Local Drivaing License Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlLDLApplicationInfo1.SetLDLApplicationID(_LDLApplication.LocalDrivingLicenseApplicationID);
        }

        private void btnIssua_Click(object sender, EventArgs e)
        {
            clsLicense license = new clsLicense();

            license.ApplicationID = _LDLApplication.ApplicationID;
            license.LicenseClass = _LDLApplication.LicenseClassID;
            license.Notes = txtNotes.Text;
            //            license.PaidFees = 0;
            clsApplicationTypes application = clsApplicationTypes.FindApplicationTypeByID(_LDLApplication.ApplicationTypeID);

            if( application == null ) 
            {
                license.PaidFees = 0;
            }
            else
            {
                license.PaidFees = application.ApplicationFees;

            }

            license.IsActive = true;
            license.IssueReason = 1;
            license.CreatedByUserID = clsSetting.UserInfo.UserID;

            if(license.Save() )
            {
                MessageBox.Show($"License issued successfuly with license Id {license.LicenseID}", "Succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Data didnot save successfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
