using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLicense
    {
        public int LicenseID { get; set; }

//        public clsLicense LicenseInfo { get; set; }
        public int ApplicationID { get; set; }

        public clsApplication ApplicationInfo { get; set; }

        public int DriverID { get; set; }

        public clsDrivers DriverInfo { get; set; }

        public int LicenseClass { get; set; }

        public clsLicenseClasses LicenseClassInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public double PaidFees { get; set; }
        public bool IsActive { get; set; }
        public byte IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        public clsUsers UserInfo { get; set; }

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
            this.ApplicationInfo = clsApplication.Find(ApplicationID);
            this.DriverInfo = clsDrivers.FindDriverByDriverID(DriverID);
            this.LicenseClassInfo = clsLicenseClasses.Find(LicenseClass);
            this.UserInfo = clsUsers.FindUserByUserID(CreatedByUserID);
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
                this.ApplicationInfo = clsApplication.Find(ApplicationID);
                this.DriverInfo = clsDrivers.FindDriverByDriverID(DriverID);
                this.LicenseClassInfo = clsLicenseClasses.Find(LicenseClass);
                this.UserInfo = clsUsers.FindUserByUserID(CreatedByUserID);
                return true;
            }

            return false;
        }

        private bool UpdateLicense()
        {
            return clsLicenseDataAccess.UpdateLicenseToNotActive(this.LicenseID,
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
            int DetainID = 0;
            return clsLicenseDataAccess.CheckIfLicenseIsDetained(ref DetainID,LicenseID);
        }
    }
}
