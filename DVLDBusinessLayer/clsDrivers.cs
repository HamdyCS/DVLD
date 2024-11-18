using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsDrivers
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }

        public clsPeople PersonInfo { get; set; }

        public int CreatedByUserID { get; set; }

        public clsUsers UserInfo { get; set; }
        public DateTime CreatedDate { get; set; }

        enum enMode { eAdd = 1, eUpdate = 2 }

        enMode _Mode;

        private clsDrivers(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            this.PersonInfo = clsPeople.FindPeopleByID(PersonID);
            this.UserInfo = clsUsers.FindUserByUserID(this.CreatedByUserID);
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
                this.PersonInfo = clsPeople.FindPeopleByID(PersonID);
                this.UserInfo = clsUsers.FindUserByUserID(this.CreatedByUserID);
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
}
