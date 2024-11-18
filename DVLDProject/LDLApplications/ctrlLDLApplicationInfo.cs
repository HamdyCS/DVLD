using DVLDProject.People;
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
    public partial class ctrlLDLApplicationInfo : UserControl
    {

        public int LDLApplicationID { get; set; }

        public clsLDLApplication LDLApplication { get; set; }

        public clsLicense LicenseInfo { get; set; }
        
        private DataTable _dt;

        private void FillDataInForm()
        {
            _dt = clsLDLApplication.GetAllLDLAppplicationInfo(LDLApplicationID);
            
            if(_dt==null)
            {
                return;
            }

            labLDLApplicationID.Text = _dt.Rows[0][0].ToString();
            labClassName.Text = _dt.Rows[0][1].ToString();
            labPasssedTests.Text = _dt.Rows[0][3].ToString() + "/3";

            labApplicationID.Text = _dt.Rows[0][5].ToString();
            labApplicationStatus.Text = _dt.Rows[0][4].ToString();
            labApplicationFees.Text =Convert.ToDouble( _dt.Rows[0][6]).ToString();
            labAppliactionType.Text = _dt.Rows[0][7].ToString();
            labApplicant.Text = _dt.Rows[0][2].ToString();

            labApplicationDate.Text = Convert.ToDateTime( _dt.Rows[0][8]).ToShortDateString().ToString();
            labApplicationStatusDate.Text = Convert.ToDateTime(_dt.Rows[0][9]).ToShortDateString().ToString();
            labCreatedBy.Text = _dt.Rows[0][10].ToString();
        }
        public void SetLDLApplicationID(int LDLApplicationID)
        {
            if(LDLApplicationID<0)
            {
                return;
            }

            this.LDLApplicationID = LDLApplicationID;

            LDLApplication = clsLDLApplication.FindLDLApplication(LDLApplicationID);

            if (LDLApplication != null)
            {
                LicenseInfo = clsLicense.Find(LDLApplication.GetLicenseID());
            }

            if(LicenseInfo==null)
            {
                LLapShowLicenseInfo.Enabled = false;
            }

            FillDataInForm();
        }

        public ctrlLDLApplicationInfo()
        {
            InitializeComponent();
        }

        private void LinkLabViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Convert.ToInt32( _dt.Rows[0][11].ToString())<1)
            { return; }

            frmPersonDetails frm = new frmPersonDetails(Convert.ToInt32(_dt.Rows[0][11].ToString()));
            frm.ShowDialog();
            FillDataInForm();
        }

        private void LLapShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (LDLApplication != null)
            {
                frmLicenseInfo frm = new frmLicenseInfo(LicenseInfo.LicenseID);
                frm.ShowDialog();

            }
            else
            {
                MessageBox.Show("License Not found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
