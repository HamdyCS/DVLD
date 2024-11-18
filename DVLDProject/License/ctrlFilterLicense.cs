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
    public partial class ctrlFilterLicense : UserControl
    {
        public event EventHandler MyEventWhenBtnSearchClick;
        protected virtual void OnMyEvent(EventArgs e)
        {
            MyEventWhenBtnSearchClick?.Invoke(this, e);
        }
        public ctrlFilterLicense()
        {
            InitializeComponent();
        }

        public int LicenseID {  get; set; }

        public void SetLicenseID(int licenseID) 
        {
            this.LicenseID = licenseID;
            txtLicenseID.Text = licenseID.ToString();
            ctrlLicenseInfo1.SetLicenseID(LicenseID);
            gpFilter.Enabled = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If the key is not a digit or control key, ignore the key press
                e.Handled = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LicenseID = Convert.ToInt32(txtLicenseID.Text);
            ctrlLicenseInfo1.SetLicenseID(LicenseID);
            OnMyEvent(EventArgs.Empty); 
        }

        public void DisableFilter()
        {
            gpFilter.Enabled = false;
        }
        
    }
}
