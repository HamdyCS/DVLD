using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsUsers
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }

        public clsPeople PersonInfo { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public static bool CheckIfUserNameInSystem(string userName)
        {
            return clsUsersDataAccess.CheckIfUserNameInSystem(userName);
        }

        public static bool CheckIfUserNameAndPasswordIsTrue(string userName, string password)
        {
            return clsUsersDataAccess.CheckIfUserNameAndPasswordIsTrue(userName, password);
        }

        public static bool CheckIfUserIsActive(string userName)
        {
            return clsUsersDataAccess.CheckIfUserIsActive(userName);
        }

        public static bool GetUserInfoFromWindowsRegistry(ref string userName, ref string password)
        {
            return clsUsersDataAccess.GetUserInfoFromWindowsRegistry(ref userName, ref password);
        }

        public static bool DeleteAllRecordInRemeberUserFile()
        {
            return clsUsersDataAccess.DeleteAllRecordInRemeberUserFile();
        }

        public static bool SaveUserInfoInWindowsRegistry(string userName, string password)
        {
            return clsUsersDataAccess.SaveUserInfoInWindowsRegistry(userName, password);
        }

        public static DataTable GetAllUsersInfo()
        {
            return clsUsersDataAccess.GetAllUsersInfo();
        }

        public static DataTable GetUserInfo(string UserName)
        {
            return clsUsersDataAccess.GetUserInfo(UserName);
        }

        enum enMode { eAddNewUser = 1, eUpdateUser = 2 }
        enMode _Mode;
        private clsUsers(int userID, int personID, string userName, string password, bool isActive)
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;
            this.PersonInfo = clsPeople.FindPeopleByID(PersonID);

            _Mode = enMode.eUpdateUser;
        }

        public clsUsers(int PersonID)
        {
            this.UserID = -1;
            this.PersonID = PersonID;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.IsActive = false;

            _Mode = enMode.eAddNewUser;
        }

        public static clsUsers FindUserByUserID(int UserID)
        {
            int PersonID = 0;
            string UserName = string.Empty;
            string Password = string.Empty;
            bool IsActive = false;

            if (!clsUsersDataAccess.FindUserByUserID(UserID, ref PersonID
                , ref UserName, ref Password, ref IsActive))
            {
                return null;
            }
            else
            {
                return new clsUsers(UserID, PersonID, UserName, Password, IsActive);
            }
        }

        public static clsUsers FindUserByUserName(string UserName)
        {
            int PersonID = 0;
            int UserID = 0;
            string Password = string.Empty;
            bool IsActive = false;

            if (!clsUsersDataAccess.FindUserByUserName(UserName,ref UserID,
                ref PersonID,ref Password,ref IsActive))
            {
                return null;
            }
            else
            {
                return new clsUsers(UserID, PersonID, UserName, Password, IsActive);
            }
        }
        public static bool DeleteUser(int UserID)
        {
            return clsUsersDataAccess.DeleteUserByUserID(UserID);
        }

        private bool AddNewUser()
        {
            if (CheckIfUserNameInSystem(this.UserName)
                ||
                clsUsersDataAccess.CheckIfThisPersonIsUserByPersonID(this.PersonID))
            {
                return false;
            }

            int UserID = this.UserID;
            int PersonID = this.PersonID;
            string UserName = this.UserName;
            string Password = this.Password;
            bool IsActive = this.IsActive;

            if (!clsUsersDataAccess.AddNewUserByPersonID(PersonID, ref UserID, UserName, Password, IsActive))
            {
                return false;
            }

            this.UserID = UserID;
            this.PersonInfo = clsPeople.FindPeopleByID(PersonID);

            return true;
        }

        private bool UpdateUser()
        {
            return clsUsersDataAccess.UpdateUserByUserID(this.UserID
                , this.UserName, this.Password, this.IsActive);
        }

        public bool save()
        {
            switch (_Mode)
            {
                case enMode.eAddNewUser:
                    {
                        if (AddNewUser())
                        {
                            _Mode = enMode.eUpdateUser;
                            return true;
                        }
                    }
                    break;

                case enMode.eUpdateUser:
                    {
                        if (UpdateUser())
                        {
                            return true;
                        }
                    }
                    break;
            }

            return false;

        }
        public static bool CheckIfThisPersonIsUserByPersonID(int PersonID)
        {
            return clsUsersDataAccess.CheckIfThisPersonIsUserByPersonID(PersonID);
        }

    }
}
