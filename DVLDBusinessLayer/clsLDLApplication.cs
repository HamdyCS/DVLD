using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLDLApplication : clsApplication
    {
        public int LocalDrivingLicenseApplicationID { get; set; }

        public int LicenseClassID { get; set; }

        public clsLicenseClasses LicenseClassInfo { get; set; }

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
            this.LicenseClassInfo = clsLicenseClasses.Find(LicenseClassID);
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
                this.LicenseClassInfo = clsLicenseClasses.Find(LicenseClassID);

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

        public static bool CheckIfPersonHasActiveApplicationInThisLicenseClass
            (ref int ApplicationID, int LicenseClassID, int PersonID)
        {
            return clsLDLApplicationDataAccess.CheckIfPersonHasActiveApplicationInThisLicenseClass(ref ApplicationID, LicenseClassID, PersonID);
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
}
