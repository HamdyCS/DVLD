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
    public partial class frmManageApplicationTypes : Form
    {
        public frmManageApplicationTypes()
        {
            InitializeComponent();
            RefreshDataTableContact();
            GetNumberOfRecord();
        }

        DataTable _dt;
        DataView _dv;
        void RefreshDataTableContact()
        {
            _dt = clsApplicationTypes.GetAllApplicationTypesContent();
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

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            RefreshDataViewContact();
            RefreshDataGridViewContact();
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
    }
}
