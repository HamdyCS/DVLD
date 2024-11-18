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
using System.Linq.Expressions;
namespace DVLDProject
{
    public partial class frmLocalDrivingLicenseApplicationsList : Form
    {
        public frmLocalDrivingLicenseApplicationsList()
        {
            InitializeComponent();
        }

        DataTable dt;
        DataView dv;

        private void RefreshDataTable()
        {
            dt = clsLDLApplication.GetAllLocalDrivingApplications();
        }

        private void RefreshDataView()
        {
            dv = dt.DefaultView;
        }

        private void RefreshDataGridView()
        {
            dataGridView1.DataSource = dv;
        }

        private void RefreshNumberOfRecord()
        {
            lapNumberOfRecords.Text = dv.Count.ToString();
        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            RefreshDataTable();
            RefreshDataView();
            RefreshDataGridView();
            RefreshNumberOfRecord();
        }
      
        private void comFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            dv.RowFilter = null;
            RefreshNumberOfRecord();

            if(comFilterBy.Text=="None")
            {
                txtFilterBy.Visible = false;
                comStatusFilter.Visible = false;
                return;
            }

            if(comFilterBy.Text== "Status") 
            {
                txtFilterBy.Visible = false;
                comStatusFilter.Visible = true;
                return;
            }

            //txtFilterBy.Focus();

            comStatusFilter.Visible = false;

            txtFilterBy.Visible = true;

        }     

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (txtFilterBy.Text == string.Empty)
            {
                dv.RowFilter = null;
                RefreshNumberOfRecord();
                return;

            }

            if (comFilterBy.Text== "L.D.L.AppID")
            {
                dv.RowFilter = $"[L.D.L.AppID] = {txtFilterBy.Text}";
                RefreshNumberOfRecord();
                return;
            }
            
