using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTestAppointments
    {
        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }

        public clsTestTypes TestTypeInfo { get; set; }

        public int LocalDrivingLicenseApplicationID { get; set; }

        public clsLDLApplication LocalDrivingLicenseApplicationInfo {  get; set; }
        public DateTime AppointmentDate { get; set; }
        public double PaidFees { get; set; }
        public int CreatedByUserID { get; set; }

        public clsUsers  UserInfo { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        
        public clsApplication RetakeTestApplicationInfo { get; set; }
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
            this.TestTypeInfo = clsTestTypes.FindTestTypeByID(TestTypeID);
            this.LocalDrivingLicenseApplicationInfo = clsLDLApplication.FindLDLApplication(LocalDrivingLicenseApplicationID);
            this.UserInfo = clsUsers.FindUserByUserID(CreatedByUserID);

            if(RetakeTestApplicationID>0)
            {
                this.RetakeTestApplicationInfo = clsApplication.Find(RetakeTestApplicationID);
            }
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
                this.TestTypeInfo = clsTestTypes.FindTestTypeByID(TestTypeID);
                this.LocalDrivingLicenseApplicationInfo = clsLDLApplication.FindLDLApplication(LocalDrivingLicenseApplicationID);
                this.UserInfo = clsUsers.FindUserByUserID(CreatedByUserID);

                if (RetakeTestApplicationID > 0)
                {
                    this.RetakeTestApplicationInfo = clsApplication.Find(RetakeTestApplicationID);
                }
                return true;
            }

            return false;
        }

        private bool UpdateAppointmentDate()
        {
            if( clsTestAppointmentsDataAccess.UpdateAppointmentDate
                (TestAppointmentID, AppointmentDate))
            {
                if (RetakeTestApplicationID > 0)
                {
                    this.RetakeTestApplicationInfo = clsApplication.Find(RetakeTestApplicationID);
                }
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
}
