using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccess;

namespace DVLDBusinessLayer
{
    public class clsReplaceForDamageOrLostLicense : clsApplication
    {
        public int OldLicenseID { get; set; }
        public int NewLicenseID { get; set; }

        private enum enMode { eLost = 3 , eDamaged = 4}

        private clsLicense _OldLicense;

        private clsLicense _NewLicense;

        private clsDrivers _Driver;

        enMode _Mode;

        public clsReplaceForDamageOrLostLicense(bool Lost = true) : base()
        {
            this.OldLicenseID = 0;
            this.NewLicenseID = 0;
 
            if(Lost) 
            {
                _Mode = enMode.eLost;
            }
            else
            {
                _Mode = enMode.eDamaged;
            }
        }

        public static bool CheckIfLicenseIsExpired(int LicenseID)
        {
            clsLicense License = clsLicense.Find(LicenseID);

            if (License == null)
            {
                return false;
            }
            return (DateTime.Now > License.ExpirationDate);
        }

        public static bool CheckIfLicenseIsActive(int LicenseID)
        {
            clsLicense License = clsLicense.Find(LicenseID);

            if (License == null)
            {
                return false;
            }
            return License.IsActive;
        }

        private bool _ChangeOldLicenseToNotActive()
        {
            _OldLicense.IsActive = false;
            return _OldLicense.Save();
        }

        private bool _CreateApplication()
        {

            // Created by user Id in Ui Layer

            ApplicantPersonID = _Driver.PersonID;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = (int)_Mode;
            ApplicationStatus = 3;
            LastStatusDate = DateTime.Now;

            clsApplicationTypes applicationType = clsApplicationTypes.FindApplicationTypeByID((int)_Mode);

            if (applicationType == null)
            {
                base.PaidFees = 0;
            }
            else
            {
                base.PaidFees = applicationType.ApplicationFees;
            }

            return base.save();
        }

        private bool _CreateNewLicense()
        {
            _NewLicense = new clsLicense();
            _NewLicense.ApplicationID = ApplicationID;
            _NewLicense.DriverID = _OldLicense.DriverID;
            _NewLicense.LicenseClass = _OldLicense.LicenseClass;
            _NewLicense.IssueDate = DateTime.Now;

            clsLicenseClasses licenseClasse = clsLicenseClasses.Find(_OldLicense.LicenseClass);

            if (licenseClasse != null)
            {
                _NewLicense.ExpirationDate = DateTime.Now.AddYears(licenseClasse.DefaultValidityLength);
                _NewLicense.PaidFees = licenseClasse.ClassFees;
            }
            else
            {
                _NewLicense.ExpirationDate = DateTime.Now;
                _NewLicense.PaidFees = 0;
            }

            _NewLicense.Notes = "";
            _NewLicense.IsActive = true;
            
            if(_Mode==enMode.eLost)
            {
                _NewLicense.IssueReason = 4;
            }
            else if(_Mode==enMode.eDamaged)
            {
                _NewLicense.IssueReason = 3;

            }
            _NewLicense.CreatedByUserID = base.CreatedByUserID;

            return _NewLicense.Save();

        }

        public bool Save()
        {
            _OldLicense = clsLicense.Find(OldLicenseID);

            if (_OldLicense == null)
            {
                return false;
            }

            _Driver = clsDrivers.FindDriverByDriverID(_OldLicense.DriverID);

            if (_Driver == null)
            {
                return false;
            }

            if (CheckIfLicenseIsExpired(OldLicenseID))
            {
                return false;
            }

            if (!CheckIfLicenseIsActive(this.OldLicenseID))
            {
                return false;
            }

            if (!_ChangeOldLicenseToNotActive())
            {
                return false;
            }

            if (!_CreateApplication())
            {
                return false;
            }

            if (!_CreateNewLicense())
            {
                return false;
            }

            NewLicenseID = _NewLicense.LicenseID;
            return true;

        }

    }
}