                dv.RowFilter = $"[{comFilterBy.Text}] like '{txtFilterBy.Text}%'";
                RefreshNumberOfRecord();
                return;
       
        }

        private void comStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comStatusFilter.Text=="All")
            {
                dv.RowFilter = null;
                RefreshNumberOfRecord();
                return;
            }

            dv.RowFilter = $"[Status] = '{comStatusFilter.Text}'";
            RefreshNumberOfRecord();

        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comFilterBy.Text == "L.D.L.AppID")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    // إذا كان ليس رقماً، فقم بإلغاء الحرف المكتوب
                    e.Handled = true;

                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmNewAndUpdateLocalDrivingLicenseApplication frm = new frmNewAndUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();
            RefreshDataTable();
            RefreshDataView();
            RefreshDataGridView();
        }

        private void canclApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLDLApplication lDLApplication = clsLDLApplication.FindLDLApplication(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            if (lDLApplication != null)
            {

                if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (lDLApplication.ChangeApplicationStatus(2))
                    {
                        MessageBox.Show("Data saved successfuly", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                    else
                    {
                        MessageBox.Show("Data didnot save successfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            else
            {
                MessageBox.Show("Local Driving License not found!", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            RefreshDataTable();
            RefreshDataView();
            RefreshDataGridView();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            
            int PaseedTests = Convert.ToInt16(dataGridView1.CurrentRow.Cells[5].Value);
            string Status = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            editApplicationToolStripMenuItem.Enabled = false;
            deleteApplicationToolStripMenuItem.Enabled = false;
            canclApplicationToolStripMenuItem.Enabled = false;
            schToolStripMenuItem.Enabled = false;

            sechduleVisionTestToolStripMenuItem.Enabled = false;
            sechduleWriteenToolStripMenuItem.Enabled = false;
            sechduleStreetTestToolStripMenuItem.Enabled = false;

            issuaToolStripMenuItem.Enabled = false;
            showLicenseToolStripMenuItem.Enabled = false;

            if(Status == "Cancelled")
            {
                return;
            }

            if (Status == "New" && PaseedTests==3)
            {
                editApplicationToolStripMenuItem.Enabled = true;
                deleteApplicationToolStripMenuItem.Enabled = true;
                canclApplicationToolStripMenuItem.Enabled = true;
                schToolStripMenuItem.Enabled = true;

                schToolStripMenuItem.Enabled = false;
                issuaToolStripMenuItem.Enabled = true;

                return;
            }

            if (Status == "Completed")
            {
                showLicenseToolStripMenuItem.Enabled=true;
                return;
            }

            if (PaseedTests == 0)
            {
                editApplicationToolStripMenuItem.Enabled = true;
                deleteApplicationToolStripMenuItem.Enabled = true;
                canclApplicationToolStripMenuItem.Enabled = true;
                schToolStripMenuItem.Enabled = true;

                sechduleVisionTestToolStripMenuItem.Enabled=true;
                sechduleWriteenToolStripMenuItem.Enabled = false;
                sechduleStreetTestToolStripMenuItem.Enabled = false;

                issuaToolStripMenuItem.Enabled = false; 
                showLicenseToolStripMenuItem.Enabled = false;
                    return;

            }

            if (PaseedTests == 1)
            {
                editApplicationToolStripMenuItem.Enabled = true;
                deleteApplicationToolStripMenuItem.Enabled = true;
                canclApplicationToolStripMenuItem.Enabled = true;
                schToolStripMenuItem.Enabled = true;

                sechduleVisionTestToolStripMenuItem.Enabled = false;
                sechduleWriteenToolStripMenuItem.Enabled = true;
                sechduleStreetTestToolStripMenuItem.Enabled = false;

                issuaToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;
                return;


            }

            if (PaseedTests == 2)
            {
                editApplicationToolStripMenuItem.Enabled = true;
                deleteApplicationToolStripMenuItem.Enabled = true;
                canclApplicationToolStripMenuItem.Enabled = true;
                schToolStripMenuItem.Enabled = true;

                sechduleVisionTestToolStripMenuItem.Enabled = false;
                sechduleWriteenToolStripMenuItem.Enabled = false;
                sechduleStreetTestToolStripMenuItem.Enabled = true;

                issuaToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;

                return;

            }


        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLDLApplicationInfo frm = new frmLDLApplicationInfo(Convert.ToInt32( dataGridView1.CurrentRow.Cells[0].Value));
            frm.ShowDialog ();
            RefreshDataTable();
            RefreshDataView();
            RefreshDataGridView();
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsLDLApplication.DeleteLDLApplication(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value)))
                {
                    MessageBox.Show("Local Driving License Appliaction Delete Successfuly", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to delete Local Driving License Appliaction", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                RefreshDataTable();
                RefreshDataView();
                RefreshDataGridView();
            }
        }

        private void sechduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestAppointments frm = new frmManageTestAppointments(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), 1);
            frm.ShowDialog ();
            RefreshDataTable();
            RefreshDataView();
            RefreshDataGridView();
        }

        private void sechduleWriteenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestAppointments frm = new frmManageTestAppointments(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), 2);
            frm.ShowDialog();
            RefreshDataTable();
            RefreshDataView();
            RefreshDataGridView();
        }

        private void sechduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestAppointments frm = new frmManageTestAppointments(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), 3);
            frm.ShowDialog();
            RefreshDataTable();
            RefreshDataView();
            RefreshDataGridView();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLDLApplication LDLApplication = clsLDLApplication.FindLDLApplication(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            if (LDLApplication != null) 
            {
                frmLicenseHistory frm = new frmLicenseHistory(LDLApplication.ApplicantPersonID);
                frm.ShowDialog();
            }
        }

        private void issuaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssuaDrivingLicenseFirstTime frm = new frmIssuaDrivingLicenseFirstTime(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            RefreshDataTable();
            RefreshDataView();
            RefreshDataGridView();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLDLApplication lDLApplication = clsLDLApplication.FindLDLApplication(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            
            frmLicenseInfo frm = new frmLicenseInfo(lDLApplication.GetLicenseID());
            frm.ShowDialog();
                
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is not Implemented yet!", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
