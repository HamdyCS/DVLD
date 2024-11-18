using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsInternationalLicense : clsApplication
    {

        public int InternationalLicenseID {  get; set; }
//        public int ApplicationID { get; set; }
        public int DriverID { get; set; }

        public clsDrivers DriverInfo {  get; set; }

        public int IssuedUsingLocalLicenseID { get; set; }

        public clsLicense LocalLicenseInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
//        public int CreatedByUserID { get; set; }

        enum enMode
        {
            eAdd = 1,
            eUpdate = 2
        }

        enMode _Mode;
        private clsInternationalLicense(int internationalLicenseID,
            int ApplicationID, int driverID,
            int issuedUsingLocalLicenseID, DateTime issueDate,
            DateTime expirationDate, bool isActive, int createdByUserID
            ,
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
            this.InternationalLicenseID = internationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = driverID;
            this.IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;
            this.IsActive = isActive;
            this.CreatedByUserID = createdByUserID;
            this.LocalLicenseInfo = clsLicense.Find(this.IssuedUsingLocalLicenseID);
            this.DriverInfo = clsDrivers.FindDriverByDriverID(this.DriverID);
            _Mode = enMode.eUpdate;
        }

        public clsInternationalLicense():base()
        {
            this.InternationalLicenseID = 0;
            this.ApplicationID = 0;
            this.DriverID = 0;
            this.IssuedUsingLocalLicenseID = 0;
            this.IssueDate = DateTime.MinValue;
            this.ExpirationDate = DateTime.MinValue;
            this.IsActive = false;
            this.CreatedByUserID = 0;
            _Mode = enMode.eAdd;
        }

        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID = 0;
            int DriverID = 0;
            int IssuedUsingLocalLicenseID = 0;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue ;
            bool IsActive = false;
            int CreatedByUserID = 0;

            
            if(clsInternationalLicenseDataAccess.Find(InternationalLicenseID,
                ref ApplicationID,ref DriverID,ref IssuedUsingLocalLicenseID,
                ref IssueDate,ref ExpirationDate,ref IsActive,
                ref CreatedByUserID))
            {
                clsApplication application = clsApplication.Find(ApplicationID);

                if(application==null)
                {
                    return null;
                }

                return new clsInternationalLicense(InternationalLicenseID,
                    ApplicationID,DriverID,IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,
                    IsActive,CreatedByUserID,application.ApplicantPersonID,
                    application.ApplicationDate,application.ApplicationTypeID,
                    application.ApplicationStatus,application.LastStatusDate,
                    application.PaidFees,application.CreatedByUserID);
            }
            return null;
    }

        private bool _CheckIfLicenceIsNotExpired()
        {
            clsLicense License = clsLicense.Find(IssuedUsingLocalLicenseID);

            if(License==null)
            {
                return false;
            }

            return License.ExpirationDate > DateTime.Now;
        }

        private bool _CheckIfLicenceIsActive()
        {
            clsLicense License = clsLicense.Find(IssuedUsingLocalLicenseID);

            if (License == null)
            {
                return false;
            }

            return License.IsActive = true;
        }

        public static bool CheckIfThisPersonDonotHaveActiveInternationalLicense(ref int InternationalLicense,int PersonID)
        {
            DataTable dt = GetAllInternationalPersonLicense(PersonID);

            if(dt.Rows.Count>0)
            {
                DataView dv = dt.DefaultView;

                dv.RowFilter = "[Is Active] = 1";

                if(dv.Count>0)
                {
                    InternationalLicense = Convert.ToInt32(dv[0][0].ToString());
                    return false;
                }

            }

            return true;
        }

        private bool _CheckIfThisPersonDonotHaveActiveInternationalLicense()
        {
            clsDrivers driver = clsDrivers.FindDriverByDriverID(this.DriverID);

            if(driver==null) 
            {
                return false;
            }

            int InternationalLicense = 0;
            return CheckIfThisPersonDonotHaveActiveInternationalLicense(ref InternationalLicense, driver.PersonID);

        }

        private bool _CheckIfThisPersonHaveOrdinarydrivinglicense()
        {
            clsDrivers driver = clsDrivers.FindDriverByDriverID(this.DriverID);

            if (driver == null)
            {
                return false;
            }

            return clsLicense.CheckIfPersonHasAlreadyLicenseInTheLicenseClass(driver.PersonID, 3);
        }

        private bool _AddNewInternationalLicense()
        {

            if (!_CheckIfLicenceIsNotExpired())
            {
                return false;
            }

            if (!_CheckIfLicenceIsActive())
            {
                return false;
            }

            if (!_CheckIfThisPersonDonotHaveActiveInternationalLicense())
            {
                return false;
            }

            if (!_CheckIfThisPersonHaveOrdinarydrivinglicense())
            {
                return false;
            }

            clsDrivers driver = clsDrivers.FindDriverByDriverID(this.DriverID);

            if (driver == null)
            {
                return false;
            }

            ApplicantPersonID = driver.PersonID;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = 6;
            ApplicationStatus = 3;
            LastStatusDate = DateTime.Now;

            clsApplicationTypes applicationType = clsApplicationTypes.FindApplicationTypeByID(6);

            if(applicationType == null)
            {
                return false;
            }

            PaidFees = applicationType.ApplicationFees;
 

            if (!base.save())
            {
                return false;

            }

            int internationalLicenseID = 0;
            this.IsActive = true;
            if (clsInternationalLicenseDataAccess.AddNewInternationalLicense(ref internationalLicenseID,
                ApplicationID, DriverID, IssuedUsingLocalLicenseID, DateTime.Now, DateTime.Now.AddYears(1),
                IsActive, CreatedByUserID))
            {
                this.InternationalLicenseID = internationalLicenseID;
                this.LocalLicenseInfo = clsLicense.Find(this.IssuedUsingLocalLicenseID);
                this.DriverInfo = clsDrivers.FindDriverByDriverID(this.DriverID);

                return true;
            }

            return false;
        }

        private bool _Update()
        {
            if(!base.save()) 
            {
                return false;
            }

            return clsInternationalLicenseDataAccess.Update(InternationalLicenseID,
                ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                IssueDate, ExpirationDate, IsActive, CreatedByUserID);
        }

        public static bool Delete(int InternationalLicenseID)
        {
            return clsInternationalLicenseDataAccess.Delete(InternationalLicenseID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.eAdd:
                    {
                        if (_AddNewInternationalLicense())
                        {
                            return true;
                            _Mode = enMode.eUpdate;
                        }
                    }
                    break;

                case enMode.eUpdate:
                    {
                        if (_Update())
                        {
                            return true;
                        }
                    }
                    break;

            }

            return false;
        }

        public static DataTable GetAllInternationalPersonLicense(int PersonID)
        {
            return clsInternationalLicenseDataAccess.GetAllInternationalPersonLicense(PersonID);
        }

        public static DataTable GetAllInternationalLicense()
        {
            return clsInternationalLicenseDataAccess.GetAllInternationalLicense();
        }

    }
}
