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
    public partial class frmInternationalLicenseInfo : Form
    {
        int _InternationalLicenseID;
        public frmInternationalLicenseInfo(int InternationalLicenseID)
        {
            InitializeComponent();
           _InternationalLicenseID = InternationalLicenseID;
        }

        private void frmInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlInternationalLicenseInfo1.SetInternationalLicenseID(_InternationalLicenseID);
        }
    }
}
