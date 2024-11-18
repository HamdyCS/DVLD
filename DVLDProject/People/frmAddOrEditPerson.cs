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
using Guna.UI2.WinForms;
using DVLDBusinessLayer;
using System.IO;
using System.Threading;

using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Text.RegularExpressions;


namespace DVLDProject
{
    public partial class frmAddOrEditPerson : Form
    {
        public delegate void DataBackEventHandler(int PersonID);

        public event DataBackEventHandler DataBack;

 //       string _ImagePath = "";
  //      int _Gendor = 0;
        int _PersonID = -1;
       // bool _NotImageFromUser = false;
        clsPeople _Person;

        enum enMode {eAddNewPerson = 1, eUpdatePerson = 2};
        private enMode _Mode;


        private void ApplyUpdateMode(int PersonID)
        {
            labHeader.Text = "Update Person";

            _Person = clsPeople.FindPeopleByID(PersonID);

            if (_Person == null)
            {
                MessageBox.Show("Person not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            labPersonID.Text = _Person.GetPersonID().ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;

            txtNationalNo.Text = _Person.NationalNo;

            if (_Person.Gendor == 0)
            {

                rbtnMale.Checked = true;
            }
            else if (_Person.Gendor == 1)
            {
                rbtnFemale.Checked = true;
            }

            txtEmail.Text = _Person.Email;

            txtAddress.Text = _Person.Address;

            txtPhone.Text = _Person.Phone;

            dtbDateOfBirth.Value = _Person.DateOfBirth;

            comCountry.SelectedIndex = _Person.NationalityCountryID - 1;

            try
            {
                if (_Person.ImagePath != "")
                {
                    //picPersonImage.Load(_Person.ImagePath);
                    picPersonImage.ImageLocation = _Person.ImagePath;
                }
                else
                {
                    if(_Person.Gendor==0)
                    {
                        picPersonImage.Image = Resources.Male_512;
                    }
                    else
                    {
                        picPersonImage.Image = Resources.Female_512;
                    }
                }

            }
            catch (Exception ex)
            {
                if (_Person.Gendor == 0)
                {
                    picPersonImage.Image = Resources.Male_512;
                }
                else
                {
                    picPersonImage.Image = Resources.Female_512;
                }
            }

            linkLabelRemove.Visible = (_Person.ImagePath != "");

        }

        public frmAddOrEditPerson(int PersonID)
        {
            InitializeComponent();
           _Mode = enMode.eUpdatePerson;
            
            _PersonID = PersonID;
            
        }

        public frmAddOrEditPerson()
        {
            InitializeComponent();
            _Mode=enMode.eAddNewPerson;
        }

        private void FillCountryCombobox()
        {
            DataTable dt = clsPeople.GetAllCountriesName();

            foreach (DataRow dr in dt.Rows) 
            {
                comCountry.Items.Add(dr[0].ToString());
            }
        }

        private void frmAddOrEditPersonInfo_Load(object sender, EventArgs e)
        {

            rbtnMale.Checked = true;
           

            DateTime MaxDate = new DateTime();
            MaxDate = DateTime.Now;

            MaxDate = MaxDate.AddYears(-18);

            dtbDateOfBirth.MaxDate = MaxDate;
            dtbDateOfBirth.MinDate = DateTime.Now.AddYears(-120);

            FillCountryCombobox();

            comCountry.SelectedIndex = 51 - 1;//select Egypt

            if (_Mode == enMode.eUpdatePerson)
            {
                ApplyUpdateMode(_PersonID);
            }
            
        }

        private void rbtnMale_CheckedChanged(object sender, EventArgs e)
        {
            if(((RadioButton)sender).Checked && picPersonImage.ImageLocation == null) 
            {
                picPersonImage.Image = Resources.Male_512;
            }
              //  _Gendor = 0;
        }

        private void rbtnFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked && picPersonImage.ImageLocation == null)
            {
                picPersonImage.Image = Resources.Female_512;
            }
               // _Gendor = 1;
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {

            if(_Mode == enMode.eUpdatePerson && clsPeople.IsNationalNoInSystem(txtNationalNo.Text.Trim()))
            {
                return;
            }

            if(string.IsNullOrEmpty(txtNationalNo.Text.Trim())
                ||clsPeople.IsNationalNoInSystem(txtNationalNo.Text.Trim())) 
            {
                e.Cancel = true;
                errorProvider.SetError(txtNationalNo, "National number is used for another person!");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(txtNationalNo, "");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {

            if(string.IsNullOrEmpty(txtEmail.Text))
            {
                return;
            }

            //if (!txtEmail.Text.Contains("@gmail.com"))
            //{
            //    e.Cancel = true;

            //   txtEmail.Focus();

            //    errorProvider.SetError(txtEmail, "Incorrect Format!");
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider.SetError(txtEmail, "");
            //}

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            var regex =  Regex.IsMatch(txtEmail.Text, pattern, RegexOptions.IgnoreCase);
            if(!regex)
            {
                e.Cancel = true;
                errorProvider.SetError(txtEmail, "Incorrect Format!");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(txtEmail, "");
            }
        }

        private void ValidatingTextBox(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty( ((Guna2TextBox)sender).Text ))
            {
                e.Cancel = true;

                ((Guna2TextBox)sender).Focus();

                errorProvider.SetError(((Guna2TextBox)sender), "Please Fill it!");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError( ((Guna2TextBox)sender), "");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Form not vaild", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string FolderPath = "C:\\DVLD-People-Images";
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            //if(picPersonImage.Tag.ToString() == "0"|| picPersonImage.Tag.ToString() == "1")
            //{
            //    _NotImageFromUser = true;
            //}

            if(_Mode == enMode.eAddNewPerson)
            {
                _Person = new clsPeople();
               

            }

            if(_Mode == enMode.eUpdatePerson)
            {
                //delete image
                if (picPersonImage.ImageLocation != _Person.ImagePath && _Person.ImagePath != "")
                    {
                   
                    for (int i = 0; i < 3; i++)
                    {
                        try
                        {

                            File.Delete(_Person.ImagePath);
                            break;
                        }
                        catch (IOException)
                        {
                            Thread.Sleep(1000);
                        }
                    }

                }

            }
        

            Guid guid = Guid.NewGuid();

            //copy image
            if (picPersonImage.ImageLocation != null && picPersonImage.ImageLocation != _Person.ImagePath)
            {
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        FileInfo info = new FileInfo(picPersonImage.ImageLocation);
                        File.Copy(picPersonImage.ImageLocation , $"{FolderPath}\\{guid}{info.Extension}");

                        _Person.ImagePath = $"{FolderPath}\\{guid}{info.Extension}";
                        break;
                    }
                    catch (IOException)
                    {
                        Thread.Sleep(1000);
                    }
                }
            }

            // fill person data in object
            _Person.NationalNo = txtNationalNo.Text;

            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;

            _Person.DateOfBirth = dtbDateOfBirth.Value;

            if(rbtnMale.Checked)
            {
                _Person.Gendor = 0;
            }
            else if(rbtnFemale.Checked)
            {
                _Person.Gendor = 1;
            }
           // _Person.Gendor = _Gendor;

            _Person.Address = txtAddress.Text;

            _Person.Phone = txtPhone.Text;

            _Person.Email = txtEmail.Text;

            _Person.NationalityCountryID = comCountry.SelectedIndex+1;

            

            if(picPersonImage.ImageLocation != null)
            {
                _Person.ImagePath = $"{FolderPath}\\{guid}.jpg";
            }
            else
            {
                _Person.ImagePath = "";
            }


            if ( _Person.save() )
            {
                MessageBox.Show("Data saved successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(_Person.GetPersonID());
                this.Close();
                return;
            }
           else
            {
                MessageBox.Show("Data donot save successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void linkSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set file filters for all image types
            openFileDialog1.Filter = "Image Files (*.bmp, *.jpg, *.jpeg, *.gif, *.png)|*.bmp;*.jpg;*.jpeg;*.gif;*.png|All files (*.*)|*.*";

            // Set the initial directory to open when the dialog appears
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            // Show the file open dialog to the user and check if OK was clicked
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
               
                // Get the selected file path and display it
                string filePath = openFileDialog1.FileName;

                //_ImagePath = filePath;

                picPersonImage.ImageLocation = filePath;
                linkLabelRemove.Visible = true;
               // MessageBox.Show(filePath);

                // Optionally, you can perform necessary actions with the selected file here
            }
           

        }

        private void linkLabelRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            picPersonImage.ImageLocation = null; 
            if(rbtnMale.Checked)
            {
                picPersonImage.Image = Resources.Male_512;
            }
            else if(rbtnFemale.Checked)
            {
                picPersonImage.Image = Resources.Female_512;
            }
        }

        
    }
}
