using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTests
    {
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }

        public clsTestAppointments TestAppointmentInfo { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsUsers UserInfo { get; set; }

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
                this.TestAppointmentInfo = clsTestAppointments.FindTestAppointmentByID(TestAppointmentID);
                this.UserInfo = clsUsers.FindUserByUserID(CreatedByUserID); 
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
}
