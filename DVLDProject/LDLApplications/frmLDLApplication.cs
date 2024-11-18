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
    public partial class frmLDLApplicationInfo : Form
    {
        public frmLDLApplicationInfo(int LDLApplicationID)
        {
            InitializeComponent();
            ctrlLDLApplicationInfo1.SetLDLApplicationID(LDLApplicationID);
        }
    }
}
