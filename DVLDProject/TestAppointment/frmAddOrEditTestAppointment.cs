
using DVLDProject.Properties;

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
    public partial class frmAddOrEditTestAppointment : Form
    {
        enum enTestType { Vision = 1, Written = 2, Street = 3 }

        enTestType _TestType;

        enum enMode { eAdd = 1, eUpdate = 2 }

        enMode _Mode;

        int _LocalDrivingLicenseApplicationID;

        clsTestAppointments TestAppointment;
        private void ChangeFormTestType()
        {
            if (_TestType == enTestType.Written)
            {
                picTestPhoto.Image = Resources.Written_Test_512;
                this.Text = "Written Test";
                labHeader.Text = "Written Test";
                return;
            }

            if (_TestType == enTestType.Street)
            {
                picTestPhoto.Image = Resources.street_test_512;
                this.Text = "Street Test";
                labHeader.Text = "Street Test";
                return;
            }

        }

        private void ChangeFormToAddNewMode()
        {
            _Mode = enMode.eAdd;

            int Trail = 0;

            DataTable dt = clsLDLApplication.GetAllLDLAppplicationInfo(_LocalDrivingLicenseApplicationID);
            labLDLApplicationID.Text = _LocalDrivingLicenseApplicationID.ToString();

            labLicenceClass.Text = dt.Rows[0][1].ToString();
            labName.Text = dt.Rows[0][2].ToString();
            dateTimePicker1.MinDate = DateTime.Now;

            clsTestTypes TestType = clsTestTypes.FindTestTypeByID(Convert.ToInt32(_TestType));

            labFees.Text = TestType.TestTypeFees.ToString();

            if (clsTestAppointments.CheckIfThisLDLAppFailedThisTest(_LocalDrivingLicenseApplicationID,Convert.ToInt32(_TestType),ref Trail))
            {
                gbRetakeTestInfo.Enabled = true;
                clsApplicationTypes RetakeApplicationType = clsApplicationTypes.FindApplicationTypeByID(7);

                LabRetakeTestFees.Text = RetakeApplicationType.ApplicationFees.ToString();
                labTotalFees.Text =  (Convert.ToDouble(RetakeApplicationType.ApplicationFees) + TestType.TestTypeFees).ToString();

            }
            else
            {
                gbRetakeTestInfo.Enabled = false;

            }
            labTrial.Text = Trail.ToString();

        }

        private void ChangeFormToUpdateMode(int TestAppointmentID)
        {
            _Mode = enMode.eUpdate;

             TestAppointment = clsTestAppointments.FindTestAppointmentByID(TestAppointmentID);

            if(TestAppointment.IsLocked)
            {
                dateTimePicker1.Enabled = false;
                btnSave.Enabled = false;
                labTestIsLocked.Visible = true;
            }

            if(TestAppointment==null)
            {
                MessageBox.Show("Test Appointment not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            labLDLApplicationID.Text = TestAppointment.TestAppointmentID.ToString();

            DataTable dt = clsLDLApplication.GetAllLDLAppplicationInfo(_LocalDrivingLicenseApplicationID);
            labLicenceClass.Text = dt.Rows[0][1].ToString();

            labLDLApplicationID.Text = _LocalDrivingLicenseApplicationID.ToString();
            labName.Text = dt.Rows[0][2].ToString();

            int Trail = 0;

            clsTestAppointments.CheckIfThisLDLAppFailedThisTest(_LocalDrivingLicenseApplicationID, Convert.ToInt32(_TestType), ref Trail);
           
            labTrial.Text = Trail.ToString();

            labFees.Text = TestAppointment.PaidFees.ToString();

           dateTimePicker1.Text = TestAppointment.AppointmentDate.ToString();

            if(TestAppointment.RetakeTestApplicationID>0)
            {
                gbRetakeTestInfo.Enabled = true;
                clsApplication RetakeApplication = clsApplication.Find(TestAppointment.RetakeTestApplicationID);

                if(RetakeApplication==null)
                {
                    MessageBox.Show("Retake Application not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close(); return;
                }

                labRetakeTestID.Text = RetakeApplication.ApplicationID.ToString();
                LabRetakeTestFees.Text = RetakeApplication.PaidFees.ToString() ;
                labTotalFees.Text = (RetakeApplication.PaidFees + TestAppointment.PaidFees).ToString();
            }
        }

        public frmAddOrEditTestAppointment(int LocalDrivingLicenseApplicationID, int TestType)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            if (TestType != 1 && TestType != 2 && TestType != 3)
            {
                btnSave.Enabled = false;
                return;
            }

            _TestType = (enTestType)TestType;

            ChangeFormTestType();
            ChangeFormToAddNewMode();
        }

        public frmAddOrEditTestAppointment(int LocalDrivingLicenseApplicationID, int TestType,int TestAppointmentID)
        {
            InitializeComponent();

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            if (TestType != 1 && TestType != 2 && TestType != 3)
            {
                btnSave.Enabled = false;
                return;
            }

            _TestType = (enTestType)TestType;

            ChangeFormTestType();

            ChangeFormToUpdateMode(TestAppointmentID);
        }
  
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool AddNewTestAppointment()
        {
             TestAppointment = new clsTestAppointments();

            int TestTypeID = Convert.ToInt32(_TestType);

            

            if (clsTestAppointments.CheckIfThisLDLAppHasActiveTestAppointment(_LocalDrivingLicenseApplicationID, TestTypeID))
            { return false; }

            if (clsTestAppointments.CheckIfThisLDLAppPassedThisTest(_LocalDrivingLicenseApplicationID, TestTypeID))
            {
                return false;
            }

            TestAppointment.TestTypeID = TestTypeID;
            TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            TestAppointment.AppointmentDate = dateTimePicker1.Value;
            TestAppointment.PaidFees = Convert.ToDouble(labFees.Text);
            TestAppointment.CreatedByUserID = clsSetting.UserInfo.UserID;
            TestAppointment.IsLocked = false;



            return TestAppointment.Save();
        }

        private bool UpdateTestAppointment()
        {
            if(TestAppointment== null)
            {
                return false;
            }

            TestAppointment.AppointmentDate = dateTimePicker1.Value;

            return TestAppointment.Save();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.eAdd)
            {
                if(AddNewTestAppointment())
                {
                    MessageBox.Show("Data saved successfuly", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Data didnot save successfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            
            if(_Mode==enMode.eUpdate)
            {
                if (UpdateTestAppointment())
                {
                    MessageBox.Show("Data Updated successfuly", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Data didnot Updated successfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }


        }
    }
}
