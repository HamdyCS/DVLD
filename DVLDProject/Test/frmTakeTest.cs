
using DVLDProject.Properties;
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
    public partial class frmTakeTest : Form
    {
        enum enTestType { Vision = 1, Written = 2, Street = 3 }

        enTestType _TestType;

        int _TestAppointmentID;

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
        public frmTakeTest(int TestAppointmentID, int TestTypeID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _TestType = (enTestType)TestTypeID;

            ChangeFormTestType();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            TestAppointment = clsTestAppointments.FindTestAppointmentByID(_TestAppointmentID);

            if (TestAppointment == null)
            {
                MessageBox.Show("Test Appointment is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (TestAppointment.IsLocked)
            {
                MessageBox.Show("Test Appointment Is Locked", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            
            int Trail = 0;// المحاولات

            if (clsTestAppointments.CheckIfThisLDLAppFailedThisTest(TestAppointment.LocalDrivingLicenseApplicationID, Convert.ToInt32(_TestType), ref Trail))
            {
                labTrial.Text = Trail.ToString();
            }

            
            
            labLDLApplicationID.Text = TestAppointment.LocalDrivingLicenseApplicationID.ToString();

            labLicenceClass.Text = TestAppointment.LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName;
            labName.Text = TestAppointment.LocalDrivingLicenseApplicationInfo.PersonInfo.GetFullName();
            labDate.Text = TestAppointment.AppointmentDate.ToShortDateString();
            labFees.Text = TestAppointment.PaidFees.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsTests Test = new clsTests();
            Test.TestAppointmentID = TestAppointment.TestAppointmentID;
            Test.TestResult = rbPass.Checked;
            Test.Notes = txtNote.Text;
            Test.CreatedByUserID = clsSetting.UserInfo.UserID;

            if(Test.Save())
            {
                
                MessageBox.Show("Data saved successfuly", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                this.Close();
                return;
            }
            else
            {
                
                    MessageBox.Show("Data didnot save successfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }
    }
}
