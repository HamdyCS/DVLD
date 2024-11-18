using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsPeople
    {
        private int _PersonID;

        public int GetPersonID()
        {
            return _PersonID;
        }

        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }

        public string FullName 
        { 
            get 
            {
                return FirstName + ' ' + SecondName + ' ' + 
                    ThirdName + ' ' + LastName;
            }
        }

        public DateTime DateOfBirth { get; set; }
        public int Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }

        enum enMode { eAddNewPerson = 1, eUpdatePerson = 2 };
        private enMode _Mode;

        public clsPeople()
        {
            _Mode = enMode.eAddNewPerson;

            _PersonID = -1;
            NationalNo = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            DateOfBirth = DateTime.MinValue;
            Gendor = -1;
            Address = "";
            Phone = "";
            Email = "";
            NationalityCountryID = -1;
            ImagePath = "";
        }

        private clsPeople(int PersonID, string NationalNo,
               string FirstName, string SecondName, string ThirdName,
               string LastName, DateTime DateOfBirth, int Gendor,
               string Address, string Phone, string Email, int NationalityCountryID,
               string ImagePath)
        {
            _Mode = enMode.eUpdatePerson;

            this._PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
        }

        public static DataTable GetLittleInfoAboutPeople()
        {
            DataTable dt = new DataTable();

            dt = clsPeoplesDataAccess.GetLittleInfoAboutPeople();

            return dt;
        }

        public static clsPeople FindPeopleByID(int PersonID)
        {

            string NationalNo = "";
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.MinValue;
            int Gendor = -1;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalityCountryID = -1;
            string ImagePath = "";

            if (clsPeoplesDataAccess.FindPersonByID(PersonID,
                 ref NationalNo, ref FirstName,
                 ref SecondName, ref ThirdName,
                 ref LastName, ref DateOfBirth,
                 ref Gendor, ref Address, ref Phone,
                 ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPeople(PersonID, NationalNo, FirstName,
                    SecondName, ThirdName, LastName, DateOfBirth,
                    Gendor, Address, Phone, Email, NationalityCountryID,
                    ImagePath);
            }

            return null;
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPeoplesDataAccess.DeletePerson(PersonID);
        }

        private bool UpdatePerson()
        {
            return clsPeoplesDataAccess.UpdatePerson(_PersonID, NationalNo,
                FirstName, SecondName, ThirdName, LastName, DateOfBirth
                , Gendor, Address, Phone, Email, NationalityCountryID,
                ImagePath);
        }

        private bool AddNewPerson()
        {
            int PersonID = -1;

            if (clsPeoplesDataAccess.AddNewPerson(ref PersonID, NationalNo,
                 FirstName, SecondName, ThirdName, LastName, DateOfBirth
                 , Gendor, Address, Phone, Email, NationalityCountryID,
                 ImagePath))
            {
                this._PersonID = PersonID;
                return true;
            }

            return false;
        }

        public bool save()
        {
            switch (_Mode)
            {
                case enMode.eAddNewPerson:
                    {
                        if (AddNewPerson())
                        {
                            _Mode = enMode.eUpdatePerson;
                            return true;
                        }
                    }
                    break;
                case enMode.eUpdatePerson:
                    {
                        if (UpdatePerson())
                        {
                            return true;
                        }
                    }
                    break;
            }

            return false;
        }

        public static DataTable GetAllCountriesName()
        {
            return clsPeoplesDataAccess.GetAllCountriesName();
        }

        //public static int GetNumberOfPeopleInSystem()
        //{
        //    return clsPeoplesDataAcess.GetNumberOfPeopleInSystem();
        //}

        public static bool IsNationalNoInSystem(string NationalNo)
        {
            return clsPeoplesDataAccess.IsNationalNoInSystem(NationalNo);
        }

        public static string GetCountryNameByPersonID(int personID)
        {
            return clsPeoplesDataAccess.GetCountryNameByPersonID(personID);
        }

        public string GetFullName()
        {
            return FullName;
        }
    }
}
