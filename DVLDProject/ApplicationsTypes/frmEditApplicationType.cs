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
    public partial class frmEditApplicationType : Form
    {
        clsApplicationTypes _ApplicationType;
        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationType = clsApplicationTypes.FindApplicationTypeByID(ApplicationTypeID);
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            if (_ApplicationType == null)
            {
                MessageBox.Show("Application Type is not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            labID.Text = _ApplicationType.ApplicationTypesID.ToString();
            txtTitle.Text = _ApplicationType.ApplicationTypesTitle;
            txtFees.Text = _ApplicationType.ApplicationFees.ToString();   
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtTitle.Text == string.Empty)
            {
                MessageBox.Show("Please fill title", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtFees.Text == string.Empty)
            {
                MessageBox.Show("Please fill Fees", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _ApplicationType.ApplicationTypesTitle = txtTitle.Text;
            _ApplicationType.ApplicationFees = Convert.ToSingle(txtFees.Text);

            if(_ApplicationType.Save())
            {
                MessageBox.Show("Data saved successfuly", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            MessageBox.Show("Failed to save data", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // إذا كان ليس رقماً، فقم بإلغاء الحرف المكتوب
                e.Handled = true;
            }
        }
    }
}
