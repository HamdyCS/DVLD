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
    public partial class ctrlUserInfo : UserControl
    {

        public void SetUserID(int UserID)
        {
            clsUsers User = clsUsers.FindUserByUserID(UserID);

            if (User != null)
            {
                ctrlPersonInfo1.SetPersonID(User.PersonID);

                labUserID.Text = User.UserID.ToString();
                labUserName.Text = User.UserName.ToString();

                if (User.IsActive)
                {
                    labIsActive.Text = "Yes";
                }
                else
                {
                    labIsActive.Text = "No";

                }
            }

        }

        public ctrlUserInfo()
        {
            InitializeComponent();
        }
    }
}
