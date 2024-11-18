using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLDProject.clsSetting;
using DVLDBusinessLayer;

namespace DVLDProject
{
    public partial class frmManageDrivers : Form
    {
        public frmManageDrivers()
        {
            InitializeComponent();
        }

        DataTable dt;
        DataView dv;

        private void RefreshDataTable()
        {
            dt = clsDrivers.GetAllDrivers();
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

        private void RemoveFilter()
        {
            dv.RowFilter = null;

        }

        private void FillComboBoxItems()
        {

            foreach (DataColumn column in dt.Columns)
            {
                if(column.ColumnName == "Date")
                {
                    continue;
                }
                comFilterBy.Items.Add(column.ColumnName);
            }
        }

        private void frmManageDrivers_Load(object sender, EventArgs e)
        {
            RefreshDataTable();
            RefreshDataView();
            RefreshDataGridView();
            RefreshNumberOfRecord();
            FillComboBoxItems();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comFilterBy.Text == "Driver ID" || comFilterBy.Text == "Person ID" || comFilterBy.Text == "Active Licenses")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    // إذا كان ليس رقماً، فقم بإلغاء الحرف المكتوب
                    e.Handled = true;

                }
            }
        }

        private void comFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            RemoveFilter();
            RefreshDataGridView();
            RefreshNumberOfRecord();

            if(comFilterBy.Text=="None")
            {
                txtFilter.Visible = false;
                return;
            }

            txtFilter.Visible = true;

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if(txtFilter.Text == string.Empty)
            {
                RemoveFilter();
                RefreshDataGridView();
                RefreshNumberOfRecord();
                return;
            }
            if(comFilterBy.Text == "Driver ID" || comFilterBy.Text == "Person ID" || comFilterBy.Text == "Active Licenses")
            {
                dv.RowFilter = $"[{comFilterBy.Text}] = {txtFilter.Text}";
            }
            else
            {
                dv.RowFilter = $"[{comFilterBy.Text}] like '{txtFilter.Text}%'";
            }

            RefreshNumberOfRecord();

        }


    }
}
