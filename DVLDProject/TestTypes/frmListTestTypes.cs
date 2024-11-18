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
    public partial class frmListTestTypes : Form
    {
        
        public frmListTestTypes()
        {
            InitializeComponent();
            RefreshDataTableContact();
            GetNumberOfRecord();
        }

        DataTable _dt;
        DataView _dv;
        void RefreshDataTableContact()
        {
            _dt = clsTestTypes.GetAllTestTypesContent();
        }
        void RefreshDataViewContact()
        {
            _dv = _dt.DefaultView;
        }

        void RefreshDataGridViewContact()
        {
            dataGridView1.DataSource = _dt.DefaultView;
        }
        void GetNumberOfRecord()
        {
            labNumberOfRecord.Text = _dt.Rows.Count.ToString();
        }

      

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicatonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmEditApplicationType(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            RefreshDataTableContact();
            RefreshDataGridViewContact();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            RefreshDataViewContact();
            RefreshDataGridViewContact();
        }

        private void editApplicatonToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form frm = new frmUpdateTestType(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            RefreshDataTableContact();
            RefreshDataGridViewContact();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
