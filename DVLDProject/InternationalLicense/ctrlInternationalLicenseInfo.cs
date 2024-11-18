using DVLDBusinessLayer;
using DVLDProject.Properties;
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
    public partial class ctrlInternationalLicenseInfo : UserControl
    {
        public int InternationalLicenseID { get; set; }

        clsInternationalLicense _InternationalLicense;
        clsDrivers _driver;
        clsPeople _person;

        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private void FillDataInForm()
        {
            
            labName.Text = _person.FirstName + ' ' + _person.SecondName + ' ' + _person.ThirdName + ' ' + _person.LastName;
            labILicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            labLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            labNationalNumber.Text = _person.NationalNo;

            if (_person.Gendor == 0)
            {
                labGendor.Text = "Male";
                picPerson.Image = Resources.Male_512;
            }

            else if (_person.Gendor == 1)
            {
                labGendor.Text = "Female";
                picPerson.Image = Resources.Female_512;
            }

            labIssuaDate.Text = _InternationalLicense.IssueDate.ToShortDateString();
            labApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            
            if (_InternationalLicense.IsActive)
            {
                labIsActive.Text = "Yes";
            }
            else
            {
                labIsActive.Text = "No";
            }

            labDateOfBirth.Text = _person.DateOfBirth.ToShortDateString();

            labDriverID.Text = _InternationalLicense.DriverID.ToString();

            labExpirationDate.Text = _InternationalLicense.ExpirationDate.ToShortDateString();

            

            try
            {
                picPerson.Load(_person.ImagePath);
            }
            catch (Exception ex)
            {

            }

        }

        public void SetInternationalLicenseID(int InternationalLicenseID)
        {
            this.InternationalLicenseID = InternationalLicenseID;

            _InternationalLicense = clsInternationalLicense.Find(InternationalLicenseID);

            if (_InternationalLicense == null)
            {
                return;
            }

            

            _driver = clsDrivers.FindDriverByDriverID(_InternationalLicense.DriverID);

            if (_driver == null)
            {
                return;
            }

            _person = clsPeople.FindPeopleByID(_driver.PersonID);

            if (_person == null)
            {
                return;
            }

            FillDataInForm();
        }

    }
}
