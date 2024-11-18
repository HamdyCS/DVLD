using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsDetainLicense
    {
        public int DetainID { get; set; } 
        public int LicenseID { get; set; }
        public  clsLicense LicenseInfo { get; set; }
        public DateTime DetainDate { get; set; }
        public double FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUsers CreatedByUserInfo {  get; set; } 
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }

        public clsUsers ReleasedByUserInfo { get; set; }
        public int ReleaseApplicationID { get; set; }

        public  clsApplication ReleaseApplicationInfo {  get; set; }



        enum enMode
        {
            eAdd = 1,
            eUpdate = 2
        }

        private enMode _Mode;

        public clsDetainLicense()
        {
            _Mode = enMode.eAdd;
            DetainID = 0;
            LicenseID = 0;
            DetainDate = DateTime.MinValue;
            FineFees = 0;
            CreatedByUserID = 0;
            IsReleased = false;
            ReleaseDate = DateTime.MinValue;
            ReleasedByUserID = 0;
            ReleaseApplicationID = 0;
        }

        private clsDetainLicense(int detainID, int licenseID, 
            DateTime detainDate, double fineFees, int createdByUserID,
            bool isReleased, DateTime releaseDate, 
            int releasedByUserID, int releaseApplicationID)
        {
            DetainID = detainID;
            LicenseID = licenseID;
            DetainDate = detainDate;
            FineFees = fineFees;
            CreatedByUserID = createdByUserID;
            IsReleased = isReleased;
            ReleaseDate = releaseDate;
            ReleasedByUserID = releasedByUserID;
            ReleaseApplicationID = releaseApplicationID;
            this.LicenseInfo = clsLicense.Find(this.LicenseID);
            this.CreatedByUserInfo = clsUsers.FindUserByUserID(this.CreatedByUserID);

            if(ReleasedByUserID>0)
            {
                this.ReleasedByUserInfo = clsUsers.FindUserByUserID(ReleasedByUserID);
            }

            if (this.ReleaseApplicationID > 0)
            {
                this.ReleaseApplicationInfo = clsApplication.Find(this.ReleaseApplicationID);
            }

            _Mode = enMode.eUpdate;
        }

        public static clsDetainLicense Find(int DetainID)
        {

            int licenseID = 0;
            DateTime detainDate = DateTime.MinValue;
            double fineFees = 0;
            int createdByUserID = 0;
            bool isReleased = false; 
            DateTime releaseDate = DateTime.MinValue;
            int releasedByUserID = 0; 
            int releaseApplicationID = 0;

            if(!clsDetainLicenseDataAccess.Find(DetainID, ref licenseID, 
                ref detainDate, ref fineFees,
                ref createdByUserID, ref isReleased,ref releaseDate,
                ref releasedByUserID, ref releaseApplicationID))
            {
                return null;
            }

            return new clsDetainLicense(DetainID, licenseID, detainDate,
                fineFees, createdByUserID, isReleased, releaseDate,
                releasedByUserID, releaseApplicationID);
        }

        public static bool DeleteDetainLicense(int detainID) 
        {
            return clsDetainLicenseDataAccess.DeleteDetainLicense(detainID);
        }

        private  bool _AddNewDetainLicense()
        {
            int detainID = 0;
            if (CheckIfLicenseIsdetainedAndNotRelease(ref detainID,this.LicenseID))
            {
                return false;
            }

            int DetainID = 0;
            DetainDate = DateTime.Now;
            IsReleased = false;
            ReleaseDate = DateTime.MinValue;
            ReleasedByUserID = 0;
            ReleaseApplicationID = 0;

            if ( clsDetainLicenseDataAccess.AddNewDetainLicense(ref DetainID
                ,LicenseID,DetainDate,FineFees,CreatedByUserID,
                IsReleased,ReleaseDate,ReleasedByUserID,
                ReleaseApplicationID))
            {
                this.DetainID = DetainID;
                this.LicenseInfo = clsLicense.Find(this.LicenseID);
                this.CreatedByUserInfo = clsUsers.FindUserByUserID(this.CreatedByUserID);
                return true;
            }
            return false;
        }

        private bool _UpdatedetainLicense()
        {
            if( clsDetainLicenseDataAccess.UpdateDetainLicense(DetainID
                , LicenseID, DetainDate, FineFees, CreatedByUserID,
                IsReleased, ReleaseDate, ReleasedByUserID,
                ReleaseApplicationID))
            {
                this.ReleasedByUserInfo = clsUsers.FindUserByUserID(ReleasedByUserID);
                this.ReleaseApplicationInfo = clsApplication.Find(this.ReleaseApplicationID);
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
                        if (_AddNewDetainLicense())
                        {
                            return true;
                            _Mode = enMode.eUpdate;
                        }
                    }
                    break;

                case enMode.eUpdate:
                    {
                        if (_UpdatedetainLicense())
                        {
                            return true;
                        }
                    }
                    break;

            }

            return false;
        }
        public static bool CheckIfLicenseIsdetainedAndNotRelease(ref int DetainID, int LicenseID )
        {
            

            return clsLicenseDataAccess.CheckIfLicenseIsDetained(ref DetainID, LicenseID);
        }

        public static DataTable GetAllDetainLicenses()
        {
            return clsDetainLicenseDataAccess.GetAllDetainLicenses();
        }
    }
}
