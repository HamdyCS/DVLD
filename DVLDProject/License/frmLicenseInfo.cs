﻿using System;
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
    public partial class frmLicenseInfo : Form
    {
        public frmLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            ctrlLicenseInfo1.SetLicenseID(LicenseID);
        }
    }
}
