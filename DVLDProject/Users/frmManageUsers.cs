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
    public partial class frmManageUsers : Form
    {
        DataTable dtUserInfo;
        DataView dvUserInfo;
        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddOrEditUser();
            frm.ShowDialog();
            RefreshUsersInfo();
            dvUserInfo = dtUserInfo.DefaultView;
            dataGridView1.DataSource = dvUserInfo;
            RefreshNumberOfRecord();
        }

        private void RefreshUsersInfo()
        {
            dtUserInfo = clsUsers.GetAllUsersInfo();
        }

        private void RefreshNumberOfRecord()
        {
            labNumberOfRecords.Text = dvUserInfo.Count.ToString();
        }

        private void FillComboBoxItems()
        {

            foreach (DataColumn column in dtUserInfo.Columns)
            {
               comFilterBy.Items.Add(column.ColumnName);
            }
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {

            RefreshUsersInfo();

            dvUserInfo = dtUserInfo.DefaultView;

            dataGridView1.DataSource = dvUserInfo;


            FillComboBoxItems();

            RefreshNumberOfRecord();
        }

        private void comFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvUserInfo.RowFilter = null;

            txtFilter.Visible = false;
            comActive.Visible = false;

            RefreshNumberOfRecord();

            if (comFilterBy.SelectedIndex==0) 
            {
                return;
            }

            if(comFilterBy.Text== "Is Active")
            {
                comActive.Visible = true;
                txtFilter.Visible = false;
                return;
            }

            comActive.Visible = false;
            txtFilter.Visible = true;

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if(txtFilter.Text == string.Empty)
            {
                dvUserInfo.RowFilter = null;
                RefreshNumberOfRecord();
                return;
            }

            if (comFilterBy.Text == "User ID" || comFilterBy.Text == "Person ID")
            {
                dvUserInfo.RowFilter = $"[{comFilterBy.Text}] = {txtFilter.Text}";
               
            }

            else
            {

                dvUserInfo.RowFilter = $"[{comFilterBy.Text}] like '{txtFilter.Text}%'";
            }

            RefreshNumberOfRecord();

        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comFilterBy.Text == "User ID" || comFilterBy.Text == "Person ID")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    // إذا كان ليس رقماً، فقم بإلغاء الحرف المكتوب
                    e.Handled = true;

                }
            }
        }

        private void comActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comActive.SelectedIndex==0)
            {
                dvUserInfo.RowFilter = null;
            }

            if(comActive.SelectedIndex == 1)
            {
                dvUserInfo.RowFilter = "[Is Active] = true";
            }

            if (comActive.SelectedIndex == 2)
            {
                dvUserInfo.RowFilter = "[Is Active] = false";
            }

            RefreshNumberOfRecord();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsUsers.DeleteUser(Convert.ToInt32( dataGridView1.CurrentRow.Cells[0].Value)))
            {
                MessageBox.Show("User Delete Successfuly", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("User is not deleted due to data connected to it", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            RefreshUsersInfo();
            dvUserInfo = dtUserInfo.DefaultView;
            dataGridView1.DataSource = dvUserInfo;
            RefreshNumberOfRecord();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmUserInfo(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangePassword(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddOrEditUser(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            RefreshUsersInfo();
            dvUserInfo = dtUserInfo.DefaultView;
            dataGridView1.DataSource = dvUserInfo;

            RefreshNumberOfRecord();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddOrEditUser();
            frm.ShowDialog();
            RefreshUsersInfo();
            dvUserInfo = dtUserInfo.DefaultView;
            dataGridView1.DataSource = dvUserInfo;

            RefreshNumberOfRecord();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is not Implemented yet!", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is not Implemented yet!", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
