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
    public partial class frmLicenseHistory : Form
    {
        DataTable _dtLocalLicense;
        DataTable _dtInternationalLicense;

        DataView _dvLocalLicense;
        DataView _dvInternationalLicense;

        int _PersonID;
        public frmLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex==0)
            {
                if (_dvLocalLicense.Count > 0)
                {
                    labNumberOfRecords.Text = _dvLocalLicense.Count.ToString();
                }
                else
                {
                    labNumberOfRecords.Text = "0";
                }
                return;
            }

            if (tabControl1.SelectedIndex == 1)
            {
                if (_dvInternationalLicense.Count > 0)
                {
                    labNumberOfRecords.Text = _dvInternationalLicense.Count.ToString();
                }
                else
                {
                    labNumberOfRecords.Text = "0";
                }
                return;
            }
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            ctrlPersonInfo1.SetPersonID(_PersonID);

            _dtLocalLicense = clsLicense.GetAllLocalPersonLicense(_PersonID);
            _dvLocalLicense = _dtLocalLicense.DefaultView;
            dgvLocalLicense.DataSource = _dvLocalLicense;

            if ( _dvLocalLicense.Count>0 ) 
            {
                labNumberOfRecords.Text = _dvLocalLicense.Count.ToString();
            }

            _dtInternationalLicense = clsInternationalLicense.GetAllInternationalPersonLicense(_PersonID);
            _dvInternationalLicense = _dtInternationalLicense.DefaultView;
            dvgInternationalLicense.DataSource = _dvInternationalLicense;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(Convert.ToInt32(dgvLocalLicense.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseInfo frm = new frmInternationalLicenseInfo(Convert.ToInt32(dvgInternationalLicense.CurrentRow.Cells[0].Value));
            frm.ShowDialog(this);

        }
    }
}
