using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using DVLDProject.People;
using DVLDBusinessLayer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DVLDProject
{
    public partial class frmManagePeople : Form
    {
        public frmManagePeople()
        {
            InitializeComponent();

        }

        private DataView view = new DataView();

        private void RefreshNumberOfRecords()
        {
            lapNumberOfRecords.Text = view.Count.ToString();
        }
        private void GetDefaultView()
        {
            view = clsPeople.GetLittleInfoAboutPeople().DefaultView;
            dataGridView1.DataSource = view;

        }
        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            GetDefaultView();

            RefreshNumberOfRecords();

            comFilterBy.Focus();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comFilterBy.SelectedIndex==1 || comFilterBy.SelectedIndex == 9)
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
            if(comFilterBy.SelectedIndex==0)
            {
                txtFilterBy.Visible = false;
                GetDefaultView();
                RefreshNumberOfRecords();
            }
            else
            {
                txtFilterBy.Visible = true;
                txtFilterBy.Focus();
            }


        }

        private void FilterView()
        {
            try
            {

                if (comFilterBy.SelectedIndex == 1)
                {

                    view.RowFilter = $"[Person ID] = {txtFilterBy.Text}";
                }
                else
                {
                    view.RowFilter = $"[{comFilterBy.Text}] like '{txtFilterBy.Text}%'";
                }
                RefreshNumberOfRecords();
            }
            catch (Exception ex) 
            {

            }


        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            FilterView();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            
            Form frm = new frmAddOrEditPerson();
            frm.ShowDialog();
            GetDefaultView();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if( MessageBox.Show("Are you sure you want to delete person["+
                dataGridView1.CurrentRow.Cells[0].Value.ToString() + "]","Confirm delete",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK ) 
            {
                if(clsPeople.DeletePerson(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value) ))
                {
                    MessageBox.Show("Person Deleted succesfully", "succesful", MessageBoxButtons.OK
                        , MessageBoxIcon.Question);
                }
                else
                {
                    MessageBox.Show("Person was not deleted because it has data linked to it",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

            GetDefaultView();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is not Implemented yet!", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is not Implemented yet!", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddOrEditPerson(Convert.ToInt32( dataGridView1.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            GetDefaultView();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddOrEditPerson();
            frm.ShowDialog();
            GetDefaultView();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmPersonDetails(Convert.ToInt32( dataGridView1.CurrentRow.Cells[0].Value ) );
            frm.ShowDialog();
            GetDefaultView();
        }
    }
}

