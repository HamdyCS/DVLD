using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTestTypes
    {
        public int TestTypeID { get; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public double TestTypeFees { get; set; }

        public static DataTable GetAllTestTypesContent()
        {
            return clsTestTypesDataAccess.GetAllTestTypesContent();
        }
        private clsTestTypes(int TestTypeID,
           string TestTypeTitle, string TestTypeDescription,
           double TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
        }

        public static clsTestTypes FindTestTypeByID(int TestTypeID)
        {
            string TestTypeTitle = "";
            string TestTypeDescription = "";
            double TestTypeFees = 0;

            if (!clsTestTypesDataAccess.FindTestTypeByID(TestTypeID, ref TestTypeTitle,
                ref TestTypeDescription, ref TestTypeFees))
            {
                return null;
            }

            return new clsTestTypes(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
        }

        private bool UpdateApplicationType()
        {
            if (TestTypeTitle == string.Empty || (TestTypeDescription == string.Empty || TestTypeFees < 0))
            {
                return false;
            }
            return clsTestTypesDataAccess.UpdateTestTypes(this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
        }

        public bool Save()
        {
            return UpdateApplicationType();
        }
    }
}
