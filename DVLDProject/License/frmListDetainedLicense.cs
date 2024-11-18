using DVLDBusinessLayer;
using DVLDProject.People;
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
    public partial class frmListDetainedLicense : Form
    {
        public frmListDetainedLicense()
        {
            InitializeComponent();
        }

        DataTable _dt;
      //  DataView _dv;

        private void RefreshDataTable()
        {
            _dt = clsDetainLicense.GetAllDetainLicenses();
        }

        //private void RefreshDataView()
        //{
        //    _dv = _dt.DefaultView;
        //    _dv.RowFilter = null;
        //}

        private void RefreshDataGridView()
        {
            dataGridView1.DataSource = _dt;
        }

        private void RefreshAllDataInForm()
        {
            RefreshDataTable();
 //           RefreshDataView();
            RefreshDataGridView();
            labNumberOfRecords.Text = _dt.DefaultView.Count.ToString();
        }
        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the key pressed is a digit or a control key (like backspace)
            if(comFilterBy.Text== "N.No.")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar))
                {
                    // If it isn't, ignore the key press
                    e.Handled = true;
                }
                return;
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If the key is not a digit or control key, ignore the key press
                e.Handled = true;
            }
        }

        private void frmListDetainedLicense_Load(object sender, EventArgs e)
        {
            RefreshAllDataInForm();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
            RefreshAllDataInForm();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseLicense frm = new frmReleaseLicense();
            frm.ShowDialog();
            RefreshAllDataInForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            //RefreshDataView();
            //      RefreshDataGridView();
            _dt.DefaultView.RowFilter = null;
            labNumberOfRecords.Text = _dt.DefaultView.Count.ToString();

            if (comFilterBy.Text == "None")
            {
                txtFilterBy.Visible = false;
                return;
            }

            txtFilterBy.Visible = true;

        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if(txtFilterBy.Text == "") 
            {
                //RefreshDataView();
                //RefreshDataGridView();
                //labNumberOfRecords.Text = _dv.Count.ToString();
                _dt.DefaultView.RowFilter = null;
                return;
            }

            if(comFilterBy.Text == "N.No.")
            {
                _dt.DefaultView.RowFilter = $"[{comFilterBy.Text}] like '{txtFilterBy.Text}%'";
              //  RefreshDataGridView();
                labNumberOfRecords.Text = _dt.DefaultView.Count.ToString();
                return;
            }

            _dt.DefaultView.RowFilter = $"[{comFilterBy.Text}] = {txtFilterBy.Text}";
            //RefreshDataGridView();
            labNumberOfRecords.Text = _dt.DefaultView.Count.ToString();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value);

            clsLicense license = clsLicense.Find(LicenseID);

            if (license == null)
            {
                return;
            }

            clsDrivers driver = clsDrivers.FindDriverByDriverID(license.DriverID);

            if (driver == null) 
            {
                return;
            }

            frmPersonDetails frm = new frmPersonDetails(driver.PersonID);
            frm.ShowDialog();
        }

        private void showLicensDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value);
            frmLicenseInfo frm = new frmLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value);

            clsLicense license = clsLicense.Find(LicenseID);

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

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int Detaind = 0;
            int LicenseID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value);

            if (clsDetainLicense.CheckIfLicenseIsdetainedAndNotRelease(ref Detaind, LicenseID)) 
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = true;
            }
            else
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = false;

            }



        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseLicense frm = new frmReleaseLicense(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            RefreshAllDataInForm();
        }
    }
}
