
using DVLDProject.Properties;
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
    public partial class ctrlLicenseInfo : UserControl
    {
        public int LicenseID { get ; set ; }

        clsLicense _license;
        clsLicenseClasses _licenseClasse;
        clsDrivers _driver;
        clsPeople _person;

        private void FillDataInForm()
        {
            labClassName.Text = _licenseClasse.ClassName;
            labName.Text = _person.FirstName +  ' ' + _person.SecondName + ' ' + _person.ThirdName + ' ' + _person.LastName;
            labLicenseID.Text = _license.LicenseID.ToString();
            labNationalNumber.Text = _person.NationalNo;

            if (_person.Gendor == 0) 
            {
                labGendor.Text = "Male";
                picPerson.Image = Resources.Male_512;
            }

            else if(_person.Gendor == 1)
            {
                labGendor.Text = "Female";
                picPerson.Image = Resources.Female_512;
            }

            labIssuaDate.Text = _license.IssueDate.ToShortDateString();

            switch (_license.IssueReason)
            {
                case 1:
                    labIssueReason.Text = "First Time";
                    break;

                case 2:
                    labIssueReason.Text = "Renew";
                    break;

                case 3:
                    labIssueReason.Text = "Replacement for Damaged";
                    break;

                case 4:
                    labIssueReason.Text = "Replacement for Lost";
                    break;
            }

            if(_license.Notes==string.Empty)
            {
                labNotes.Text = "No Notes";
            }
            else
            {
                labNotes.Text = _license.Notes;
            }

            if(_license.IsActive)
            {
                labIsActive.Text = "Yes";
            }
            else
            {
                labIsActive.Text = "No";
            }

            labDateOfBirth.Text = _person.DateOfBirth.ToShortDateString();

            labDriverID.Text = _driver.DriverID.ToString();

            labExpirationDate.Text = _license.ExpirationDate.ToShortDateString();

            if(_license.CheckIfLicenseIsDetained())
            {
                labIsDetiained.Text = "Yes";
            }
            else
            {
                labIsDetiained.Text = "No";

            }

            try
            {
                picPerson.Load(_person.ImagePath);
            }
            catch (Exception ex)
            {

            }

        }
        public void SetLicenseID(int LicenseID)
        {
            this.LicenseID = LicenseID;

           _license = clsLicense.Find(LicenseID);

            if (_license == null)
            {
                return;
            }

            _licenseClasse = clsLicenseClasses.Find(_license.LicenseClass);

            if(_licenseClasse==null)
            {
                return;
            }

            _driver = clsDrivers.FindDriverByDriverID(_license.DriverID);

            if(_driver == null)
            {
                return;
            }

            _person = clsPeople.FindPeopleByID(_driver.PersonID);

            if(_person == null)
            {
                return;
            }

            FillDataInForm();
        }
        public ctrlLicenseInfo()
        {
            InitializeComponent();
        }
    }
}
