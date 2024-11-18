using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject
{
    public partial class ctrlFilterPeople : UserControl
    {
 
        public ctrlFilterPeople()
        {
            InitializeComponent();

        }

        

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((comFilterBy.Text == "Person ID"))
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    // إذا كان ليس رقماً، فقم بإلغاء الحرف المكتوب
                    e.Handled = true;

                }
            }
        }

        public int PersonID { get; set; }

        public void SetPersonID(int personID)
        {
            ctrlPersonInfo1.SetPersonID(personID);
            this.gbFilter.Enabled = false;
            this.PersonID = personID;
        }




        DataTable dt;
        DataView dv;
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
             dt = clsPeople.GetLittleInfoAboutPeople();
             dv = dt.DefaultView;

            string sq;

            if ((comFilterBy.Text == "Person ID"))
            {
                sq = "";
            }
            else
            {
                sq = "'";
            }

            try
            {
                dv.RowFilter = $"[{comFilterBy.Text}] =  {sq}{txtFilterBy.Text}{sq} ";
                ctrlPersonInfo1.SetPersonID(Convert.ToInt32(dv[0][0]));
                
            }
            catch (Exception ex)
            {
                ctrlPersonInfo1.SetPersonID(-1);

            }

            PersonID = ctrlPersonInfo1.PersonID;
            
        }

        private void btnAddNewPerson_Click_1(object sender, EventArgs e)
        {
            frmAddOrEditPerson frm = new frmAddOrEditPerson();
            frm.DataBack += FormAddNewPersonDataBack;
            frm.ShowDialog();
        }

        private void FormAddNewPersonDataBack(int PersonID)
        {
            comFilterBy.SelectedIndex = 0;
            txtFilterBy.Text = PersonID.ToString();
            //ctrlPersonInfo1.SetPersonID(PersonID);

            //dt = clsPeople.GetLittleInfoAboutPeople();
            //dv = dt.DefaultView;
            clsPeople person = clsPeople.FindPeopleByID(PersonID);

            if(person == null) 
            {
                MessageBox.Show("People not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
                
            ctrlPersonInfo1.SetPersonID(person.GetPersonID());

            this.PersonID = ctrlPersonInfo1.PersonID;
            
        }

        private void ctrlFilterPeople_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // إبلاغ النظام بأننا نعالج مفتاح Enter
                e.IsInputKey = true;
            }
        }

        private void ctrlFilterPeople_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // محاكاة الضغط على الزر الأول عند الضغط على Enter
                btnSearch.PerformClick();
            }
        }

        private void comFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Focus();
        }

        private void txtFilterBy_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)13)// entar
            //{

            //    btnSearch.PerformClick();
            //}

            if(e.KeyChar == (char)Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void ctrlFilterPeople_Load(object sender, EventArgs e)
        {
            comFilterBy.Focus();
        }

        
    }
}
