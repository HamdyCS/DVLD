using DVLDDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLicenseClasses
    {
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public double ClassFees { get; set; }

        enum enMode { eAdd = 1, eUpdate = 2 }
        enMode _Mode;

        private clsLicenseClasses(int LicenseClassID,
             string ClassName, string ClassDescription,
            byte MinimumAllowedAge, byte DefaultValidityLength,
            double ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;

            _Mode = enMode.eUpdate;

        }

        public static clsLicenseClasses Find(int LicenseClassID)
        {

            string ClassName = string.Empty;
            string ClassDescription = string.Empty;
            byte MinimumAllowedAge = 0;
            byte DefaultValidityLength = 0;
            double ClassFees = 0;

            if (clsLicenseClassesDataAccess.FindLicenseClass
                (LicenseClassID, ref ClassName, ref ClassDescription,
                ref MinimumAllowedAge, ref DefaultValidityLength,
                ref ClassFees))
            {
                return new clsLicenseClasses(LicenseClassID, ClassName, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength,
                    ClassFees);
            }

            return null;

        }

    }
}
