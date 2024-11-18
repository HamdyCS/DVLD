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
    public partial class frmUpdateTestType : Form
    {
        clsTestTypes _TestType;
        public frmUpdateTestType(int TextTypeID)
        {
            InitializeComponent();
            _TestType = clsTestTypes.FindTestTypeByID(TextTypeID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUpdateTextType_Load(object sender, EventArgs e)
        {
            if (_TestType == null)
            {
                MessageBox.Show("Test Type is not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            labID.Text = _TestType.TestTypeID.ToString();
            txtTitle.Text = _TestType.TestTypeTitle; 
            txtDescription.Text = _TestType.TestTypeDescription;
            txtFees.Text = _TestType.TestTypeFees.ToString();
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // إذا كان ليس رقماً، فقم بإلغاء الحرف المكتوب
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text == string.Empty)
            {
                MessageBox.Show("Please fill title", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtDescription.Text == string.Empty)
            {
                MessageBox.Show("Please fill Description", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtFees.Text == string.Empty)
            {
                MessageBox.Show("Please fill Fees", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType.TestTypeTitle = txtTitle.Text;
            _TestType.TestTypeDescription = txtDescription.Text;
            _TestType.TestTypeFees = Convert.ToUInt32(txtFees.Text);

            if (_TestType.Save())
            {
                MessageBox.Show("Data saved successfuly", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            MessageBox.Show("Failed to save data", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
