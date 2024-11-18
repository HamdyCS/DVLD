using DVLDProject.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDProject
{
    public partial class frmManageTestAppointments : Form
    {
        enum enTestType { Vision = 1,Written = 2, Street = 3 }

        enTestType _TestType;

        int _LocalDrivingLicenseApplicationID;

        DataTable _dt;
        DataView _dv;

        private void RefreshAllDataInForm()
        {
            _dt = clsTestAppointments.GetAllTestAppointmentByLDLAppIDAndTestTypeID(_LocalDrivingLicenseApplicationID, Convert.ToInt32(_TestType));
            _dv = _dt.DefaultView;
            dataGridView1.DataSource = _dv;
            labNumberOfRecords.Text = _dv.Count.ToString();
        }
        private void ChangeFormTestType()
        {
            if(_TestType == enTestType.Written)
            {
                picTestPhoto.Image = Resources.Written_Test_512;
                this.Text = "Written Test Appointments";
                labHeader.Text = "Written Test Appointments";
                return;
            }

            if(_TestType==enTestType.Street)
            {
                picTestPhoto.Image = Resources.street_test_512;
                this.Text = "Street Test Appointments";
                labHeader.Text = "Street Test Appointments";
                return;
            }

        }
        public frmManageTestAppointments(int LocalDrivingLicenseApplicationID, int TestType)
        {
            InitializeComponent();

            if(TestType != 1 && TestType!= 2 && TestType!= 3)
            {
                btnAddNewAppointments.Enabled = false;
                return;
            }
            _TestType =(enTestType) TestType;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            ChangeFormTestType();
        }

        
       
        private void frmManageTestAppointments_Load_1(object sender, EventArgs e)
        {
            ctrlLDLApplicationInfo1.SetLDLApplicationID(_LocalDrivingLicenseApplicationID);
            RefreshAllDataInForm();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnAddNewAppointments_Click(object sender, EventArgs e)
        {
            int TestTypeID = Convert.ToInt32(_TestType);
            if (clsTestAppointments.CheckIfThisLDLAppHasActiveTestAppointment(_LocalDrivingLicenseApplicationID, TestTypeID))
            {
                MessageBox.Show("Person already have an" +
                    " active appoointment for this test" +
                    " you cannot add new appoointment.","Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(clsTestAppointments.CheckIfThisLDLAppPassedThisTest(_LocalDrivingLicenseApplicationID,TestTypeID))
            {
                MessageBox.Show("this Person already passed " +
                                    " the test before, you can only" +
                                    " retake failed test.", "Not Allowed",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmAddOrEditTestAppointment frm = new frmAddOrEditTestAppointment(_LocalDrivingLicenseApplicationID, TestTypeID);
            frm.ShowDialog();
            RefreshAllDataInForm();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestTypeID = Convert.ToInt32(_TestType);

            frmAddOrEditTestAppointment frm = new frmAddOrEditTestAppointment(_LocalDrivingLicenseApplicationID, TestTypeID, Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            RefreshAllDataInForm();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeTest frm = new frmTakeTest(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), Convert.ToInt32(_TestType));
            frm.ShowDialog();
            RefreshAllDataInForm();
        }
    }
}
