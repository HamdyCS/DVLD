using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DVLDBusinessLayer
{
    public class clsReleaseDetainedLicense : clsApplication
    {
        private clsLicense _License;

        public int LicenseID {  get; set; } 

        private clsDrivers _Driver;

        private clsDetainLicense _DetainLicense;

        public clsReleaseDetainedLicense() : base()
        {
            LicenseID = 0;
        }

        private bool _CreateApplication()
        {
            

            ApplicantPersonID = _Driver.PersonID;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = 5;
            ApplicationStatus = 3;
            LastStatusDate = DateTime.Now;

            clsApplicationTypes applicationType = clsApplicationTypes.FindApplicationTypeByID(5);

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

        private bool _UpdateDetainLicense()
        {
            _DetainLicense.IsReleased = true;
            _DetainLicense.ReleaseDate = DateTime.Now;
            _DetainLicense.ReleasedByUserID = CreatedByUserID;
            _DetainLicense.ReleaseApplicationID = ApplicationID;
            return _DetainLicense.Save();
        }

        private bool _ReleaseLicense()
        {
            _License = clsLicense.Find(LicenseID);

            if (_License == null)
            {
                return false;
            }

            int DetainID = 0;
            if (!clsDetainLicense.CheckIfLicenseIsdetainedAndNotRelease(ref DetainID, LicenseID))
            {
                return false;
            }

            _Driver = clsDrivers.FindDriverByDriverID(_License.DriverID);
            if (_Driver == null)
            {
                return false;
            }

            _DetainLicense = clsDetainLicense.Find(DetainID);

            if (_DetainLicense == null)
            {
                return false;
            }

            if(!_CreateApplication())
            {
                return false;
            }

            if (!_UpdateDetainLicense())
            {
                return false;
            }

            return true;
        }

        public bool  Save()
        {
            return _ReleaseLicense();
        }
    }
}
