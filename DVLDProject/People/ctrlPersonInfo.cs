using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDProject.Properties;
using DVLDBusinessLayer;

namespace DVLDProject.People
{
    public partial class ctrlPersonInfo : UserControl
    {
       
        public int PersonID
        {
            get;
            set;
        }

        //public int PersonID
        //{
        //    get 
        //    {
        //        return PersonID; 
        //    }
        //    set 
        //    {
        //        PersonID = value;
        //        this.PersonID = PersonID;
        //        FillAllDataInForm();
        //    }
        //}

        public void SetPersonID(int PersonID)
        {
  
           this. PersonID = PersonID;
            FillAllDataInForm();
            //linkLabelEditPersonInfo.Visible = true;
        }

        private void FillAllDataInForm()
        {
            clsPeople person = clsPeople.FindPeopleByID(PersonID);

            if (person == null)
            {
                lapPersonID.Text = "N/A";
                lapName.Text = "N/A";
                lapNationalNo.Text = "N/A";
                lapEmail.Text = "N/A";
                lapAddress.Text = "N/A";
                lapDateOfBirth.Text = "N/A";
                lapPhone.Text = "N/A";
                lapCountry.Text = "N/A";
                lapGendor.Text = "N/A";

                picPersonImage.Image = Resources.Male_512;

                return;
            }

            linkLabelEditPersonInfo.Visible = true;


            lapPersonID.Text = person.GetPersonID().ToString();

            lapName.Text = person.FirstName +" "+ person.SecondName + " " + person.ThirdName + " " + person.LastName;

            lapNationalNo.Text = person.NationalNo;

            lapEmail.Text = person.Email;

            lapAddress.Text = person.Address;

            lapDateOfBirth.Text = person.DateOfBirth.ToShortDateString();

            lapPhone.Text = person.Phone;

            lapCountry.Text = clsPeople.GetCountryNameByPersonID(PersonID);

            try
            {

            picPersonImage.ImageLocation = person.ImagePath;
            }
            catch (Exception ex) 
            {
            }

            if(person.Gendor==0)
            {

                lapGendor.Text = "Male";
            }
            else if(person.Gendor==1)
            {
                lapGendor.Text = "Female";

            }
        }

        public ctrlPersonInfo()
        {
            InitializeComponent();
        }

        

       

        private void linkLabelEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmAddOrEditPerson(PersonID);
            frm.ShowDialog();
            SetPersonID(PersonID);
        }
    }
}
