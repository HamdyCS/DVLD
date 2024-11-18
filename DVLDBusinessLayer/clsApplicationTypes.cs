using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsApplicationTypes
    {
        public int ApplicationTypesID { get; }
        public string ApplicationTypesTitle { get; set; }
        public Single ApplicationFees { get; set; }

        public static DataTable GetAllApplicationTypesContent()
        {
            return clsApplicationTypesDataAccess.GetAllApplicationTypesContent();
        }
        private clsApplicationTypes(int ApplicationTypesID,
           string ApplicationTypesTitle, Single ApplicationFees)
        {
            this.ApplicationTypesID = ApplicationTypesID;
            this.ApplicationTypesTitle = ApplicationTypesTitle;
            this.ApplicationFees = ApplicationFees;
        }

        public static clsApplicationTypes FindApplicationTypeByID(int ApplicationTypesID)
        {
            string ApplicationTypesTitle = "";
            Single ApplicationFees = 0;

            if (!clsApplicationTypesDataAccess.FindApplicationTypeByID(
                ApplicationTypesID,
                ref ApplicationTypesTitle, ref ApplicationFees))
            {
                return null;
            }

            return new clsApplicationTypes(ApplicationTypesID, ApplicationTypesTitle, ApplicationFees);
        }

        private bool UpdateApplicationType()
        {
            if (ApplicationTypesTitle == string.Empty || ApplicationFees < 0)
            {
                return false;
            }
            return clsApplicationTypesDataAccess.UpdateApplicationTypes(this.ApplicationTypesID, this.ApplicationTypesTitle, this.ApplicationFees);
        }

        public bool Save()
        {
            return UpdateApplicationType();
        }

        public static double GetApplicationFees(int ApplicationTypesID)
        {
            return clsApplicationTypesDataAccess.GetApplicationFees(ApplicationTypesID);
        }


    }
}
