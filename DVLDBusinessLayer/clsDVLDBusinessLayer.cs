using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusinessLayer
{
    public class clsDVLDBusinessLayer
    {
       
    }

    public class clsApplication
    {

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public byte ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public double PaidFees { get; set; }
        public int CreatedByUserID { get; set; }

        enum enMode { eAdd = 1, eUpdate = 2 }
        enMode _Mode;
        protected clsApplication(int ApplicationID,
             int ApplicantPersonID, DateTime ApplicationDate,
             int ApplicationTypeID, byte ApplicationStatus,
             DateTime LastStatusDate, double PaidFees,
             int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            _Mode = enMode.eUpdate;
        }

        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.MinValue;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = 0;
            this.LastStatusDate = DateTime.MinValue;
            this.PaidFees = -1;
            this.CreatedByUserID = -1;
            _Mode = enMode.eAdd;
        }

        public static clsApplication Find(int ApplicationID)
        {

            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.MinValue;
            int ApplicationTypeID = -1;
            byte ApplicationStatus = 0;
            DateTime LastStatusDate = DateTime.MinValue;
            Double PaidFees = -1;
            int CreatedByUserID = -1;

            if (clsApplicationsDataAccess.FindApplicationByID(ApplicationID, ref ApplicantPersonID,
                ref ApplicationDate, ref ApplicationTypeID,
                ref ApplicationStatus,
                ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID,
                    ApplicantPersonID, ApplicationDate,
                    ApplicationTypeID, ApplicationStatus,
                    LastStatusDate, PaidFees, CreatedByUserID);
            }
            return null;
        }

        private bool AddNewApplication()
        {
            int ApplicationID = -1;

            if (clsApplicationsDataAccess.AddNewApplication(ref ApplicationID, this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate
                , this.PaidFees, this.CreatedByUserID))
            {
                this.ApplicationID = ApplicationID;
                return true;
            }

            return false;
        }

        private bool UpdateApplication()
        {
            return clsApplicationsDataAccess.UpdateApplication(this.ApplicationID,
                this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
                this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            return clsApplicationsDataAccess.DeleteApplication(ApplicationID);
        }

        public bool save()
        {

            switch (_Mode)
            {
                case enMode.eAdd:
                    {
                        if (AddNewApplication())
                        {
                            _Mode = enMode.eUpdate;
                            return true;
                        }
                    }
                    break;

                case enMode.eUpdate:
                    {
                        if (UpdateApplication())
                        {
                            return true;
                        }
                    }
                    break;
            }

            return false;
        }

        public bool ChangeApplicationStatus(byte Status)
        {
            return clsApplicationsDataAccess.ChangeApplicationStatus(ApplicationID, Status);
        }
    }

    public class clsApplicationTypes
    {
        public int ApplicationTypesID { get; }
        public string ApplicationTypesTitle { get; set; }
        public Single ApplicationFees { get; set; }

        public static DataTable GetAllApplicationTypesContent()
        {
            return clsApplicationTypesDataAccess.GetAllApplicationTypesContent();
        }
        private clsApplicationTypes(int ApplicationTypesID,
           string ApplicationTypesTitle, Single ApplicationFees)
        {
            this.ApplicationTypesID = ApplicationTypesID;
            this.ApplicationTypesTitle = ApplicationTypesTitle;
            this.ApplicationFees = ApplicationFees;
        }

        public static clsApplicationTypes FindApplicationTypeByID(int ApplicationTypesID)
        {
            string ApplicationTypesTitle = "";
            Single ApplicationFees = 0;

            if (!clsApplicationTypesDataAccess.FindApplicationTypeByID(
                ApplicationTypesID,
                ref ApplicationTypesTitle, ref ApplicationFees))
            {
                return null;
            }

            return new clsApplicationTypes(ApplicationTypesID, ApplicationTypesTitle, ApplicationFees);
        }

        private bool UpdateApplicationType()
        {
            if (ApplicationTypesTitle == string.Empty || ApplicationFees < 0)
            {
                return false;
            }
            return clsApplicationTypesDataAccess.UpdateApplicationTypes(this.ApplicationTypesID, this.ApplicationTypesTitle, this.ApplicationFees);
        }

        public bool Save()
        {
            return UpdateApplicationType();
        }

        public static double GetApplicationFees(int ApplicationTypesID)
        {
            return clsApplicationTypesDataAccess.GetApplicationFees(ApplicationTypesID);
        }


    }

    public class clsDrivers
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        enum enMode { eAdd = 1, eUpdate = 2 }

        enMode _Mode;

        private clsDrivers(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;

            _Mode = enMode.eUpdate;
        }

        public clsDrivers()
        {
            this.DriverID = 0;
            this.PersonID = 0;
            this.CreatedByUserID = 0;
            this.CreatedDate = DateTime.MinValue;

            _Mode = enMode.eAdd;
        }

        public static clsDrivers FindDriverByPersonID(int PersonID)
        {
            int DriverID = 0;
            int CreatedByUserID = 0;
            DateTime CreatedDate = DateTime.MinValue;

            if (clsDriversDataAccess.FindDriverByPersonID(ref DriverID,
                PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }

            return null;
        }

        public static clsDrivers FindDriverByDriverID(int DriverID)
        {
            int PersonID = 0;
            int CreatedByUserID = 0;
            DateTime CreatedDate = DateTime.MinValue;

            if (clsDriversDataAccess.FindDriver(DriverID,
              ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }

            return null;
        }

        private bool AddNewDriver()
        {

            int DriverID = 0;
            CreatedDate = DateTime.Now;
            if (clsDriversDataAccess.AddNewDriver(ref DriverID
                , this.PersonID, this.CreatedByUserID,
                this.CreatedDate))
            {
                this.DriverID = DriverID;
                return true;
            }

            return false;
        }

        public bool save()
        {
            switch (_Mode)
            {
                case enMode.eAdd:
                    {
                        if (AddNewDriver())
                        {
                            _Mode = enMode.eUpdate;
                            return true;
                        }
                    }
                    break;

                case enMode.eUpdate:
                    {

                    }
                    break;
            }

            return false;
        }

        public static DataTable GetAllDrivers()
        {
            return clsDriversDataAccess.GetAllDrivers();
        }
    }

    public class clsInternationalLicense
    {
        public static DataTable GetAllInternationalPersonLicense(int PersonID)
        {
            return clsInternationalLicenseDataAccess.GetAllInternationalPersonLicense(PersonID);
        }
    }

    public class clsLDLApplication : clsApplication
    {
        public int LocalDrivingLicenseApplicationID { get; set; }

        public int LicenseClassID { get; set; }

        enum enMode { eAdd = 1, eUpdate = 2 }
        enMode _Mode;
        public clsLDLApplication() : base()
        {
            LocalDrivingLicenseApplicationID = -1;
            LicenseClassID = -1;
            this._Mode = enMode.eAdd;

        }

        private clsLDLApplication(int localDrivingLicenseApplicationID,
            int licenseClassID, int ApplicationID,
             int ApplicantPersonID, DateTime ApplicationDate,
             int ApplicationTypeID, byte ApplicationStatus,
             DateTime LastStatusDate, double PaidFees,
             int CreatedByUserID)
            : base(ApplicationID,
              ApplicantPersonID, ApplicationDate,
              ApplicationTypeID, ApplicationStatus,
              LastStatusDate, PaidFees,
             CreatedByUserID)
        {
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.LicenseClassID = licenseClassID;
            this._Mode = enMode.eUpdate;
        }

        public static clsLDLApplication FindLDLApplication(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1;
            int LicenseClassID = -1;

            if (clsLDLApplicationDataAccess.FindLDLApplication(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID))
            {
                clsApplication application = clsApplication.Find(ApplicationID);

                if (application != null)
                {
                    return new clsLDLApplication(LocalDrivingLicenseApplicationID,
                        LicenseClassID, ApplicationID, application.ApplicantPersonID,
                        application.ApplicationDate, application.ApplicationTypeID,
                        application.ApplicationStatus,
                        application.LastStatusDate, application.PaidFees,
                        application.CreatedByUserID);
                }
            }

            return null;
        }

        private bool AddNewLDLApplication()
        {
            if (!base.save())
            {
                return false;
            }

            int LocalDrivingLicenseApplicationID = 0;

            if (clsLDLApplicationDataAccess.AddNewLDLApplication(ref LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID))
            {
                this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
                return true;
            }

            return false;
        }

        private bool UpdateLDLApplication()
        {
            if (!base.save())
            {
                return false;
            }

            return clsLDLApplicationDataAccess.UpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);
        }

        public static bool DeleteLDLApplication(int LocalDrivingLicenseApplicationID)
        {
            clsLDLApplication lDLApplication = clsLDLApplication.FindLDLApplication(LocalDrivingLicenseApplicationID);

            if (lDLApplication == null)
            {
                return false;
            }

            if (!clsLDLApplicationDataAccess.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID))
            {
                return false;
            }

            return clsApplication.DeleteApplication(lDLApplication.ApplicationID);
        }

        public static DataTable GetAllLocalDrivingApplications()
        {
            return clsLDLApplicationDataAccess.GetAllLocalDrivingApplications();
        }

        public static DataTable GetAllLicenseClassName()
        {
            return clsLDLApplicationDataAccess.GetAllLicenseClassName();
        }

        public static double GetLDLApplicationFees()
        {
            return clsApplicationTypes.GetApplicationFees(1);
        }

        public bool save()
        {

            switch (this._Mode)
            {
                case enMode.eAdd:
                    {
                        if (AddNewLDLApplication())
                        {
                            this._Mode = enMode.eUpdate;
                            return true;
                        }
                    }
                    break;

                case enMode.eUpdate:
                    {
                        if (this.UpdateLDLApplication())
                        {
                            return true;
                        }
                    }
                    break;
            }

            return false;
        }

        public static bool CheckIfPersonHasActiveApplicationWithTheSameLicenseClassName
            (ref int ApplicationID, int LicenseClassID, int PersonID)
        {
            return clsLDLApplicationDataAccess.CheckIfPersonHasActiveApplicationWithTheSameLicenseClassName(ref ApplicationID, LicenseClassID, PersonID);
        }

        public static DataTable GetAllLDLAppplicationInfo(int LocalDrivingLicenseApplicationID)
        {
            return clsLDLApplicationDataAccess.GetAllLDLAppplicationInfo(LocalDrivingLicenseApplicationID);
        }

        public int GetLicenseID()
        {
            if (ApplicationStatus != 3)
            {
                return 0;
            }

            return clsLDLApplicationDataAccess.GetLicenseID(LocalDrivingLicenseApplicationID);
        }

    }

    public class clsLicense
    {
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClass { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public double PaidFees { get; set; }
        public bool IsActive { get; set; }
        public byte IssueReason { get; set; }
        public int CreatedByUserID { get; set; }


        enum enMode { eAdd = 1, eUpdate = 2 }
        enMode _Mode;
        public clsLicense()
        {
            LicenseID = 0;
            ApplicationID = 0;
            DriverID = 0;
            LicenseClass = 0;
            IssueDate = DateTime.MinValue;
            ExpirationDate = DateTime.MinValue;
            Notes = "";
            PaidFees = 0;
            IsActive = false;
            IssueReason = 0;
            CreatedByUserID = 0;
            _Mode = enMode.eAdd;
        }

        private clsLicense(int licenseID, int applicationID, int driverID, int licenseClass, DateTime issueDate, DateTime expirationDate, string notes, double paidFees, bool isActive, byte issueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClass = licenseClass;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;

            _Mode = enMode.eUpdate;
        }

        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = 0;
            int DriverID = 0;
            int LicenseClass = 0;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            string Notes = string.Empty;
            double PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 0;
            int CreatedByUserID = 0;

            if (clsLicenseDataAccess.FindLicense(LicenseID
                , ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate,
               ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive,
               ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID,
                    ApplicationID, DriverID, LicenseClass, IssueDate,
                    ExpirationDate, Notes, PaidFees, IsActive,
                    IssueReason, CreatedByUserID);
            }

            return null;

        }

        private bool AddNewLicense()
        {
            clsApplication application = clsApplication.Find(ApplicationID);

            clsUsers user = clsUsers.FindUserByUserID(CreatedByUserID);

            if (user == null)
            {
                return false;
            }

            if (application == null)
            {
                return false;
            }

            clsDrivers driver = clsDrivers.FindDriverByPersonID(application.ApplicantPersonID);

            if (driver == null)
            {
                driver = new clsDrivers();
                driver.PersonID = application.ApplicantPersonID;
                driver.CreatedByUserID = CreatedByUserID;

                if (!driver.save())
                {
                    return false;
                }
            }

            DriverID = driver.DriverID;

            clsLicenseClasses licenseClasse = clsLicenseClasses.Find(this.LicenseClass);

            if (licenseClasse == null)
            {
                return false;
            }

            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now.AddYears(licenseClasse.DefaultValidityLength);




            int LicenseID = 0;



            if (clsLicenseDataAccess.AddNewLicense(ref LicenseID,
                this.ApplicationID, this.DriverID, this.LicenseClass,
                this.IssueDate, this.ExpirationDate, this.Notes
                , this.PaidFees, this.IsActive, this.IssueReason,
                this.CreatedByUserID))
            {
                application.ApplicationStatus = 3;
                application.save();
                this.LicenseID = LicenseID;
                return true;
            }

            return false;
        }

        private bool UpdateLicense()
        {
            return clsLicenseDataAccess.UpdateLicense(this.LicenseID,
                this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate,
                this.ExpirationDate, this.Notes, this.PaidFees,
                this.IsActive, this.IssueReason,
                this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.eAdd:
                    {
                        if (AddNewLicense())
                        {
                            return true;
                            _Mode = enMode.eUpdate;
                        }
                    }
                    break;

                case enMode.eUpdate:
                    {
                        if (UpdateLicense())
                        {
                            return true;
                        }
                    }
                    break;

            }

            return false;
        }

        public static bool CheckIfPersonHasAlreadyLicenseInTheLicenseClass(int PersonID, int LicenseClassID)
        {
            return clsLicenseDataAccess.CheckIfPersonHasAlreadyLicenseInTheLicenseClass(PersonID, LicenseClassID);
        }

        public static DataTable GetAllLocalPersonLicense(int PersonID)
        {
            return clsLicenseDataAccess.GetAllLocalPersonLicense(PersonID);
        }

        public bool CheckIfLicenseIsDetained()
        {
            return clsLicenseDataAccess.CheckIfLicenseIsDetained(LicenseID);
        }
    }

    public class clsLicenseClasses
    {
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public double ClassFees { get; set; }

        enum enMode { eAdd = 1, eUpdate = 2 }
        enMode _Mode;

        private clsLicenseClasses(int LicenseClassID,
             string ClassName, string ClassDescription,
            byte MinimumAllowedAge, byte DefaultValidityLength,
            double ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;

            _Mode = enMode.eUpdate;

        }

        public static clsLicenseClasses Find(int LicenseClassID)
        {

            string ClassName = string.Empty;
            string ClassDescription = string.Empty;
            byte MinimumAllowedAge = 0;
            byte DefaultValidityLength = 0;
            double ClassFees = 0;

            if (clsLicenseClassesDataAccesscs.FindLicenseClass
                (LicenseClassID, ref ClassName, ref ClassDescription,
                ref MinimumAllowedAge, ref DefaultValidityLength,
                ref ClassFees))
            {
                return new clsLicenseClasses(LicenseClassID, ClassName, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength,
                    ClassFees);
            }

            return null;

        }

    }

    public class clsPeople
    {
        int PersonID;

        public int GetPersonID()
        {
            return PersonID;
        }

        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
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

            PersonID = -1;
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

            this.PersonID = PersonID;
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

            dt = clsPeoplesDataAcess.GetLittleInfoAboutPeople();

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

            if (clsPeoplesDataAcess.FindPersonByID(PersonID,
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
            return clsPeoplesDataAcess.DeletePerson(PersonID);
        }

        private bool UpdatePerson()
        {
            return clsPeoplesDataAcess.UpdatePerson(PersonID, NationalNo,
                FirstName, SecondName, ThirdName, LastName, DateOfBirth
                , Gendor, Address, Phone, Email, NationalityCountryID,
                ImagePath);
        }

        private bool AddNewPerson()
        {
            int PersonID = -1;

            if (clsPeoplesDataAcess.AddNewPerson(ref PersonID, NationalNo,
                 FirstName, SecondName, ThirdName, LastName, DateOfBirth
                 , Gendor, Address, Phone, Email, NationalityCountryID,
                 ImagePath))
            {
                this.PersonID = PersonID;
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
            return clsPeoplesDataAcess.GetAllCountriesName();
        }

        //public static int GetNumberOfPeopleInSystem()
        //{
        //    return clsPeoplesDataAcess.GetNumberOfPeopleInSystem();
        //}

        public static bool IsNationalNoInSystem(string NationalNo)
        {
            return clsPeoplesDataAcess.IsNationalNoInSystem(NationalNo);
        }

        public static string GetCountryNameByPersonID(int personID)
        {
            return clsPeoplesDataAcess.GetCountryNameByPersonID(personID);
        }
    }

    public class clsTestAppointments
    {
        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public double PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }

        enum enMode { eAdd = 1, eUpdate = 2 };

        enMode _Mode;

        private clsTestAppointments(int testAppointmentID, int testTypeID, int localDrivingLicenseApplicationID, DateTime appointmentDate, double paidFees, int createdByUserID, bool isLocked, int retakeTestApplicationID)
        {
            TestAppointmentID = testAppointmentID;
            TestTypeID = testTypeID;
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            IsLocked = isLocked;
            RetakeTestApplicationID = retakeTestApplicationID;
            _Mode = enMode.eUpdate;
        }

        public clsTestAppointments()
        {
            TestAppointmentID = 0;
            TestTypeID = 0;
            LocalDrivingLicenseApplicationID = 0;
            AppointmentDate = DateTime.MinValue;
            PaidFees = 0;
            CreatedByUserID = 0;
            IsLocked = false;
            RetakeTestApplicationID = 0;

            _Mode = enMode.eAdd;
        }

        public static DataTable GetAllTestAppointmentByLDLAppIDAndTestTypeID
            (int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentsDataAccess.GetAllTestAppointmentByLDLAppIDAndTestTypeID(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static clsTestAppointments FindTestAppointmentByID(int TestAppointmentID)
        {

            int TestTypeID = 0;
            int LocalDrivingLicenseApplicationID = 0;
            DateTime AppointmentDate = DateTime.MinValue;
            double PaidFees = 0;
            int CreatedByUserID = 0;
            bool IsLocked = false;
            int RetakeTestApplicationID = 0;

            if (clsTestAppointmentsDataAccess.FindAppointment(TestAppointmentID
                , ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate,
                ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
            {
                return new clsTestAppointments(TestAppointmentID,
                    TestTypeID, LocalDrivingLicenseApplicationID,
                    AppointmentDate, PaidFees,
                    CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }

            return null;
        }

        private bool AddNewTestAppointment()
        {
            int Trail = 0;
            if (clsTestAppointments.CheckIfThisLDLAppFailedThisTest(LocalDrivingLicenseApplicationID, TestTypeID, ref Trail))
            {
                clsApplication RetakeApplication = new clsApplication();

                DataTable LDLInfo = clsLDLApplication.GetAllLDLAppplicationInfo(LocalDrivingLicenseApplicationID);

                clsApplicationTypes RetakeApplicationType = clsApplicationTypes.FindApplicationTypeByID(7);

                RetakeApplication.ApplicantPersonID = Convert.ToInt32(LDLInfo.Rows[0][11]);
                RetakeApplication.ApplicationDate = DateTime.Now;
                RetakeApplication.ApplicationTypeID = 7;
                RetakeApplication.ApplicationStatus = 3;
                RetakeApplication.LastStatusDate = DateTime.Now;
                RetakeApplication.PaidFees = RetakeApplicationType.ApplicationFees;
                RetakeApplication.CreatedByUserID = CreatedByUserID;

                if (!RetakeApplication.save())
                {
                    return false;
                }

                RetakeTestApplicationID = RetakeApplication.ApplicationID;
            }
            else
            {
                RetakeTestApplicationID = 0;
            }

            int TestAppointmentID = 0;

            if (clsTestAppointmentsDataAccess.AddNewTestAppointment
                (ref TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
                AppointmentDate, PaidFees, CreatedByUserID, IsLocked,
                RetakeTestApplicationID))
            {
                this.TestAppointmentID = TestAppointmentID;
                return true;
            }

            return false;
        }

        private bool UpdateAppointmentDate()
        {
            return clsTestAppointmentsDataAccess.UpdateAppointmentDate
                (TestAppointmentID, AppointmentDate);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.eAdd:
                    {
                        if (AddNewTestAppointment())
                        {
                            _Mode = enMode.eUpdate;
                            return true;
                        }
                    }
                    break;

                case enMode.eUpdate:
                    {
                        if (UpdateAppointmentDate())
                        {
                            return true;
                        }
                    }
                    break;
            }

            return false;

        }

        public static bool CheckIfThisLDLAppHasActiveTestAppointment
            (int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentsDataAccess.CheckIfThisLDLAppHasActiveTestAppointment(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public bool UpdateAppointmentLockedToTrue()
        {
            return clsTestAppointmentsDataAccess.UpdateAppointmentActive(TestAppointmentID, true);
        }

        public static bool CheckIfThisLDLAppPassedThisTest
            (int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentsDataAccess.CheckIfThisLDLAppPassedThisTest(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static bool CheckIfThisLDLAppFailedThisTest
            (int LocalDrivingLicenseApplicationID, int TestTypeID, ref int Trail)
        {
            return clsTestAppointmentsDataAccess.CheckIfThisLDLAppFailedThisTest(LocalDrivingLicenseApplicationID, TestTypeID, ref Trail);
        }
    }

    public class clsTests
    {
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        enum enMode
        {
            eAdd = 1
        };

        enMode _Mode;
        public clsTests()
        {
            _Mode = enMode.eAdd;
            TestID = 0;
            TestAppointmentID = 0;
            TestResult = false;
            Notes = string.Empty;
            CreatedByUserID = 0;

        }

        private bool AddNewTest()
        {
            int TestID = 0;



            if (clsTestsDataAccess.AddNewTest(ref TestID, TestAppointmentID
                , TestResult, Notes, CreatedByUserID))
            {
                this.TestID = TestID;

                clsTestAppointments TestAppointment = clsTestAppointments.FindTestAppointmentByID(TestAppointmentID);

                if (TestAppointment == null)
                {
                    return false;
                }

                TestAppointment.UpdateAppointmentLockedToTrue();

                return true;
            }
            return false;
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.eAdd:
                    {
                        if (AddNewTest())
                        {
                            return true;
                        }
                    }
                    break;

            }

            return false;
        }



    }

    public class clsTestTypes
    {
        public int TestTypeID { get; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public double TestTypeFees { get; set; }

        public static DataTable GetAllTestTypesContent()
        {
            return clsTestTypesDataAccess.GetAllTestTypesContent();
        }
        private clsTestTypes(int TestTypeID,
           string TestTypeTitle, string TestTypeDescription,
           double TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
        }

        public static clsTestTypes FindTestTypeByID(int TestTypeID)
        {
            string TestTypeTitle = "";
            string TestTypeDescription = "";
            double TestTypeFees = 0;

            if (!clsTestTypesDataAccess.FindTestTypeByID(TestTypeID, ref TestTypeTitle,
                ref TestTypeDescription, ref TestTypeFees))
            {
                return null;
            }

            return new clsTestTypes(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
        }

        private bool UpdateApplicationType()
        {
            if (TestTypeTitle == string.Empty || (TestTypeDescription == string.Empty || TestTypeFees < 0))
            {
                return false;
            }
            return clsTestTypesDataAccess.UpdateTestTypes(this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
        }

        public bool Save()
        {
            return UpdateApplicationType();
        }
    }

    public class clsUsers
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public static bool CheckIfUserNameInSystem(string userName)
        {
            return clsUsersDataAccess.CheckIfUserNameInSystem(userName);
        }

        public static bool CheckIfUserNameAndPasswordIsTrue(string userName, string password)
        {
            return clsUsersDataAccess.CheckIfUserNameAndPasswordIsTrue(userName, password);
        }

        public static bool CheckIfUserIsActive(string userName)
        {
            return clsUsersDataAccess.CheckIfUserIsActive(userName);
        }

        public static bool GetUserInfoInRemeberUserFile(ref string userName, ref string password)
        {
            return clsUsersDataAccess.GetUserInfoInRemeberUserFile(ref userName, ref password);
        }

        public static bool DeleteAllRecordRemeberUserFile()
        {
            return clsUsersDataAccess.DeleteAllRecordRemeberUserFile();
        }

        public static bool SaveUserInfoRemeberUserFile(string userName, string password)
        {
            return clsUsersDataAccess.SaveUserInfoRemeberUserFile(userName, password);
        }

        public static DataTable GetAllUserInfo()
        {
            return clsUsersDataAccess.GetAllUserInfo();
        }

        public static DataTable GetUserInfo(string UserName)
        {
            return clsUsersDataAccess.GetUserInfo(UserName);
        }

        enum enMode { eAddNewUser = 1, eUpdateUser = 2 }
        enMode _Mode;
        private clsUsers(int userID, int personID, string userName, string password, bool isActive)
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;

            _Mode = enMode.eUpdateUser;
        }

        public clsUsers(int PersonID)
        {
            this.UserID = -1;
            this.PersonID = PersonID;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.IsActive = false;

            _Mode = enMode.eAddNewUser;
        }

        public static clsUsers FindUserByUserID(int UserID)
        {
            int PersonID = 0;
            string UserName = string.Empty;
            string Password = string.Empty;
            bool IsActive = false;

            if (!clsUsersDataAccess.FindUserByUserID(UserID, ref PersonID
                , ref UserName, ref Password, ref IsActive))
            {
                return null;
            }
            else
            {
                return new clsUsers(UserID, PersonID, UserName, Password, IsActive);
            }
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUsersDataAccess.DeleteUserByUserID(UserID);
        }

        private bool AddNewUser()
        {
            if (CheckIfUserNameInSystem(this.UserName)
                ||
                clsUsersDataAccess.CheckIfThisPersonIsUserByPersonID(this.PersonID))
            {
                return false;
            }

            int UserID = this.UserID;
            int PersonID = this.PersonID;
            string UserName = this.UserName;
            string Password = this.Password;
            bool IsActive = this.IsActive;

            if (!clsUsersDataAccess.AddNewUserByPersonID(PersonID, ref UserID, UserName, Password, IsActive))
            {
                return false;
            }

            this.UserID = UserID;

            return true;
        }

        private bool UpdateUser()
        {
            return clsUsersDataAccess.UpdateUserByUserID(this.UserID
                , this.UserName, this.Password, this.IsActive);
        }

        public bool save()
        {
            switch (_Mode)
            {
                case enMode.eAddNewUser:
                    {
                        if (AddNewUser())
                        {
                            _Mode = enMode.eUpdateUser;
                            return true;
                        }
                    }
                    break;

                case enMode.eUpdateUser:
                    {
                        if (UpdateUser())
                        {
                            return true;
                        }
                    }
                    break;
            }

            return false;

        }
        public static bool CheckIfThisPersonIsUserByPersonID(int PersonID)
        {
            return clsUsersDataAccess.CheckIfThisPersonIsUserByPersonID(PersonID);
        }

    }



}
