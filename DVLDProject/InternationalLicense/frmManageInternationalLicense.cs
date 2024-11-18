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
    public partial class frmManageInternationalLicense : Form
    {
        public frmManageInternationalLicense()
        {
            InitializeComponent();
        }

        private DataTable _dt;
        private DataView _dv;

        private void _RefreashDataTable()
        {
            _dt = clsInternationalLicense.GetAllInternationalLicense();
        }

        private void _RefreashDataInDataGridView()
        {
            _dv = _dt.DefaultView;
            _dv.RowFilter = null;
            dataGridView1.DataSource = _dv;
            labNumberOfRecords.Text = _dv.Count.ToString();
        }
        private void frmManageInternationalLicense_Load(object sender, EventArgs e)
        {
            _RefreashDataTable();   
            _RefreashDataInDataGridView(); 
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();    
            frm.ShowDialog();
            _RefreashDataTable();
            _RefreashDataInDataGridView();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            _RefreashDataInDataGridView();

            if (comFilterBy.SelectedIndex == 0)
            {
                txtFilterBy.Visible = false;
                return;
            }

            txtFilterBy.Visible = true;
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if(txtFilterBy.Text==""||string.IsNullOrWhiteSpace(txtFilterBy.Text))
            {
                _RefreashDataInDataGridView();
                return;
            }
            _dv.RowFilter = $"[{comFilterBy.Text}] = {txtFilterBy.Text}";
            dataGridView1.DataSource = _dv;
            labNumberOfRecords.Text = _dv.Count.ToString();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // منع إدخال الحرف
            }
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsInternationalLicense InternationalLicense = clsInternationalLicense.Find(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));

            if (InternationalLicense == null) 
            {
                return;
            }
            clsDrivers driver = clsDrivers.FindDriverByDriverID(InternationalLicense.DriverID);

            if(driver == null) 
            {
                return;
            }

            frmPersonDetails frm = new frmPersonDetails(driver.PersonID);
            frm.ShowDialog();
        }

        private void ShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            clsInternationalLicense InternationalLicense = clsInternationalLicense.Find(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));

            if (InternationalLicense == null)
            {
                return;
            }
            clsDrivers driver = clsDrivers.FindDriverByDriverID(InternationalLicense.DriverID);

            if (driver == null)
            {
                return;
            }

            frmLicenseHistory frm = new frmLicenseHistory(driver.PersonID);
            frm.ShowDialog();


        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
            frmInternationalLicenseInfo frm = new frmInternationalLicenseInfo(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
        }
    }
}
