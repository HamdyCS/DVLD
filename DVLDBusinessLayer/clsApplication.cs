using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsApplication
    {

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clsPeople  PersonInfo { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }

        public clsApplicationTypes ApplicationTypeInto { get; set; }
        public byte ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public double PaidFees { get; set; }
        public int CreatedByUserID { get; set; }

        public clsUsers UserInfo { get; set; }

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

            this.PersonInfo = clsPeople.FindPeopleByID(ApplicantPersonID);
            this.ApplicationTypeInto = clsApplicationTypes.FindApplicationTypeByID(ApplicationTypeID);
            this.UserInfo = clsUsers.FindUserByUserID(CreatedByUserID);

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
                this.PersonInfo = clsPeople.FindPeopleByID(ApplicantPersonID);
                this.PersonInfo = clsPeople.FindPeopleByID(ApplicantPersonID);
                this.ApplicationTypeInto = clsApplicationTypes.FindApplicationTypeByID(ApplicationTypeID);
                this.UserInfo = clsUsers.FindUserByUserID(CreatedByUserID);
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
}
