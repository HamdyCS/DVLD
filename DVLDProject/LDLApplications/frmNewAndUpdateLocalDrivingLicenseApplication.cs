
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
    public partial class frmNewAndUpdateLocalDrivingLicenseApplication : Form
    {
        enum enMode { eAdd = 1, eUpdate = 2 }
        enMode _Mode;

        clsLDLApplication _LDLApplication;
       
        void ChangeFormToUpdateMode(int LDLApplicationID)
        {
            this.Name = "Update Local Driving License Application"; 
            labHeader.Text = "Update Local Driving License Application";

            _LDLApplication = clsLDLApplication.FindLDLApplication(LDLApplicationID);

            if(_LDLApplication == null)
            {
                MessageBox.Show("Local Driving License Application is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlFilterPeople1.SetPersonID(_LDLApplication.ApplicantPersonID);

//            ctrlFilterPeople1.Enabled = false;

            labLDLApplicationID.Text = _LDLApplication.LocalDrivingLicenseApplicationID.ToString();

            labApplicationDate.Text = _LDLApplication.ApplicationDate.ToString();

            comLicenseClass.SelectedIndex = _LDLApplication.LicenseClassID - 1;

            clsUsers user = clsUsers.FindUserByUserID(_LDLApplication.CreatedByUserID);

            if(user != null)
            {
                labCreatedBy.Text = user.UserName; 
            }
               
            
        }

        public frmNewAndUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _LDLApplication = new clsLDLApplication();
            labApplicationDate.Text = DateTime.Now.ToShortDateString();
            _Mode = enMode.eAdd;
        }

        public frmNewAndUpdateLocalDrivingLicenseApplication(int LDLApplicationID)
        {
            InitializeComponent();
            _Mode = enMode.eUpdate;
            

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            DataTable dt = clsLDLApplication.GetAllLicenseClassName();

            foreach(DataRow dr in dt.Rows) 
            {
                comLicenseClass.Items.Add(dr[0].ToString());
            }

            if(_Mode == enMode.eAdd) 
            {
                comLicenseClass.SelectedIndex = 2;
            }

            labApplicationFees.Text = clsLDLApplication.GetLDLApplicationFees().ToString();
            labCreatedBy.Text = clsSetting.UserInfo.UserName;

           
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private bool SaveNewLDLApplication()
        {
            int ApplicationID = -1;

            if (clsLDLApplication.CheckIfPersonHasActiveApplicationInThisLicenseClass(ref ApplicationID,comLicenseClass.SelectedIndex+1, ctrlFilterPeople1.PersonID))
            {
                MessageBox.Show($"Choose another license class" +
                    $", the selected person already have an active applicatin" +
                    $" for the selected class with" +
                    $" id = {ApplicationID}  ", "Not allowed", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);

                return false;
            }

            if(clsLicense.CheckIfPersonHasAlreadyLicenseInTheLicenseClass(ctrlFilterPeople1.PersonID, comLicenseClass.SelectedIndex + 1))
            {
                MessageBox.Show("Person already have " +
                    "a license with the same appliend driving " +
                    "class choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            _LDLApplication.ApplicantPersonID = ctrlFilterPeople1.PersonID;
            _LDLApplication.ApplicationDate = DateTime.Now;
            _LDLApplication.ApplicationTypeID = 1;
            _LDLApplication.ApplicationStatus = 1;
            _LDLApplication.LastStatusDate = DateTime.Now;
            _LDLApplication.PaidFees = Convert.ToDouble(labApplicationFees.Text);
            _LDLApplication.CreatedByUserID = clsSetting.UserInfo.UserID;
            _LDLApplication.LicenseClassID = comLicenseClass.SelectedIndex + 1;

            if(_LDLApplication.save())
            {
                return true;
            }

            return false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if(_Mode==enMode.eAdd)
            {
                if(!SaveNewLDLApplication())
                {
                    MessageBox.Show("Data didnot save successfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Data saved successfuly", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChangeFormToUpdateMode(_LDLApplication.LocalDrivingLicenseApplicationID);
            }

            // لسة الedit متعملش
        }
    }
}
